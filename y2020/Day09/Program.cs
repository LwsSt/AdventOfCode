using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace AOC2020.Day09
{
    public class Program
    {
        public const string FileName = "input.txt";
        public const int Preamble = 25;

        public static void Main()
        {
            var input = ParseInput();
            Part1(input);
        }

        public static void Part1(List<long> input)
        {
            List<long> invalidNums = new List<long>();
            for (int i = Preamble; i < input.Count; i++)
            {
                long target = input[i];
                var range = input.Skip(i - Preamble).Take(Preamble);
                if (!TestRange(range, target))
                {
                    invalidNums.Add(target);
                }
            }

            foreach (var num in invalidNums)
            {
                Console.WriteLine(num);
            }

            bool TestRange(IEnumerable<long> range, long target)
            {
                var nums = range.ToHashSet();
                foreach (var n in nums)
                {
                    long test = target - n;
                    if (nums.Contains(test))
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public static List<long> ParseInput() => File.ReadLines($"Day09\\{FileName}")
                .Select(l => long.Parse(l))
                .ToList();
    }
}