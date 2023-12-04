using System;

namespace AdventOfCode2023
{
    public record PointOfInterest(string Text, int Row, int Column)
    {
        public int Number => int.Parse(Text);
    }
}
