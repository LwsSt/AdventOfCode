using System;
using System.Collections.Generic;

namespace AOC2019
{
    public class IntcodeComputer
    {
        delegate void Instruction(int[] memory, int instructionPtr, out int instructionLength);
        private readonly Dictionary<int, Instruction> instructions;
        private readonly Func<int> readInput;
        private readonly Action<int> writeOutput;
        private int relativeBase = 0;

        public IntcodeComputer(Func<int> input, Action<int> output)
        {
            instructions = new Dictionary<int, Instruction>()
            {
                [1] = Add,
                [2] = Mul,
                [3] = In,
                [4] = Out,
                [5] = JIT,
                [6] = JIF,
                [7] = LT,
                [8] = Eq,
            };

            this.readInput = input;
            this.writeOutput = output;
        }

        public int[] Run(int[] input)
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

        private void Add(int[] memory, int instrPtr, out int instructionLength)
        {
            instructionLength = 4;

            int value1 = GetParameterValue(memory, instrPtr, 0);
            int value2 = GetParameterValue(memory, instrPtr, 1);
            int outputArg = GetOutPtr(memory, instrPtr, 2);

            memory[outputArg] = value1 + value2;
        }

        private void Mul(int[] memory, int instrPtr, out int instructionLength)
        {
            instructionLength = 4;

            int value1 = GetParameterValue(memory, instrPtr, 0);
            int value2 = GetParameterValue(memory, instrPtr, 1);
            int outputArg = GetOutPtr(memory, instrPtr, 2);

            memory[outputArg] = value1 * value2;
        }

        private void In(int[] memory, int instrPtr, out int instructionLength)
        {
            instructionLength = 2;

            int outputArg = GetOutPtr(memory, instrPtr, 0);

            int input = this.readInput();

            memory[outputArg] = input;
        }

        private void Out(int[] memory, int instrPtr, out int instructionLength)
        {
            instructionLength = 2;

            int value = GetParameterValue(memory, instrPtr, 0);

            this.writeOutput(value);
        }

        private void JIT(int[] memory, int instrPtr, out int instructionLength)
        {
            int value = GetParameterValue(memory, instrPtr, 0);
            if (value != 0)
            {
                int pointer = GetParameterValue(memory, instrPtr, 1);
                instructionLength = pointer - instrPtr;
            }
            else
            {
                instructionLength = 3;
            }
        }

        private void JIF(int[] memory, int instrPtr, out int instructionLength)
        {
            int value = GetParameterValue(memory, instrPtr, 0);
            if(value == 0)
            {
                int pointer = GetParameterValue(memory, instrPtr, 1);
                instructionLength = pointer - instrPtr;

            }
            else
            {
                instructionLength = 3;
            }
        }

        private void LT(int[] memory, int instrPtr, out int instructionLength)
        {
            instructionLength = 4;
            int arg1 = GetParameterValue(memory, instrPtr, 0);
            int arg2 = GetParameterValue(memory, instrPtr, 1);
            int outputArg = GetOutPtr(memory, instrPtr, 2);
            
            int output = arg1 < arg2 ? 1 : 0;

            memory[outputArg] = output;
        }

        private void Eq(int[] memory, int instrPtr, out int instructionLength)
        {
            instructionLength = 4;
            int arg1 = GetParameterValue(memory, instrPtr, 0);
            int arg2 = GetParameterValue(memory, instrPtr, 1);
            int outputArg = GetOutPtr(memory, instrPtr, 2);
            
            int output = arg1 == arg2 ? 1 : 0;

            memory[outputArg] = output;
        }

        private void Rel(int[] memory, int instrPtr, out int instructionLength)
        {
            instructionLength = 2;
            int arg = GetParameterValue(memory, instrPtr, 0);
            relativeBase += arg;
        }

        private int GetOpCode(int fullCode) => fullCode % 100;
        private int GetParameterMode(int fullCode, int parameterIdx)
        {
            double shift = (100 * Math.Pow(10, parameterIdx));
            return (int)((fullCode / shift) % 10);
        }

        private int GetOutPtr(int[] memory, int instrPtr, int parameterIdx)
        {
            int opCode = memory[instrPtr];
            int parameter = memory[instrPtr + parameterIdx + 1];
            int parameterMode = GetParameterMode(opCode, parameterIdx);

            if (parameterMode == 0) return parameter;
            if (parameterMode == 2) return parameter + relativeBase;
            else throw new Exception($"Unknown parameter mode: {parameterMode}");
        }

        private int GetParameterValue(int[] memory, int instrPtr, int parameterIdx)
        {
            int opCode = memory[instrPtr];
            int parameterMode = GetParameterMode(opCode, parameterIdx);
            int parameter = memory[instrPtr + 1 + parameterIdx];

            if (parameterMode == 1) return parameter;
            if (parameterMode == 0) return memory[parameter];
            if (parameterMode == 2) return memory[relativeBase + parameter];
            throw new Exception("Invalid parameter mode");
        }
    }

    public static class ArrayExtensions
    {
        public static string Print(this int[] arr) => string.Join(',', arr);
    }
}