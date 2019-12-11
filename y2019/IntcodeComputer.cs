using System;
using System.Collections.Generic;

namespace AOC2019
{
    public class IntcodeComputer
    {
        delegate void Instruction(long[] memory, long instructionPtr, out int instructionLength);
        private readonly Dictionary<long, Instruction> instructions;
        private readonly Func<long> readInput;
        private readonly Action<long> writeOutput;
        private long relativeBase = 0;

        public IntcodeComputer(Func<long> input, Action<long> output)
        {
            instructions = new Dictionary<long, Instruction>()
            {
                [1] = Add,
                [2] = Mul,
                [3] = In,
                [4] = Out,
                [5] = JIT,
                [6] = JIF,
                [7] = LT,
                [8] = Eq,
                [9] = Rel,
            };

            this.readInput = input;
            this.writeOutput = output;
        }

        public long[] Run(long[] input)
        {
            var memory = new long[input.Length];
            Array.Copy(input, memory, input.Length);
            int opCodeLength = 0;

            for (long instrPtr = 0; instrPtr < memory.Length; )
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

        private void Add(long[] memory, long instrPtr, out int instructionLength)
        {
            instructionLength = 4;

            long value1 = GetParameterValue(memory, instrPtr, 0);
            long value2 = GetParameterValue(memory, instrPtr, 1);
            long outputArg = GetOutPtr(memory, instrPtr, 2);

            memory[outputArg] = value1 + value2;
        }

        private void Mul(long[] memory, long instrPtr, out int instructionLength)
        {
            instructionLength = 4;

            long value1 = GetParameterValue(memory, instrPtr, 0);
            long value2 = GetParameterValue(memory, instrPtr, 1);
            long outputArg = GetOutPtr(memory, instrPtr, 2);

            memory[outputArg] = value1 * value2;
        }

        private void In(long[] memory, long instrPtr, out int instructionLength)
        {
            instructionLength = 2;

            long outputArg = GetOutPtr(memory, instrPtr, 0);

            long input = this.readInput();

            memory[outputArg] = input;
        }

        private void Out(long[] memory, long instrPtr, out int instructionLength)
        {
            instructionLength = 2;

            long value = GetParameterValue(memory, instrPtr, 0);

            this.writeOutput(value);
        }

        private void JIT(long[] memory, long instrPtr, out int instructionLength)
        {
            long value = GetParameterValue(memory, instrPtr, 0);
            if (value != 0)
            {
                long pointer = GetParameterValue(memory, instrPtr, 1);
                instructionLength = (int)(pointer - instrPtr);
            }
            else
            {
                instructionLength = 3;
            }
        }

        private void JIF(long[] memory, long instrPtr, out int instructionLength)
        {
            long value = GetParameterValue(memory, instrPtr, 0);
            if(value == 0)
            {
                long pointer = GetParameterValue(memory, instrPtr, 1);
                instructionLength = (int)(pointer - instrPtr);

            }
            else
            {
                instructionLength = 3;
            }
        }

        private void LT(long[] memory, long instrPtr, out int instructionLength)
        {
            instructionLength = 4;
            long arg1 = GetParameterValue(memory, instrPtr, 0);
            long arg2 = GetParameterValue(memory, instrPtr, 1);
            long outputArg = GetOutPtr(memory, instrPtr, 2);
            
            long output = arg1 < arg2 ? 1 : 0;

            memory[outputArg] = output;
        }

        private void Eq(long[] memory, long instrPtr, out int instructionLength)
        {
            instructionLength = 4;
            long arg1 = GetParameterValue(memory, instrPtr, 0);
            long arg2 = GetParameterValue(memory, instrPtr, 1);
            long outputArg = GetOutPtr(memory, instrPtr, 2);
            
            int output = arg1 == arg2 ? 1 : 0;

            memory[outputArg] = output;
        }

        private void Rel(long[] memory, long instrPtr, out int instructionLength)
        {
            instructionLength = 2;
            int arg = (int)GetParameterValue(memory, instrPtr, 0);
            relativeBase += arg;
        }

        private int GetOpCode(long fullCode) => (int)(fullCode % 100);
        private int GetParameterMode(long fullCode, int parameterIdx)
        {
            double shift = (100 * Math.Pow(10, parameterIdx));
            return (int)((fullCode / shift) % 10);
        }

        private long GetOutPtr(long[] memory, long instrPtr, int parameterIdx)
        {
            long opCode = memory[instrPtr];
            long parameter = memory[instrPtr + parameterIdx + 1];
            int parameterMode = GetParameterMode(opCode, parameterIdx);

            if (parameterMode == 0) return parameter;
            if (parameterMode == 2) return parameter + relativeBase;
            else throw new Exception($"Unknown parameter mode: {parameterMode}");
        }

        private long GetParameterValue(long[] memory, long instrPtr, int parameterIdx)
        {
            long opCode = memory[instrPtr];
            int parameterMode = GetParameterMode(opCode, parameterIdx);
            long parameter = memory[instrPtr + 1 + parameterIdx];

            if (parameterMode == 1) return parameter;
            if (parameterMode == 0) return memory[parameter];
            if (parameterMode == 2) return memory[relativeBase + parameter];
            throw new Exception("Invalid parameter mode");
        }
    }

    public static class ArrayExtensions
    {
        public static string Print(this long[] arr) => string.Join(',', arr);
    }
}