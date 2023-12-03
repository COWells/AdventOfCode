using CommonUtils;
using System.Collections.Generic;

namespace AdventOfCode2023
{
    public class Day02 : AdventDay
    {
        public override string CurrentDay => "Day02";

        public override void SolveTask1(List<string> input)
        {
            int gameCount = 0;
            int IDSum = 0;
            var allowedCubes = new Dictionary<CubeColoursEnum, int>()
            {
                {CubeColoursEnum.red, 12 },
                {CubeColoursEnum.green, 13 },
                {CubeColoursEnum.blue, 14 },
            };

            foreach (var game in input)
            {
                gameCount++;

                var maximalCubes = CreateEmptyCubeColoursDictionary();

                foreach (var round in ParseRounds(game))
                {
                    maximalCubes.AddRoundToCubeDictionary(round);
                }

                if (IsGamePossible(maximalCubes, allowedCubes))
                {
                    IDSum += gameCount;
                }
            }

            Logger.OutputToFile($"Task 1: {IDSum}", CurrentDay);
        }

        public override void SolveTask2(List<string> input)
        {
            int IDSum = 0;

            foreach (var game in input)
            {
                int power = 1;

                var maximalCubes = CreateEmptyCubeColoursDictionary();

                foreach (var round in ParseRounds(game))
                {
                    maximalCubes.AddRoundToCubeDictionary(round);
                }

                foreach (var key in maximalCubes.Keys)
                {
                    power *= maximalCubes[key];
                }

                IDSum += power;
            }

            Logger.OutputToFile($"Task 2: {IDSum}", CurrentDay);
        }

        private Dictionary<CubeColoursEnum, int> CreateEmptyCubeColoursDictionary() => new Dictionary<CubeColoursEnum, int>()
            {
                {CubeColoursEnum.red, 0 },
                {CubeColoursEnum.green, 0 },
                {CubeColoursEnum.blue, 0 },
            };

        private string[] ParseRounds(string game)
        {
            //Remove "Game: " from the input string and remove all spaces
            game = StringFormatter.SplitAndRemovingTrailingWhitespace(game, ':')[1];
            var rounds = StringFormatter.SplitAndRemovingTrailingWhitespace(game, ';');
            return rounds;
        }

        private bool IsGamePossible(Dictionary<CubeColoursEnum, int> maximalCubes, Dictionary<CubeColoursEnum, int> allowedCubes)
        {
            foreach (var key in allowedCubes.Keys)
            {
                if (allowedCubes[key] < maximalCubes[key])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
