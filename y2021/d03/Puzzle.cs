using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.y2021.d03
{
    public class Puzzle : IPuzzle
    {
        private readonly List<int> input;

        public Puzzle()
        {
            input = File.ReadLines(@"y2021/d03/input.puzzle")
                .Select(l => Convert.ToInt32(l, 2))
                .ToList();
        }

        public void Part1()
        {
            int[] onesCount = new int[12];

            foreach (var l in input)
            {
                for (int i = 0; i < onesCount.Length ; i++)
                {
                    int shiftMagnitude = onesCount.Length - i -1;
                    int shifted = (l >> (1 * shiftMagnitude));
                    onesCount[i] += (shifted & 1) == 1 ? 1 : 0;
                }
            }

            int totalLines = input.Count;
            int gamma = 0;
            int epsilon = 0;
            for (int i = 0; i < onesCount.Length ; i++)
            {
                int shiftMagnitude = onesCount.Length - i - 1;
                int gammaBit = onesCount[i] > totalLines/2 ? 1 : 0;
                int epsilonBit = onesCount[i] > totalLines/2 ? 0 : 1;

                gamma |= (gammaBit << shiftMagnitude);
                epsilon |= (epsilonBit << shiftMagnitude);
            }

            Console.WriteLine(gamma);
            Console.WriteLine(epsilon);

            Console.WriteLine(gamma * epsilon);
        }

        public void Part2()
        {
            throw new NotImplementedException();
        }
    }
}