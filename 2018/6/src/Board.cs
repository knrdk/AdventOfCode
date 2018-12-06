using System;

namespace src
{
    class Board
    {
        int[,] _data;
        int _width;
        int _height;

        public Board(int width, int height)
        {
            _data = new int[3 * width, 3 * height];
            _width = width;
            _height = height;
        }
        public int Get(int x, int y)
        {
            return _data[_width + x, _height + y];
        }

        public void Set(int x, int y, int value)
        {
            _data[_width + x, _height + y] = value;
        }
    }
}
