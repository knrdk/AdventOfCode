using System;

namespace src
{
    class Command
    {
        public string Instruction { get; }
        public int InputA { get; }
        public int InputB { get; }
        public int Output { get; }

        public Command(string instruction, int inputA, int inputB, int output)
        {
            Instruction = instruction;
            InputA = inputA;
            InputB = inputB;
            Output = output;
        }
    }
}
