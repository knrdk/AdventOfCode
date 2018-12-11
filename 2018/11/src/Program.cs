using System;

namespace src
{
    class Program
    {
        static void Main(string[] args)
        {
            const int size = 300;
            int serialNumber = int.Parse(args[0]);

            int[,] powerLevels = new int[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    int x = i + 1;
                    int y = j + 1;
                    powerLevels[i, j] = CalculatePowerLevel(serialNumber, x, y);
                }
            }

            int maxValue = int.MinValue;
            int maxX = -1;
            int maxY = -1;
            for (int i = 0; i < size - 2; i++)
            {
                for (int j = 0; j < size - 2; j++)
                {
                    int currentValue = CalculateValueOfSquare(powerLevels, i, j);
                    if (currentValue > maxValue)
                    {
                        maxValue = currentValue;
                        maxX = i + 1;
                        maxY = j + 1;
                    }
                }
            }

            Console.WriteLine($"{maxX}, {maxY}");
        }

        public static int CalculatePowerLevel(int serialNumber, int x, int y)
        {
            int rackId = x + 10;
            int powerLevel = rackId * y + serialNumber;
            powerLevel *= rackId;

            int hundreadsDigit = (powerLevel / 100) % 10;
            return hundreadsDigit - 5;
        }

        public static int CalculateValueOfSquare(int[,] powerLevels, int i, int j)
        {
            int result = 0;
            for (int a = i; a < i + 3; a++)
            {
                for (int b = j; b < j + 3; b++)
                {
                    result += powerLevels[a, b];
                }
            }
            return result;
        }
    }
}
