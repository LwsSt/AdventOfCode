using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.y2021.d07
{
    public class Puzzle : IPuzzle
    {
        private readonly List<int> input;

        public Puzzle()
        {
            input = File.ReadAllText(@"y2021\d07\test-input.puzzle")
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(int.Parse)
                .ToList();
        }

        public void Part1()
        {
            int range = input.Max();

            int fuel = Enumerable.Range(0, range)
                .Select(pos => CalculateCost(pos))
                .Min();

            Console.WriteLine(fuel);

            int CalculateCost(int position)
            {
                return input
                    .Select(pos => Math.Abs(pos - position))
                    .Sum();
            }
        }

        public void Part2()
        {
            throw new NotImplementedException();
        }
    }
}