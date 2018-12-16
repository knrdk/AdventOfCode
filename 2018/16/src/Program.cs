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
            string programFileName = args[1];
            IEnumerable<(int[] startRegisters, int[] input, int[] expectedRegister)> inputs = SamplesParser.GetInput(samplesFileName);

            var operandNumberToNamesSetDictionary = new Dictionary<int, HashSet<string>>();
            Processor proc = new Processor();
            int part1Result = 0;
            foreach ((int[] startRegisters, int[] input, int[] expectedRegister) in inputs)
            {
                HashSet<string> matchingOperands = GetMatchingOperands(proc, startRegisters, input, expectedRegister).ToHashSet();
                part1Result += matchingOperands.Count() >= 3 ? 1 : 0;

                int operand = input[0];
                if (operandNumberToNamesSetDictionary.ContainsKey(operand))
                {
                    operandNumberToNamesSetDictionary[operand].IntersectWith(matchingOperands);
                }
                else
                {
                    operandNumberToNamesSetDictionary[operand] = matchingOperands;
                }
            }
            Console.WriteLine(part1Result);

            // PART 2
            var operandToInstructionNameMap = CalculateOperandNames(operandNumberToNamesSetDictionary);
            Processor proc2 = new Processor();
            foreach (int[] instruction in ProgramParser.GetInput(programFileName))
            {
                int operand = instruction[0];
                proc2.Execute(operandToInstructionNameMap[operand], instruction[1], instruction[2], instruction[3]);
            }
            Console.WriteLine(proc2.Registers[0]);
        }

        private static Dictionary<int, string> CalculateOperandNames(Dictionary<int, HashSet<string>> operandNumberToNamesSetDictionary)
        {
            var operandToInstructionNameMap = new Dictionary<int, string>();
            while (operandNumberToNamesSetDictionary.Any())
            {
                var operandWithOneValue = operandNumberToNamesSetDictionary.Where(x => x.Value.Count() == 1).First();
                int operand = operandWithOneValue.Key;
                string name = operandWithOneValue.Value.Single();
                operandToInstructionNameMap[operand] = name;
                operandNumberToNamesSetDictionary.Remove(operand);

                foreach (var set in operandNumberToNamesSetDictionary.Values)
                {
                    set.Remove(name);
                }
            }
            return operandToInstructionNameMap;
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
