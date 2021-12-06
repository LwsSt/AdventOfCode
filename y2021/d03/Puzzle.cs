using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Collections;

namespace AdventOfCode.y2021.d03
{
    public class Puzzle : IPuzzle
    {
        private const int Length = 12;
        private readonly List<BitArray> input;

        public Puzzle()
        {
            input = File.ReadLines(@"y2021/d03/input.puzzle")
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
            var specimens = input;

            for (int i = Length - 1; i >= 0 && specimens.Count > 1; i--)
            {
                specimens = Filter(specimens, i, true);
            }

            int oxygenRate = ToInt(specimens.First());

            specimens = input;
            for (int i = Length - 1; i >= 0 && specimens.Count > 1; i--)
            {
                specimens = Filter(specimens, i, false);
            }

            int co2Rate = ToInt(specimens.First());

            Console.WriteLine(oxygenRate);
            Console.WriteLine(co2Rate);

            Console.WriteLine(oxygenRate * co2Rate);
        }

        private List<BitArray> Filter(List<BitArray> list, int position, bool getMostCommon)
        {
            int discriminant = (int)Math.Round((double)list.Count / 2, MidpointRounding.AwayFromZero);
            int onesCount = list.Where(b => b[position]).Count();

            bool mostCommon = false;

            if (getMostCommon)
            {
                if (onesCount == discriminant)
                    mostCommon = true;
                else
                    mostCommon = onesCount >= discriminant;
            }
            else
            {
                if (onesCount == discriminant)
                    mostCommon = false;
                else
                    mostCommon = !(onesCount >= discriminant);
            }

            return list.Where(l => l[position] == mostCommon).ToList();
        }

        private static void Print(BitArray bit)
        {
            for (int i = bit.Length - 1; i >= 0 ; i--)
            {
                Console.Write("{0:1;0;0}", bit[i].GetHashCode());
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
            BitArray bitArray = new BitArray(new[] { input });
            bitArray.Length = Length;
            return bitArray;
        }
    }
}