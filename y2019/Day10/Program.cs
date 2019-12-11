using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AOC2019.Day10
{
    class Program
    {
        public static void Main(string[] args)
        {
            // var lines = File.ReadAllLines(@"Day10/input.txt");
            // var asteriods = Parse(lines).ToHashSet();
            
            // var asteroid = Process(asteriods);
            // Console.WriteLine($"");

            PrintPart1Examples();
        }

        static IEnumerable<Asteroid> Parse(string[] lines)
        {
            for (int y = 0; y < lines.Length; y++)
            {
                char[] columns = lines[y].ToCharArray();
                for (int x = 0; x < columns.Length; x++)
                {
                    if (columns[x] == '#')
                    {
                        yield return new Asteroid(x, y);
                    }
                }
            }
        }

        static void PrintPart1Examples()
        {
            foreach (var test in Test.Tests)
            {
                var asteriods = Parse(test);
                Console.WriteLine($"Max asteroids: {0}", Process(asteriods));
            }
        }

        static int Process(IEnumerable<Asteroid> asteroids)
        {
            var asteroidCount = new Dictionary<Asteroid, int>();
            foreach (var origin in asteroids)
            {
                var uniqueAngles = asteroids
                    .Where(a => a != origin)
                    .Select(a => new Asteroid(a.X - origin.X, a.Y - origin.Y))
                    .GroupBy(a => Math.Atan2(a.Y, a.X))
                    .Count();

                asteroidCount.Add(origin, uniqueAngles);

                // foreach (var (_, grouped) in relativeAsteroids)
                // {
                //     grouped.OrderBy()
                // }

                // double Distance(Asteroid asteroid) => Math.Sqrt(Math.Pow(asteroid.X, 2) + Math.Pow(asteroid.Y, 2));
            }

            var angles = asteroidCount.Select(kvp => kvp.Value).ToHashSet();
        }
    }

    struct Asteroid
    {
        public int X { get; }
        public int Y { get; }

        public Asteroid(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString() => $"Ast({X},{Y})";

        public override int GetHashCode() => HashCode.Combine(X, Y);

        public override bool Equals(object obj) =>
            obj is Asteroid that &&
            that.X == this.X &&
            that.Y == this.Y;

        public static bool operator ==(Asteroid a, Asteroid b) => a.Equals(b);

        public static bool operator !=(Asteroid a, Asteroid b) => !(a.Equals(b));
    }

    static class Extensions
    {
        public static void AddOrUpdate(this Dictionary<double, List<Asteroid>> dict, double angle, Asteroid asteroid)
        {
            if (dict.TryGetValue(angle, out List<Asteroid> asteroids))
            {
                asteroids.Add(asteroid);
            }
            else
            {
                dict.Add(angle, new List<Asteroid>() { asteroid });
            }
        }
    }

    static class Test
    {
        public static IEnumerable<string[]> Tests = new[]
        {
            new[]
            {
                "......#.#.",
                "#..#.#....",
                "..#######.",
                ".#.#.###..",
                ".#..#.....",
                "..#....#.#",
                "#..#....#.",
                ".##.#..###",
                "##...#..#.",
                ".#....####",
            },
            new[]
            {
                "#.#...#.#.",
                ".###....#.",
                ".#....#...",
                "##.#.#.#.#",
                "....#.#.#.",
                ".##..###.#",
                "..#...##..",
                "..##....##",
                "......#...",
                ".####.###.",
            },
            new[]
            {
                ".#..#..###",
                "####.###.#",
                "....###.#.",
                "..###.##.#",
                "##.##.#.#.",
                "....###..#",
                "..#.#..#.#",
                "#..#.#.###",
                ".##...##.#",
                ".....#.#..",
            },
            new[]
            {
                ".#..##.###...#######",
                "##.############..##.",
                ".#.######.########.#",
                ".###.#######.####.#.",
                "#####.##.#.##.###.##",
                "..#####..#.#########",
                "####################",
                "#.####....###.#.#.##",
                "##.#################",
                "#####.##.###..####..",
                "..######..##.#######",
                "####.##.####...##..#",
                ".#####..#.######.###",
                "##...#.##########...",
                "#.##########.#######",
                ".####.#.###.###.#.##",
                "....##.##.###..#####",
                ".#.#.###########.###",
                "#.#.#.#####.####.###",
                "###.##.####.##.#..##",
            }
        };
    }
}
