﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace src
{
    class Program
    {
        static void Main(string[] args)
        {
            // read input
            string fileName = args[0];
            string[] inputLines = File.ReadAllLines(fileName);
            string firstLine = inputLines.First();
            IEnumerable<string> commandLines = inputLines.Skip(1);

            // parse input
            int instructionPointerRegister = ParseInstructionPointerLine(firstLine);
            Command[] commands = commandLines.Select(ParseCommandLine).ToArray();

            SolvePart1(instructionPointerRegister, commands);
            SolvePart2(instructionPointerRegister, commands);
        }

        private static void SolvePart1(int instructionPointerRegister, Command[] commands)
        {
            // execute program
            var programExecutor = new ProgramExecutor(instructionPointerRegister, commands);
            programExecutor.ExecuteWholeProgram();

            // print result
            Console.WriteLine($"Part1: {programExecutor}");
        }

        private static void SolvePart2(int instructionPointerRegister, Command[] commands)
        {
            // execute program
            var processor = new Processor(6);
            processor.Registers[0] = 1;
            var programExecutor = new ProgramExecutor(processor, instructionPointerRegister, commands);
            programExecutor.ExecuteWholeProgram();

            // print result
            Console.WriteLine($"Part2: {programExecutor}");
        }


        private static int ParseInstructionPointerLine(string instructionPointerLine)
        {
            string registerNumberAsString = instructionPointerLine.Split(' ').Skip(1).First();
            int instructionPointerRegister = int.Parse(registerNumberAsString);
            return instructionPointerRegister;
        }

        private static Command ParseCommandLine(string commandLine)
        {
            string[] splitted = commandLine.Split(' ');
            string instruction = splitted[0];
            int inputA = int.Parse(splitted[1]);
            int inputB = int.Parse(splitted[2]);
            int output = int.Parse(splitted[3]);
            return new Command(instruction, inputA, inputB, output);
        }
    }
}
