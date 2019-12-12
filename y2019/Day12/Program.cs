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
        {
            PrintPart1Examples();
        }

        static void PrintPart1Examples()
        {
            string input = @"<x=-1, y=0, z=2>
<x=2, y=-10, z=-7>
<x=4, y=-8, z=8>
<x=3, y=5, z=-1>";

            var planets = Parse(input);
            foreach (var planet in planets)
            {
                Console.WriteLine(planet);
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

        static IEnumerable<Planet[]> RunSimulation(Planet[] planets)
        {
            while (true)
            {
                foreach (var (planet1, planet2) in GetPairs(planets))
                {
                    if (planet1.Position.X < planet2.Position.X)
                    {
                        planet1.Position.X += 1;
                        planet2.Position.X -= 1;
                    }
                    else if (planet1.Position.X > planet2.Position.X)
                    {
                        planet1.Position.X -= 1;
                        planet2.Position.X += 1;
                    }

                    if (planet1.Position.Y < planet2.Position.Y)
                    {
                        planet1.Position.Y += 1;
                        planet2.Position.Y -= 1;
                    }
                    else if (planet1.Position.Y > planet2.Position.Y)
                    {
                        planet1.Position.Y -= 1;
                        planet2.Position.Y += 1;
                    }

                    if (planet1.Position.Z < planet2.Position.Z)
                    {
                        planet1.Position.Z += 1;
                        planet2.Position.Z -= 1;
                    }
                    else if (planet1.Position.Z > planet2.Position.Z)
                    {
                        planet1.Position.Z -= 1;
                        planet2.Position.Z += 1;
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

        public override string ToString() => $"Planet(Pos:{Position}, Pos:{Velocity})";

        public Planet Copy() => new Planet(Position.Copy(), Velocity.Copy());
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

        public Vector Copy() => new Vector(X, Y, Z);
    }
}