using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Text.RegularExpressions;

namespace AOC2020.Day14
{
    public class Program
    {
        private const string FileName = "input.txt";

        private static readonly Regex MemorySetRegex = new Regex(@"mem\[(?<address>\d+)\] = (?<value>\d+)", RegexOptions.Compiled);
        private static readonly Regex MaskSetRegex = new Regex(@"mask = (?<mask>[01X]+)", RegexOptions.Compiled);

        public static void Main()
        {
            Part1();
        }

        public static void Part1()
        {
            var state = new State();

            foreach (var line in File.ReadLines($"Day14\\{FileName}"))
            {
                if (line.StartsWith("mem"))
                {
                    var match = MemorySetRegex.Match(line);
                    int address = int.Parse(match.Groups["address"].Value);
                    long value = long.Parse(match.Groups["value"].Value);

                    state.SetMemory(address, value);
                }
                else if(line.StartsWith("mask"))
                {
                    var match = MaskSetRegex.Match(line);
                    string maskStr = match.Groups["mask"].Value;
                    int length = maskStr.Length - 1;
                    var mask = maskStr
                        .Select((c, idx) => (idx: length - idx, @char: c))
                        .Where(cs => cs.@char != 'X')
                        .ToDictionary(cs => cs.idx, cs => cs.@char == '1');

                    state.SetMask(mask);
                }
                else
                {
                    throw new Exception($"Invalid line {line}");
                }
            }

            Console.WriteLine(state.SumMemory());
        }   
    }

    public class State
    {
        private Dictionary<int, bool> mask = new Dictionary<int, bool>();
        private readonly Dictionary<int, long> memory = new Dictionary<int, long>();

        public void SetMemory(int address, long value)
        {
            long actualValue = ApplyMask(value);
            memory[address] = actualValue;
        }

        public void SetMask(Dictionary<int, bool> mask) => this.mask = mask;

        public long SumMemory() => memory.Values.Sum();

        private long ApplyMask(long value)
        {
            var bytes = BitConverter.GetBytes(value);
            var bitArray = new BitArray(bytes);
            foreach (var (address, bit) in mask)
            {
                if (address > bitArray.Count)
                {
                    continue;
                }

                bitArray.Set(address, bit);
            }

            var output = new byte[bitArray.Count / 8];
            bitArray.CopyTo(output, 0);
            return BitConverter.ToInt64(output);
        }
    }
}