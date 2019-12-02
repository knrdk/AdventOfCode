using System;
using System.IO;
using System.Linq;

namespace _2
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputPath = args[0];
            Console.WriteLine($"Processing input file: {inputPath}");

            int[] input = File
                .ReadAllLines(inputPath)
                .First()
                .Split(',')
                .Select(x=>int.Parse(x))
                .ToArray();               

            input[1] = 12;
            input[2] = 2;

            Console.WriteLine($"Solution part 1: {ExecuteProgram(input)}");
        }

        private static int ExecuteProgram(int[] program)
        {
            int i = 0;
            while (true)
            {
                int optcode = program[i];
                if (optcode == 1)
                {
                    int arg0 = program[program[i + 1]];
                    int arg1 = program[program[i + 2]];
                    int result = arg0 + arg1;
                    program[program[i + 3]] = result;
                }
                else if (optcode == 2)
                {
                    int arg0 = program[program[i + 1]];
                    int arg1 = program[program[i + 2]];
                    int result = arg0 * arg1;
                    program[program[i + 3]] = result;
                }
                else if (optcode == 99)
                {
                    return program[0];
                }
                else
                {
                    throw new InvalidOperationException("Unknown optcode");
                }

                i += 4;
            }
        }
    }
}
