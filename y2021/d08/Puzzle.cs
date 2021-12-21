using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.y2021.d08
{
    public class Puzzle : IPuzzle
    {
        private readonly string[] input;

        public Puzzle()
        {
            input = File.ReadAllLines(@"y2021\d08\input.puzzle");
        }

        public void Part1()
        {
            var uniqueNumbers = new HashSet<int>() { 2, 4, 3, 7 };

            int count = input
                .Select(s => s.Split('|', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[1])
                .SelectMany(s => s.Split(' '))
                .Select(s => s.Length)
                .Where(c => uniqueNumbers.Contains(c))
                .Count();

            Console.WriteLine(count);
        }

        public void Part2()
        {
            throw new NotImplementedException();
        }
    }
}