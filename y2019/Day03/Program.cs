using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC2019.Day03
{
    class Program
    {
        public static void Main(string[] args)
        {

        }
    }

    class Wire
    {
        private readonly Point start;
        private readonly Point end;

        public Wire(Point start, Point end)
        {
            this.start = start;
            this.end = end;
        }

        public IEnumerable<Point> GetPoints()
        {
            yield return start;
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
    }
}