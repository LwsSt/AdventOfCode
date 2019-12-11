using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AOC2019.Day09
{
    class Program
    {
        public static void Main(string[] args)
        // public void Main(string[] args)
        {
            var memory = File.ReadAllText(@"Day09\input.txt")
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(long.Parse)
                .Concat(new long[1_000])
                .ToArray();

            var computer = new IntcodeComputer(() => 2L, Console.WriteLine);
            computer.Run(memory);
            // PrintPart1Example();
        }

        static void PrintPart1Example()
        {
            var testInput = new []
            {
                new long[]{109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99},
                new long[]{1102,34915192,34915192,7,4,7,99,0},
                new long[]{104,1125899906842624,99}
            };

            var computer = new IntcodeComputer(() => 0L, Console.WriteLine);
            foreach (var test in testInput)
            {
                var memory = test.Concat(new long[1_000]).ToArray();
                Console.WriteLine("Test");
                computer.Run(memory);
                Console.WriteLine();
            }
        }
    }
}