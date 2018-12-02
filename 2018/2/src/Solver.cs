using System;
using System.Collections.Generic;

namespace src
{
    public class Solver
    {
        public static int Solve(IEnumerable<string> inputs)
        {
            int totalDoubleLettersCount = 0;
            int totalTrippleLettersCount = 0;

            foreach (string input in inputs)
            {
                (bool hasDoubleLetters, bool hasTrippleLetters) = SolveOneLine(input);
                totalDoubleLettersCount += hasDoubleLetters ? 1 : 0;
                totalTrippleLettersCount += hasTrippleLetters ? 1 : 0;
            }

            int checksum = totalDoubleLettersCount * totalTrippleLettersCount;
            return checksum;
        }

        public static (bool hasDoubleLetters, bool hasTrippleLetters) SolveOneLine(string input)
        {
            const int firstAsciiIndex = 97;
            const int numberOfAsciiCharacters = 26;

            int[] counts = new int[numberOfAsciiCharacters];
            foreach (char c in input)
            {
                int index = c - firstAsciiIndex;
                counts[index]++;
            }

            bool hasDoubleLetters = false;
            bool hasTrippleLetters = false;
            for (int i = 0; i < numberOfAsciiCharacters; i++)
            {
                hasDoubleLetters |= counts[i] == 2;
                hasTrippleLetters |= counts[i] == 3;
            }

            return (hasDoubleLetters, hasTrippleLetters);
        }
    }
}
