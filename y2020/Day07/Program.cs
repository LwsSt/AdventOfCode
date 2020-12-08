using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Diagnostics;
using QuikGraph;

namespace AOC2020.Day07
{
    public class Program
    {
        private static readonly Regex lineRegex = new Regex(@"(\w+ \w+) bags contain ([^.]*).", RegexOptions.Compiled);
        private static readonly Regex innerBagREgex = new Regex(@"(\d+) (\w+ \w+) bags?", RegexOptions.Compiled);
        public void Main()
        {
            Part1();
            Part2();
        }

        public static void Part1()
        {
            var graph = CreateGraphPart1();
            int count = GetContainingBags("shiny gold")
                .Distinct()
                .Count();

            Console.WriteLine(count);

            IEnumerable<string> GetContainingBags(string targetBag)
            {
                var edges = graph.OutEdges(targetBag);
                foreach (var edge in edges)
                {
                    yield return edge.Target;
                    var innerBags = GetContainingBags(edge.Target);
                    foreach (var bag in innerBags)
                    {
                        yield return bag;
                    }
                }
            }
        }

        public static void Part2()
        {
            var graph = CreateGraphPart2();
            int sum = GetContainingBags("shiny gold", 1)
                .Select(kvp => kvp.amount)
                .Sum();
            Console.WriteLine(sum);

            IEnumerable<(int amount, string name)> GetContainingBags(string targetBag, int amount)
            {
                var edges = graph.OutEdges(targetBag);
                int sum = edges.Select(e => e.Tag).Sum();
                yield return (sum * amount, targetBag);
                foreach (var edge in edges)
                {
                    foreach (var bag in GetContainingBags(edge.Target, edge.Tag * amount))
                    {
                        yield return bag;
                    }
                }
            }
        }

        public static AdjacencyGraph<string, TaggedEdge<string, int>> CreateGraphPart2()
        {
            var graph = new AdjacencyGraph<string, TaggedEdge<string, int>>();

            foreach (var rule in ParseInput())
            {
                string name = rule.Name;
                graph.AddVertex(name);
                foreach (var (innerName, amount) in rule.InnerBags)
                {
                    graph.AddVertex(innerName);
                    graph.AddEdge(new TaggedEdge<string, int>(name, innerName, amount));
                }
            }

            return graph;
        }

        public static AdjacencyGraph<string, TaggedEdge<string, int>> CreateGraphPart1()
        {
            var graph = new AdjacencyGraph<string, TaggedEdge<string, int>>();

            foreach (var rule in ParseInput())
            {
                string name = rule.Name;
                graph.AddVertex(name);
                foreach (var (innerName, amount) in rule.InnerBags)
                {
                    graph.AddVertex(innerName);
                    graph.AddEdge(new TaggedEdge<string, int>(innerName, name, amount));
                }
            }

            return graph;
        }

        public static IEnumerable<Rule> ParseInput()
        {
            foreach (var line in File.ReadLines(@"Day07\input.txt"))
            {
                var lineMatch = lineRegex.Match(line);
                string name = lineMatch.Groups[1].Value;
                var innerBags = ParseInnerBags(lineMatch.Groups[2].Value);

                yield return new Rule(name, innerBags);
            }

            Dictionary<string, int> ParseInnerBags(string data)
            {
                if (data == "no other bags")
                {
                    return new Dictionary<string, int>();
                }
                
                return data.Split(",", StringSplitOptions.RemoveEmptyEntries)
                    .Select(r => innerBagREgex.Match(r))
                    .Where(m => m.Success)
                    .Select(m => (amount: int.Parse(m.Groups[1].Value), name: m.Groups[2].Value))
                    .ToDictionary(kvp => kvp.name, kvp => kvp.amount);
            }
        }
    }

    public class Rule
    {
        public Rule(string name, Dictionary<string, int> innerBags)
        {
            Name = name;
            InnerBags = innerBags;
        }

        public string Name { get; }
        public Dictionary<string, int> InnerBags { get; }
    }
}