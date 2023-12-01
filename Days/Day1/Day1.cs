﻿using CommonUtils;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2023
{
    public class Day1 : BaseAdventDay
    {
        protected override string CurrentDay => "Day1";

        private static Dictionary<string, int> m_DigitDictionary = new Dictionary<string, int>()
        {
            { "one", 1 },
            { "two", 2 },
            { "three", 3 },
            { "four", 4 },
            { "five", 5 },
            { "six", 6 },
            { "seven", 7 },
            { "eight", 8 },
            { "nine", 9 },
        };

        protected override string SolveTask1(List<string> input)
        {
            int count = 0;
            foreach (var line in input)
            {
                int firstDigit = int.Parse(line.First(x => char.IsDigit(x)).ToString());
                int lastDigit = int.Parse(line.Last(x => char.IsDigit(x)).ToString());

                count += firstDigit * 10 + lastDigit;
            }

            return $"Task 1: {count}";
        }

        protected override string SolveTask2(List<string> input)
        {
            int count = 0;
            foreach (var line in input)
            {
                var firstDigit = FindFirstDigit(line, reverseDictionary: false);
                var lastDigit = FindFirstDigit(StringFormatter.ReverseString(line), reverseDictionary: true);

                count += firstDigit * 10 + lastDigit;
            }

            return $"Task 2: {count}";
        }

        private static int FindFirstDigit(string line, bool reverseDictionary)
        {
            int firstDigit = -1;
            var firstIndex = line.Length;

            if (int.TryParse(line.FirstOrDefault(x => char.IsDigit(x)).ToString(), out firstDigit))
            {
                firstIndex = line.IndexOf(line.First(x => char.IsDigit(x)));
            }

            foreach (var digit in m_DigitDictionary)
            {
                var index = reverseDictionary
                    ? line.IndexOf(StringFormatter.ReverseString(digit.Key))
                    : line.IndexOf(digit.Key);

                if (index != -1 && index < firstIndex)
                {
                    firstDigit = digit.Value;
                    firstIndex = index;
                }
            }

            return firstDigit;
        }
    }
}
