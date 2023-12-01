using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Aoc4
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputFile = @"C:\Users\knrdk\source\repos\AdventOfCode\2020\4\input.txt";

            string[] requiredFields = {
                "byr",
                "iyr",
                "eyr",
                "hgt",
                "hcl",
                "ecl",
                "pid",
            };

            string[] optionalFields = {
                "cid",
            };

            var validators = new Dictionary<string, Func<string, bool>>()
            {
                ["byr"] = x => { int.TryParse(x, out int parsed); return parsed >= 1920 && parsed <= 2002; },
                ["iyr"] = x => { int.TryParse(x, out int parsed); return parsed >= 2010 && parsed <= 2020; },
                ["eyr"] = x => { int.TryParse(x, out int parsed); return parsed >= 2020 && parsed <= 2030; },
                ["hgt"] = IsHeightValid,
                ["hcl"] = IsHairColorValid,
                ["ecl"] = x => new[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" }.Contains(x),
                ["pid"] = x => { long.TryParse(x, out long parsed); return x.Length == 9 && parsed < 1000000000; },
                ["cid"] = x => true,
            };

            string[] allValidFields = requiredFields.Concat(optionalFields).ToArray();

            var currentFields = new List<(string key, string value)>();
            int numberOfValidPassportsForPart1 = 0;
            int numberOfValidPassportsForPart2 = 0;
            foreach (var line in File.ReadAllLines(inputFile))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    bool hasOnlyValidFields = currentFields.Select(x => x.key).All(x => allValidFields.Contains(x));
                    bool hasAllRequiredFields = requiredFields.All(x => currentFields.Select(x => x.key).Contains(x));
                    bool isValidForPart1 = hasOnlyValidFields && hasAllRequiredFields;
                    if (isValidForPart1)
                    {
                        numberOfValidPassportsForPart1++;

                        bool isValidForPart2 = currentFields.All(x => validators[x.key](x.value));
                        if (isValidForPart2)
                        {
                            numberOfValidPassportsForPart2++;
                        }
                    }
                    currentFields.Clear();
                }
                else
                {
                    var fieldsInCurrentLine = line.Split(' ').Select(x => (x.Split(':').First(), x.Split(':').Skip(1).First()));
                    currentFields.AddRange(fieldsInCurrentLine);
                }
            }
            Console.WriteLine($"Part1 solution: {numberOfValidPassportsForPart1}");
            Console.WriteLine($"Part2 solution: {numberOfValidPassportsForPart2}");
        }

        private static bool IsHairColorValid(string input)
        {
            var chars = input.AsEnumerable<char>().ToArray();
            if (chars.Length != 7 || chars[0] != '#')
            {
                return false;
            }
            char[] validChars = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };
            return chars.Skip(1).All(x => validChars.Contains(x));
        }

        public static bool IsHeightValid(string input)
        {
            bool isDigit = int.TryParse(new string(input.Take(input.Length - 2).ToArray()), out int digit);
            if (!isDigit)
            {
                return false;
            }

            string twoLastChars = new string(input.Skip(input.Length - 2).ToArray());
            if (twoLastChars == "cm")
            {
                return digit >= 150 && digit <= 193;
            }
            else if (twoLastChars == "in")
            {
                return digit >= 59 && digit <= 76;
            }

            return false;
        }
    }
}
