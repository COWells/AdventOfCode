using CommonUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    class Program
    {
        static void Main(string[] args)
        {
            SolveDay(AdventDayFactory.CreateDay<Day1>());
        }

        private static void SolveDay(AdventDay adventDay)
        {
            List<string> input = FileReader.ReadAllLines(adventDay.CurrentDay);
            adventDay.SolveTask1(input);
            adventDay.SolveTask2(input);
        }
    }
}
