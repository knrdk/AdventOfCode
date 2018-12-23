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

            Dictionary<long, int> resourceValues = new Dictionary<long, int>();
            Area area = BuildArea(inputLines);
            for (long i = 0; i < 1000; i++)
            {
                area = area.GetNext();

                int value = area.ResourceValues;
                if (resourceValues.Values.Contains(value))
                {
                    long key = resourceValues.Single(x=>x.Value == value).Key;
                    Console.WriteLine($"{i+1} - {key} - {value}");
                }
                else
                {
                    resourceValues[i] = value;
                }
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
