using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Collections;

namespace AdventOfCode.y2021.d03
{
    public class Puzzle : IPuzzle
    {
        private const int Length = 5;
        private readonly List<BitArray> input;

        public Puzzle()
        {
            input = File.ReadLines(@"y2021/d03/test-input.puzzle")
                .Select(l => Convert.ToInt32(l, 2))
                .Select(Create)
                .ToList();
        }

        public void Part1()
        {
            int[] onesCount = new int[Length];

            foreach (var bitArray in input)
            {
                for (int i = 0; i < bitArray.Length ; i++)
                {
                    onesCount[i] += bitArray[i] ? 1 : 0;
                }
            }

            int totalLines = input.Count;
            var gammaArray = new BitArray(Length);
            var epsilonArray = new BitArray(Length);
            for (int i = 0; i < onesCount.Length ; i++)
            {
                gammaArray[i] = (onesCount[i] > totalLines / 2);
                epsilonArray[i] = !(onesCount[i] > totalLines / 2);
            }

            Print(gammaArray);
            Print(epsilonArray);

            int gamma = ToInt(gammaArray);
            int epsilon = ToInt(epsilonArray);

            Console.WriteLine(gamma);
            Console.WriteLine(epsilon);

            Console.WriteLine(gamma * epsilon);
        }

        public void Part2()
        {
        }

        private static void Print(BitArray bit)
        {
            foreach (var b in bit)
            {
                Console.Write("{0:1;0;0}", b.GetHashCode());
            }

            Console.WriteLine();
        }

        private static int ToInt(BitArray arr)
        {
            int[] output = new int[1];
            arr.CopyTo(output, 0);

            return output[0];
        }

        private static BitArray Create(int input)
        {
            var array = new BitArray(Length);

            for (int i = 0; i < Length; i++)
            {
                array[i] = ((input >> i) & 1) == 1;
            }

            return array;
        }
    }
}