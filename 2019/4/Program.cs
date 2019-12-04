using System;
using System.Linq;

namespace _4
{
    class Program
    {
        static void Main(string[] args)
        {
            const int start = 171309;
            const int end = 643603;

            int numberOfValidNumbersForPart1 = Enumerable
                .Range(start, end - start + 1)
                .Where(IsValidForPart1)
                .Count();

            Console.WriteLine($"Part 1 solution: {numberOfValidNumbersForPart1}");

            int numberOfValidNumbersForPart2 = Enumerable
                .Range(start, end - start + 1)
                .Where(IsValidForPart2)
                .Count();

            Console.WriteLine($"Part 2 solution: {numberOfValidNumbersForPart2}");
        }

        private static bool IsValidForPart1(int x)
        {
            byte[] digits = GetDigits(x);
            bool haveEqualNeighbours = false;
            for (int i = 0; i < 5; i++)
            {
                if (digits[i] > digits[i + 1])
                {
                    return false;
                }
                haveEqualNeighbours |= digits[i] == digits[i + 1];
            }
            return haveEqualNeighbours;
        }

        private static bool IsValidForPart2(int x)
        {
            byte[] digits = GetDigits(x);
            int groupSize = 0;
            bool hadValidGroup = false;
            for (int i = 0; i < 5; i++)
            {
                if (digits[i] > digits[i + 1])
                {
                    return false;
                }
                bool areNeighboursEqual = digits[i] == digits[i + 1];
                if (areNeighboursEqual)
                {
                    groupSize = groupSize > 0 ? groupSize + 1 : 2;
                }
                else
                {
                    if (groupSize == 2)
                    {
                        hadValidGroup = true;
                    }
                    groupSize = 0;
                }
            }
            return hadValidGroup || groupSize == 2;
        }

        private static byte[] GetDigits(int x)
        {
            byte[] digits = new byte[6];
            for (int i = 0; i < 6; i++)
            {
                digits[5 - i] = (byte)(x % 10);
                x /= 10;
            }
            return digits;
        }
    }
}
