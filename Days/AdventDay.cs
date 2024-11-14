using System.Collections.Generic;

namespace AdventOfCode_Days
{
    public abstract class AdventDay
    {
        public abstract string CurrentDay { get; }

        public abstract void SolveTask1(List<string> input);

        public abstract void SolveTask2(List<string> input);
    }
}