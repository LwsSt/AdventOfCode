using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AOC2019.Day08
{
    class Program
    {
        public static void Main(string[] args)
        {
            int[] input = File.ReadAllText(@"Day08\input.txt")
                .Where(c => Char.IsNumber(c))
                .Select(c => c - '0')
                .ToArray();
            
            int output = Solve(input, 6, 25);
            Console.WriteLine("Output: {0}", output);
        }

        static int Solve(int[] input, int height, int width)
        {
            var layers = ProcessInput(input, height, width).ToArray();

            int fewestZeros = FindLayer(layers);

            int ones = layers[fewestZeros][1];
            int twos = layers[fewestZeros][2];

            return ones * twos;
        }

        static int FindLayer(int[][] layers)
        {

            return layers
                .Select((arr, idx) => (zeros: arr[0], layer: idx))
                .OrderBy(kvp => kvp.zeros)
                .First().layer;
        }

        static IEnumerable<int[]> ProcessInput(int[] input, int height, int width)
        {
            int length = (height * width);
            int idx = 0;

            while (idx < input.Length)
            {
                int[] dict = new int[3];
                int end = idx + length;
                for (; idx < end && idx < input.Length; idx++)
                {
                    int pointer = input[idx];
                    dict[pointer]++;
                }

                yield return dict;
            }
        }
    }
}