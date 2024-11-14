using AdventOfCode_Days;
using CommonUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode_2023
{
    public class Day05 : AdventDay
    {
        public override string CurrentDay => "2023/Day05";

        public override void SolveTask1(List<string> input)
        {
            var maps = GenerateMaps(input);

            var seeds = (from m in Regex.Matches(input[0], @"\d+") // Get all the seed ints
                         select long.Parse(m.Value)).ToArray();

            var lowestLocation = long.MaxValue;
            foreach (var seed in seeds)
            {
                var location = ResolveAll(seed, maps);
                lowestLocation = Math.Min(lowestLocation, location);
            }

            Logger.OutputToFile($"Task 1: {lowestLocation}", CurrentDay);
        }

        public override void SolveTask2(List<string> input)
        {
            var maps = GenerateMaps(input);

            var seedData = (from m in Regex.Matches(input[0], @"\d+") // Get all the seed ints
                         select long.Parse(m.Value)).ToArray();

            var seeds = GenerateSeedRanges(seedData);

            var lowestLocation = long.MaxValue;
            foreach (var seed in seeds)
            {
                var seedRange = new Range(seed.Start, seed.Length, null, 0);
                var ranges = new List<Range>() { seedRange };

                foreach (var map in maps)
                {
                    var newRanges = new List<Range>();

                    foreach (var range in ranges)
                    {
                        newRanges.AddRange(ResolveRange(range, map));
                    }
                    ranges = newRanges;
                }

                var lowestLocationInSeedRange = ranges.Min(r => r.Start);
                lowestLocation = Math.Min(lowestLocation, lowestLocationInSeedRange);
            }

            Logger.OutputToFile($"Task 2: {lowestLocation}", CurrentDay);
        }

        private List<List<MapRow>> GenerateMaps(List<string> input)
        {
            var maps = new List<List<MapRow>>();
            var mapBuilder = new List<MapRow>();

            // Skip the first line as we handle seeds seperately.
            foreach (var line in input.Skip(1))
            {
                if (line.Contains("map"))
                {
                    continue;
                }

                if (string.IsNullOrWhiteSpace(line))
                {
                    if (mapBuilder.Count > 0)
                    {
                        maps.Add(mapBuilder);
                    }
                    mapBuilder = [];
                    continue;
                }

                var parts = line.Split(' ').Select(long.Parse).ToArray();
                mapBuilder.Add(new MapRow(parts[1], parts[0], parts[2]));
            }

            if (mapBuilder.Count > 0)
            {
                maps.Add(mapBuilder);
            }

            return maps;
        }

        private List<SeedRange> GenerateSeedRanges(long[] seedData)
        {
            var seedRanges = new List<SeedRange>();
            for (var i = 0; i < seedData.Length; i += 2)
            {
                seedRanges.Add(new SeedRange(seedData[i], seedData[i + 1]));
            }
            return seedRanges;
        }

        private long ResolveAll(long value, List<List<MapRow>> maps)
        {
            foreach (var map in maps)
            {
                value = Resolve(value, map); // Update value
            }
            return value;
        }

        private long Resolve(long value, List<MapRow> map)
        {
            foreach (var row in map)
            {
                var offset = value - row.Source;
                if (offset >= 0 && offset < row.Length)
                {
                    return row.Destination + offset;
                }
            }
            return value;
        }

        private static List<Range> ResolveRange(Range inputRange, List<MapRow> map)
        {
            var outputs = new List<Range>();
            var inputs = new Queue<Range>();

            inputs.Enqueue(inputRange);

            while (inputs.TryDequeue(out var input))
            {
                var foundRow = false;

                foreach (var row in map)
                {
                    if (row.Source >= input.End || input.Start >= row.SourceEnd)
                    {
                        continue;
                    }

                    var start = Math.Max(row.Source, input.Start);
                    var end = Math.Min(row.SourceEnd, input.End);
                    var shift = row.Destination - row.Source;
                    var shifted = new Range(start + shift, end - start, input, shift);
                    outputs.Add(shifted);

                    if (input.Start < start)
                    {
                        var before = input with { Start = input.Start, Length = start - input.Start };
                        inputs.Enqueue(before);
                    }

                    if (input.End > end)
                    {
                        var after = input with { Start = end, Length = input.End - end };
                        inputs.Enqueue(after);
                    }

                    foundRow = true;
                    break;
                }

                if (!foundRow)
                {
                    outputs.Add(input);
                }
            }

            return outputs;
        }
    }
}
