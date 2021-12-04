using System;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            Debug.Assert(args.Length == 3, "Expected 3 arguments");

            var puzzleCoords = args.Select(a => int.Parse(a)).ToList();

            var puzzles = Assembly.GetExecutingAssembly()
                .DefinedTypes
                .Where(t => t.ImplementedInterfaces.Contains(typeof(IPuzzle)))
                .ToDictionary<TypeInfo, string, Func<IPuzzle>>(
                    t => t.Namespace.Replace("AdventOfCode.", string.Empty),
                    t => () => (IPuzzle)Activator.CreateInstance(t));
                
            int year = puzzleCoords[0];
            int day = puzzleCoords[1];
            int part = puzzleCoords[2];

            if (!puzzles.TryGetValue($"y{year}.d{day:00}", out var puzzleFunc))
            {
                Console.WriteLine($"No puzzle for y{year}.d{day:00}");
                return;
            }

            var puzzle = puzzleFunc();

            switch (part)
            {
                case 1: 
                    puzzle.Part1();
                    break;
                case 2:
                    puzzle.Part2();
                    break;
                default:
                    Console.WriteLine("No puzzle");
                    break;
            }

            Console.WriteLine("--- END ---");
        }
    }
}
