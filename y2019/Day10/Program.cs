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
            var lines = File.ReadAllLines(@"Day10/input.txt");
            var asteriods = Parse(lines).ToHashSet();
        }

        static IEnumerable<Asteriod> Parse(string[] lines)
        {
            for (int y = 0; y < lines.Length; y++)
            {
                char[] columns = lines[y].ToCharArray();
                for (int x = 0; x < columns.Length; x++)
                {
                    if (columns[x] == '#')
                    {
                        yield return new Asteriod(x, y);
                    }
                }
            }
        }

        static void Process(HashSet<Asteriod> asteriods)
        {

        }

        static int HCF(int a, int b)
        {
            int max = Math.Max(a, b);
            for (int f = max - 1; f >= 2 ; f--)
            {
                if (a % f == 0 && b % f == 0)
                {
                    return f;
                }
            }

            return -1;
        }
    }

    struct Asteriod
    {
        public int X { get; }
        public int Y { get; }

        public Asteriod(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString() => $"Ast({X},{Y})";

        public override int GetHashCode() => HashCode.Combine(X, Y);

        public override bool Equals(object obj) =>
            obj is Asteriod that &&
            that.X == this.X &&
            that.Y == this.Y;
    }
}
