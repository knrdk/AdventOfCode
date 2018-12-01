using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace src
{
    public class Solver
    {
        public static int Solve(int[] frequencies)
        {
            int finalFrequency = 0;

            var partialFrequencies = new HashSet<int>();
            partialFrequencies.Add(0);
            foreach (int frequency in InfiniteRepeat(frequencies))
            {
                finalFrequency += frequency;
                if (partialFrequencies != null)
                {
                    if (partialFrequencies.Contains(finalFrequency))
                    {
                        // repetition detected
                        return finalFrequency;
                    }
                    else
                    {
                        partialFrequencies.Add(finalFrequency);
                    }
                }
            }
            throw new InvalidOperationException("It should not get here");
        }

        private static IEnumerable<T> InfiniteRepeat<T>(T[] items){
            int size = items.Count();
            int i=0;
            while(true){
                yield return items[i];
                i++;
                if(i == size){
                    i = 0;
                }
            } 
        }
    }
}
