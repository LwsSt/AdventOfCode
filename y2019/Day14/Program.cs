using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC2019.Day14
{
    class Program
    {
        public static void Main(string[] args)
        {

        }

        static void PrintPart1Examples()
        {

        }

        static void SolvePart1(Dictionary<string, Reagent> recipes)
        {
            var fuel = recipes["FUEL"];
        }
    }

    class Reagent
    {
        public Reagent(string name, int quantity)
        {
            Name = name;
            Quantity = quantity;
        }

        public string Name { get; }
        public int Quantity { get; private set; }
        public List<Reagent> Ingredients { get; } = new List<Reagent>();
        public Reagent FindIngredient(string ingredientName) => Ingredients.FirstOrDefault(i => i.Name == ingredientName);
        public void AddIngredient(Reagent ingredient) => Ingredients.Add(ingredient);
    }

    static class Parser
    {
        static Dictionary<string, Reagent> BuildRecipeList(IEnumerable<string> lines)
        {
            return lines.Select(ParseLine)
                .ToDictionary(r => r.Name);
        }

        static Reagent ParseLine(string str)
        {
            string[] parts = str.Split("=>", StringSplitOptions.RemoveEmptyEntries);
            var output = Parse(parts[1].Trim());
            var ingredients = parts[0].Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .Select(Parse);

            foreach (var ingredient in ingredients)
            {
                output.AddIngredient(ingredient);
            }

            return output;
        }

        static Reagent Parse(string str)
        {
            var match = Regex.Match(str, @"(?<quantity>\d+) (?<name>[A-Z]+)");
            string quantity = match.Groups["quantity"].Value;
            string name = match.Groups["name"].Value;
            return new Reagent(name, int.Parse(quantity));
        }
    }

}