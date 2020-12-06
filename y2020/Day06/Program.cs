using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace AOC2020.Day06
{
    public class Program
    {
        public static void Main()
        {
            Part1();
        }

        public static void Part1()
        {
            int sum = ParseInput()
                .Sum(r => r.Count);

            Console.WriteLine(sum);
        }

        public static IEnumerable<HashSet<char>> ParseInput()
        {
            var reader = File.OpenText(@"Day06\input.txt");
            var responses = new HashSet<char>();
            while(!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                if (string.IsNullOrWhiteSpace(line))
                {
                    yield return responses;
                    responses = new HashSet<char>();
                }

                foreach (var @char in line)
                {
                    responses.Add(@char);
                }
            }

            if (responses.Count > 0)
            {
                yield return responses;
            }
        }
    }
}