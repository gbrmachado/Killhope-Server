using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Killhope.Plugins.Rocks.Domain
{
    /// <summary>
    /// Immutable class specifying a position (such as a selection) over a string.
    /// </summary>
    [DebuggerDisplay("{Start} : {End}")]
    public class Position : IComparable<Position>
    {
        private int start;
        private int end;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="start">The position of the first character in the selection.</param>
        /// <param name="end">The position of the first character outside the selection.</param>
        public Position(int start, int end)
        {
            // TODO: Make immutable Equals and GetHashCode.
            if (start < 0)
                throw new ArgumentException("start must be positive.");

            if (end < start)
                throw new ArgumentException("end must be greater than or equal to start.");

            this.start = start;
            this.end = end;
        }

        /// <summary>
        /// The position of the first character in the selection.
        /// </summary>
        public int Start { get { return start; } }

        /// <summary>
        /// The position of the first character outside the selection.
        /// </summary>
        public int End { get { return end; } }

        public int CompareTo(Position other)
        {
            //By definition, any object compares greater than (or follows) null, and two null references compare equal to each other.
            //http://msdn.microsoft.com/en-us/library/system.icomparable.compareto.aspx

            if (other == null)
                return 1;

            //Note that Int32.CompareTo() is used to avoid the possibility of overflows.

            //Otherwise, do this based on the start parameter.
            if (this.start != other.start)
                return this.start.CompareTo(other.start);

            //If they are equal, compare the ends.


            return this.End.CompareTo(other.End);
        }

        public string Substring(string input)
        {
            return input.Substring(this.Start, this.Length);
        }

        public int Length { get { return this.End - this.Start; } }

        internal Position Shift(int shiftAmount)
        {
            return new Position(this.Start - shiftAmount, this.End - shiftAmount);
        }
        internal Position Shift(Tuple<int, int> shiftToUse)
        {
            return new Position(this.Start - shiftToUse.Item1, this.End - shiftToUse.Item2);
        }

        internal OverlapType GetOverlap(Position potentialInternal)
        {
            bool afterEnd = this.End <= potentialInternal.Start;

            //Our start is LEFT of the other start
            bool startOverlap = this.Start < potentialInternal.Start;
            //Our end is RIGHT of the other end.
            bool endOverlap = this.End > potentialInternal.End;

            if (afterEnd)
                return OverlapType.None;

            if (startOverlap && !endOverlap)
                return OverlapType.Right;

            if (startOverlap && endOverlap)
                return Position.OverlapType.Total;


            throw new NotImplementedException();

        }

        public enum OverlapType
        {
            /// <summary>
            /// The specified parameter starts after the end of the previous tag.
            /// </summary>
            None = 0,
            Left = 1,
            /// <summary>
            /// The specified parameter starts before the end of the previous tag, but the end finishes afterwards.
            /// <u> aa <b>cc</u></b>
            /// </summary>
            Right = 2,
            /// <summary>
            /// The specified parameter starts after the start of the previous tag, and the end finises before the end of the previous tag.
            /// example: <u> <b></b> </u>
            /// </summary>
            Total = 3
        }


    }
}
