using System;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

namespace Aoc01
{
    class Program
    {
        static async Task Main(string[] args)
        {
            const int ExpectedSum = 2020;

            string inputFilePath = @"C:\Users\knrdk\source\repos\AdventOfCode\2020\1\C#\Aoc01\input.txt";
            string[] allLines = await File.ReadAllLinesAsync(inputFilePath);
            int[] inputNumbers = allLines.Select(int.Parse).OrderBy(x => x).ToArray();

            SolveFirstPart(ExpectedSum, inputNumbers);
            SolveSecondPart(ExpectedSum, inputNumbers);
        }

        private static void SolveFirstPart(int ExpectedSum, int[] inputNumbers)
        {
            Console.WriteLine("Solving first part");
            (int a, int b) = Solve(ExpectedSum, inputNumbers, startIndex: 0);
            Console.WriteLine($"Solution found: {a}*{b}={a * b}");
        }

        private static void SolveSecondPart(int ExpectedSum, int[] inputNumbers)
        {
            Console.WriteLine("Solving second part");
            for (int i = 0; i < inputNumbers.Length - 2; i++)
            {
                int a = inputNumbers[i];
                int expectedPartialSum = ExpectedSum - a;
                (int b, int c) = Solve(expectedPartialSum, inputNumbers, i);
                if (b != 0)
                {
                    Console.WriteLine($"Solution found: {a}*{b}*{c}={a * b * c}");
                }
            }
        }

        private static (int a, int b) Solve(int ExpectedSum, int[] inputNumbers, int startIndex)
        {
            int currentLowIndex = startIndex;
            int currentHighIndex = inputNumbers.Length - 1;

            while (currentLowIndex < currentHighIndex)
            {
                int a = inputNumbers[currentLowIndex];
                int b = inputNumbers[currentHighIndex];
                int currentSum = a + b;

                if (currentSum == ExpectedSum)
                {
                    return (a, b);
                }
                else if (currentSum < ExpectedSum)
                {
                    currentLowIndex++;
                }
                else
                {
                    currentHighIndex--;
                }
            }

            return (0, 0);
        }
    }
}
