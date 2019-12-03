using System;
using System.IO;
using System.Linq;

namespace AOC2019.Day01
{
    class Program
    {
        static void Main(string[] args)
        {
            Part2Example();

            var puzzleInput = File.ReadAllLines(@"Day01\input.txt");
            int totalFuel = puzzleInput
                .Select(str => int.Parse(str))
                .Select(MassCalculator.CalculateTotal)
                .Sum();

            Console.WriteLine($"Total Fuel {totalFuel}");
        }

        public static void Part1Example()
        {
            var testInputs = new[]
            {
                (mass: 12, fuel: 2),
                (mass: 14, fuel: 2),
                (mass: 1969, fuel: 654),
                (mass: 100756, fuel: 33583)
            };

            foreach (var (mass, fuel) in testInputs)
            {
                Console.WriteLine($"Mass {mass, -6} expected Fuel: {fuel, -6} Actual: {MassCalculator.Calculate(mass)}");
            }
        }

        public static void Part2Example()
        {
            var testInputs = new[]
            {
                (mass: 14, fuel: 2),
                (mass: 1969, fuel: 966),
                (mass: 100756, fuel: 50346)
            };

            foreach (var (mass, fuel) in testInputs)
            {
                Console.WriteLine($"Mass {mass, -6} expected Fuel: {fuel, -6} Actual: {MassCalculator.CalculateTotal(mass)}");
            }
        }
    }

    public static class MassCalculator
    {
        public static int Calculate(int mass)
        {
            double fuel = Math.Round((double)mass / 3, MidpointRounding.ToZero);
            fuel =  fuel - 2;

            return (int) fuel;
        }

        public static int CalculateTotal(int mass)
        {
            int fuel = 0;

            int additionalFuel = Calculate(mass);
            
            while(additionalFuel >= 0)
            {
                fuel += additionalFuel;
                additionalFuel = Calculate(additionalFuel);
            }

            return fuel;
        }
    }
}
