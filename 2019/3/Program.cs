using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _3
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputPath = args[0];
            Console.WriteLine($"Processing input file: {inputPath}");

            var fileLines = File.ReadAllLines(inputPath);
            var firstLineParsed = ParseInput(fileLines[0]);
            var secondLineParsed = ParseInput(fileLines[1]);

            var firstLinePoints = GetAllPoints(firstLineParsed);
            var secondLinePoints = GetAllPoints(secondLineParsed);

            var commonPoints = firstLinePoints.Intersect(secondLinePoints);

            int solution = commonPoints.Select(x => Math.Abs(x.Item1) + Math.Abs(x.Item2)).OrderBy(x => x).First();
            Console.WriteLine($"First part solution: {solution}");

            int min = int.MaxValue;
            foreach (var point in commonPoints)
            {
                int i1 = firstLinePoints.IndexOf(point) + 1;
                int i2 = secondLinePoints.IndexOf(point) + 1;
                int sum = i1 + i2;
                min = Math.Min(min, sum);
            }
            Console.WriteLine($"Second part solution: {min}");
        }

        static List<(int, int)> GetAllPoints(IEnumerable<(int, Direction)> input)
        {
            var allPoints = new List<(int, int)>();
            (int, int) currentPoint = (0, 0);
            foreach ((int, Direction) move in input)
            {
                for (int i = 0; i < move.Item1; i++)
                {
                    if (move.Item2 == Direction.Top)
                    {
                        currentPoint = (currentPoint.Item1, currentPoint.Item2 + 1);
                    }
                    else if (move.Item2 == Direction.Down)
                    {
                        currentPoint = (currentPoint.Item1, currentPoint.Item2 - 1);
                    }
                    else if (move.Item2 == Direction.Right)
                    {
                        currentPoint = (currentPoint.Item1 + 1, currentPoint.Item2);
                    }
                    else if (move.Item2 == Direction.Left)
                    {
                        currentPoint = (currentPoint.Item1 - 1, currentPoint.Item2);
                    }
                    else
                    {
                        throw new InvalidOperationException("Direction unknown");
                    }

                    allPoints.Add(currentPoint);
                }
            }
            return allPoints;
        }

        static IEnumerable<(int, Direction)> ParseInput(string inputLine)
        {
            return inputLine
                .Split(',')
                .Select(ParseElement);
        }

        static (int, Direction) ParseElement(string element)
        {
            Direction direction = Direction.None;
            if (element[0] == 'R')
            {
                direction = Direction.Right;
            }
            else if (element[0] == 'L')
            {
                direction = Direction.Left;
            }
            else if (element[0] == 'U')
            {
                direction = Direction.Top;
            }
            else if (element[0] == 'D')
            {
                direction = Direction.Down;
            }

            int value = int.Parse(element.Substring(1));

            return (value, direction);
        }
    }

    enum Direction
    {
        None,
        Left,
        Right,
        Top,
        Down
    }
}
