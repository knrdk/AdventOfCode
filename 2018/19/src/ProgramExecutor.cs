using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace src
{
    class ProgramExecutor
    {
        private Processor _processor;
        private Command[] _commands;
        private int _instructionPointerRegister;

        private long CommandToExecute => _processor.Registers[_instructionPointerRegister];

        public bool IsEnded => CommandToExecute >= _commands.Length;

        public ProgramExecutor(int instructionPointerRegister, Command[] commands)
        : this(new Processor(6), instructionPointerRegister, commands)
        { }

        public ProgramExecutor(Processor processor, int instructionPointerRegister, Command[] commands)
        {
            _processor = processor;
            _instructionPointerRegister = instructionPointerRegister;
            _commands = commands;
        }

        public void ExecuteWholeProgram()
        {
            while (!IsEnded)
            {
                ExecuteNextCommand();
            }
        }

        public void ExecuteNextCommand()
        {
            _processor.Execute(_commands[CommandToExecute]);
            _processor.Registers[_instructionPointerRegister]++;
        }

        public override string ToString()
        {
            return _processor.ToString();
        }
    }
}
