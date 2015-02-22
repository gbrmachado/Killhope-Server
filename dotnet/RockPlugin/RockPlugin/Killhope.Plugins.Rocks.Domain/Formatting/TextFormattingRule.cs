using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Killhope.Plugins.Rocks.Domain
{
    public abstract class TextFormattingRule
    {
        /// <summary>
        /// Returns all 
        /// </summary>
        /// <param name="input"></param>
        /// <exception cref="OverlappingTagException"></exception>
        /// <returns></returns>
        public abstract IEnumerable<Position> GetAllPositons(string input);

        /// <summary>
        /// Removes the processed data around the provided input
        /// </summary>
        /// <param name="input"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public abstract string Process(string input, Position position);

        /// <summary>
        /// Reverse of process
        /// </summary>
        /// <param name="input">the input to append the current tag to</param>
        /// <param name="position"></param>
        /// <returns></returns>
        public abstract IEnumerable<Replacement> UnProcess(string input, Position position);

        public abstract int TotalLength { get; }
        public abstract int StartLength { get; }
        public abstract int EndLength { get; }

        public abstract Tuple<int, int> ShiftAmount { get; }

        public class Replacement
        {
            public Replacement(string value, Position pos)
            {
                this.Value = value;
                this.Position = pos;
            }

            public Position Position { get; private set; }
            public string Value { get; private set; }
        }
    }
}
