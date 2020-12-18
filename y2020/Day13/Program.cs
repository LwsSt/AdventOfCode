using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace AOC2020.Day13
{
    public class Program
    {
        // private const int DepartureTime = 939;
        // private static readonly int[] BusIDs = new[] { 7, 13, 59, 31, 19 };        
        private const int DepartureTime = 1001171;
        private static readonly int[] BusIDs = new[] {17,41,37,367,19,23,29,613,13 };
        
        private static readonly int[] BusIDs_2 = new[] {7,-1,-1,-1,-1,-1,-1,41,-1,-1,-1,37,-1,-1,-1,-1,-1,367,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,19,-1,-1,-1,23,-1,-1,-1,-1,-1,29,-1,613,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,13 };

        private static readonly int[][] Tests = new int[][]
        {
            new int[]{7,13,0,0,59,0,31,19},
            new int[]{17,0,13,19},
            new int[]{67,7,59,61},
            new int[]{67,0,7,59,61},
            new int[]{67,7,0,59,61},
            new int[]{1789,37,47,1889}
        };

        private static readonly long[] Test_Answers = new long[]
        {
            1068781,
            3417,
            754018,
            779210,
            1261476,
            1202161486
        };

        public static void Main()
        {
            Part1();
            for (int i = 0; i < Tests.Length; i++)
            {
                long actual = IterateBusRoutes(Tests[i]);
                long expected = Test_Answers[i];
                Console.WriteLine("Actual: {0}, Expected: {1}", actual, expected);
            }
        }

        public static void Part1()
        {
            var dictionary = BusIDs.ToDictionary(b => b, _ => 0);
            foreach (var id in dictionary.Keys.ToList())
            {
                int time = 0;
                while (time < DepartureTime)
                {
                    time += id;
                }

                dictionary[id] = time - DepartureTime;
            }

            int shortestWaitTime = dictionary.Values.Min();
            int busId = dictionary.Where(kvp => kvp.Value == shortestWaitTime)
                .First().Key;

            Console.WriteLine(busId * shortestWaitTime);
        }

        public static void Part2()
        {
            long timestamp = IterateBusRoutes(BusIDs_2);
        }

        public static long IterateBusRoutes(int[] busIds)
        {
            for (long timestamp = 0; ; timestamp++)
            {
            }
        }
    }
}