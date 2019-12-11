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
            //PrintPart1Examples();
            // PrintPart2Examples();

            string input = File.ReadAllText(@"Day07\input.txt");
            long[] memory = input.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(s => long.Parse(s))
                .ToArray();
            
            Console.WriteLine("Max output:");
            long max = RunAmplification(memory).Max();
            Console.WriteLine(max);
        }

        static IEnumerable<long> RunAmplification(long[] memory)
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
                (new long[]{3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0}, new[]{4,3,2,1,0}),
                (new long[]{3,23,3,24,1002,24,10,24,1002,23,-1,23,101,5,23,23,1,24,23,23,4,23,99,0,0}, new[]{0,1,2,3,4}),
                (new long[]{3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0}, new[]{1,0,4,3,2}),
            };

            foreach(var (memory, phase) in testInputs)
            {
                var output = RunAmplificationWithPhase(memory, phase);
                Console.WriteLine(output.Print());
            }
        }

        static void PrintPart2Examples()
        {
            var testInputs = new[]
            {
                (new long[]{3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5}, new[]{9,8,7,6,5}),
                (new long[]{3,52,1001,52,-5,52,3,53,1,52,56,54,1007,54,5,55,1005,55,26,1001,54,-5,54,1105,1,12,1,53,54,53,1008,54,0,55,1001,55,1,55,2,53,55,53,4,53,1001,56,-1,56,1005,56,6,99,0,0,0,0,10}, new[]{9,7,8,5,6}),
            }; 

            foreach(var (memory, phase) in testInputs)
            {
                var output = RunAmplificationWithPhase(memory, phase);
                Console.WriteLine(output.Print());
            }
        }

        static long[] RunAmplificationWithPhase(long[] memory, int[] phase)
        {
            var queues = new BlockingCollection<long>[phase.Length];
            for (int i = 0; i < queues.Length; i++)
            {
                queues[i] = new BlockingCollection<long>();
                queues[i].Add(phase[i]);
            }
            queues.First().Add(0); // Prime first input

            var threads = new Thread[phase.Length];
            for (int i = 0; i < phase.Length; i++)
            {
                var inQ = queues[i];
                var outQ = queues[(i + 1) % queues.Length];

                var computer = new IntcodeComputer(inQ.Take, outQ.Add);

                threads[i] = new Thread(obj => computer.Run((long[]) obj));
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
            // int[] numbers = new int[]{ 0, 1, 2, 3, 4 };
            int[] numbers = new int[]{ 5, 6, 7, 8, 9 };
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
}