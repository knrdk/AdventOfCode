using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace src
{
    public class SamplesParser
    {
        private static Regex registerRegex = new Regex(@"^.*\[(.*)]$", RegexOptions.Compiled);

        public static IEnumerable<(int[] start, int[] input, int[] expected)> GetInput(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);
            for (int i = 0; i + 2 < lines.Length; i += 4)
            {
                string beforeRegister = lines[i];
                string instruction = lines[i + 1];
                string afterRegister = lines[i + 2];

                int[] start = ParseRegisterLine(beforeRegister);
                int[] expected = ParseRegisterLine(afterRegister);
                int[] input = ParseInputLine(instruction);

                yield return (start, input, expected);
            }
        }

        private static int[] ParseRegisterLine(string registerLine)
        {
            Match match = registerRegex.Match(registerLine);
            string commaSeparatedValues = match.Groups[1].Value;
            return commaSeparatedValues
                .Split(',')
                .Select(x => int.Parse(x))
                .ToArray();
        }

        private static int[] ParseInputLine(string inputLine)
        {
            return inputLine
                .Split(' ')
                .Select(x => int.Parse(x))
                .ToArray();
        }
    }
}
