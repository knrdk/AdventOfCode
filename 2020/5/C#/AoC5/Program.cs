using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC5
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputFile = @"C:\Users\knrdk\source\repos\AdventOfCode\2020\5\input.txt";

            int maxSeatId = 0;
            bool[] reservedSeats = new bool[1024];
            foreach (var line in File.ReadAllLines(inputFile))
            {
                int currentSeatId = ConvertToSeatId(line);
                reservedSeats[currentSeatId] = true;
                maxSeatId = Math.Max(maxSeatId, currentSeatId);
            }
            Console.WriteLine($"Part1 solution: {maxSeatId}");

            bool previosSeatWasReserved = false;
            for (int i = 0; i < 1024; i++)
            {
                if (!previosSeatWasReserved)
                {
                    previosSeatWasReserved = reservedSeats[i];
                    continue;
                }

                if (!reservedSeats[i])
                {
                    Console.WriteLine($"Part2 solution: {i}");
                    break;
                }
            }
        }

        private static int ConvertToSeatId(string binarySpacePartitioning)
        {
            int row = DecodeBinaryNumber(binarySpacePartitioning.Take(7), 'B');
            int column = DecodeBinaryNumber(binarySpacePartitioning.Skip(7).Take(3), 'R');
            return 8 * row + column;
        }

        private static int DecodeBinaryNumber(IEnumerable<char> number, char signalCharacter)
        {
            int result = 0;
            foreach (char current in number)
            {
                result *= 2;
                if (current == signalCharacter)
                {
                    result++;
                }
            }
            return result;
        }
    }
}
