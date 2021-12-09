using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode.y2021.d05
{
    public class Puzzle : IPuzzle
    {
        private static readonly Regex lineRegex = new Regex(@"(?<x1>\d+),(?<y1>\d+) -> (?<x2>\d+),(?<y2>\d+)");
        private readonly List<Line> lines;
        
        public Puzzle()
        {
            lines = File.ReadLines(@"y2021\d05\input.puzzle")
                .Select(ParseLine)
                .ToList();
                
            Line ParseLine(string line)
            {
                var match = lineRegex.Match(line);
                int x1 = int.Parse(match.Groups["x1"].Value);
                int y1 = int.Parse(match.Groups["y1"].Value);
                int x2 = int.Parse(match.Groups["x2"].Value);
                int y2 = int.Parse(match.Groups["y2"].Value);

                return new Line(
                    new Point(x1, y1),
                    new Point(x2, y2)
                );
            }
        }

        public void Part1()
        {
            var straightLines = lines
                .Where(l => l.Start.X == l.End.X || l.Start.Y == l.End.Y)
                .ToList();

            var intersecting =
                from l1 in straightLines
                from l2 in straightLines
                where l1 != l2
                where Intersect(l1, l2)
                select (l1, l2);

            int count = intersecting
                .SelectMany(ls => ls.l1.Points.Intersect(ls.l2.Points))
                .Distinct()
                .Count();

            Console.WriteLine(count);

            bool Intersect(Line l1, Line l2)
            {
                int x = l1.Start.X;
                int y = l1.Start.Y;

                return (Math.Min(l2.Start.X, l2.End.X) <= x && x <= Math.Max(l2.Start.X, l2.End.X)) ||
                    (Math.Min(l2.Start.Y, l2.End.Y) <= y && y <= Math.Max(l2.Start.Y, l2.End.Y));
                // return (l2.Start.X <= x && x <= l2.End.X) ||
                //     (l2.Start.Y <= y && y <= l2.End.Y);
            }
        }

        public void Part2()
        {
        }
    }

    public record Point(int X, int Y);

    public record Line(Point Start, Point End)
    {
        public IEnumerable<Point> Points
        {
            get
            {
                if (Start.X == End.X)
                {
                    int min = Math.Min(Start.Y, End.Y);
                    int max = Math.Max(Start.Y, End.Y);
                    for (int y = min; y <= max; y++)
                    {
                        yield return new Point(Start.X, y);
                    }
                }
                else
                {
                    int min = Math.Min(Start.X, End.X);
                    int max = Math.Max(Start.X, End.X);
                    for (int x = min; x <= max; x++)
                    {
                        yield return new Point(x, Start.Y);
                    }
                }
            }
        }
    }
}