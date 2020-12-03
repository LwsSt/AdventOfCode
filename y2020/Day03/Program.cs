using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AOC2020.Day03
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var map = ParseInput();
            Part1(map);
        }

        public static void Part1(Space[,] map)
        {
            int trees = TraverseSlope(map, 1, 3);

            Console.WriteLine(trees);
        }

        public static int TraverseSlope(Space[,] map, int heigthDiff, int widthDiff)
        {
            int trees = 0;
            int heigth = map.GetLength(0);
            int width = map.GetLength(1);
            int w = 0;
            for (int h = 0; h < heigth; h += heigthDiff)
            {
                if (map[h,w] == Space.Tree)
                {
                    trees++;
                }

                w += widthDiff;
                w %= width;
            }

            return trees;
        }

        public static Space[,] ParseInput()
        {
            var lines = File.ReadAllLines(@"Day03\input.txt");
            int height = lines.Length;
            int width = lines[0].Length;
            var map = new Space[height, width];

            for (int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
                {
                    map[h, w] = lines[h][w] == '#' ? Space.Tree : Space.Open;
                }
            }

            return map;
        }
    }

    public enum Space
    {
        Tree, Open
    }
}