using Killhope.Plugins.Rocks.Domain.Formatting.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Killhope.Plugins.Rocks.Domain.Formatting
{
    /// <summary>
    /// Formatting for a HTML tag with a start and end element. Example: <![CDATA[<b></b>]]>
    /// Immutable
    /// </summary>
    public class HTMLDualTagFormatting : TextFormattingRule
    {
        private readonly Regex startPattern;
        private readonly Regex endPattern;
        private readonly string tagName;

        public HTMLDualTagFormatting(string tagName)
        {
            if (tagName == null)
                throw new ArgumentNullException("tagName");

            this.tagName = tagName;
            this.startPattern = new Regex(this.StartTag);
            this.endPattern = new Regex(this.EndTag);
        }

        public string TagName { get { return tagName; } }

        public string StartTag { get { return String.Format("<{0}>", this.TagName); } }
        public string EndTag { get { return String.Format("</{0}>", this.TagName); } }

        public override int StartLength { get { return TagName.Length + "<>".Length; } }
        public override int EndLength { get { return StartLength + "/".Length; } }
        public override int TotalLength { get { return StartLength + EndLength; } }

        public override IEnumerable<Position> GetAllPositons(string input)
        {
            Match currentStart = startPattern.Match(input, 0);
            Match currentEnd = endPattern.Match(input, 0);

            int startLength = this.StartLength;

            if (currentStart.Success && currentEnd.Success && currentEnd.Index <= currentStart.Index)
                throw new InvalidFormatException("end tag before start tag.");

            while (currentStart.Success && currentEnd.Success)
            {
                //confirm that we don't have two start tags before an end tag.
                //(in parsing, this should never happen).
                Match nextStart = currentStart.NextMatch();
                Match nextEnd = currentEnd.NextMatch();

                if (nextStart.Success && nextStart.Index <= currentEnd.Index)
                    throw new OverlappingTagException(input, this.TagName, currentStart.Index, nextStart.Index, currentEnd.Index);

                //If the next End tag is before the next start tag.
                if (nextEnd.Success && nextEnd.Index <= nextStart.Index)
                    throw new InvalidFormatException("end tag before start tag.");

                yield return new Position(currentStart.Index + startLength, currentEnd.Index);

                currentEnd = nextEnd;
                currentStart = nextStart;
            }

            //If the start hasn't been found, but the end has, throw an error.
            if (!currentStart.Success && currentEnd.Success)
                throw new InvalidFormatException(String.Format("Closing tag without opening tag at position: {0}", currentEnd.Index));

            //Alternately, If the start has been found, but the end hasn't, throw an error.
            if (currentStart.Success && !currentEnd.Success)
                throw new InvalidFormatException(String.Format("Opening tag without closing tag at position: {0}", currentEnd.Index));


        }

        public override string Process(string input, Position position)
        {
            string untouched = input.Substring(0, position.Start - this.StartLength);
            string modified = position.Substring(input);

            //TODO: Need to test this condition explicitly.
            if(position.End - this.EndLength != input.Length && position.End + this.EndLength < input.Length)
                modified += input.Substring(position.End + this.EndLength);

            return untouched + modified;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input">The input string to add the specified HTML tag to.</param>
        /// <param name="position">The position of the current element that should be re-wrapped in the HTML tag.</param>
        /// <returns></returns>
        public override IEnumerable<Replacement> UnProcess(string input, Position position)
        {
            //TODO: Make more efficient.
            //string untouched = input.Substring(0, position.Start);

            return new List<Replacement> { new Replacement(this.StartTag, new Position(position.Start, position.Start)),
            new Replacement(this.EndTag, new Position(position.End, position.End))};

            //untouched += this.StartTag;
            //untouched += position.Substring(input);
            //untouched += this.EndTag;
            //untouched += input.Substring(position.End);
            //return untouched;
        }

        public override Tuple<int, int> ShiftAmount
        {
            get { return new Tuple<int,int>(this.StartLength,this.StartLength); }
        }
    }
}
