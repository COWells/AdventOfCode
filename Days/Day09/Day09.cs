using CommonUtils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2023
{
    public class Day09 : AdventDay
    {
        public override string CurrentDay => "Day09";

        public override void SolveTask1(List<string> input)
        {
            var dataset = ParseInput(input);
            var answer = dataset.Sum(line => ExtrapolateRecursive(line));

            Logger.OutputToFile($"Task 1: {answer}", CurrentDay);
        }

        public override void SolveTask2(List<string> input)
        {
            var dataset = ParseInput(input);
            var answer = dataset.Sum(line => ExtrapolateRecursive(line.Reverse().ToArray()));

            Logger.OutputToFile($"Task 2: {answer}", CurrentDay);
        }

        private int[][] ParseInput(List<string> input)
            => input.Select(line => StringFormatter.SplitAndRemovingTrailingWhitespace(line, ' ')
                    .Select(s => int.Parse(s)).ToArray()).ToArray();   


        // Thought that this would have to be more sophisticated
        // (Using derivatives or binomial expansion to calculate this without recursion)
        // However, since the test inputs are sufficiently small, this works.
        private int ExtrapolateRecursive(int[] list)
        {
            if (list.Length == 0 || list.All(x => x == 0))
            {
                return 0;
            }

            if (list.Length == 1)
            {
                return list[0];
            }

            int[] differences = new int[list.Length - 1];
            for (int i = 0; i < differences.Length; i++) 
            {
                differences[i] = list[i + 1] - list[i];
            }
            var nextDifference = ExtrapolateRecursive(differences);
            return list.Last() + nextDifference;
        }
    }
}
