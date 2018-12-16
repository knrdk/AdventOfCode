using System;
using System.Collections.Generic;

namespace src
{
    class InputParser
    {
        public static Game Parse(string[] inputs)
        {
            int height = inputs.Length;
            int width = inputs[0].Length;

            Map map = new Map(width, height);
            List<Unit> units = new List<Unit>();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    char currentChar = inputs[y][x];
                    if (currentChar == '#')
                    {
                        map.AddWall(x, y);
                    }
                    else if (currentChar == 'G')
                    {
                        var unit = new Unit(x, y, UnitType.Goblin);
                        units.Add(unit);
                    }
                    else if (currentChar == 'E')
                    {
                        var unit = new Unit(x, y, UnitType.Elf);
                        units.Add(unit);
                    }
                }
            }

            var game = new Game(map, units);
            return game;
        }
    }
}