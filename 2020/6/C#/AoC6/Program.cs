using System;
using System.IO;
using System.Linq;

namespace AoC6
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputFile = @"C:\Users\knrdk\source\repos\AdventOfCode\2020\6\input.txt";

            const int a_ascii_code = 97;

            int resultPart1 = 0;
            int resultPart2 = 0;
            bool[] answersPart1 = new bool[26];
            bool[] answersPart2 = new bool[26];
            SetAllItems(answersPart2);
            bool lastLineWasEmpty = true;
            foreach (var line in File.ReadAllLines(inputFile))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    if (lastLineWasEmpty)
                    {
                        continue;
                    }
                    resultPart1 += answersPart1.Where(x => x).Count();
                    resultPart2 += answersPart2.Where(x => x).Count();
                    answersPart1 = new bool[26];
                    answersPart2 = new bool[26];
                    SetAllItems(answersPart2);
                    lastLineWasEmpty = true;
                    continue;
                }
                lastLineWasEmpty = false;
                bool[] currentAnswers = new bool[26];
                foreach (char x in line)
                {
                    answersPart1[x - a_ascii_code] = true;
                    currentAnswers[x - a_ascii_code] = true;
                }

                for (int i = 0; i < answersPart2.Length; i++)
                {
                    answersPart2[i] &= currentAnswers[i];
                }
            }

            Console.WriteLine($"Part1 solution: {resultPart1}");
            Console.WriteLine($"Part2 solution: {resultPart2}");
        }

        private static void SetAllItems(bool[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = true;
            }
        }
    }
}
