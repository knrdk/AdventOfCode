using System;
using System.IO;
using System.Linq;
using System.Text;

namespace src
{
    class Program
    {
        static void Main(string[] args)
        {
            const int BigSmallLetterAsciiDifference = 32;
            string filename = args[0];
            StringBuilder polymer = File.ReadAllLines(filename).Aggregate(new StringBuilder(), (sb, x) => sb.Append(x));
            
            int i = 0;
            while (i < polymer.Length - 1)
            {                
                int diff = Math.Abs(polymer[i] - polymer[i + 1]);
                if (diff == BigSmallLetterAsciiDifference)
                {
                    polymer.Remove(i, 2);
                    i = Math.Max(0, i - 1);
                }
                else
                {
                    i++;
                }
            }

            int polymerLength = polymer.Length;
            Console.WriteLine($"Polymer length: {polymerLength}");
        }
    }
}
