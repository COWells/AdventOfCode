using AdventOfCode_Days;
using CommonUtils;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net.WebSockets;
using System.Reflection.Metadata.Ecma335;
using System.Runtime;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode_2023
{
    public class Day08 : AdventDay
    {
        private readonly string nodeRegex = @"\w+";
        public override string CurrentDay => "2023/Day08";

        public override void SolveTask1(List<string> input)
        {
            var steps = input[0];
            var nodes = ParseInput(input.Skip(2));

            var numberOfSteps = FindPath("AAA", n => n.Id == "ZZZ", nodes, steps).Count;
            Console.WriteLine($"Task 1: {numberOfSteps}");
        }

        public override void SolveTask2(List<string> input)
        {
            // Note we are told that number of nodes ending with 'A' is equal to number of nodes ending with 'Z'
            var steps = input[0];
            var nodes = ParseInput(input.Skip(2));

            var pathLengths = nodes.Values.Where(n => n.Id.EndsWith('A')).
                              Select(node => FindPath(node.Id, node => node.Id.EndsWith('Z'), nodes, steps)).
                              Select(path => (long)path.Count);

            // Discover that each path has prime decomposition of 281 x P (for some prime P)
            // This means that if we if we divide by 281, we have 6 pairwise coprime integers
            // Thus, we can calculate the LCM by the Chinese Remainder Theorem
            // (Since these will all be 0 mod p, this becomes the trivial case of simply calculating the LCM).
            var LCM = LowestCommonMultiple(pathLengths);
            Console.WriteLine($"Task 2: {LCM}");
        }

        private ImmutableDictionary<string, Node> ParseInput(IEnumerable<string> input)
            => input.Select(line => Regex.Matches(line, nodeRegex))
                    .Select(parts => new Node(parts[0].Value, parts[1].Value, parts[2].Value))
                    .ToImmutableDictionary(node => node.Id);

        private List<string> FindPath(string from, Func<Node, bool> condition, ImmutableDictionary<string, Node> nodes, string instructions)
        {
            var path = new List<string>();
            var current = nodes[from];
            var stepCount = 0;

            while (!condition(current))
            {
                path.Add(current.Id);
                current = nodes[instructions[stepCount % instructions.Length] == 'L' ? current.Left : current.Right];
                stepCount++;
            }

            return path;
        }

        private long LowestCommonMultiple(IEnumerable<long> numbers) 
            => numbers.Aggregate((long)1, (current, number) => current / GreatestCommonDivisor(current, number) * number);

        private long GreatestCommonDivisor(long a, long b)
        {
            while (b != 0)
            {
                a %= b;
                (a, b) = (b, a);
            }
            return a;
        }
    }
}
