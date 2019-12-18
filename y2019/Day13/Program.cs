using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AOC2019.Day13
{
    class Program
    {
        // public static void Main(string[] args)
        public void Main(string[] args)
        {
            string input = File.ReadAllText(@"Day13\input.txt");
            long[] memory = input.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(long.Parse)
                .Concat(new long[1_000])
                .ToArray();
            
            var game = new Game();
            Console.WriteLine("Running game");
            game.Run(memory);
            var blockTiles = game.Tiles.Where(t => t.TileId == TileId.Block).Count();
            Console.WriteLine("Block tiles: {0}", blockTiles);
        }
    }

    class Game
    {
        private IntcodeComputer computer;
        private BlockingCollection<long> computerOutput;
        public HashSet<Tile> Tiles { get; }

        public Game()
        {
            computerOutput = new BlockingCollection<long>();
            computer = new IntcodeComputer(() => 0L, computerOutput.Add);
            Tiles = new HashSet<Tile>();
        }

        public void Run(long[] memory)
        {
            computer.Run(memory);
            computerOutput.CompleteAdding();
            var iter = computerOutput.GetConsumingEnumerable().GetEnumerator();
            while(iter.MoveNext())
            {
                long x = iter.Current;
                iter.MoveNext();
                long y = iter.Current;
                iter.MoveNext();
                long tileId = iter.Current;

                Tiles.Add(new Tile()
                {
                    X = x,
                    Y = y,
                    TileId = (TileId)tileId
                });
            }
        }
    }

    class Tile
    {
        public long X { get; set; }
        public long Y { get; set; }
        public TileId TileId { get; set; }

        public override int GetHashCode() => HashCode.Combine(X, Y, TileId);
    }

    enum TileId : long
    {
        Empty = 0,
        Wall = 1,
        Block = 2,
        Paddle = 3,
        Ball = 4,
    }
}