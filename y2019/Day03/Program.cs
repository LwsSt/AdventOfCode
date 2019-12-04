using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AOC2019.Day03
{
    class Program
    {
        public static void Main(string[] args)
        {
            //PrintPart1Examples();

            string[] input = File.ReadAllLines(@"Day03\input.txt");

            var wires1 = Parser.ParseWires(input[0].Split(',', StringSplitOptions.RemoveEmptyEntries));
            var wires2 = Parser.ParseWires(input[1].Split(',', StringSplitOptions.RemoveEmptyEntries));

            PrintCrossingPoints(wires1, wires2);
        }

        private static void PrintCrossingPoints(IEnumerable<Wire> wires1, IEnumerable<Wire> wires2)
        {
            var product = from wire1 in wires1
                          from wire2 in wires2
                          select (wire1, wire2);

            var crossingPoints = product
                .Where(wires => wires.wire1.Crosses(wires.wire2))
                .Select(wires => wires.wire1.CrossingPoint(wires.wire2));

            var shortestCrossingDistance = crossingPoints
                .Select(point => Math.Abs(point.X) + Math.Abs(point.Y))
                .Min();

            Console.WriteLine($"CrossingDistance: {shortestCrossingDistance}");

            // Console.WriteLine("Crossing Points:");
            // foreach (var point in crossingPoints)
            // {
            //     Console.WriteLine($"{point.ToString()}: Length: {Math.Abs(point.X) + Math.Abs(point.Y)}");
            // }
        }

        private static void PrintPart1Examples()
        {
            var testInputs = new[]
            {
                ("R8,U5,L5,D3", "U7,R6,D4,L4"),
                ("R75,D30,R83,U83,L12,D49,R71,U7,L72", "U62,R66,U55,R34,D71,R55,D58,R83"),
                ("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51", "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7"),
            };

            foreach (var test in testInputs)
            {
                var wires1 = Parser.ParseWires(test.Item1.Split(',', StringSplitOptions.RemoveEmptyEntries));
                var wires2 = Parser.ParseWires(test.Item2.Split(',', StringSplitOptions.RemoveEmptyEntries));

                Console.WriteLine("Example:");
                PrintCrossingPoints(wires1, wires2);

                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }
    
    static class Parser 
    {
        public static IEnumerable<Wire> ParseWires(IEnumerable<string> instructions)
        {
            var origin = new Point(0, 0);

            foreach (var instr in instructions)
            {
                var (direction, length) = ParseInstruction(instr);

                var next = default(Point);
                switch(direction) 
                {
                    case 'R':
                        next = new Point(origin.X + length, origin.Y);
                        break;
                    case 'L':
                        next = new Point(origin.X - length, origin.Y);
                        break;
                    case 'U':
                        next = new Point(origin.X, origin.Y + length);
                        break;
                    case 'D':
                        next = new Point(origin.X, origin.Y - length);
                        break;
                    default:
                        throw new  Exception($"Instrustion not recognised: {instr}");
                }

                yield return new Wire(origin, next);
                
                origin = next;
            }
        }

        private static (char direction, int length) ParseInstruction(string instruction)
        {
            char direction = instruction[0];
            int length = int.Parse(instruction.Substring(1));

            return (direction, length);
        }
    }

    class Wire
    {
        public Wire(Point start, Point end)
        {
            Start = start;
            End = end;
        }

        public Point Start { get; }
        public Point End { get; }

        private bool IsVertical => Start.X == End.X;
        private bool IsHorizontal => Start.Y == End.Y;

        private int LowerX => Math.Min(Start.X, End.X);
        private int LowerY => Math.Min(Start.Y, End.Y);
        private int UpperX => Math.Max(Start.X, End.X);
        private int UpperY => Math.Max(Start.Y, End.Y);

        public Point CrossingPoint(Wire that)
        {
            if (this.IsVertical)
            {
                Debug.Assert(that.IsHorizontal, "Other wire should be horizontal");

                return new Point(this.Start.X, that.Start.Y);
            }
            else
            {
                Debug.Assert(that.IsVertical, "Other wire should be vertical");

                return new Point(that.Start.X, this.Start.Y);

            }
        }

        public bool Crosses(Wire that) 
        {
            if ((this.IsVertical && that.IsVertical) ||
                (this.IsHorizontal && that.IsHorizontal))
            {
                return false;
            }

            if (this.IsVertical)
            {
                Debug.Assert(that.IsHorizontal, "Other wire should be horizontal");

                return that.LowerX <= this.Start.X && this.Start.X <= that.UpperX &&
                    this.LowerY <= that.Start.Y && that.Start.Y <= this.UpperY;
            }
            else
            {
                Debug.Assert(that.IsVertical, "Other wire should be vertical");

                return this.LowerX <= that.Start.X && that.Start.X <= this.UpperX &&
                    that.LowerY <= this.Start.Y && this.Start.Y <= that.UpperY;
            }
        }
    }

    struct Point
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }

        public override bool Equals(object obj)
        {
            return obj != null &&
                obj is Point that &&
                that.X == this.X &&
                that.Y == this.Y;
        }

        public override int GetHashCode() => HashCode.Combine(X, Y);

        public override string ToString() => $"Point({X},{Y})";
    }
}