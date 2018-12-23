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
        {
            _processor = new Processor(6);
            _instructionPointerRegister = instructionPointerRegister;
            _commands = commands;
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
