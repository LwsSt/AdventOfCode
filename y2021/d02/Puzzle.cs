using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.y2021.d02
{
    public class Puzzle : IPuzzle
    {
        public void Part1()
        {
            int distance = 0;
            int depth = 0;

            foreach (var (command, param) in ParseInput())
            {
                switch (command)
                {
                    case Command.Down:
                        depth += param;
                        break;
                    case Command.Up:
                        depth -= param;
                        break;
                    case Command.Forward:
                        distance += param;
                        break;
                    default:
                        break;
                }
            }

            Console.WriteLine(distance * depth);
        }

        public void Part2()
        {
            int distance = 0;
            int depth = 0;
            int aim = 0;

            foreach (var (command, param) in ParseInput())
            {
                switch (command)
                {
                    case Command.Down:
                        aim += param;
                        break;
                    case Command.Up:
                        aim -= param;
                        break;
                    case Command.Forward:
                        distance += param;
                        depth += (param * aim);
                        break;
                    default:
                        break;
                }
            }

            Console.WriteLine(distance * depth);
        }

        IEnumerable<(Command, int)> ParseInput()
        {
            foreach (var line in File.ReadLines(@"y2021\d02\input.puzzle"))
            {
                string[] param = line.Split(' ', System.StringSplitOptions.RemoveEmptyEntries);
                
                yield return (ParseCommand(param[0]), int.Parse(param[1]));
            }

            Command ParseCommand(string cmd) => cmd switch 
            {
                "up" => Command.Up,
                "down" => Command.Down,
                "forward" => Command.Forward,
                _ => throw new System.Exception()
            };
        }
    }

    public enum Command
    {
        Up,
        Down,
        Forward,
    }
}