using System;
using System.Collections.Generic;
using System.Linq;

namespace src
{
    class Program
    {
        static void Main(string[] args)
        {
            string samplesFileName = args[0];
            IEnumerable<(int[] startRegisters, int[] input, int[] expectedRegister)> inputs = SamplesParser.GetInput(samplesFileName);


            Processor proc = new Processor();
            int part1Result = 0;
            foreach ((int[] startRegisters, int[] input, int[] expectedRegister) in inputs)
            {
                string[] matchingOperands = GetMatchingOperands(proc, startRegisters, input, expectedRegister).ToArray();
                part1Result += matchingOperands.Length >= 3 ? 1 : 0;
            }
            Console.WriteLine(part1Result);
        }

        private static IEnumerable<string> GetMatchingOperands(Processor proc, int[] startRegisters, int[] input, int[] expectedOutput)
        {
            foreach (string operand in proc.AvailableOperands)
            {
                startRegisters.CopyTo(proc.Registers, 0);

                proc.Execute(operand, input[1], input[2], input[3]);

                if (proc.Registers.SequenceEqual(expectedOutput))
                {
                    yield return operand;
                }
            }
        }
    }
}
