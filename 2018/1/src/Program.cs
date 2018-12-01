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
            IEnumerable<int> frequenciesChanges = Parser.Parse(input);
            int finalFrequency = frequenciesChanges.Sum();
            Console.WriteLine(finalFrequency);
        }
    }
}
