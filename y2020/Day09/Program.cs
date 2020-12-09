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
            long invalidNum = Part1(input);
            Part2(input, invalidNum);
        }

        public static long Part1(List<long> input)
        {
            long invalidNum = 0;
            for (int i = Preamble; i < input.Count; i++)
            {
                long target = input[i];
                var range = input.Skip(i - Preamble).Take(Preamble);
                if (!TestRange(range, target))
                {
                    invalidNum = target;
                }
            }

            Console.WriteLine(invalidNum);
            return invalidNum;

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

        public static void Part2(List<long> input, long targetNum)
        {
            int upper = input.IndexOf(targetNum);
            upper--;

            while(true)
            {
                long sum = 0;
                // Console.WriteLine(input[first]);
                int lower = upper;
                for (; sum < targetNum; lower--)
                {
                    Console.WriteLine("ADD {0, 13}", input[lower]);
                    sum += input[lower];
                    Console.WriteLine("SUM {0, 13}", sum);
                }

                if (sum == targetNum)
                {
                    var range = input.Skip(lower).Take(upper - lower).ToList();
                    long min = range.Min();
                    long max = range.Max();
                    Console.WriteLine(min + max);
                    return;
                }
                Console.WriteLine("Next Range");
                upper--;
            }
        }

        public static List<long> ParseInput() => File.ReadLines($"Day09\\{FileName}")
                .Select(l => long.Parse(l))
                .ToList();
    }
}