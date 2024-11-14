namespace AdventOfCode_2023
{
    internal record MapRow(long Source, long Destination, long Length)
    {
        public long SourceEnd => Source + Length;
    }
}