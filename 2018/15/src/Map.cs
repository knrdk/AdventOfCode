using System;
using System.Text;

namespace src
{
    class Map
    {
        public int Width { get; }
        public int Height { get; }
        private bool[,] _walls;
        public Map(int width, int height)
        {
            Width = width;
            Height = height;
            _walls = new bool[width, height];
        }

        public void AddWall(int x, int y)
        {
            if (x < Width && y < Height)
            {
                _walls[x, y] = true;
            }
        }

        public bool HasWall(int x, int y)
        {
            if (x < Width && y < Height)
            {
                return _walls[x, y];
            }
            return true;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    sb.Append(_walls[x, y] ? '#' : '.');
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}