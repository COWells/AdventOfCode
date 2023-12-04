using CommonUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2023
{
    public class Day04 : AdventDay
    {
        public override string CurrentDay => "Day04";

        public override void SolveTask1(List<string> input)
        {
            var sum = 0;
            var scratchCardList = Parse(input, new Regex(@"\d+")); // One or more digits

            foreach (var card in scratchCardList)
            {
                if (card.Matches != 0)
                {
                    sum += int.Parse(Math.Pow(2, card.Matches - 1).ToString());
                }
            }

            Logger.OutputToFile($"Task 1: {sum}", CurrentDay);
        }

        public override void SolveTask2(List<string> input)
        {
            var scratchCardList = Parse(input, new Regex(@"\d+")); // One or more digits
            var counts = scratchCardList.Select(_ => 1).ToArray(); // Create an array - Same size as scratchCardList with only 1s

            for (var i = 0; i < scratchCardList.Count(); i++)
            {
                var (card, count) = (scratchCardList[i], counts[i]);
                for (var j = 0; j < card.Matches; j++)
                {
                    counts[i + j + 1] += count;
                }
            }

            Logger.OutputToFile($"Task 2: {counts.Sum()}", CurrentDay);
        }

        public List<ScratchCard> Parse(List<string> input, Regex rx)
        {
            var scratchCardList = new List<ScratchCard>();

            for (int i = 0; i < input.Count; i++)
            {
                var parts = input[i].Split(':', '|');
                var left = from match in Regex.Matches(parts[1], rx.ToString()) select match.Value;
                var right = from match in Regex.Matches(parts[2], rx.ToString()) select match.Value;

                scratchCardList.Add(new ScratchCard(left, right));
            }

            return scratchCardList;
        }
    }
}
