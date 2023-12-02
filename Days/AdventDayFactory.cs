using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    public static class AdventDayFactory
    {
        public static T CreateDay<T>() where T : AdventDay, new()
        {
            return new T();
        }
    }
}
