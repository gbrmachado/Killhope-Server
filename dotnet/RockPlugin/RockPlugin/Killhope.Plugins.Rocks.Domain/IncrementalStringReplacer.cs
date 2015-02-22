using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Killhope.Plugins.Rocks.Domain
{
    /// <summary>
    /// A state machine used to handle positional updates on a string.
    /// </summary>
    public class IncrementalStringReplacer
    {
        private string value;

        /// <summary>
        /// The last start position
        /// </summary>
        /// <remarks>The place where no new position can be before. Any string position before this + increment can be marked as done.</remarks>
        int previousStart;

        StringBuilder builder;

        private List<TextFormattingRule.Replacement> todo = new List<TextFormattingRule.Replacement>();

        public IncrementalStringReplacer(string value)
        {
            this.value = value;
            this.builder = new StringBuilder();
        }

        public void IncrementPosition(Position p, TextFormattingRule rule)
        {
            if (p.Start < previousStart)
                throw new ArgumentException("The supplied string position starts before the end of the processed and finalised data. Most likely caused by passing an unsorted collection to the state machine.");


            if (p.Start > previousStart)
            {
                var toApply = from r in todo where r.Position.Start <= p.Start orderby r.Position.Start ascending select r;


                int currentStart = previousStart;
                foreach (var r in toApply)
                {
                    if (r.Position.Start > currentStart)
                        builder.Append(value.Substring(currentStart, r.Position.Start - currentStart));
                    currentStart = r.Position.Start;

                    builder.Append(r.Value);
                }

                //TODO: Add rest of string.
                builder.Append(value.Substring(currentStart, p.Start - currentStart));

                previousStart = p.Start;

                foreach (var pp in toApply)
                    todo.Remove(pp);

            }


            var rules = rule.UnProcess(value, p);

            var toProcess = from r in rules where r.Position.Start == p.Start select r;

            foreach (var r in toProcess)
                builder.Append(r.Value);

            //The range is inserted at 0 to keep the ordering of the tags if the position is the same,
            //for example; <u><b> should be matched to </b></u>
            todo.InsertRange(0, rules.Except(toProcess));

        }


        private string getUnprocessedText()
        {
            var builder = new StringBuilder();
            var toApply = from r in todo orderby r.Position.Start ascending select r;

            int currentStart = previousStart;
            foreach (var r in toApply)
            {
                if (r.Position.Start > currentStart)
                    builder.Append(value.Substring(currentStart, r.Position.Start - currentStart));
                currentStart = r.Position.Start;

                builder.Append(r.Value);
            }

            //TODO: Add rest of string.
            builder.Append(value.Substring(currentStart));

            return builder.ToString();

        }

        public string Value
        {
            get { return builder.ToString() + getUnprocessedText(); }
        }

        private class replacement
        {
            public replacement(Position p, TextFormattingRule rule)
            {
                this.p = p;
                this.rule = rule;
            }

            public Position p { get; private set; }
            public TextFormattingRule rule { get; private set; }
        }

    }
}
