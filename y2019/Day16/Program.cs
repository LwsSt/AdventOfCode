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
            PrintPart1Examples();
            // PrintPart2Examples();

            // string text = File.ReadAllText(@"Day16\input.txt");
            // var input = text.ToCharArray().Select(c => c - '0').ToArray();

            // Console.WriteLine("Calculating");
            // var output = Iterate(input);
            // Console.WriteLine("Result: {0}", string.Join("", output.Take(8)));
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
                Console.WriteLine("Calculating...");
                Iterate(input);
                Console.WriteLine("Input {0} Output {1}", test, string.Join("", input.Take(8)));
            }
        }

        static void PrintPart2Examples()
        {
            var testInput = new[]
            {
                "03036732577212944063491565474664",
                "02935109699940807407585447034323",
                "03081770884921959731165446850517",
            };

            foreach (var test in testInput)
            {
                int messageOffset = int.Parse(test.Substring(0, 7));
                
                var input = string.Join("", Enumerable.Repeat(test, 10_000))
                    .Select(c => c - '0').ToArray();

                Console.WriteLine("Calculating...");
                Iterate(input);
                Console.WriteLine(string.Join("", input.Skip(messageOffset).Take(8)));
            }
        }

        static void Iterate(int[] input)
        {
            for (int i = 0; i < 100; i++)
            {
                // Console.WriteLine(string.Join("", input));
                CalculateNextStep(input);
            }
        }

        static void CalculateNextStep(int[] input)
        {
            for (int idx = input.Length - 1; idx >= 0 ; idx--)
            {
                if (idx == input.Length - 1)
                {
                    continue;
                }

                input[idx] = (input[idx] + input[idx + 1]) % 10;
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