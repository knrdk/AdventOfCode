using System;
using System.Collections.Generic;

namespace src
{
    class Processor
    {
        private Dictionary<string, Action<int, int, int>> _instructions;
        public int[] Registers { get; set; }
        public IEnumerable<string> AvailableOperands => _instructions.Keys;

        public Processor()
        {
            Registers = new int[4];
            InitializeInstructions();
        }

        public void Execute(string operation, int inputA, int inputB, int output)
        {
            Action<int, int, int> actionToExecute = _instructions[operation];
            actionToExecute(inputA, inputB, output);
        }

        public void InitializeInstructions()
        {
            _instructions = new Dictionary<string, Action<int, int, int>>
            {
                ["addr"] = (a, b, c) => { Registers[c] = Registers[a] + Registers[b]; },
                ["addi"] = (a, b, c) => { Registers[c] = Registers[a] + b; },
                ["mulr"] = (a, b, c) => { Registers[c] = Registers[a] * Registers[b]; },
                ["muli"] = (a, b, c) => { Registers[c] = Registers[a] * b; },
                ["banr"] = (a, b, c) => { Registers[c] = Registers[a] & Registers[b]; },
                ["bani"] = (a, b, c) => { Registers[c] = Registers[a] & b; },
                ["borr"] = (a, b, c) => { Registers[c] = Registers[a] | Registers[b]; },
                ["bori"] = (a, b, c) => { Registers[c] = Registers[a] | b; },
                ["setr"] = (a, b, c) => { Registers[c] = Registers[a]; },
                ["seti"] = (a, b, c) => { Registers[c] = a; },
                ["gtir"] = (a, b, c) => { Registers[c] = a > Registers[b] ? 1 : 0; },
                ["gtri"] = (a, b, c) => { Registers[c] = Registers[a] > b ? 1 : 0; },
                ["gtrr"] = (a, b, c) => { Registers[c] = Registers[a] > Registers[b] ? 1 : 0; },
                ["eqir"] = (a, b, c) => { Registers[c] = a == Registers[b] ? 1 : 0; },
                ["eqri"] = (a, b, c) => { Registers[c] = Registers[a] == b ? 1 : 0; },
                ["eqrr"] = (a, b, c) => { Registers[c] = Registers[a] == Registers[b] ? 1 : 0; }
            };
        }
    }
}
