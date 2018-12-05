using System;
using System.IO;
using System.Linq;
using System.Text;

namespace src
{
    class Program
    {
        private const int BigSmallLetterAsciiDifference = 32;

        static void Main(string[] args)
        {
            string filename = args[0];
            StringBuilder polymer = File.ReadAllLines(filename).Aggregate(new StringBuilder(), (sb, x) => sb.Append(x));

            // part 1
            StringBuilder shortenedPolymer = ShortenPolymer(polymer);
            int polymerLength = shortenedPolymer.Length;
            Console.WriteLine($"Polymer length: {polymerLength}");

            // part 2
            String shortenedPolymerText = shortenedPolymer.ToString();
            int shortestPolymerLength = CalculateShortestPolymerLength(shortenedPolymerText);
            Console.WriteLine($"Shortest polymer: {shortestPolymerLength}");
        }

        private static StringBuilder ShortenPolymer(StringBuilder polymer)
        {
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

            return polymer;
        }

        private static int CalculateShortestPolymerLength(string polymer)
        {
            int shortestPolymerLength = int.MaxValue;
            for (char bigCharacter = (char)65; bigCharacter < 91; bigCharacter++)
            {
                char smallCharacter = (char)(bigCharacter + BigSmallLetterAsciiDifference);
                StringBuilder polymerToTest = new StringBuilder(polymer);
                polymerToTest.Replace(smallCharacter.ToString(), string.Empty);
                polymerToTest.Replace(bigCharacter.ToString(), string.Empty);
                StringBuilder reducedPolymer = ShortenPolymer(polymerToTest);
                shortestPolymerLength = Math.Min(shortestPolymerLength, reducedPolymer.Length);
            }
            return shortestPolymerLength;
        }
    }
}
