using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace AOC2020.Day08
{
    public class Program
    {
        public const string FileName = "input.txt";
        public static void Main()
        {
            var instructions = ParseInput();
            Part1(instructions);
        }

        public static void Part1(List<Instruction> instructions)
        {
            var (accumulator, _) = RunProgram(instructions);

            Console.WriteLine(accumulator);
        }

        public static (int accumulator, bool terminated) RunProgram(List<Instruction> instructions)
        {
            int pointer = 0;
            int accumulator = 0;
            var visitedInstructions = new HashSet<Instruction>();

            while(pointer < instructions.Count && visitedInstructions.Add(instructions[pointer]))
            {
                // Console.Write("{0, 3}  ", pointer);
                // Console.WriteLine(instructions[pointer]);
                switch (instructions[pointer].Op)
                {
                    case Operation.Nop:
                        pointer++;
                        break;
                    case Operation.Acc:
                        accumulator += instructions[pointer].Arg;
                        pointer++;
                        break;
                    case Operation.Jmp:
                        pointer += instructions[pointer].Arg;
                        break;
                    default:
                        throw new Exception("Invalid Operation");
                }
            }
            
            return (accumulator, pointer >= instructions.Count);
        }

        public static List<Instruction> ParseInput()
        {
            return Parse().ToList();

            IEnumerable<Instruction> Parse()
            {
                foreach (var (line, idx) in File.ReadLines($"Day08\\{FileName}").Select((line, idx) => (line, idx)))
                {
                    var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    var operation = GetOp(parts[0]);
                    int arg = int.Parse(parts[1]);

                    yield return new Instruction()
                    {
                        Op = operation,
                        Arg = arg,
                        Index = idx
                    };
                }
            }

            Operation GetOp(string op) => op switch 
            {
                "acc" => Operation.Acc,
                "jmp" => Operation.Jmp,
                "nop" => Operation.Nop,
                _ => throw new ArgumentException($"Operation not recognised {op}")
            };
        }
    }

    public class Instruction
    {
        public Operation Op { get; set; }
        public int Arg { get; set; }
        public int Index { get; set; }

        public override string ToString() => $"{Op} {Arg}";

        public override int GetHashCode() => HashCode.Combine(Op, Arg, Index);

        public override bool Equals(object obj) =>
            obj is Instruction that &&
            this.Op == that.Op &&
            this.Arg == that.Arg &&
            this.Index == that.Index;
    }

    public enum Operation
    {
        Acc,
        Jmp,
        Nop
    }
}