using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode.y2021.d04
{
    public class Puzzle : IPuzzle
    {
        private readonly List<int> calledNumbers;
        private readonly List<Board> boards;

        public Puzzle()
        {
            var input = File.ReadAllLines(@"y2021/d04/input.puzzle");
            calledNumbers = input[0]
                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            boards = Parse(input.Skip(2).Where(l => !string.IsNullOrEmpty(l)).ToList()).ToList();
        }

        public void Part1()
        {
            int winningNumber = default;
            Board winningBoard = default;

            foreach (var number in calledNumbers)
            {
                foreach (var (board, idx) in boards.Select((b, i) => (b, i)))
                {
                    if (board.MarkNumber(number))
                    {
                        winningNumber = number;
                        winningBoard = board;
                        goto Success;
                    }
                }

                if (winningBoard != null)
                    break;
            }

            Success:
            if (winningBoard != null)
            {
                int sum = winningBoard.SumUnmarkedNumber();

                Console.WriteLine(sum);
                Console.WriteLine(winningNumber);
                Console.WriteLine(sum * winningNumber);
            }
        }

        public void Part2()
        {
        }

        private IEnumerable<Board> Parse(List<string> input)
        {
            for (int i = 0; i < input.Count; )
            {
                int[,] board = new int[5, 5];
                for (int y = 0; y < 5; i++, y++)
                {
                    input[i].Split(' ', StringSplitOptions.RemoveEmptyEntries)
                        .Select((num, x) => (x, num: int.Parse(num)))
                        .ToList()
                        .ForEach(pair => board[pair.x, y] = pair.num);

                }

                yield return new Board(board);
            }
        }
    }

    public class Board
    {
        private readonly Dictionary<int, (int x, int y)> numbers = new Dictionary<int, (int, int)>();
        private readonly bool[,] gameBoard = new bool[5, 5];

        public Board(int[,] board)
        {
            for (int x = 0; x < gameBoard.GetLength(0); x++)
            {
                for (int y = 0; y < gameBoard.GetLength(1); y++)
                {
                    int number = board[x, y];
                    numbers[number] = (x, y);
                }
            }
        }

        public int SumUnmarkedNumber()
        {
            return numbers
                .Where(kvp => !gameBoard[kvp.Value.x, kvp.Value.y])
                .Select(kvp => kvp.Key)
                .Sum();
        }

        public bool MarkNumber(int number)
        {
            if (numbers.TryGetValue(number, out var coords))
            {
                gameBoard[coords.x, coords.y] = true;
                return CheckWin();
            }

            return false;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            foreach (var (num, coords) in numbers)
            {
                builder.AppendFormat("({0}, {1}) = {2}", coords.x, coords.y, num);
                builder.AppendLine();
            }

            return builder.ToString();
        }

        private bool CheckWin()
        {
            for (int x = 0; x < gameBoard.GetLength(0); x++)
            {
                bool success = true;
                for (int y = 0; y < gameBoard.GetLength(1); y++)
                {
                    success &= gameBoard[x, y];
                }

                if(success)
                    return true;
            }

            for (int y = 0; y < gameBoard.GetLength(0); y++)
            {
                bool success = true;
                for (int x = 0; x < gameBoard.GetLength(1); x++)
                {
                    success &= gameBoard[x, y];
                }

                if(success)
                    return true;
            }

            return false;
        }
    }
}