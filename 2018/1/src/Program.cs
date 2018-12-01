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
            IEnumerable<string> input = File.ReadLines(args[0]);
            int[] frequenciesChanges = Parser.Parse(input).ToArray();
            int finalFrequency = frequenciesChanges.Sum();
            int repetedFrequency = Solver.Solve(frequenciesChanges);

            Console.WriteLine($"{finalFrequency}, {repetedFrequency}");
        }
    }
}
