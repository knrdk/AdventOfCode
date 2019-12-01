using System;
using System.IO;
using System.Linq;

namespace _1
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputPath = args[0];
            Console.WriteLine($"Processing input file: {inputPath}");

            int[] input = File
                .ReadAllLines(inputPath)
                .Select(x => int.Parse(x))
                .ToArray();

            Console.WriteLine($"Part 1 solution: {SolvePart1(input)}");
            Console.WriteLine($"Part 2 solution: {SolvePart2(input)}");
        }

        static int SolvePart1(int[] input) => input
            .Select(CalculateRequiredFuelForMass)
            .Sum();

        static int SolvePart2(int[] input) => input
            .Select(CalculateTotalRequiredFuelForMass)
            .Sum();

        private static int CalculateRequiredFuelForMass(int mass) => mass / 3 - 2;

        private static int CalculateTotalRequiredFuelForMass(int mass)
        {
            int requiredFuel = CalculateRequiredFuelForMass(mass);

            return requiredFuel > 0
                ? requiredFuel + CalculateTotalRequiredFuelForMass(requiredFuel)
                : 0;
        }
    }
}
