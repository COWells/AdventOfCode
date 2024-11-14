using CommonUtils;
using System;
using System.Collections.Generic;

namespace AdventOfCode_2023
{
    internal static class Day02ExtensionMethods
    {
        internal static Dictionary<CubeColoursEnum, int> AddRoundToCubeDictionary(this Dictionary<CubeColoursEnum, int> maximalCubes, string round)
        {
            var cubesPulled = StringFormatter.SplitAndRemovingTrailingWhitespace(round, ',');

            foreach (var cubePull in cubesPulled)
            {
                var quantityAndColour = StringFormatter.SplitAndRemovingTrailingWhitespace(cubePull, ' ');
                int quantity = int.Parse(quantityAndColour[0].ToString());
                CubeColoursEnum colour = (CubeColoursEnum)Enum.Parse(typeof(CubeColoursEnum), quantityAndColour[1]);
                maximalCubes[colour] = Math.Max(maximalCubes[colour], quantity);
            }

            return maximalCubes;
        }
    }
}
