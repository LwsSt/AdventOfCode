using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode.y2020.d11
{
    public class Program : IPuzzle
    {
        private readonly Space[,] floorPlan;

        public Program()
        {
            floorPlan = ParseInput();
        }

        public void Part1()
        {
            int occupiedSeats = IterateSeatingPlan(floorPlan, CountOccupiedSeats);
            Console.Write(occupiedSeats);
            
            int CountOccupiedSeats(int tWidth, int tHeight, Space[,] floorPlan)
            {
                int count = 0;
                for (int w = tWidth - 1; w <= tWidth + 1; w++)
                {
                    for (int h = tHeight - 1; h <= tHeight + 1; h++)
                    {
                        if (0 <= w && w < floorPlan.GetLength(0) &&
                            0 <= h && h < floorPlan.GetLength(1))
                        {
                            if (tHeight == h && tWidth == w)
                            {
                                continue;
                            }

                            if (floorPlan[w, h] == Space.Occupied)
                            {
                                count++;
                            }
                        }
                    }
                }
                
                return count;
            }
        }

        public void Part2()
        {
            
        }

        public static int IterateSeatingPlan(Space[,] floorPlan, Func<int, int, Space[,], int> countOccupiedSeats)
        {
            var comparer = new FloorplanComparer();

            do 
            {
                var nextPlan = (Space[,])floorPlan.Clone();

                for (int w = 0; w < floorPlan.GetLength(0); w++)
                {
                    for (int h = 0; h < floorPlan.GetLength(1); h++)
                    {
                        int occupiedCount = countOccupiedSeats(w, h, floorPlan);
                        
                        if (floorPlan[w, h] == Space.Occupied && occupiedCount >= 4)
                        {
                            nextPlan[w, h] = Space.Unoccupied;
                        }
                        
                        if (floorPlan[w, h] == Space.Unoccupied && occupiedCount == 0)
                        {
                            nextPlan[w, h] = Space.Occupied;
                        }
                    }
                }

                if (comparer.Equals(floorPlan, nextPlan))
                {
                    break;
                }

                floorPlan = nextPlan;
            } while (true);

            int occupiedSeats = CountTotalOccupiedSeats(floorPlan);
            return occupiedSeats;
        }

        public static Space[,] ParseInput()
        {
            var lines = File.ReadAllLines(@"y2020\d11\input.puzzle");
            int width = lines[0].Length;
            int height = lines.Length;

            var floorPlan = new Space[height, width];

            for (int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
                {
                    floorPlan[h, w] = lines[h][w] == 'L' ? Space.Unoccupied : Space.Floor;
                }
            }

            return floorPlan;
        }

        public static int CountTotalOccupiedSeats(Space[,] floorPlan)
        {
            int count = 0;
            for (int w = 0; w < floorPlan.GetLength(0); w++)
            {
                for (int h = 0; h < floorPlan.GetLength(1); h++)
                {
                    if (floorPlan[w, h] == Space.Occupied)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        public static void PrintGrid(Space[,] floorPlan)
        {
            for (int w = 0; w < floorPlan.GetLength(0); w++)
            {
                for (int h = 0; h < floorPlan.GetLength(1); h++)
                {
                    var c = floorPlan[w, h] switch
                    {
                        Space.Floor => '.',
                        Space.Occupied => '#',
                        Space.Unoccupied => 'L',
                        _ => throw new Exception()
                    };
                    Console.Write(c);
                }
                Console.WriteLine();
            }
            
            Console.WriteLine();
        }
    }

    public class FloorplanComparer : IEqualityComparer<Space[,]>
    {
        public bool Equals(Space[,] x, Space[,] y)
        {
            if (x == null || y == null)
            {
                return false;
            }

            for (int w = 0; w < x.GetLength(0); w++)
            {
                for (int h = 0; h < x.GetLength(1); h++)
                {
                    if (x[w, h] != y[w, h])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public int GetHashCode([DisallowNull] Space[,] obj)
        {
            var hashCode = new HashCode();
            for (int w = 0; w < obj.GetLength(0); w++)
            {
                for (int h = 0; h < obj.GetLength(1); h++)
                {
                    hashCode.Add((w, h, obj[w, h]));
                }
            }

            return hashCode.ToHashCode();
        }
    }

    public enum Space { Unoccupied, Occupied, Floor }
}