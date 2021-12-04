using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using AdventOfCode;

namespace AdventOfCode.y2020.d01
{
    public class Program : IPuzzle
    {
        private readonly HashSet<int> costs;

        public Program()
        {
            costs = File.ReadAllLines(@"y2020\d01\input.puzzle")
                .Select(l => int.Parse(l))
                .ToHashSet();
        }

        public void Part1()
        {
            foreach (int number_1 in costs)
            {
                int number_2 = 2020 - number_1;
                if (costs.Contains(number_2))
                {
                    Console.WriteLine(number_1 * number_2);
                    break;
                }
            }
        }

        public void Part2()
        {
            int max = costs.Max();
            int min = costs.Min();
            
            foreach(int number_1 in costs)
            {
                int test_number = 2020 - number_1;

                if (test_number < min || max < test_number)
                    continue;
                
                foreach (int number_2 in costs.Where(c => c < test_number && c != number_1))
                {
                    int number_3 = test_number - number_2;

                    if (costs.Contains(number_3) && number_1 + number_3 + number_2 == 2020)
                    {
                        Console.WriteLine(number_1 * number_2 * number_3);
                        return;
                    }
                }
            }
        }
    }
}