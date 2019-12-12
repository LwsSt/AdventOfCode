using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AOC2019.Day10
{
    class Program
    {
        // public static void Main(string[] args)
        public void Main(string[] args)
        {
            var lines = File.ReadAllLines(@"Day10/input.txt");
            var asteroids = Parse(lines).ToHashSet();
            
            var origin = FindBestAsteroid(asteroids).Key;
            
            Console.WriteLine("Best asteroid: {0}", origin);
            var targetAsteroid = SpinLaser(asteroids.Where(a => a != origin), origin);
            
            Console.WriteLine("200th Asteroid: {0}", targetAsteroid);

            //PrintPart1Examples();
            // PrintPart2Examples();
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
                Console.WriteLine($"Max asteroids: {0}", FindBestAsteroid(asteriods).Value);
            }
        }

        static void PrintPart2Examples()
        {
            var test = Test.Tests.Last();
            var asteroids = Parse(test).ToList();
            var origin = FindBestAsteroid(asteroids).Key;

            Console.WriteLine("Best asteroid: {0}", origin);
            var targetAsteroid = SpinLaser(asteroids.Where(a => a != origin), origin);
            
            Console.WriteLine("200th Asteroid: {0}", targetAsteroid);
        }

        static Asteroid SpinLaser(IEnumerable<Asteroid> input, Asteroid origin)
        {
            var relativeAsteroids = input
                .Select(a => new Asteroid(a.X - origin.X, a.Y - origin.Y))
                .GroupBy(a => Normalize(a));

            var sortedAsteroids = new SortedDictionary<double, Queue<Asteroid>>();
            foreach (var asteroid in relativeAsteroids)
            {
                var asteroidsByDistance = asteroid.OrderByDescending(Distance).ToList();
                sortedAsteroids.Add(asteroid.Key, new Queue<Asteroid>(asteroidsByDistance));
            }

            int asteroidCount = 1;

            while (sortedAsteroids.Any(kvp => kvp.Value.Count > 0))
            {
                foreach (var (_, asteroids) in sortedAsteroids)
                {
                    if (asteroids.TryDequeue(out Asteroid asteroid))
                    {
                        // Console.WriteLine("Asteroid {0,-3}: {1}", asteroidCount, new Asteroid(origin.X + asteroid.X, origin.Y + asteroid.Y));
                        asteroidCount++;
                        if (asteroidCount == 201)
                        {
                            return new Asteroid(origin.X + asteroid.X, origin.Y + asteroid.Y);
                        }
                    }
                }
            }
                

            return default;
            
            double Distance(Asteroid asteroid) => asteroid.X + asteroid.Y;
            // double Distance(Asteroid asteroid) => Math.Sqrt(Math.Pow(asteroid.X, 2) + Math.Pow(asteroid.Y, 2));
            double Normalize(Asteroid asteroid) 
            {
                const double piOver2 = (Math.PI / 2);
                const double twoPi = (Math.PI * 2);

                double theta = piOver2 + Math.Atan2(asteroid.Y, asteroid.X);
                return (theta < 0) ? (theta + twoPi) : theta;
            }
        }

        static KeyValuePair<Asteroid, int> FindBestAsteroid(IEnumerable<Asteroid> asteroids)
        {
            var asteroidCount = new Dictionary<Asteroid, int>();
            foreach (var origin in asteroids)
            {
                var uniqueAngles = asteroids
                    .Where(a => a != origin)
                    .Select(a => new Asteroid(a.X - origin.X, a.Y - origin.Y))
                    .GroupBy(a => Math.Atan2(a.Y, a.X))
                    .Count();

                asteroidCount[origin] = uniqueAngles;

                // foreach (var (_, grouped) in relativeAsteroids)
                // {
                //     grouped.OrderBy()
                // }

                // double Distance(Asteroid asteroid) => Math.Sqrt(Math.Pow(asteroid.X, 2) + Math.Pow(asteroid.Y, 2));
            }

            return asteroidCount.OrderByDescending(kvp => kvp.Value).First();
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
