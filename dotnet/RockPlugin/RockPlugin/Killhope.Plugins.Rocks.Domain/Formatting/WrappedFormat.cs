using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Killhope.Plugins.Rocks.Domain.Formatting
{
    class WrappedFormat
    {

        public Position Position { get; set; }
        public TextFormattingRule Rule;
        public Position OriginalPosition;
        private List<Tuple<TextFormattingRule, Position.OverlapType>> WrappedRules = new List<Tuple<TextFormattingRule, Position.OverlapType>>();

        /// <summary>
        /// T
        /// </summary>
        /// <param name="rule"></param>
        /// <param name="toProcess"></param>
        public WrappedFormat(TextFormattingRule rule, Position toProcess)
        {
            // TODO: Complete member initialization
            this.Rule = rule;
            this.Position = toProcess;
        }

        public void AddRule(TextFormattingRule rule, Position.OverlapType overlap)
        {
            if (this.Rule is SingleHTMLTag)
            {
                //TODO: This should be an assertion, we should NOT be able to get here, as a single HTML tag 
                throw new InvalidOperationException("Logical error: a rule may not be added to a single element.");
            }

            WrappedRules.Add(new Tuple<TextFormattingRule, Position.OverlapType>(rule, overlap));
        }

        public IEnumerable<TextFormattingRule> wrappedRules()
        {
            return from s in WrappedRules select s.Item1;
        }

        /// <summary>
        /// The position of the formatting over the modifed, wrapped string
        /// </summary>
        public Position ModifiedPosition { 
            get 
            {
                return new Position(this.Position.Start - getStartChange(), this.Position.End - getEndChange());


            } 
        }

        private int getStartChange()
        {
            int total  = 0;
            //total += this.Rule.StartLength;
            foreach (var tuple in this.WrappedRules)
            {
                if (tuple.Item2 == Domain.Position.OverlapType.Total) { 

                }
                else if (tuple.Item2 == Domain.Position.OverlapType.Left)
                    throw new NotImplementedException();
                else if (tuple.Item2 == Domain.Position.OverlapType.Right) {

                }
                else
                    throw new InvalidOperationException("Invalid value doe OverlapType");

            }
            return total;
        }

        private int getEndChange()
        {
            int total  = 0; 
            //total += this.Rule.StartLength;
            foreach (var tuple in this.WrappedRules)
            {
                if (tuple.Item2 == Domain.Position.OverlapType.Total)
                    total += tuple.Item1.TotalLength;
                else if (tuple.Item2 == Domain.Position.OverlapType.Left)
                    throw new NotImplementedException();
                else if (tuple.Item2 == Domain.Position.OverlapType.Right) {
                    total += tuple.Item1.StartLength;
                }
                else
                    throw new InvalidOperationException("Invalid value doe OverlapType");

            }
            return total;
        }
        
    }
}
