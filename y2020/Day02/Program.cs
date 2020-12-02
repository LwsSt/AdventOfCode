using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AOC2020.Day02
{
    public class Program
    {
        const string LinePattern = @"(\d+)-(\d+) (\w): (\w+)";

        public static void Main(string[] args)
        {
            var regex = new Regex(LinePattern, RegexOptions.Compiled);

            var lines = File.ReadAllLines(@"Day02\input.txt")
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

            Part1(lines);
        }

        public static void Part1(IEnumerable<PasswordLine> lines)
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
    }

    public class PasswordLine
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public char Character { get; set; }
        public string Password { get; set; }
    }
}