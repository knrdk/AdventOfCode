using System;

namespace src
{
    class Point
    {
        public int Id { get; }
        public int X { get; }
        public int Y { get; }

        public Point(int id, int x, int y)
        {
            Id = id;
            X = x;
            Y = y;
        }

        public int CalculateDistance(Point a)
        {
            return Math.Abs(X - a.X) + Math.Abs(Y - a.Y);
        }

        public int CalculateDistance(int x, int y)
        {
            return Math.Abs(X - x) + Math.Abs(Y - y);
        }
    }
}
