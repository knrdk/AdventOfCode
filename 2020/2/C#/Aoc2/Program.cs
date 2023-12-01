using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Aoc2
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputFile = @"C:\Users\knrdk\source\repos\AdventOfCode\2020\2\input.txt";
            var regex = new Regex(@"^([\d]+)-([\d]+) (.*): (.*)$", RegexOptions.Compiled); // example: "9-10 b: bbktbbbxhfbpb"

            int numberOfValidPasswordForFirstPart = 0;
            int numberOfValidPasswordForSecondPart = 0;
            foreach (var line in File.ReadAllLines(inputFile))
            {
                var match = regex.Match(line);
                var min = int.Parse(match.Groups[1].Value);
                var max = int.Parse(match.Groups[2].Value);
                char letter = match.Groups[3].Value.ToCharArray().Single();
                string password = match.Groups[4].Value;

                int numberOfOccurences = password.Count(x => x == letter);
                bool isValidForFirstPart = numberOfOccurences >= min && numberOfOccurences <= max;

                if (isValidForFirstPart)
                {
                    numberOfValidPasswordForFirstPart++;
                }

                bool isValidForSecondPart = (min - 1 < password.Length && password[min - 1] == letter)
                    != (max - 1 < password.Length && password[max - 1] == letter);

                if (isValidForSecondPart)
                {
                    numberOfValidPasswordForSecondPart++;
                }
            }
            Console.WriteLine($"Part 1 solution: {numberOfValidPasswordForFirstPart}");
            Console.WriteLine($"Part 2 solution: {numberOfValidPasswordForSecondPart}");

        }
    }
}
