using System;
using System.Collections.Generic;
using System.Linq;

namespace src
{
    class Program
    {
        static void Main(string[] args)
        {
            int input = 824501;
            SolvePart1(input);
            SolvePart2(new byte[] { 8, 2, 4, 5, 0, 1 });
        }

        private static void SolvePart1(int input)
        {
            int requiredNumberOfRecipes = input + 10;
            List<byte> recipes = new List<byte>(requiredNumberOfRecipes + 1) { 3, 7 };
            int current1 = 0;
            int current2 = 1;

            while (recipes.Count < requiredNumberOfRecipes)
            {
                int sum = recipes[current1] + recipes[current2];
                byte unitDigit = (byte)(sum % 10);
                byte decimalDigit = (byte)(sum / 10);
                if (decimalDigit != 0)
                {
                    recipes.Add(decimalDigit);
                }
                recipes.Add(unitDigit);

                current1 = (current1 + 1 + recipes[current1]) % recipes.Count;
                current2 = (current2 + 1 + recipes[current2]) % recipes.Count;
            }

            IEnumerable<byte> solutionScores = recipes.Skip(input).Take(10);
            string solution = string.Join("", solutionScores);
            Console.WriteLine(solution);
        }

        private static void SolvePart2(byte[] expected)
        {
            List<byte> recipes = new List<byte>(2000000) { 3, 7 };
            int current1 = 0;
            int current2 = 1;

            while (true)
            {
                int sum = recipes[current1] + recipes[current2];
                byte unitDigit = (byte)(sum % 10);
                byte decimalDigit = (byte)(sum / 10);
                if (decimalDigit > 0)
                {
                    recipes.Add(decimalDigit);
                    if (Validate(expected, recipes))
                    {
                        return;
                    }
                }
                recipes.Add(unitDigit);
                if (Validate(expected, recipes))
                {
                    return;
                }

                current1 = (current1 + 1 + recipes[current1]) % recipes.Count;
                current2 = (current2 + 1 + recipes[current2]) % recipes.Count;
            }
        }

        private static bool Validate(byte[] expected, List<byte> recipes)
        {
            int startIndex = recipes.Count - expected.Length;
            if (startIndex < 0)
            {
                return false;
            }

            byte[] candidate = new byte[expected.Length];
            recipes.CopyTo(startIndex, candidate, 0, expected.Length);
            bool result = ByteArrayCompare(expected, candidate);
            if (result)
            {
                Console.WriteLine(startIndex);
            }
            return result;
        }

        static bool ByteArrayCompare(ReadOnlySpan<byte> a1, ReadOnlySpan<byte> a2)
        {
            return a1.SequenceEqual(a2);
        }
    }
}
