using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode.y2020.d04
{
    public class Program : IPuzzle
    {
        private static readonly Regex passportRegex = new Regex(@"((?<key>\w{3}):(?<val>[#\w]+))\s+", RegexOptions.Compiled);
        private static readonly Regex hairColorRegex = new Regex(@"^#[0-9a-f]{6}$", RegexOptions.Compiled);
        private static readonly Regex passportIdRegex = new Regex(@"^\d{9}$", RegexOptions.Compiled);
        private static readonly HashSet<string> validEyeColors = new HashSet<string>(){"amb", "blu", "brn", "gry", "grn", "hzl", "oth" };

        private readonly List<Passport> passports;

        public Program()
        {
            passports = ParseInput().Select(data => new Passport(data)).ToList();
        }

        public void Part1()
        {
            int validPassports = passports.Count(p => IsValid(p));
            Console.WriteLine(validPassports);

            
        }

        public void Part2()
        {
            var validPassports = passports
                .Where(p => IsValid(p))
                .Where(p => FullValidation(p));

            int validPassportCount = 0;
            foreach (var passport in validPassports)
            {
                //Console.WriteLine(passport);
                validPassportCount++;
            }

            Console.WriteLine(validPassportCount);
        }

        public static bool IsValid(Passport passport) => 
            passport.BirthYear != null &&
            passport.IssueYear != null &&
            passport.ExpirationYear != null &&
            passport.Height != null &&
            passport.HairColor != null &&
            passport.EyeColor != null &&
            passport.PassportId != null;

        public static bool FullValidation(Passport passport)
        {
            return IsBirthYearValid(passport.BirthYear) &&
                IsIssueYearValid(passport.IssueYear) &&
                IsExpiryYearValid(passport.ExpirationYear) &&
                IsHairColorValid(passport.HairColor) &&
                IsEyeColourValid(passport.EyeColor) &&
                IsPassportIdValid(passport.PassportId) &&
                IsHeightValid(passport.Height);

            bool IsBirthYearValid(string birthYr) => birthYr.Length == 4 && 1920 <= int.Parse(birthYr) && int.Parse(birthYr) <= 2002;
            bool IsIssueYearValid(string issueYr) => issueYr.Length == 4 && 2010 <= int.Parse(issueYr) && int.Parse(issueYr) <= 2020;
            bool IsExpiryYearValid(string expiryYr) => expiryYr.Length == 4 && 2020 <= int.Parse(expiryYr) && int.Parse(expiryYr) <= 2030;
            bool IsHairColorValid(string hairColor) => hairColorRegex.IsMatch(hairColor);
            bool IsEyeColourValid(string eyeColor) => validEyeColors.Contains(eyeColor);
            bool IsPassportIdValid(string passportId) => passportIdRegex.IsMatch(passportId);
            bool IsHeightValid(string heightStr)
            {
                if(heightStr.EndsWith("cm"))
                {
                    int height = int.Parse(heightStr.Replace("cm", ""));
                    return 150 <= height && height <= 193;
                }
                else if (heightStr.EndsWith("in"))
                {
                    int height = int.Parse(heightStr.Replace("in", ""));
                    return 59 <= height && height <= 76;
                }
                else
                {
                    return false;
                }
            }
        }

        public static IEnumerable<Dictionary<string, string>> ParseInput()
        {
            using var reader = File.OpenText(@"y2020\d04\input.puzzle");
            string line = null;
            while(!reader.EndOfStream)
            {
                string data = string.Empty;
                do {
                    line = reader.ReadLine();
                    data += line;
                    data += " ";
                } while (!string.IsNullOrEmpty(line));

                var matches = passportRegex.Matches(data);
                yield return matches
                    .Select(m => (key: m.Groups["key"].Value, val: m.Groups["val"].Value))
                    .ToDictionary(kvp => kvp.key, kvp => kvp.val);
            }
        }
    }

    public class Passport
    {
        public Passport(Dictionary<string, string> data)
        {
            BirthYear = GetOrNull(data, "byr");
            IssueYear = GetOrNull(data, "iyr");
            ExpirationYear = GetOrNull(data, "eyr");
            Height = GetOrNull(data, "hgt");
            HairColor = GetOrNull(data, "hcl");
            EyeColor = GetOrNull(data, "ecl");
            PassportId = GetOrNull(data, "pid");
            CountryId = GetOrNull(data, "cid");
        }

        public string BirthYear { get; set; }
        public string IssueYear { get; set; }
        public string ExpirationYear { get; set; }
        public string Height { get; set; }
        public string HairColor { get; set; }
        public string EyeColor { get; set; }
        public string PassportId { get; set; }
        public string CountryId { get; set; }

        public override string ToString()
        {
            return $"byr:{BirthYear}, iyr:{IssueYear}, eyr:{ExpirationYear}, hgt:{Height,5}, hcl:{HairColor}, ecl:{EyeColor}, pid:{PassportId}";
        }

        private static string GetOrNull(Dictionary<string, string> data, string key) => data.TryGetValue(key, out string val) ? val : null;
    }
}
