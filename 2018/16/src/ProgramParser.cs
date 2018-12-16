using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace src
{
    public class ProgramParser
    {
        public static IEnumerable<int[]> GetInput(string fileName)
        {
            return File.ReadAllLines(fileName).Select(ParseInputLine);
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
