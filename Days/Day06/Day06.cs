using CommonUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    public class Day06 : AdventDay
    {
        public override string CurrentDay => "Day06";

        public override void SolveTask1(List<string> input)
        {;
            var times = ParseInts(input[0]);
            var distances = ParseInts(input[1]);
            var product = 1;

            for (var i = 0; i < times.Length; i++)
            {
                product *= SolveEquation(times[i], distances[i]);
            }

            Logger.OutputToFile($"Task 1: {product}", CurrentDay);
        }

        public override void SolveTask2(List<string> input)
        {
            var time = ParseTask2Input(input[0]);
            var distance = ParseTask2Input(input[1]);

            Logger.OutputToFile($"Task 2: {SolveEquation(time, distance)}", CurrentDay);
        }

        private long[] ParseInts(string input)
            => (from m in Regex.Matches(input, @"\d+") // Get all ints
                select long.Parse(m.Value)).ToArray();

        private long ParseTask2Input(string input)
        {
            var times = StringFormatter.SplitAndRemovingTrailingWhitespace(input, ':')[1];
            return long.Parse(string.Concat(times.Where(c => !char.IsWhiteSpace(c))));
        }

        // x(T-x) > D  where x is the time holding the button, T is the total race time and D is the distance we must exceed
        // xT - x^2 > D
        // x^2 - xT < -D
        // x^2 - xT + D < 0
        // Which is a standard quadratic equation - Find the roots of this equation and then find all integers between the roots.
        private int SolveEquation(long time, long distance)
        {
            // -0.0001 and +0.0001 add offsets to deal with exact integer roots
            var upperRoot = ((time + Math.Sqrt(Math.Pow(time, 2) - (4 * distance * 1))) / 2) - 0.0001;
            var lowerRoot = ((time - Math.Sqrt(Math.Pow(time, 2) - (4 * distance * 1))) / 2) + 0.0001;

            var differenceBetweenRoots = (int)Math.Floor(upperRoot) - (int)Math.Floor(lowerRoot);
            return (int)(Math.Floor(upperRoot) - Math.Floor(lowerRoot));
        }
    }
}
