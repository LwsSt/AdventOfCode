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
            long total = 0;
            foreach (var line in input)
            {
                string[] parts = line.Split('|', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                var codes = parts[0];
                var display = parts[1];

                var lookup = Decode(codes);
                int output = DecodeOutput(lookup, display);

                total += output;
            }

            Console.WriteLine(total);
        }

        private static int DecodeOutput(Dictionary<string, int> lookup, string display)
        {
            var digits = display.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(s => new string(s.OrderBy(c => c).ToArray()))
                .ToList();

            const int power = 3;
            int output = 0;

            for (int i = 0; i < digits.Count; i++)
            {
                int digit = lookup[digits[i]];
                output += (int)Math.Pow(10, power - i) * digit;
            }

            return output;
        }

        private static Dictionary<string, int> Decode(string line)
        {
            var codes = line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .OrderBy(s => s.Length)
                .Select(s => new string(s.OrderBy(c => c).ToArray()))
                .ToList();

            var codeLookup = new Dictionary<string, int>();
            string code_1 = codes[0];
            string code_7 = codes[1];
            string code_4 = codes[2];
            string code_8 = codes.Last();
            
            codeLookup[code_1] = 1;
            codeLookup[code_7] = 7;
            codeLookup[code_4] = 4;
            codeLookup[code_8] = 8;

            foreach (var code in codes.Skip(3).Take(6))
            {
                if (code.Length == 5)
                {
                    if (code.ContainsUnordered(code_1))
                    {
                        codeLookup[code] = 3;
                    }
                    else if (code.Difference(code_4).Length == 2)
                    {
                        codeLookup[code] = 5;
                    }
                    else
                    {
                        codeLookup[code] = 2;
                    }
                }
                else if (code.Length == 6)
                {
                    if (code.Difference(code_1).Length == 5)
                    {
                        codeLookup[code] = 6;
                    }
                    else if (code.Difference(code_4).Length == 2)
                    {
                        codeLookup[code] = 9;
                    }
                    else
                    {
                        codeLookup[code] = 0;
                    }
                }
                else
                {
                    throw new Exception($"Unrecognised code length {code}");
                }
            }
            
            return codeLookup;
        }
    }

    public static class StringExtensions
    {
        public static bool ContainsUnordered(this string @this, string other)
        {
            bool contains = true;
            foreach (var @char in other)
            {
                contains &= @this.Contains(@char);
            }

            return contains;
        }

        public static string Difference(this string @this, string other)
        {
            foreach (var @char in other)
            {
                @this = @this.Replace(@char.ToString(), "");
            }

            return @this;
        }
    }
}