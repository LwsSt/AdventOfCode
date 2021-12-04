using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode.y2020.d02
{
    public class Program : IPuzzle
    {
        const string LinePattern = @"(\d+)-(\d+) (\w): (\w+)";

        private readonly List<PasswordLine> lines;

        public Program()
        {
            var regex = new Regex(LinePattern, RegexOptions.Compiled);

            lines = File.ReadAllLines(@"y2020\d02\input.puzzle")
                .Select(l => regex.Match(l))
                .Where(m => m.Success)
                .Select(m => new PasswordLine()
                {
                    Min = int.Parse(m.Groups[1].Value),
                    Max = int.Parse(m.Groups[2].Value),
                    Character = m.Groups[3].Value[0],
                    Password = m.Groups[4].Value
                })
                .ToList();
        }

        public void Part1()
        {
            int validPasswords = lines
                .Where(pl =>
                {
                    int characters = pl.Password.Where(c => c == pl.Character).Count();
                    return pl.Min <= characters && characters <= pl.Max;
                })
                .Count();
            Console.WriteLine(validPasswords);
        }

        public void Part2()
        {
            int validPasswords = lines
                .Where(pl => pl.Password[pl.Min - 1] == pl.Character ^ pl.Password[pl.Max - 1] == pl.Character)
                .Count();

            Console.WriteLine(validPasswords);
        }
    }

    public class PasswordLine
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public char Character { get; set; }
        public string Password { get; set; }
    }
}