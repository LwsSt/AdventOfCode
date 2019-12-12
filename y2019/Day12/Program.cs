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
            return null;
        }
    }

    class Planet
    {
        public Planet(int x, int y, int z)
        {
            Position = new Vector(x, y, z);
            Velocity = new Vector(0, 0, 0);
        }
        
        public Vector Position { get; }
        public Vector Velocity { get; }

        public override string ToString() => $"Planet()";
    }

    class Vector
    {
        public Vector(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public int X { get; }
        public int Y { get; }
        public int Z { get; }

        public override string ToString() => $"({X},{Y},{Z})";
    }
}