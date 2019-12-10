using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace AOC2019.Day07
{
    class Program
    {
        const int Length = 5;

        public static void Main(string[] args)
        // public void Main(string[] args)
        {

            string input = File.ReadAllText(@"Day07\input.txt");
            int[] memory = input.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(s => int.Parse(s))
                .ToArray();
            
            Console.WriteLine("Max output:");
            int max = RunAmplification(memory).Max();
            Console.WriteLine(max);
        }

        static IEnumerable<int> RunAmplification(int[] memory)
        {
            foreach (var phase in GetAllPhases())
            {
                yield return RunAmplificationWithPhase(memory, phase).Last();
            }
        }

        static void PrintPart1Examples()
        {
            var testInputs = new[]
            {
                (new int[]{3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0}, new[]{4,3,2,1,0}),
                (new int[]{3,23,3,24,1002,24,10,24,1002,23,-1,23,101,5,23,23,1,24,23,23,4,23,99,0,0}, new[]{0,1,2,3,4}),
                (new int[]{3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0}, new[]{1,0,4,3,2}),
            };

            foreach(var (memory, phase) in testInputs)
            {
                var output = RunAmplificationWithPhase(memory, phase);
                Console.WriteLine(output.Print());
            }
        }

        static int[] RunAmplificationWithPhase(int[] memory, int[] phase)
        {
            var queues = new BlockingCollection<int>[phase.Length];
            for (int i = 0; i < queues.Length; i++)
            {
                queues[i] = new BlockingCollection<int>();
                queues[i].Add(phase[i]);
            }
            queues.First().Add(0);

            var threads = new Thread[phase.Length];
            for (int i = 0; i < phase.Length; i++)
            {
                var inQ = queues[i];
                var outQ = queues[(i + 1) % queues.Length];

                var computer = new IntcodeComputer(inQ.Take, outQ.Add);

                threads[i] = new Thread(obj => computer.Run((int[]) obj));
                threads[i].Start(memory);
            }

            foreach (var t in threads)
            {
                t.Join();
            }

            return queues.First().ToArray();
        }

        public static IEnumerable<int[]> GetAllPhases()
        {
            int[] numbers = new int[]{ 0, 1, 2, 3, 4 };
            return GetPermutations(numbers, numbers.Length);
            IEnumerable<int[]> GetPermutations(int[] arr, int size)
            {
                if (size == 1)
                {
                    yield return arr;
                }
                
                for (int i = 0; i < size; i++)
                {
                    foreach (var perm in GetPermutations(arr, size - 1))
                    {
                        yield return perm;
                    }
        
                    // if size is odd, swap first and last 
                    // element 
                    if (size % 2 == 1) 
                    { 
                        int temp = arr[0]; 
                        arr[0] = arr[size-1]; 
                        arr[size-1] = temp; 
                    } 
                    else
                    { 
                        int temp = arr[i]; 
                        arr[i] = arr[size-1]; 
                        arr[size-1] = temp; 
                    } 
                }
            }
        }
    }

    class Buffer
    {
        private readonly int[] buffer;
        private int pointer;

        public Buffer(int[] buffer)
        {
            this.buffer = buffer;
        }

        public int ReadInput() => buffer[pointer++];

        public void WriteOutput(int value) => buffer[pointer + 1] = value;
    }

    public class IntcodeComputer
    {
        delegate void Instruction(int[] memory, int instructionPtr, out int instructionLength);
        private Dictionary<int, Instruction> instructions;
        private Func<int> readInput;
        private Action<int> writeOutput;

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

        private static void Add(int[] memory, int instrPtr, out int instructionLength)
        {
            instructionLength = 4;
            int outputArg = memory[instrPtr + 3];

            int value1 = GetParameterValue(memory, instrPtr, 0);
            int value2 = GetParameterValue(memory, instrPtr, 1);

            memory[outputArg] = value1 + value2;
        }

        private static void Mul(int[] memory, int instrPtr, out int instructionLength)
        {
            instructionLength = 4;
            int outputArg = memory[instrPtr + 3];

            int value1 = GetParameterValue(memory, instrPtr, 0);
            int value2 = GetParameterValue(memory, instrPtr, 1);

            memory[outputArg] = value1 * value2;
        }

        private void In(int[] memory, int instrPtr, out int instructionLength)
        {
            instructionLength = 2;

            int outputArg = memory[instrPtr + 1];

            int input = this.readInput();

            memory[outputArg] = input;
        }

        private void Out(int[] memory, int instrPtr, out int instructionLength)
        {
            instructionLength = 2;

            int value = GetParameterValue(memory, instrPtr, 0);

            this.writeOutput(value);
        }

        private static void JIT(int[] memory, int instrPtr, out int instructionLength)
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

        private static void JIF(int[] memory, int instrPtr, out int instructionLength)
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

        private static void LT(int[] memory, int instrPtr, out int instructionLength)
        {
            instructionLength = 4;
            int arg1 = GetParameterValue(memory, instrPtr, 0);
            int arg2 = GetParameterValue(memory, instrPtr, 1);
            int outputArg = memory[instrPtr + 3];

            
            int output = arg1 < arg2 ? 1 : 0;

            memory[outputArg] = output;
        }

        private static void Eq(int[] memory, int instrPtr, out int instructionLength)
        {
            instructionLength = 4;
            int arg1 = GetParameterValue(memory, instrPtr, 0);
            int arg2 = GetParameterValue(memory, instrPtr, 1);
            int outputArg = memory[instrPtr + 3];
            
            int output = arg1 == arg2 ? 1 : 0;

            memory[outputArg] = output;
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