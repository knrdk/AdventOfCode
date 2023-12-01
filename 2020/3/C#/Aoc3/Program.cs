using System;
using System.IO;
using System.Linq;

namespace Aoc3
{
    class Program
    {
        static void Main(string[] args)
        {
            // Input parser
            string inputFile = @"C:\Users\knrdk\source\repos\AdventOfCode\2020\3\input.txt";

            string[] allLines = File.ReadAllLines(inputFile);
            int inputHeight = allLines.Length;
            int inputWidth = allLines.First().Length;

            bool[,] map = new bool[inputHeight, inputWidth];
            for (int i = 0; i < inputHeight; i++)
            {
                for (int j = 0; j < inputWidth; j++)
                {
                    bool isTree = '#' == allLines[i].ElementAt(j);
                    map[i, j] = isTree;
                }
            }

            // Part 1
            int slopeRight = 3;
            int slopeDown = 1;

            int numberOfTrees = FindNumberOfTrees(map, slopeRight, slopeDown);
            Console.WriteLine($"Part 1 solution: {numberOfTrees}");

            // Part 2
            (int slopeRight, int slopeDown)[] inputs = new[] {
                (1, 1),
                (3, 1),
                (5, 1),
                (7, 1),
                (1, 2),
            };

            long numberOfTreesForPartTwo = 1;
            foreach (var input in inputs)
            {
                numberOfTreesForPartTwo *= FindNumberOfTrees(map, input.slopeRight, input.slopeDown);
            }

            Console.WriteLine($"Part 1 solution: {numberOfTreesForPartTwo}");
        }

        private static int FindNumberOfTrees(bool[,] map, int slopeRight, int slopeDown)
        {
            int inputHeight = map.GetLength(0);
            int inputWidth = map.GetLength(1);

            int x = 0;
            int y = 0;

            int numberOfTrees = 0;
            while (y < inputHeight)
            {
                if (map[y, x])
                {
                    numberOfTrees++;
                }

                x = (x + slopeRight) % inputWidth;
                y += slopeDown;
            }

            return numberOfTrees;
        }
    }
}
