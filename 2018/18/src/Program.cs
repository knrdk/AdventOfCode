using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace src
{
    class Program
    {
        private static Dictionary<char, AcreTypes> _acreTypesMapping = new Dictionary<char, AcreTypes>()
        {
            ['.'] = AcreTypes.OpenGround,
            ['|'] = AcreTypes.Trees,
            ['#'] = AcreTypes.Lumberyard
        };

        static void Main(string[] args)
        {
            string fileName = args[0];
            string[] inputLines = File.ReadAllLines(fileName);

            Area area = BuildArea(inputLines);
            for (int i = 0; i < 10; i++)
            {
                area = area.GetNext();
            }
            Console.WriteLine(area);
            Console.WriteLine(area.ResourceValues);
        }

        private static Area BuildArea(string[] inputLines)
        {
            int height = inputLines.Length;
            int width = inputLines.First().Length;
            var areaBuilder = new Area.AreaBuilder(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    areaBuilder.SetValue(x, y, _acreTypesMapping[inputLines[y][x]]);
                }
            }
            return areaBuilder.Build();
        }
    }
}
