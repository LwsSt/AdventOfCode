using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace AOC2019.Day11
{
    class Program
    {
        public static void Main(string[] args)
        {
            long[] memory = File.ReadAllText(@"Day11\input.txt")
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(long.Parse)
                .Concat(new long[1_000])
                .ToArray();

            Console.WriteLine("Running painter");
            Console.WriteLine($"{RunPainter(memory)}  panels painted");
        }

        static int RunPainter(long[] memory)
        {
            var painterInput = new BlockingCollection<long>();
            var painterOutput = new BlockingCollection<long>();

            var painter = new Painter(painterInput, painterOutput.Add);
            var computer = new IntcodeComputer(painterOutput.Take, painterInput.Add);

            var painterThread = new Thread(() => painter.Run());
            var computerThread = new Thread(obj => computer.Run((long[]) obj));

            painterThread.Start();
            computerThread.Start(memory);

            computerThread.Join();
            painterInput.CompleteAdding();

            painterThread.Join();

            return painter.Panels.Keys.Count;
        }
    }

    class Painter
    {
        private readonly Dictionary<Point, Colour> panels = new Dictionary<Point, Colour>();
        private readonly BlockingCollection<long> input;
        private readonly Action<long> output;

        private Point position = new Point(0, 0);
        private Direction direction = Direction.N;

        public Painter(BlockingCollection<long> input, Action<long> output)
        {
            this.input = input;
            this.output = output;
        }

        public Dictionary<Point, Colour> Panels => panels;

        public void Run()
        {
            while(!input.IsAddingCompleted)
            {
                if (panels.TryGetValue(position, out Colour colour))
                {
                    output((long)colour);
                }
                else
                {
                    output((long) Colour.Black);
                }

                // if (input.IsAddingCompleted) 
                //     return;

                if(!input.TryTake(out long col, TimeSpan.FromSeconds(5)))
                {
                    return;
                }

                Colour colourNew = (Colour)col;
                panels[position] = colourNew;

                long dir = input.Take();
                direction = Turn(dir);
                position = Move();
            }
        }
        
        private Point Move()
        {
            switch (direction)
            {
                case Direction.N:
                    return new Point(position.X, position.Y + 1);
                case Direction.E:
                    return new Point(position.X + 1, position.Y);
                case Direction.S:
                    return new Point(position.X, position.Y - 1);
                case Direction.W:
                    return new Point(position.X - 1, position.Y);
                default:
                    throw new Exception($"Invalid direction {direction}");
            }
        }

        private Direction Turn(long input)
        {
            int turn = input == 0 ? -1 : 1;
            int dir = (int)direction;
            if (dir == 0)
            {
                dir = 4;
            }

            dir = (dir + turn) % 4;
            return (Direction)dir;
        }

        private enum Direction
        {
            N = 0, 
            E = 1,
            S = 2, 
            W = 3,
        }
    }

    enum Colour : long
    {
        Black = 0L,
        White = 1L,
    }

    struct Point
    {
        public int X { get; }
        public int Y { get; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString() => $"Point({X},{Y})";

        public override int GetHashCode() => HashCode.Combine(X, Y);

        public override bool Equals(object obj) =>
            obj is Point that &&
            that.X == this.X &&
            that.Y == this.Y;
    }
}