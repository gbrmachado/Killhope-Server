using Killhope.Plugins.Rocks.Domain.Formatting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Killhope.Plugins.Rocks.Domain
{
    /// <summary>
    /// Encapsulates a string of data, allowing arbitrary formatting rules to be applied to the string.
    /// </summary>
    public class RichText : INotifyPropertyChanged
    {

        internal const string UNDERLINE_START = "<u>";

        private Dictionary<TextFormattingRule, ICollection<Position>> Positions { get; set; }

        public RichText() : this("") { }

        public RichText(string initialValue)
        {
            if (initialValue == null)
                throw new ArgumentNullException("initialValue");

            this.Value = ReplaceFormatting(initialValue);
        }

        /// <summary>
        /// The text 
        /// </summary>
        public string Value;


        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Returns the rich text in a pseudo-html format containing all special formatting.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return StringProcessor.ApplyRules(Value, this.Positions);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <remarks>Once a string has been input to the class, any formatting commands will be stored internally, and the </remarks>
        /// <returns></returns>
        private string ReplaceFormatting(string input)
        {
            var res = StringProcessor.ProcessString(input, FormatFactory.GetInstance());
            this.Positions = res.Positions;
            return res.Value;
        }

        public IEnumerable<Position> UnderlineLocations
        {
            get
            {
                return IfFound(FormatFactory.u);
            }
        }

        public IEnumerable<Position> BoldLocations
        {
            get
            {
                return IfFound(FormatFactory.b);
            }
        }

        public IEnumerable<Position> LinebreakLocations
        {
            get
            {
                return IfFound(FormatFactory.br);
            }
        }

        /// <summary>
        /// Returns all the positions of an available for a formatting rule, or an empty collection if nothing exists.
        /// </summary>
        /// <param name="rule">The formatting rule to look for.</param>
        /// <returns></returns>
        private IEnumerable<Position> IfFound(TextFormattingRule rule)
        {
            if (rule == null)
                throw new ArgumentNullException("rule");

            return Positions.ContainsKey(rule) ? Positions[rule] : new List<Position>();
        }

        public class Image
        {
            /*
             * 
             * An image may be defined as such: {{ [index] | [dir] | [size] | [overflow] }}
             * [index] : index into the Images array
             * [dir] : L/R/C (Left/Right/Center) : Default : Left
             * [size]: size of the image: . _ represents a wildcard. Default: , (normal size of the image).
             * [overflow]: Boolean – Whether the image should be resized vertically if it would push the text down. Default: False
             */

            public enum Alignment
            {
                Left = 0,
                Center = 1,
                Right = 2
            }


            public Image(int arrayOrdinal, Alignment alignment, ImageSize size)
            {
            }
        }

    }
}
