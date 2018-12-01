using System;
using System.Globalization;
using System.Linq;
using LanguageExt;
using static LanguageExt.Option<int>;

namespace src
{
    public class Solver
    {
        public static Option<int> Solve(Option<int>[] frequencies)
        {
            HashSet<int> partialFrequencies = new HashSet<int>();
            partialFrequencies = partialFrequencies.Add(0);
            int finalFrequency = 0;
            var indifiniteFrequencies = InfiniteRepeat(frequencies);
            return Solve(indifiniteFrequencies.First(), indifiniteFrequencies.Skip(1), finalFrequency, partialFrequencies);
        }

        private static Option<int> Solve(
            Option<int> currentFrequency,
            System.Collections.Generic.IEnumerable<Option<int>> frequencies,
            int finalFrequency,
            HashSet<int> partialFrequencies
        )
        {
            return currentFrequency.Bind(x => Solve(x, frequencies, finalFrequency, partialFrequencies));
        }

        private static Option<int> Solve(
            int currentFrequency,
            System.Collections.Generic.IEnumerable<Option<int>> frequencies,
            int finalFrequency,
            HashSet<int> partialFrequencies
        )
        {
            int newFinalFrequency = finalFrequency + currentFrequency;
            return partialFrequencies.Contains(newFinalFrequency) ?
                newFinalFrequency :
                Solve(frequencies.First(), frequencies.Skip(1), newFinalFrequency, partialFrequencies.Add(newFinalFrequency));
        }

        private static System.Collections.Generic.IEnumerable<T> InfiniteRepeat<T>(T[] items)
        {
            int size = items.Count();
            int i = 0;
            while (true)
            {
                yield return items[i];
                i++;
                if (i == size)
                {
                    i = 0;
                }
            }
        }
    }
}
