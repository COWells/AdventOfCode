using CommonUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    public class Day07 : AdventDay
    {
        public override string CurrentDay => "Day07";

        public override void SolveTask1(List<string> input)
        {
            var hands = ParseInput(input).ToArray();
            Array.Sort(hands, (a, b) => CompareHands(a.hand, b.hand, "23456789TJQKA", jokersIncluded: false));

            int winnings = 0;
            for (int i = 0; i < hands.Length; i++)
            {
                winnings += (i+1) * hands[i].bid;
            }

            Console.WriteLine($"Task 1: {winnings}");
        }

        public override void SolveTask2(List<string> input)
        {
            var hands = ParseInput(input).ToArray();
            Array.Sort(hands, (a, b) => CompareHands(a.hand, b.hand, "J23456789TQKA", jokersIncluded: true));

            int winnings = 0;
            for (int i = 0; i < hands.Length; i++)
            {
                winnings += (i + 1) * hands[i].bid;
            }

            Console.WriteLine($"Task 2: {winnings}");
        }

        private List<Hand> ParseInput(List<string> input) 
        {
            List<Hand> hands = [];
            foreach (var line in input)
            {
                var parts = StringFormatter.SplitAndRemovingTrailingWhitespace(line, ' ');
                hands.Add(new Hand(parts[0], int.Parse(parts[1])));
            }

            return hands;
        }

        private int CompareHands(string a, string b, string cardOrder, bool jokersIncluded)
        {
            var handComparison = jokersIncluded 
                ? EvaluateHandWithJokers(a).CompareTo(EvaluateHandWithJokers(b))
                : EvaluateHand(a).CompareTo(EvaluateHand(b));

            if (handComparison != 0)
            {
                return handComparison;
            }

            for (int i = 0; i < a.Length && i < b.Length; i++)
            {
                int cardComparison = cardOrder.IndexOf(a[i]).CompareTo(cardOrder.IndexOf(b[i]));
                if (cardComparison != 0)
                {
                    return cardComparison;
                }
            }

            return a.Length.CompareTo(b.Length);
        }

        private HandStrengthEnum EvaluateHand(string hand)
        {
            var groups = hand.GroupBy(c => c).Select(g => (g.Key, g.Count())).OrderByDescending(g => g.Item2).ToArray();
            if (groups[0].Item2 >= 5)
            {
                return HandStrengthEnum.FIVE_OF_A_KIND;
            }
            else if (groups[0].Item2 == 4)
            {
                return HandStrengthEnum.FOUR_OF_A_KIND;
            }
            else if (groups[0].Item2 == 3 && groups[1].Item2 == 2)
            {
                return HandStrengthEnum.FULL_HOUSE;
            }
            else if (groups[0].Item2 == 3 && groups[1].Item2 == 1)
            {
                return HandStrengthEnum.THREE_OF_A_KIND;
            }
            else if (groups[0].Item2 == 2 && groups[1].Item2 == 2)
            {
                return HandStrengthEnum.TWO_PAIR;
            }
            else if (groups[0].Item2 == 2 && groups[1].Item2 == 1)
            {
                return HandStrengthEnum.ONE_PAIR;
            }
            else
            {
                return HandStrengthEnum.HIGH_CARD;
            }
        }

        private HandStrengthEnum EvaluateHandWithJokers(string hand)
        {
            if (hand.All(c => c == 'J'))
            {
                return EvaluateHand(hand);
            }

            char mostCommonNonJoker = hand.Where(card => card != 'J')
                .GroupBy(c => c)
                .MaxBy(g => g.Count()).Key;

            return EvaluateHand(hand.Replace('J', mostCommonNonJoker));
        }
    }
}
