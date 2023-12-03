using CommonUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2023
{
    public class Day03 : AdventDay
    {
        public override string CurrentDay => "Day03";

        public override void SolveTask1(List<string> input)
        {
            var symbols = Parse(input, new Regex(@"[^.0-9]")); // Anything except 0 through 9 and '.'
            var nums = Parse(input, new Regex(@"\d+")); // One or more digits

            var sum = nums.Where(num => symbols.Any(symbol => Adjacent(symbol, num)))
                          .Select(num => num.Number)
                          .Sum();

            Logger.OutputToFile($"Task 1: {sum}", CurrentDay);
        }

        public override void SolveTask2(List<string> input)
        {
            var gears = Parse(input, new Regex(@"\*")); // Exactly 1 *
            var nums = Parse(input, new Regex(@"\d+")); // One or more digits

            var sum = 0;
            foreach (var gear in gears)
            {
                var neighbours = nums.Where(num => Adjacent(num, gear))
                                     .Select(x => x.Number);

                if (neighbours.Count() == 2)
                {
                    sum += neighbours.First() * neighbours.Last();
                }
            }

            Logger.OutputToFile($"Task 2: {sum}", CurrentDay);
        }

        public IEnumerable<PointOfInterest> Parse(List<string> rows, Regex rx)
        {
            var partList = new List<PointOfInterest>();
            for (int i = 0; i < rows.Count; i++)
            {
                foreach (Match match in rx.Matches(rows[i]))
                {
                    partList.Add(new PointOfInterest(match.Value, i, match.Index));
                }
            }
            return partList;
        }

        // Checks that parts are adjacent, i.e. rows are within 1 
        // step and also the columns (using https://stackoverflow.com/a/3269471).
        private bool Adjacent(PointOfInterest p1, PointOfInterest p2)
        {
            Console.WriteLine(p1.ToString());

            return (Math.Abs(p2.Row - p1.Row) <= 1 &&
            p1.Column <= p2.Column + p2.Text.Length &&
            p2.Column <= p1.Column + p1.Text.Length);
        }
    }
}
