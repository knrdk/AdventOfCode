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
            string filename = args[0];
            string input = File.ReadAllText(filename);

            IRegex regex = Parser.Parse(input);

            Console.WriteLine($"Part1: {regex.GetLongestNonCyclicWord().Length}");

            HashSet<string> validPaths = new HashSet<string>();
            HashSet<string> allPaths = regex.GetNonCyclicWords().ToHashSet();
            foreach (var path in allPaths)
            {
                for (int i = 1000; i <= path.Length; i++)
                {
                    validPaths.Add(path.Substring(0, i));
                }
            }
            Console.WriteLine(validPaths.Count());
            Console.WriteLine(validPaths.Select(GetRoomCoordinates).ToHashSet().Count());
        }

        private static (int south, int west) GetRoomCoordinates(string path)
        {
            int s = path.Where(x => x == 'S').Count();
            int n = path.Where(x => x == 'N').Count();
            int w = path.Where(x => x == 'W').Count();
            int e = path.Where(x => x == 'E').Count();

            return (s - n, w - e);
        }
    }
}
