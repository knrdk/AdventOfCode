using System;
using System.IO;
using System.Linq;
using System.Text;

namespace src
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = args[0];
            Point[] input = File
                .ReadAllLines(fileName)
                .Select(PointParser.Parse)
                .ToArray();

            bool wasSmaller = true;
            int previousWidth = int.MaxValue;
            int previousHeight = int.MaxValue;
            Point[] previousPoints = input;
            Point[] points = input;
            int numberOfSeconds = 0;
            while (wasSmaller)
            {
                (int width, int height) = GetSize(points);

                wasSmaller = width <= previousWidth && height <= previousHeight;

                if(!wasSmaller){
                    break;
                }

                numberOfSeconds++;

                previousWidth = width;
                previousPoints = points;

                previousPoints = points;
                points = points.Select(x => x.MoveUnitOfTime()).ToArray();
            }
            Console.WriteLine($"Seconds passed: {numberOfSeconds-1}");
            PrintPoints(previousPoints);
        }

        private static (int width, int height) GetSize(Point[] points)
        {
            var orderedByX = points.OrderBy(x => x.X);
            int minX = orderedByX.First().X;
            int maxX = orderedByX.Last().X;
            var orderedByY = points.OrderBy(x => x.Y);
            int minY = orderedByY.First().Y;
            int maxY = orderedByY.Last().Y;
            
            int width = 1 + maxX - minX;
            int height = 1 + maxY - minY;

            return (width, height);
        }

        private static void PrintPoints(Point[] points)
        {
            var orderedByX = points.OrderBy(x => x.X);
            int minX = orderedByX.First().X;
            int maxX = orderedByX.Last().X;
            var orderedByY = points.OrderBy(x => x.Y);
            int minY = orderedByY.First().Y;
            int maxY = orderedByY.Last().Y;
            
            int width = 1 + maxX - minX;
            int height = 1 + maxY - minY;

            bool[,] tab = new bool[width, height];

            foreach (Point point in points)
            {
                int x = point.X - minX;
                int y = point.Y - minY;
                tab[x, y] = true;
            }

            for (int j = 0; j < height; j++)
            {
                StringBuilder sb = new StringBuilder(width);
                for (int i = 0; i < width; i++)
                {
                    sb.Append(tab[i, j] ? '#' : '.');
                }
                Console.WriteLine(sb);
            }
        }
    }
}
