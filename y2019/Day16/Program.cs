using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AOC2019.Day16
{
    class Program
    {
        public static void Main(string[] args)
        {
            // PrintPart1Examples();
            string text = File.ReadAllText(@"Day16\input.txt");
            var input = text.ToCharArray().Select(c => c - '0').ToArray();

            Console.WriteLine("Calculating");
            var output = Iterate(input);
            Console.WriteLine("Result: {0}", string.Join("", output.Take(8)));
        }

        static void PrintPart1Examples()
        {
            var testInput = new[]
            {
                "80871224585914546619083218645595",
                "19617804207202209144916044189917",
                "69317163492948606335995924319873",
            };

            foreach (var test in testInput)
            {
                var input = test.ToCharArray().Select(c => c - '0').ToArray();
                Console.WriteLine("Input {0} Output {1}", test, string.Join("", Iterate(input).Take(8)));
            }
        }

        static int[] Iterate(int[] input)
        {
            for (int i = 0; i < 100; i++)
            {
                input = CalculateNextStep(input);
            }

            return input;
        }

        static int[] CalculateNextStep(int[] numbers)
        {
            return CalculateNextStepImpl().ToArray();

            IEnumerable<int> CalculateNextStepImpl()
            {
                for (int i = 0; i < numbers.Length; i++)
                {
                    IEnumerable<int> pattern = GetPattern(i);
                    int sum = numbers.Zip(pattern, (a, b) => a * b).Sum();
                    yield return Math.Abs(sum) % 10;
                }
            }
        }

        private static readonly int[] BasePattern = new int[]{ 0, 1, 0, -1 };
        static IEnumerable<int> GetPattern(int element)
        {
            foreach (var el in GetPatternImpl(element).Skip(1))
            {
                yield return el;
            }

            while(true)
            {
                foreach (var el in GetPatternImpl(element))
                {
                    yield return el;
                }
            }

            IEnumerable<int> GetPatternImpl(int element) => BasePattern.SelectMany(i => Enumerable.Repeat(i, element + 1));
        }
    }
}