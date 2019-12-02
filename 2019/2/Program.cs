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
                .Select(x => int.Parse(x))
                .ToArray();

            Console.WriteLine($"Solution part 1: {SolvePart1(input)}");
            Console.WriteLine($"Solution part 2: {SolvePart2(input)}");
        }

        private static int SolvePart1(int[] program)
        {
            int[] copiedProgram = CopyProgram(program);

            return ExecuteProgram(copiedProgram, 12, 2);
        }

        private static int SolvePart2(int[] program)
        {
            const int expectedResult = 19690720;

            for (int noun = 0; noun < 100; noun++)
            {
                for (int verb = 0; verb < 100; verb++)
                {
                    try
                    {
                        int[] copiedProgram = CopyProgram(program);
                        int result = ExecuteProgram(copiedProgram, noun, verb);
                        if (result == expectedResult)
                        {
                            return 100 * noun + verb;
                        }
                    }
                    catch (InvalidOperationException)
                    {
                        // it means that arguments were invalid
                    }
                }
            }

            throw new ArgumentException("Program is invalid");
        }

        private static int ExecuteProgram(int[] program, int noun, int verb)
        {
            program[1] = noun;
            program[2] = verb;

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

        private static int[] CopyProgram(int[] program)
        {
            int[] copiedProgram = new int[program.Length];
            Array.Copy(program, copiedProgram, program.Length);
            return copiedProgram;
        }
    }
}
