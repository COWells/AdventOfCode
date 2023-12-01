using CommonUtils;
using System.Collections.Generic;

namespace AdventOfCode2023
{
    public abstract class BaseAdventDay
    {
        protected abstract string CurrentDay { get; }
        public void SolveDay()
        {
            List<string> input = FileReader.ReadAllLines(CurrentDay);
            Logger.OutputToFile(SolveTask1(input), CurrentDay);
            Logger.OutputToFile(SolveTask2(input), CurrentDay);
        }

        protected abstract string SolveTask1(List<string> input);

        protected abstract string SolveTask2(List<string> input);
    }
}
