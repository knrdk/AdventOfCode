using System;
using System.Collections.Generic;

namespace src
{
    class Processor
    {
        private Dictionary<string, Action<long, long, long>> _instructions;
        public long[] Registers { get; set; }
        public IEnumerable<string> AvailableOperands => _instructions.Keys;

        public Processor(int numberOfRegisters)
        {
            Registers = new long[numberOfRegisters];
            InitializeInstructions();
        }

        public void Execute(Command command)
        {
            Execute(command.Instruction, command.InputA, command.InputB, command.Output);
        }

        public void Execute(string operation, int inputA, int inputB, int output)
        {
            Action<long, long, long> actionToExecute = _instructions[operation];
            actionToExecute(inputA, inputB, output);
        }

        public void InitializeInstructions()
        {
            _instructions = new Dictionary<string, Action<long, long, long>>
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
                ["eqrr"] = (a, b, c) => { Registers[c] = Registers[a] == Registers[b] ? 1 : 0; },
                ["NONE"] = (a, b, c) => { },
                ["SPEC"] = (a, b, c) =>
                {
                    long r4 = Registers[4];
                    long r2 = Registers[2];
                    if (r4 % r2 == 0)
                    {
                        Registers[0] += Registers[2];
                    }
                    Registers[5] = Registers[4] + 1;
                },
            };
        }

        public override string ToString()
        {
            return $"[{string.Join(',', Registers)}]";
        }
    }
}
