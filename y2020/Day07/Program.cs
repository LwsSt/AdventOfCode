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
        public static void Main()
        {
            var graph = CreateGraph();
            Part1(graph);
        }

        public static void Part1(AdjacencyGraph<string, TaggedEdge<string, int>> graph)
        {
            int count = GetContainingBags("shiny gold")
                .Distinct()
                .Count();

            Console.WriteLine(count);

            IEnumerable<string> GetContainingBags(string targetBag)
            {
                var edges = graph.OutEdges(targetBag);
                foreach (var edge in edges)
                {
                    // Console.WriteLine(edge.Target);
                    yield return edge.Target;
                    var innerBags = GetContainingBags(edge.Target);
                    foreach (var bag in innerBags)
                    {
                        // Console.WriteLine(bag);
                        yield return bag;
                    }
                }
            }
        }

        public static AdjacencyGraph<string, TaggedEdge<string, int>> CreateGraph()
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
                    // Console.WriteLine("{0} -{1}-> {2}", innerName, amount, name);
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