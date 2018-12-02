using System;
using System.Collections.Generic;
using System.IO;

namespace src
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = args[0];
            string[] input = File.ReadAllLines(fileName);

            // part 1
            int result = Solver.Solve(input);
            Console.WriteLine(result);
        
            // part 2
            string result2 = Solver2.Solve(input);
            Console.WriteLine(result2);
        }
    }
}
