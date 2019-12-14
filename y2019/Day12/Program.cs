using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC2019.Day12
{
    class Program
    {
        public static void Main(string[] args)
        // public static void Main(string[] args)
        {
            string input = File.ReadAllText(@"Day12\input.txt");
            var planets = Parse(input).ToArray();
            
            // Console.WriteLine("Running simulation...");
            // var step = RunSimulation(planets).Skip(999).First();
            // Console.WriteLine("Total energy {0}", CalculateTotalEnergy(step));

            long repetition = FindRepetition(() => RunSimulation(Parse(input).ToArray()));
            Console.WriteLine("Number of repetitions {0}", repetition * 2);

            // PrintPart1Examples();
            // PrintPart2Examples();
        }

        static void PrintPart1Examples()
        {
//             string input1 = @"<x=-1, y=0, z=2>
// <x=2, y=-10, z=-7>
// <x=4, y=-8, z=8>
// <x=3, y=5, z=-1>";

            string input2 = @"<x=-8, y=-10, z=0>
<x=5, y=5, z=10>
<x=2, y=-7, z=3>
<x=9, y=-8, z=-3>";

            var afterStep99 = RunSimulation(Parse(input2).ToArray()).Skip(99).Take(4);
            foreach (var step in afterStep99)
            {
                Console.WriteLine("Total Energy {0, -5}", CalculateTotalEnergy(step));
            }
        }

        static void PrintPart2Examples()
        {
            var testInputs = new[]
            {
                @"<x=-1, y=0, z=2>
<x=2, y=-10, z=-7>
<x=4, y=-8, z=8>
<x=3, y=5, z=-1>",
                @"<x=-8, y=-10, z=0>
<x=5, y=5, z=10>
<x=2, y=-7, z=3>
<x=9, y=-8, z=-3>",
            };

            foreach (var input in testInputs)
            {
                long repetition = FindRepetition(() => RunSimulation(Parse(input).ToArray()));
                Console.WriteLine("Number of repetitions {0}", repetition * 2);
            }
        }

        static long FindRepetition(Func<IEnumerable<Planet[]>> planets)
        {
            var selectors = new Func<Planet, int>[]
            {
                p => p.Velocity.X,
                p => p.Velocity.Y,
                p => p.Velocity.Z,
            };

            var repetitions = selectors.Select(FindRepetition).ToArray();

            return LCM(repetitions);

            long FindRepetition(Func<Planet, int> selector)
            {
                var states = planets();

                long index = 1;
                foreach (var state in states)
                {
                    if (state.All(m => selector(m) == 0))
                    {
                        return index;
                    }
                    index++;
                }

                return default;
            }
        }

        static long LCM(params long[] numbers)
        {
            return numbers.Aggregate(LCM);
            
            long LCM(long a, long b)
            {
                return Math.Abs(a * b) / GCD(a, b);
            }
            
            long GCD(long a, long b)
            {
                while (a != b)
                {
                    if (a > b)
                        a -= b;
                    else
                        b -= a;
                }
                return a;
            }
        }

        static IEnumerable<Planet> Parse(string input)
        {
            var matches = Regex.Matches(input, @"<x=(?<X>-?\d+), y=(?<Y>-?\d+), z=(?<Z>-?\d+)");
            foreach (Match match in matches)
            {
                string x = match.Groups["X"].Value;
                string y = match.Groups["Y"].Value;
                string z = match.Groups["Z"].Value;

                yield return new Planet(int.Parse(x), int.Parse(y), int.Parse(z));
            }
        }

        static int CalculateTotalEnergy(IEnumerable<Planet> planets)
        {
            return planets.Select(CalculateForPlanet).Sum();

            int CalculateForPlanet(Planet planet)
            {
                int potEnergy = Math.Abs(planet.Position.X) + Math.Abs(planet.Position.Y) + Math.Abs(planet.Position.Z);
                int kinEnergy = Math.Abs(planet.Velocity.X) + Math.Abs(planet.Velocity.Y) + Math.Abs(planet.Velocity.Z);

                return potEnergy * kinEnergy;
            }
        }

        static IEnumerable<Planet[]> RunSimulation(Planet[] planets)
        {
            while (true)
            {
                foreach (var (planet1, planet2) in GetPairs(planets))
                {
                    if (planet1.Position.X < planet2.Position.X)
                    {
                        planet1.Velocity.X += 1;
                        planet2.Velocity.X -= 1;
                    }
                    else if (planet1.Position.X > planet2.Position.X)
                    {
                        planet1.Velocity.X -= 1;
                        planet2.Velocity.X += 1;
                    }

                    if (planet1.Position.Y < planet2.Position.Y)
                    {
                        planet1.Velocity.Y += 1;
                        planet2.Velocity.Y -= 1;
                    }
                    else if (planet1.Position.Y > planet2.Position.Y)
                    {
                        planet1.Velocity.Y -= 1;
                        planet2.Velocity.Y += 1;
                    }

                    if (planet1.Position.Z < planet2.Position.Z)
                    {
                        planet1.Velocity.Z += 1;
                        planet2.Velocity.Z -= 1;
                    }
                    else if (planet1.Position.Z > planet2.Position.Z)
                    {
                        planet1.Velocity.Z -= 1;
                        planet2.Velocity.Z += 1;
                    }
                }

                foreach (var planet in planets)
                {
                    planet.AddVelocity();
                }

                yield return planets;
            }

            IEnumerable<(Planet, Planet)> GetPairs(Planet[] ps)
            {
                for (int i = 0; i < ps.Length; i++)
                {
                    for (int j = i + 1; j < ps.Length; j++)
                    {
                        yield return (ps[i], ps[j]);
                    }
                }
            }
        }
    }

    class Planet
    {
        public Planet(int x, int y, int z)
        {
            Position = new Vector(x, y, z);
            Velocity = new Vector(0, 0, 0);
        }

        public Planet(Vector position, Vector velocity)
        {
            Position = position;
            Velocity = velocity;
        }

        public void AddVelocity()
        {
            Position.X += Velocity.X;
            Position.Y += Velocity.Y;
            Position.Z += Velocity.Z;
        }
        
        public Vector Position { get; }
        public Vector Velocity { get; }

        public override string ToString() => $"Planet(Pos:{Position}, Vel:{Velocity})";
    }

    class Vector
    {
        public Vector(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public override string ToString() => $"({X},{Y},{Z})";
    }
}