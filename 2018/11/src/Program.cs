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
            int maxSize = -1;
            int[,] valuesOfSquares = new int[size, size];
            for (int sizeOfSquare = 1; sizeOfSquare < size; sizeOfSquare++)
            {
                for (int i = 0; i < size - (sizeOfSquare - 1); i++)
                {
                    for (int j = 0; j < size - (sizeOfSquare - 1); j++)
                    {
                        int currentValue = CalculateValueOfSquare(powerLevels, i, j, valuesOfSquares, sizeOfSquare);
                        if (currentValue > maxValue)
                        {
                            maxValue = currentValue;
                            maxX = i + 1;
                            maxY = j + 1;
                            maxSize = sizeOfSquare;
                        }
                    }
                }
            }

            Console.WriteLine($"{maxX}, {maxY}, {maxSize}");
        }

        public static int CalculatePowerLevel(int serialNumber, int x, int y)
        {
            int rackId = x + 10;
            int powerLevel = rackId * y + serialNumber;
            powerLevel *= rackId;

            int hundreadsDigit = (powerLevel / 100) % 10;
            return hundreadsDigit - 5;
        }

        public static int CalculateValueOfSquare(int[,] powerLevels, int i, int j, int[,] valuesOfSquares, int sizeOfSquare)
        {
            int result = valuesOfSquares[i, j];
            for (int a = i; a < i + sizeOfSquare; a++)
            {
                result += powerLevels[a, (j + sizeOfSquare - 1)];
            }
            for (int b = j; b < j + sizeOfSquare -1; b++)
            {
                result += powerLevels[(i + sizeOfSquare - 1), b];
            }
            valuesOfSquares[i, j] = result;
            return result;
        }
    }
}
