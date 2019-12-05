using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC2019.Day05
{
    class Program
    {
        public static void Main(string[] args)
        {
            string puzzleInput = File.ReadAllText(@"Day05\input.txt");
            int[] memory = puzzleInput.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            IntcodeComputer.Run(memory);
        }
    }

    public static class IntcodeComputer
    {
        delegate void Instruction(int[] memory, int instructionPtr, out int instructionLength);
        private static Dictionary<int, Instruction> instructions = new Dictionary<int, Instruction>()
        {
            [1] = Add,
            [2] = Mul,
            [3] = In,
            [4] = Out,
        };

        public static int[] Run(int[] input)
        {
            var memory = new int[input.Length];
            Array.Copy(input, memory, input.Length);
            int opCodeLength = 0;

            for (int instrPtr = 0; instrPtr < memory.Length; )
            {
                int opCode = GetOpCode(memory[instrPtr]);
                if (opCode == 99)
                {
                    return memory;
                }

                if (instructions.TryGetValue(opCode, out Instruction instruction))
                {
                    instruction(memory, instrPtr, out opCodeLength);
                }
                else
                {
                    Console.WriteLine(memory.Print());
                    throw new Exception($"Unexpected opcode {opCode}");
                }

                instrPtr += opCodeLength;
            }

            return memory;
        }

        private static void Add(int[] memory, int instrPtr, out int instructionLength)
        {
            instructionLength = 4;
            int opCode = memory[instrPtr];
            int arg1 = memory[instrPtr + 1];
            int arg2 = memory[instrPtr + 2];
            int outputArg = memory[instrPtr + 3];

            int value1 = GetParameterValue(memory, instrPtr, 0);
            int value2 = GetParameterValue(memory, instrPtr, 1);

            memory[outputArg] = value1 + value2;
        }

        private static void Mul(int[] memory, int instrPtr, out int instructionLength)
        {
            instructionLength = 4;
            int opCode = memory[instrPtr];
            int arg1 = memory[instrPtr + 1];
            int arg2 = memory[instrPtr + 2];
            int outputArg = memory[instrPtr + 3];

            int value1 = GetParameterValue(memory, instrPtr, 0);
            int value2 = GetParameterValue(memory, instrPtr, 1);

            memory[outputArg] = value1 * value2;
        }

        private static void In(int[] memory, int instrPtr, out int instructionLength)
        {
            instructionLength = 2;

            int outputArg = memory[instrPtr + 1];

            Console.Write("INPUT: ");
            string inputStr = Console.ReadLine();

            int input = int.Parse(inputStr);

            memory[outputArg] = input;
        }

        private static void Out(int[] memory, int instrPtr, out int instructionLength)
        {
            instructionLength = 2;

            int opCode = memory[instrPtr];
            int arg = memory[instrPtr + 1];

            int value = GetParameterValue(memory, arg, GetParameterMode(opCode, 0));

            Console.WriteLine($"OUTPUT: {value}");
        }

        private static int GetOpCode(int fullCode) => fullCode % 100;
        private static int GetParameterMode(int fullCode, int parameterIdx)
        {
            double shift = (100 * Math.Pow(10, parameterIdx));
            return (int)((fullCode / shift) % 10);
        }
        private static int GetParameterValue(int[] memory, int instrPtr, int parameterIdx)
        {
            int opCode = memory[instrPtr];
            int parameterMode = GetParameterMode(opCode, parameterIdx);
            int parameter = memory[instrPtr + 1 + parameterIdx];

            if (parameterMode == 1) return parameter;
            if (parameterMode == 0) return memory[parameter];
            throw new Exception("Invalid parameter mode");
        }
    }

    public static class ArrayExtensions
    {
        public static string Print(this int[] arr) => string.Join(',', arr);
    }
}