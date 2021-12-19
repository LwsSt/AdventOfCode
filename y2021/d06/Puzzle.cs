using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.y2021.d06
{
    public class Puzzle : IPuzzle
    {
        private readonly List<long> input = new List<long>();

        public Puzzle()
        {
            input = File.ReadAllText(@"y2021\d06\input.puzzle")
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(long.Parse)
                .ToList();
        }
        
        public void Part1()
        {
            var timers = new List<long>(input);

            for (int day = 0; day < 80; day++)
            {
                var newTimers = new List<long>();

                for (int i = 0; i < timers.Count; i++)
                {
                    if (timers[i] == 0)
                    {
                        timers[i] = 6;
                        newTimers.Add(8);
                    }
                    else
                    {
                        timers[i] -= 1;
                    }
                }

                timers.AddRange(newTimers);
            }

            Console.WriteLine(timers.Count);
        }

        public void Part2()
        {
            
        }
    }
}