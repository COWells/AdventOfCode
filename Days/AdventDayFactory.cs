namespace AdventOfCode_Days
{
    public static class AdventDayFactory
    {
        public static T CreateDay<T>() where T : AdventDay, new()
        {
            return new T();
        }
    }
}
