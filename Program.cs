using CommonUtils;
using System.Collections.Generic;

namespace AdventOfCode2023
{
    class Program
    {
        static void Main(string[] args)
        {
            SolveDay(AdventDayFactory.CreateDay<Day08>());
        }

        private static void SolveDay(AdventDay adventDay)
        {
            List<string> input = FileReader.ReadAllLines(adventDay.CurrentDay);
            adventDay.SolveTask1(input);
            adventDay.SolveTask2(input);
        }
    }
}
