using CommonUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2023
{
    public class Day3 : AdventDay
    {
        public override string CurrentDay => "Day3";

        public override void SolveTask1(List<string> input)
        {
            var symbols = Parse(input, new Regex(@"[^.0-9]"));
            var nums = Parse(input, new Regex(@"\d+"));

            var sum = nums.Where(part => symbols.Any(s => Adjacent(s, part)))
                          .Select(x => x.Int)
                          .Sum();

            Logger.OutputToFile($"Task 1: {sum}", CurrentDay);
        }

        public override void SolveTask2(List<string> input)
        {
            var gears = Parse(input, new Regex(@"\*"));
            var numbers = Parse(input, new Regex(@"\d+"));

            var sum = 0;
            foreach (var gear in gears)
            {
                var neighbours = numbers.Where(x => Adjacent(x, gear)).Select(x => x.Int);

                if (neighbours.Count() == 2)
                {
                    sum += neighbours.First() * neighbours.Last();
                }
            }

            Logger.OutputToFile($"Task 2: {sum}", CurrentDay);
        }

        public Part[] Parse(List<string> rows, Regex rx)
        {
            var partList = new Part[] { };
            for (int i = 0; i < rows.Count; i++)
            {
                foreach (Match match in rx.Matches(rows[i]))
                {
                    partList = partList.Append(new Part(match.Value, i, match.Index)).ToArray();
                }
            }
            return partList;
        }

        // Checks that parts are adjacent, i.e. rows are within 1 
        // step and also the columns (using https://stackoverflow.com/a/3269471).
        private bool Adjacent(Part p1, Part p2)
        {
            return (Math.Abs(p2.Row - p1.Row) <= 1 &&
            p1.Column <= p2.Column + p2.Text.Length &&
            p2.Column <= p1.Column + p1.Text.Length);
        }
    }
}
