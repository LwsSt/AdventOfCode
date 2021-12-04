using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode.y2020.d05
{
    public class Program : IPuzzle
    {
        private static readonly Func<Range, Range> getLower = r => r.GetLowerRange();
        private static readonly Func<Range, Range> getUpper = r => r.GetUpperRange();

        public void Part2()
        {
            var ids = GetSeatIDs().ToList();
            int max = ids.Max();
            int min = ids.Min();
            var fullRange = Enumerable.Range(min, max - min);

            var missingIds = fullRange.Except(ids);
            foreach (var id in missingIds)
            {
                Console.WriteLine(id);
            }
        }

        public void Part1()
        {
            int max = GetSeatIDs().Max();
            Console.WriteLine(max);
        }

        public static IEnumerable<int> GetSeatIDs()
        {
            foreach (var (rows, cols) in ParseInput())
            {
                int rowNum = rows(Range.GetRowRange()).End;
                int colNum = cols(Range.GetColumnRange()).End;
                
                // Console.WriteLine("Row({0}) Col({1})", rowNum, colNum);

                yield return (rowNum * 8) + colNum;
            }
        }

        public static IEnumerable<(Func<Range, Range> rows, Func<Range, Range> columns)> ParseInput()
        {
            var lines = File.ReadLines(@"y2020\d05\input.puzzle");
            foreach (var line in lines)
            {
                string rows = line.Substring(0, 7);
                string columns = line.Substring(7);

                var rowsFunc = rows.Select(c => c == 'F' ? getLower : getUpper)
                    .Aggregate((f, g) => r => g(f(r)));

                var columnsFunc = columns.Select(c => c == 'R' ? getUpper : getLower)
                    .Aggregate((f, g) => r => g(f(r)));

                yield return (rowsFunc, columnsFunc);
            }
        }
    }

    public class Range
    {
        public static Range GetRowRange() => new Range(0, 127);
        public static Range GetColumnRange() => new Range(0, 7);

        private Range(int start, int end)
        {
            Start = start;
            End = end;
        }

        public int Start { get; }
        public int End { get; }
        public int Length => End - Start;

        public Range GetUpperRange()
        {
            int start = Start + (Length / 2);
            return new Range(start, End);
        }

        public Range GetLowerRange()
        {
            int end = Start + (Length / 2);
            return new Range(Start, end);
        }
    }
}