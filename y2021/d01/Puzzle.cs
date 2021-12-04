using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.y2021.d01
{
    public class Puzzle : IPuzzle
    {
        private readonly List<int> input;

        public Puzzle()
        {
            input = File.ReadLines(@"y2021\d01\input.puzzle")
                .Select(l => int.Parse(l))
                .ToList();
        }

        public void Part1()
        {
            int count = CalculateIncrements(input);
            Console.WriteLine(count);
        }

        public void Part2()
        {
            var input2 = SlidingWindow()
                .Select(arr => arr.Sum())
                .ToList();

            int count = CalculateIncrements(input2);
            Console.WriteLine(count);

            IEnumerable<int[]> SlidingWindow()
            {
                for (int i = 0; i < input.Count - 2; i++)
                {
                    yield return new int[] { input[i], input[i + 1], input[i + 2] };
                }
            }
        }

        private int CalculateIncrements(List<int> input)
        {
            int count = 0;
            int prev = input.First();
            foreach (var curr in input.Skip(1))
            {
                if (curr > prev)
                {
                    count++;
                }

                prev = curr;
            }

            return count;
        }
    }
}