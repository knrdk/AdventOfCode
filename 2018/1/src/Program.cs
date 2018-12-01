using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LanguageExt;
using static LanguageExt.Option<int>;
using static System.Console;

namespace src
{
    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<string> input = File.ReadLines(args[0]);
            Option<int>[] frequenciesChanges = Parser.Parse(input).ToArray();

            ShowFinalFrequency(frequenciesChanges);
            ShowFirstRepeatedFrequency(frequenciesChanges);
        }

        private static void ShowFinalFrequency(Option<int>[] frequenciesChanges)
        {
            Option<int> finalFrequency = frequenciesChanges.Fold(
                Some(0),
                (sum, current) => from s in sum from c in current select s + c
            );

            finalFrequency.IfSome(value => WriteLine(value));
        }

        private static void ShowFirstRepeatedFrequency(Option<int>[] frequenciesChanges)
        {
            Option<int> repetedFrequency = Solver.Solve(frequenciesChanges);
            repetedFrequency.IfSome(value => WriteLine(value));
        }
    }
}
