using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace AdventOfCode.y2020.d06
{
    public class Program : IPuzzle
    {
        public void Part1()
        {
            int sum = ParseInput()
                .Select(s => s.SelectMany(c => c).ToHashSet())
                .Sum(r => r.Count);

            Console.WriteLine(sum);
        }

        public void Part2()
        {
            int sum = ParseInput()
                .Select(ls => ls.Aggregate((l1, l2) => l1.Intersect(l2).ToArray()))
                .Select(r => r.Count())
                .Sum();

            Console.WriteLine(sum);
        }

        public static IEnumerable<List<char[]>> ParseInput()
        {
            var reader = File.OpenText(@"Day06\input.txt");
            var responses = new List<char[]>();
            while(!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                if (string.IsNullOrWhiteSpace(line))
                {
                    yield return responses;
                    responses = new List<char[]>();
                    continue;
                }

                responses.Add(line.ToCharArray());
            }

            if (responses.Count > 0)
            {
                yield return responses;
            }
        }
    }
}