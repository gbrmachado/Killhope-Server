using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Killhope.Plugins.Rocks.Domain.Formatting
{
    class SingleHTMLTag :TextFormattingRule
    {
        private readonly Regex pattern;

        public string TagName { get; private set; }

        public SingleHTMLTag(string tagName)
        {
            if (tagName == null)
                throw new ArgumentNullException("tagName");

            this.TagName = tagName;
            this.pattern = new Regex(this.Tag);
        }

        //Note: the slash goes AFTER.
        public string Tag { get { return String.Format("<{0}/>", this.TagName); } }

        //TODO: These break the abstraction.
        public override int StartLength { get { return TagName.Length + "</>".Length; } }
        public override int EndLength { get { return 0; } }
        public override int TotalLength { get { return StartLength + EndLength; } }

        public override IEnumerable<Position> GetAllPositons(string input)
        {
            Match currentStart = pattern.Match(input, 0);

            int TagLength = Tag.Length;

            while (currentStart.Success)
            {
                yield return new Position(currentStart.Index, currentStart.Index + TagLength);
                currentStart = currentStart.NextMatch();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="position">The Position in the text where the tag to be removed is.</param>
        /// <returns></returns>
        public override string Process(string input, Position position)
        {
            string before = input.Substring(0, position.Start);
            string after = input.Substring(position.End);
            return before + after;
        }

        public override IEnumerable<Replacement> UnProcess(string input, Position position)
        {
            if (position.Length != 0)
                throw new ArgumentException("supplied position for a single tag should have a length of 0.");

            //string before = input.Substring(0, position.Start);
            //string after = input.Substring(position.End);

            return new List<Replacement> { new Replacement(this.Tag, position) };
        }

        public override Tuple<int, int> ShiftAmount
        {
            //We want to remove the tag completely, so if the entire tag is enclosed, we want it removed whilst processing.
            get { return new Tuple<int, int>(0, this.StartLength); }
        }
    }
}
