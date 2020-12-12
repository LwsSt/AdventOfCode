using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace AOC2020.Day12
{
    public class Program
    {
        public const string FileName = "input.txt";

        public static void Main()
        {
            var input = ParseInput();
            Part1(input);
        }

        public static void Part1(List<Instruction> instructions)
        {
            var boat = new Boat()
            {
                X = 0,
                Y = 0,
                Direction = Direction.E
            };

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
        public Direction Direction { get; set; }
    }

    public abstract class Instruction
    {
        public Instruction(int parameter) => Parameter = parameter;
        public int Parameter { get; set; }

        public void Execute(Boat boat)
        {
            ExecuteCore(boat);
            Console.WriteLine("({0,2}, {1,2}) {2}", boat.X, boat.Y, boat.Direction);
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
            protected override void ExecuteCore(Boat boat) => boat.Y += Parameter;
        }

        private class South : Instruction
        {
            public South(int parameter) : base(parameter) { }
            protected override void ExecuteCore(Boat boat) => boat.Y -= Parameter;
        }

        private class East : Instruction
        {
            public East(int parameter) : base(parameter) { }
            protected override void ExecuteCore(Boat boat) => boat.X += Parameter;
        }

        private class West : Instruction
        {
            public West(int parameter) : base(parameter) { }
            protected override void ExecuteCore(Boat boat) => boat.X -= Parameter;
        }

        private class Right : Instruction
        {
            public Right(int parameter) : base(parameter) { }
            protected override void ExecuteCore(Boat boat) 
            {
                int turnAmount = Parameter / 90;
                int direction = (int)boat.Direction;
                direction += turnAmount;
                direction %= 4;
                boat.Direction = (Direction)direction;
            }
        }

        private class Left : Instruction
        {
            public Left(int parameter) : base(parameter) { }
            protected override void ExecuteCore(Boat boat) 
            {
                int turnAmount = Parameter / 90;
                int direction = (int)boat.Direction;
                direction -= turnAmount;
                direction += 4;
                direction %= 4;
                boat.Direction = (Direction)direction;
            }
        }

        private class Forward : Instruction
        {
            public Forward(int parameter) : base(parameter) { }
            protected override void ExecuteCore(Boat boat) 
            {
                Instruction dir = boat.Direction switch 
                {
                    Direction.N => new North(Parameter),
                    Direction.E => new East(Parameter),
                    Direction.S => new South(Parameter),
                    Direction.W => new West(Parameter),
                    _ => throw new Exception($"Invalid Direction {boat.Direction}")
                };

                dir.Execute(boat);
            }
        }
    }
}