using System;
using System.IO;
using System.Linq;

namespace AOC2019.Day02
{
    class Program
    {
        static void Main(string[] args)
        {
            Part1Example();

            var puzzleInput = File.ReadAllText(@"Day02\input.txt");
            var programInput = puzzleInput.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(str => int.Parse(str))
                .ToArray();

            programInput[1] = 12;
            programInput[2] = 2;
            
            var programOutput = IntcodeComputer.Run(programInput);
            Console.WriteLine($"Value at Position 0: {programOutput[0]}");
        }

        public static void Part1Example()
        {
            var testInputs = new[]
            {
                (input: new[] {1,9,10,3,2,3,11,0,99,30,40,50}, output: new[] {3500,9,10,70,2,3,11,0,99,30,40,50}),
                (input: new[] {1,0,0,0,99}, output: new[] {2,0,0,0,99}),
                (input: new[] {2,3,0,3,99}, output: new[] {2,3,0,6,99}),
                (input: new[] {2,4,4,5,99,0}, output: new[] {2,4,4,5,99,9801}),
                (input: new[] {1,1,1,4,99,5,6,0,99}, output: new[] {30,1,1,4,2,5,6,0,99}),
            };

            foreach (var (input, output) in testInputs)
            {
                Console.WriteLine($"Input {input.Print(), -20} Expected: {output.Print(), -20} Actual {IntcodeComputer.Run(input).Print(), -20}");
            }
        }
    }

    public static class IntcodeComputer
    {
        public static int[] Run(int[] input)
        {
            var output = new int[input.Length];
            Array.Copy(input, output, input.Length);

            for (int idx = 0; idx < output.Length; idx += 4)
            {
                int opCode = output[idx];
                switch (opCode)
                {
                    case 99:
                        return output;
                    case 1:
                        int addArg1 = output[idx + 1];
                        int addArg2 = output[idx + 2];
                        int addResultIdx = output[idx + 3];
                        output[addResultIdx] = output[addArg1] + output[addArg2];
                        break;
                    case 2:
                        int mulArg1 = output[idx + 1];
                        int mulArg2 = output[idx + 2];
                        int mulResultIdx = output[idx + 3];
                        output[mulResultIdx] = output[mulArg1] * output[mulArg2];
                        break;
                    default:
                        Console.WriteLine(output.Print());
                        throw new Exception($"Unexpected opcode {opCode}");
                }
            }

            return output;
        }
    }

    public static class ArrayExtensions
    {
        public static string Print(this int[] arr) => string.Join(',', arr);
    }
}