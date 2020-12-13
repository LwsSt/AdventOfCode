using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace AOC2020.Day12
{
    public class Program
    {
        public const string FileName = "input.txt";

        public void Main()
        {
            var input = ParseInput();
            Part2(input);
        }

        public static void Part2(List<Instruction> instructions)
        {
            var boat = new Boat()
            {
                X = 0,
                Y = 0,
                Waypoint = new Waypoint()
                {
                    X = 10,
                    Y = 1
                }
            };

            // Console.WriteLine("Boat ({0,2}, {1,2}) Waypoint ({2,2}, {3,2})", boat.X, boat.Y, boat.Waypoint.X, boat.Waypoint.Y);

            foreach (var instruction in instructions)
            {
                instruction.Execute(boat);
            }

            int distance = Math.Abs(boat.X) + Math.Abs(boat.Y);
            Console.WriteLine(distance);
        }

        public static List<Instruction> ParseInput()
        {
            return ParseInputImpl().ToList();

            IEnumerable<Instruction> ParseInputImpl()
            {
                foreach (var line in File.ReadLines($"Day12\\{FileName}"))
                {
                    char instruction = line[0];
                    int parameter = int.Parse(line.Substring(1));

                    yield return instruction switch 
                    {
                        'N' => Instruction.N(parameter),
                        'E' => Instruction.E(parameter),
                        'S' => Instruction.S(parameter),
                        'W' => Instruction.W(parameter),
                        'R' => Instruction.R(parameter),
                        'L' => Instruction.L(parameter),
                        'F' => Instruction.F(parameter),
                        _ => throw new Exception($"Invalid instruction {instruction}")
                    };
                }
            }
        }        
    }

    public enum Direction { N = 0, E = 1, S = 2, W = 3 }

    public class Boat
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Waypoint Waypoint { get; set; }
    }

    public class Waypoint
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public abstract class Instruction
    {
        public Instruction(int parameter) => Parameter = parameter;
        public int Parameter { get; set; }

        public void Execute(Boat boat)
        {
            ExecuteCore(boat);
            // Console.WriteLine("Boat ({0,2}, {1,2}) Waypoint ({2,2}, {3,2})", boat.X, boat.Y, boat.Waypoint.X, boat.Waypoint.Y);
        }
        protected abstract void ExecuteCore(Boat boat);

        public static Instruction N(int parameter) => new North(parameter);
        public static Instruction E(int parameter) => new East(parameter);
        public static Instruction S(int parameter) => new South(parameter);
        public static Instruction W(int parameter) => new West(parameter);
        public static Instruction R(int parameter) => new Right(parameter);
        public static Instruction L(int parameter) => new Left(parameter);
        public static Instruction F(int parameter) => new Forward(parameter);

        private class North : Instruction
        {
            public North(int parameter) : base(parameter) { }
            protected override void ExecuteCore(Boat boat) => boat.Waypoint.Y += Parameter;
        }

        private class South : Instruction
        {
            public South(int parameter) : base(parameter) { }
            protected override void ExecuteCore(Boat boat) => boat.Waypoint.Y -= Parameter;
        }

        private class East : Instruction
        {
            public East(int parameter) : base(parameter) { }
            protected override void ExecuteCore(Boat boat) => boat.Waypoint.X += Parameter;
        }

        private class West : Instruction
        {
            public West(int parameter) : base(parameter) { }
            protected override void ExecuteCore(Boat boat) => boat.Waypoint.X -= Parameter;
        }

        private class Right : Instruction
        {
            public Right(int parameter) : base(parameter) { }
            protected override void ExecuteCore(Boat boat) 
            {
                double angle = (360 - Parameter) * (Math.PI / 180);
                int x = boat.Waypoint.X;
                int y = boat.Waypoint.Y;
                int rotatedX = (int) Math.Round(x * Math.Cos(angle) - y * Math.Sin(angle));
                int rotatedY = (int) Math.Round(x * Math.Sin(angle) + y * Math.Cos(angle));
                boat.Waypoint.X = rotatedX;
                boat.Waypoint.Y = rotatedY;
            }
        }

        private class Left : Instruction
        {
            public Left(int parameter) : base(parameter) { }
            protected override void ExecuteCore(Boat boat) 
            {
                double angle = Parameter * (Math.PI / 180);
                int x = boat.Waypoint.X;
                int y = boat.Waypoint.Y;
                int rotatedX = (int) Math.Round(x * Math.Cos(angle) - y * Math.Sin(angle));
                int rotatedY = (int) Math.Round(x * Math.Sin(angle) + y * Math.Cos(angle));
                boat.Waypoint.X = rotatedX;
                boat.Waypoint.Y = rotatedY;
            }
        }

        private class Forward : Instruction
        {
            public Forward(int parameter) : base(parameter) { }
            protected override void ExecuteCore(Boat boat) 
            {
                boat.Y += boat.Waypoint.Y * Parameter;
                boat.X += boat.Waypoint.X * Parameter;
            }
        }
    }
}