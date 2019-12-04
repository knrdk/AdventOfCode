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

            int numberOfValidNumbers = Enumerable
                .Range(start, end - start + 1)
                .Where(IsValid)
                .Count();

            Console.WriteLine($"Part 1 solution: {numberOfValidNumbers}");
        }

        private static bool IsValid(int x)
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
