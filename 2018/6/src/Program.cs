using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace src
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = args[0];
            Point[] points = ParseInput(File.ReadAllLines(fileName)).ToArray();

            int maxWidth = points.OrderBy(x => x.X).Last().X;
            int maxHeight = points.OrderBy(x => x.Y).Last().Y;
            Console.WriteLine($"Width: {maxWidth}, height: {maxHeight}");

            SolvePart1(points, maxWidth, maxHeight);
            SolvePart2(points, maxWidth, maxHeight);
        }

        private static void SolvePart1(Point[] points, int maxWidth, int maxHeight)
        {
            Board board = new Board(maxWidth, maxHeight);
            for (int i = (-1) * maxWidth; i < 2 * maxWidth; i++)
            {
                for (int j = (-1) * maxHeight; j < 2 * maxHeight; j++)
                {
                    int nearestPoint = CalculateNearestPoint(i, j, points);
                    board.Set(i, j, nearestPoint);
                }
            }

            var infiniteAreaPoints = new HashSet<int>();
            for (int i = (-1) * maxWidth; i < 2 * maxWidth; i++)
            {
                infiniteAreaPoints.Add(board.Get(i, 0));
                infiniteAreaPoints.Add(board.Get(i, 2 * maxHeight - 1));
            }
            for (int j = (-1) * maxHeight; j < 2 * maxHeight; j++)
            {
                infiniteAreaPoints.Add(board.Get(0, j));
                infiniteAreaPoints.Add(board.Get(2 * maxWidth - 1, j));
            }

            var counts = new Dictionary<int, int>();
            for (int i = (-1) * maxWidth; i < 2 * maxWidth; i++)
            {
                for (int j = (-1) * maxHeight; j < 2 * maxHeight; j++)
                {
                    int pointId = board.Get(i, j);
                    if (!infiniteAreaPoints.Contains(pointId))
                    {
                        if (!counts.ContainsKey(pointId) && pointId != -1)
                        {
                            counts[pointId] = 0;
                        }
                        counts[pointId] += 1;
                    }
                }
            }

            int max = counts.Values.OrderBy(x => x).Last();
            Console.WriteLine(max);
        }

        private static void SolvePart2(Point[] points, int maxWidth, int maxHeight)
        {
            Board board = new Board(maxWidth, maxHeight);
            for (int i = (-1) * maxWidth; i < 2 * maxWidth; i++)
            {
                for (int j = (-1) * maxHeight; j < 2 * maxHeight; j++)
                {
                    int totalDistance = CalculateTotalDistance(i, j, points);
                    board.Set(i, j, totalDistance);
                }
            }

            int regionSize = 0;
            for (int i = (-1) * maxWidth; i < 2 * maxWidth; i++)
            {
                for (int j = (-1) * maxHeight; j < 2 * maxHeight; j++)
                {
                    if (board.Get(i, j) < 10000)
                    {
                        regionSize++;
                    }
                }
            }
            Console.WriteLine(regionSize);
        }

        private static int CalculateNearestPoint(int x, int y, Point[] points)
        {
            int min = int.MaxValue;
            int nearestPoint = -1;
            foreach (Point point in points)
            {
                int distance = point.CalculateDistance(x, y);
                if (distance < min)
                {
                    min = distance;
                    nearestPoint = point.Id;
                }
                else if (distance == min)
                {
                    nearestPoint = -1;
                }
            }
            return nearestPoint;
        }

        private static int CalculateTotalDistance(int x, int y, Point[] points)
        {
            int totalDistance = 0;
            foreach (Point point in points)
            {
                totalDistance += point.CalculateDistance(x, y);
            }
            return totalDistance;
        }

        private static IEnumerable<Point> ParseInput(IEnumerable<string> inputs)
        {
            int id = 1;
            foreach (string input in inputs)
            {
                yield return ParseInputLine(id, input);
                id++;
            }
        }

        private static Point ParseInputLine(int id, string input)
        {
            string[] splitted = input.Split(',');
            int x = int.Parse(splitted[0]);
            int y = int.Parse(splitted[1]);
            return new Point(id, x, y);
        }
    }
}
