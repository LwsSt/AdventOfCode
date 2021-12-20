using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.y2021.d06
{
    public class Puzzle : IPuzzle
    {
        private readonly List<int> input = new List<int>();

        public Puzzle()
        {
            input = File.ReadAllText(@"y2021\d06\input.puzzle")
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(int.Parse)
                .ToList();
        }
        
        public void Part1()
        {
            var timers = new List<int>(input);

            for (int day = 0; day < 80; day++)
            {
                var newTimers = new List<int>();

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
            var fishes = input.GroupBy(i => i)
                .Select(grp => new Fish(grp.Key, grp.Count()))
                .ToList();

            for (int i = 0; i < 256; i++)
            {
                long newFishCount = 0;

                foreach (var fish in fishes)
                {
                    bool birthedFish = fish.IncrementDay(out var count);
                    if (birthedFish)
                    {
                        newFishCount += count;
                    }
                }

                if (newFishCount > 0)
                {
                    fishes.Add(new Fish(8, newFishCount));
                }
            }

            long finalCount = 0;
            foreach (var c in fishes.Select(f => f.Count))
            {
                finalCount += c;
            }

            Console.WriteLine(finalCount);
        }
    }

    public class Fish
    {
        private int day;

        public Fish(int startDay, long count)
        {
            this.day = startDay;
            Count = count;
        }

        public long Count { get; }

        public bool IncrementDay(out long count)
        {
            if (day == 0)
            {
                day = 6;
                count = Count;
                return true;
            }
            else
            {
                day--;
                count = 0;
                return false;
            }
        }
    }
}