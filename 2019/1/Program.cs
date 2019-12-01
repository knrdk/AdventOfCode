using System;
using System.IO;
using System.Linq;

namespace _1
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputPath = args[0];
            Console.WriteLine($"Processing input file: {inputPath}");

            string[] input = File.ReadAllLines(inputPath);
            int solution = input
                .Select(x=>int.Parse(x))
                .Select(x=>x/3-2)
                .Sum();
            Console.WriteLine($"Solution: {solution}");
        }
    }
}
