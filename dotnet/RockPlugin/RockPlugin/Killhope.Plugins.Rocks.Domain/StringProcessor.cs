using Killhope.Plugins.Rocks.Domain.Formatting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Killhope.Plugins.Rocks.Domain
{
    public class StringProcessor
    {

        public static result ProcessString(string input, IEnumerable<TextFormattingRule> rules)
        {
            //TODO: This algortihm is terribly complicated due to it being single-pass.
            //Would make more sent to convert to multipass.

            var dict = new Dictionary<TextFormattingRule, ICollection<Position>>();



            List<Tuple<TextFormattingRule, IEnumerator<Position>>> positionEnumerator = generateFirstPositions(rules, input);

            //Potentially, tags can be wrapped:
            //<b><u>...</u></b>
            //<b>aa<u>bb</u>cc</b>dd
            //as this goes through sequentially, in each instance it would be:
            //<b><u>
            //Once u is modifed, b will also need to be shifted as b wraps u.



            var possibleOverlaps = new List<WrappedFormat>();




            string modifiedInput = input;
            int totalShift = 0;
            while (positionEnumerator.Count > 0)
            {
                //sort the items
                positionEnumerator.Sort((a, b) => a.Item2.Current.CompareTo(b.Item2.Current));

                IEnumerator<Position> currentEnumerator = positionEnumerator[0].Item2;
                var rule = positionEnumerator[0].Item1;


                //The position in the original string.
                Position toProcess = currentEnumerator.Current;

                //remove any non overlaps from PossibleOverlaps
                totalShift = UpdatePossibleOverlaps(dict, possibleOverlaps, totalShift, rule, toProcess);



                var currentShift = getProcessPosition(possibleOverlaps, toProcess);
                var shiftToUse = new Tuple<int, int>(currentShift.Item1 + totalShift, currentShift.Item2 + totalShift);

                //The position in the current string.
                Position processPosition = toProcess.Shift(shiftToUse);

                //The position in the string after .Process()
                Position outputPosition = processPosition.Shift(rule.ShiftAmount);


                string newInput = rule.Process(modifiedInput, processPosition);

                Debug.Assert(modifiedInput.Length - newInput.Length == rule.TotalLength);


                //foreach (var overlap in possibleOverlaps)
                //    HandleOverlap(dict, overlap, rule);


                if (!dict.ContainsKey(rule))
                    dict.Add(rule, new List<Position>());

                //Note: This works on the position of the original string, hence using toProcess.
                var wf = new WrappedFormat(rule, outputPosition);
                wf.OriginalPosition = toProcess;
                possibleOverlaps.Add(wf);

                dict[rule].Add(outputPosition);


                if (!currentEnumerator.MoveNext())
                    positionEnumerator.Remove(positionEnumerator[0]);

                modifiedInput = newInput;
            }


            foreach (var item in possibleOverlaps)
            {
                FixDict(dict, item);
            }

            return new result() { Value = modifiedInput, Positions = dict };

        }

        private static int UpdatePossibleOverlaps(
            Dictionary<TextFormattingRule, ICollection<Position>> dict,
            List<WrappedFormat> possibleOverlaps,
            int totalShift,
            TextFormattingRule rule,
            Position toProcess)
        {
            Func<Position, WrappedFormat, bool> toRemovePredicate = (pos2, outside) => outside.OriginalPosition.GetOverlap(pos2) == Position.OverlapType.None;
            IEnumerable<WrappedFormat> toRemove = from p in possibleOverlaps where toRemovePredicate(toProcess, p) select p;

            foreach (var item in toRemove.ToList())
            {
                totalShift += item.Rule.TotalLength;
                FixDict(dict, item);
                possibleOverlaps.Remove(item);
            }

            //TODO: Extracting this snippet back would remove the rule variable.
            foreach (var item in possibleOverlaps)
                item.AddRule(rule, item.OriginalPosition.GetOverlap(toProcess));

            return totalShift;
        }

        /// <summary>
        /// Transforms the input into a collection of iterators and their associated value.
        /// </summary>
        /// <param name="rules"></param>
        /// <param name="input"></param>
        /// <remarks>This collection is sorted based on the currently available position, the lowest is taken, executed and the iterator advanced. Execution stops when all iterators are completed.</remarks>
        /// <returns></returns>
        private static List<Tuple<TextFormattingRule, IEnumerator<Position>>> generateFirstPositions(IEnumerable<TextFormattingRule> rules, string input)
        {
            var pos = new List<Tuple<TextFormattingRule, IEnumerator<Position>>>();
            foreach (var r in rules)
            {
                var enumerator = r.GetAllPositons(input).GetEnumerator();
                if (enumerator.MoveNext())
                    pos.Add(new Tuple<TextFormattingRule, IEnumerator<Position>>(r, enumerator));
            }
            return pos;
        }

        private static void FixDict(Dictionary<TextFormattingRule, ICollection<Position>> dict, WrappedFormat item)
        {
            if (!item.wrappedRules().Any())
                return;

            if (!dict[item.Rule].Remove(item.Position))
                throw new InvalidOperationException("wrapped value is not contained.");

            dict[item.Rule].Add(item.ModifiedPosition);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="possibleOverlaps"></param>
        /// <param name="currentPosition">The position in the original string</param>
        /// <returns></returns>
        private static Tuple<int, int> getProcessPosition(List<WrappedFormat> possibleOverlaps, Position currentPosition)
        {
            int total1 = 0;
            int total2 = 0;
            foreach (var i in possibleOverlaps)
            {
                Position outside = i.OriginalPosition;
                var type = outside.GetOverlap(currentPosition);

                if (type == Position.OverlapType.Left)
                    throw new NotImplementedException();

                if (type == Position.OverlapType.None)
                    throw new InvalidOperationException("function should not be called on a position with no overlap");

                if (type == Position.OverlapType.Total)
                {
                    total1 += i.Rule.StartLength;
                    total2 += i.Rule.StartLength;
                }

                if (type == Position.OverlapType.Right)
                {
                    total1 += i.Rule.StartLength;
                    total2 += i.Rule.TotalLength;
                }

            }
            return new Tuple<int, int>(total1, total2);
        }

        private static int getProcessPosition(List<Tuple<TextFormattingRule, Position>> possibleOverlaps, Position currentPosition)
        {
            int total = 0;
            foreach (var i in possibleOverlaps)
            {
                Position outside = i.Item2;
                var type = outside.GetOverlap(currentPosition);

                //TODO: Only been tested on Total
                if (type == Position.OverlapType.None)
                    throw new InvalidOperationException("function should not be called on a position with no overlap");

                if (type == Position.OverlapType.Total || type == Position.OverlapType.Left)
                    total += i.Item1.StartLength;

                if (type == Position.OverlapType.Right)
                    total += i.Item1.EndLength;

            }
            return total;
        }

        internal static string ApplyRules(string value, Dictionary<TextFormattingRule, ICollection<Position>> dictionary)
        {
            //Initially, we want a sorted list of Positions with associated rule, rather than the other way around.
            //Note that a collection is used as Position is 
            var rules = generateSortedRuleList(dictionary);

            //Each time we perform a replacement, the index into the string where we perform the next increment will be shifted.
            //There are multiple cases for this to consider (related to nested tags)
            IncrementalStringReplacer r = new IncrementalStringReplacer(value);

            foreach (var kvp in rules)
            {
                foreach (var rule in kvp.Value)
                    r.IncrementPosition(kvp.Key, rule);
            }

            return r.Value;
        }

        /// <summary>
        /// Reverses the supplied dictionary.
        /// </summary>
        /// <param name="dictionary">The dictionary to reverse.</param>
        /// <returns></returns>
        private static SortedDictionary<Position, ICollection<TextFormattingRule>> generateSortedRuleList(Dictionary<TextFormattingRule, ICollection<Position>> dictionary)
        {
            if (dictionary == null)
                throw new ArgumentNullException("dictionary");

            SortedDictionary<Position, ICollection<TextFormattingRule>> rt = new SortedDictionary<Position, ICollection<TextFormattingRule>>();

            foreach (var a in dictionary)
            {
                foreach (Position pos in a.Value)
                {
                    if (!rt.ContainsKey(pos))
                        rt.Add(pos, new List<TextFormattingRule>());
                    rt[pos].Add(a.Key);
                }
            }
            return rt;
        }


    }

    public class result
    {
        //TODO: IENunerablee
        public string Value;
        public Dictionary<TextFormattingRule, ICollection<Position>> Positions;

        public result()
        {
            Positions = new Dictionary<TextFormattingRule, ICollection<Position>>();
        }
    }
}
