﻿using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2023
{
    public record ScratchCard(IEnumerable<string> Left, IEnumerable<string> Right)
    {
        public int Matches => Left.Intersect(Right).Count();
    }
}
