using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace src
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = args[0];
            char[] input = File.ReadAllText(fileName).ToCharArray();

            SolvePart1(input);
            SolvePart2(input);
        }

        private static void SolvePart1(char[] input)
        {
            int result = input
                .Select(x => x == '(' ? 1 : -1)
                .Sum();
            Console.WriteLine(result);
        }

        private static void SolvePart2(char[] input)
        {
            IEnumerable<int> directions = input.Select(x => x == '(' ? 1 : -1);
            int i = 0;
            int currentFloor = 0;
            foreach (int direction in directions)
            {
                i++;
                currentFloor += direction;
                if (currentFloor == -1)
                {
                    Console.WriteLine(i);
                    return;
                }
            }
        }
    }
}
