using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AOC2019.Day06
{
    class Program
    {
        public static void Main(string[] args)
        {
            //PrintPart1Example();

            var input = File.ReadLines(@"Day06\input.txt");
            var tree = Construct(input);

            Console.WriteLine("Total orbits in input");
            Console.WriteLine(tree.GetTotalParents());
        }

        static Tree Construct(IEnumerable<string> map)
        {
            var tree = new Tree();
            foreach (var line in map)
            {
                string[] instructions = line.Split(')', StringSplitOptions.RemoveEmptyEntries);
                Debug.Assert(instructions.Length == 2, "Line is in incorrect format");

                string orbitee = instructions[0];
                string orbiter = instructions[1];
                var parent = tree.GetOrCreate(orbitee);
                var child = tree.GetOrCreate(orbiter);
                parent.AddChild(child);
            }

            return tree;
        }

        static void PrintPart1Example()
        {
            var input = new[]
            {
                "COM)B",
                "B)C",
                "C)D",
                "D)E",
                "E)F",
                "B)G",
                "G)H",
                "D)I",
                "E)J",
                "J)K",
                "K)L",
            };

            var tree = Construct(input);
            Console.WriteLine("Total orbits: {0}", tree.GetTotalParents());
        }
    }

    public class Tree
    {
        private readonly Dictionary<string, Node> nodes = new Dictionary<string, Node>();

        public Node GetOrCreate(string label)
        {
            if (nodes.TryGetValue(label, out Node node))
            {
                return node;
            }
            else
            {
                node = new Node(label);
                nodes.Add(label, node);
                return node;
            }
        }

        public ICollection<Node> AllNodes => nodes.Values;

        public Node Root => AllNodes.Where(n => n.Parent == null).FirstOrDefault();

        public int GetTotalParents() => AllNodes.Select(n => n.GetTotalParents()).Sum();

        public void Print()
        {
            if (Root != null)
            {
                Print(Root);
            }

            void Print(Node node)
            {
                Console.WriteLine("Node: {0}", node);
                Console.WriteLine("Children: {0}", string.Join(',', node.Children));
                foreach (var child in node.Children)
                {
                    Print(child);
                }
            }
        }
    }

    public class Node
    {
        public Node(string label)
        {
            Label = label;
        }

        public string Label { get; }
        public Node Parent { get; set; }

        public HashSet<Node> Children { get; } = new HashSet<Node>();

        public void AddChild(Node node)
        {
            this.Children.Add(node);
            node.Parent = this;
        }

        public int GetTotalParents()
        {
            int parentCount = 0;
            var target = this.Parent;
            while (target != null)
            {
                parentCount++;
                target = target.Parent;
            }

            return parentCount;
        }

        public override int GetHashCode() => Label.GetHashCode();

        public override bool Equals(object obj) => 
                obj != null &&
                obj is Node that &&
                that.Label == this.Label;
        
        public override string ToString() => $"Node({Label})";
    }
}