using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AOC2019.Day08
{
    class Program
    {
        //public static void Main(string[] args)
        public void Main(string[] args)
        {
            int[] input = File.ReadAllText(@"Day08\input.txt")
                .Where(c => Char.IsNumber(c))
                .Select(c => c - '0')
                .ToArray();
            
            // int output = Solve(input, 6, 25);
            // Console.WriteLine("Output: {0}", output);

            //Part2Example();

            var output = CombineLayers(input, 6, 25);
            Print(output);
        }

        static void Part2Example()
        {
            int[] input = "0222112222120000".Select(c => c - '0').ToArray();
            int[,] output = CombineLayers(input, 2, 2);

            Print(output);
        }

        static int[,] CombineLayers(int[] input, int height, int width)
        {
            int[,] output = new int[height, width];
            var layers = ProcessInput(input, height, width).ToArray();

            for (int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
                {
                    int[,] pixel = layers.Where(l => l[h, w] != 2).First();
                    output[h, w] = pixel[h, w];
                }
            }

            return output;
        }

        static void Print(int[,] output)
        {
            for (int h = 0; h < output.GetLength(0); h++)
            {
                for (int w = 0; w < output.GetLength(1); w++)
                {
                    Console.Write(output[h, w].ToString());
                }
                Console.WriteLine();
            }
        }

        // static int Solve(int[] input, int height, int width)
        // {
        //     var layers = ProcessInput(input, height, width).ToArray();

        //     int fewestZeros = FindLayer(layers);

        //     int ones = layers[fewestZeros][1];
        //     int twos = layers[fewestZeros][2];

        //     return ones * twos;
        // }

        static int FindLayer(int[][] layers)
        {

            return layers
                .Select((arr, idx) => (zeros: arr[0], layer: idx))
                .OrderBy(kvp => kvp.zeros)
                .First().layer;
        }

        static IEnumerable<int[,]> ProcessInput(int[] input, int height, int width)
        {
            int length = (height * width);
            int idx = 0;

            while (idx < input.Length)
            {
                int end = idx + length;
                var layer = new int[height, width];
                for (int i = 0; i < height && idx < end; i++)
                {
                    for (int j = 0; j < width && idx < end; j++)
                    {
                        layer[i, j] = input[idx];
                        idx++;
                    }
                }

                yield return layer;
            }
        }
    }
}