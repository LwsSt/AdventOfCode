using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using static System.Math;

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
            }
        }

        public void Part2()
        {
            var intersecting = 
                from l1 in lines
                from l2 in lines
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
                int o1 = Orientation(l1.Start, l1.End, l2.Start);
                int o2 = Orientation(l1.Start, l1.End, l2.End);
                int o3 = Orientation(l2.Start, l2.End, l1.Start);
                int o4 = Orientation(l2.Start, l2.End, l1.End);

                if (o1 != o2 && o3 != o4)
                    return true;
            
                if (o1 == 0 && OnSegment(l1.Start, l2.Start, l1.End)) 
                    return true;
            
                if (o2 == 0 && OnSegment(l1.Start, l2.End, l1.End))
                    return true;
            
                if (o3 == 0 && OnSegment(l2.Start, l1.Start, l2.End)) 
                    return true;
            
                if (o4 == 0 && OnSegment(l2.Start, l1.End, l2.End)) 
                    return true;
            
                return false;
            }

            int Orientation(Point p, Point q, Point r)
            {
                int val = ((q.Y - p.Y) * (r.X - q.X)) - ((q.X - p.X) * (r.Y - q.Y));
        
            if (val == 0) return 0; // collinear
        
            return (val > 0) ? 1 : -1; 
            }

            static bool OnSegment(Point p, Point q, Point r)
            {
                if (q.X <= Math.Max(p.X, r.X) && q.X >= Math.Min(p.X, r.X) &&
                    q.Y <= Math.Max(p.Y, r.Y) && q.Y >= Math.Min(p.Y, r.Y))
                return true;
    
                return false;
            }
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
                else if (Start.Y == End.Y)
                {
                    int min = Math.Min(Start.X, End.X);
                    int max = Math.Max(Start.X, End.X);
                    for (int x = min; x <= max; x++)
                    {
                        yield return new Point(x, Start.Y);
                    }
                }
                else
                {
                    int x0 = Start.X;
                    int y0 = Start.Y;
                    int rangeX = End.X - Start.X;
                    int rangeY = End.Y - Start.Y;

                    for (int i = 0; i <= Math.Abs(rangeX); i++)
                    {
                        yield return new Point(x0 + i * Sign(rangeX), y0 + i * Sign(rangeY));
                    }
                }

                int Sign(int n) => n >= 0 ? 1 : -1;
            }
        }
    }
}