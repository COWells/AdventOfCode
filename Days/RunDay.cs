using CommonUtils;
using System.Collections.Generic;

namespace AdventOfCode_Days
{
    class RunDay
    {
        static void Main(string[] args)
        {
            SolveDay(AdventDayFactory.CreateDay<AdventOfCode_2023.Day05>());
        }

        private static void SolveDay(AdventDay adventDay)
        {
            List<string> input = FileReader.ReadAllLines(adventDay.CurrentDay);
            adventDay.SolveTask1(input);
            adventDay.SolveTask2(input);
        }
    }
}
