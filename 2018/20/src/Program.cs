using System;
using System.IO;

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
        }
    }
}
