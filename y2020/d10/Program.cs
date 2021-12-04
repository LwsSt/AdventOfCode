using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace AdventOfCode.y2020.d10
{
    public class Program : IPuzzle
    {
        private readonly List<int> input;

        public Program()
        {
            input = ParseInput();
        }

        public void Part1()
        {
            var differences = CalculateDifferences().ToList();

            if (differences.Any(i => i > 3))
            {
                throw new Exception("Invalid differences");
            }

            int diff_1 = differences.Count(i => i == 1);
            int diff_3 = differences.Count(i => i == 3);

            Console.WriteLine("1: {0}", diff_1);
            Console.WriteLine("3: {0}", diff_3);
            Console.WriteLine("{0}", diff_1 * diff_3);

            IEnumerable<int> CalculateDifferences()
            {
                yield return input[0];
                for (int i = 0; i < input.Count - 1; i++)
                {
                    yield return input[i + 1] - input[i];
                }

                yield return 3;         
            }
        }

        public void Part2()
        {
            input.Insert(0, 0);
            Dictionary<int, long> sums = input.ToDictionary(i => i, _ => 0L);
            sums[0] = 1;

            for (int i = 1; i < input.Count; i++)
            {
                for (int j = 1; j <= 3; j++)
                {
                    if (sums.ContainsKey(input[i] - j))
                    {
                        sums[input[i]] += sums[input[i] - j];
                    }
                }
            }

            Console.WriteLine(sums[input[^1]]);
        }

        public List<int> ParseInput()
        {
            return File.ReadLines(@"y2020\d10\input.puzzle")
                .Select(l => int.Parse(l))
                .OrderBy(i => i)
                .ToList();
        }
    }
}