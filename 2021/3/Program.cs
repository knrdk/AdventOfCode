using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AoC3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            const string inputFile = "input.txt";

            string[] input = File.ReadAllLines(inputFile);

            int height = input.Length;
            int width = input[0].Length;


            int[] counts = new int[width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    char current = input[i][j];
                    counts[j] += current == '1' ? 1 : 0;
                }
            }

            bool[] commonBits = new bool[width];
            int gammaRate = 0;
            int epsilonRate = 0;
            for (int j = 0; j < width; j++)
            {
                commonBits[j] = counts[j] > height - counts[j];

                gammaRate *= 2;
                epsilonRate *= 2;
                if (commonBits[j])
                {
                    gammaRate++;
                }
                else
                {
                    epsilonRate++;
                }
            }

            int consumption = gammaRate * epsilonRate;

            Console.WriteLine($"Gamma: {gammaRate}");
            Console.WriteLine($"Epsilon: {epsilonRate}");
            Console.WriteLine($"Consumption: {consumption}");

            // -- Part 2 --
            string[] oxygenGeneratorRatingCandidates = input;
            for (int j = 0; j < width; j++)
            {
                if (oxygenGeneratorRatingCandidates.Length == 1)
                {
                    break;
                }

                int countsOfOne = 0;
                for (int i = 0; i < oxygenGeneratorRatingCandidates.Length; i++)
                {
                    char current = oxygenGeneratorRatingCandidates[i][j];
                    countsOfOne += current == '1' ? 1 : 0;
                }

                char expectedBit = countsOfOne >= oxygenGeneratorRatingCandidates.Length - countsOfOne ? '1' : '0';

                oxygenGeneratorRatingCandidates = oxygenGeneratorRatingCandidates.Where(x => x[j] == expectedBit).ToArray();
            }

            int oxygenGeneratorRating = Convert.ToInt32(oxygenGeneratorRatingCandidates.Single(), 2);
            Console.WriteLine($"OxygenGeneratorRating: {oxygenGeneratorRating}");

            string[] co2ScrubberCandidates = input;
            for (int j = 0; j < width; j++)
            {
                if (co2ScrubberCandidates.Length == 1)
                {
                    break;
                }

                int countsOfOne = 0;
                for (int i = 0; i < co2ScrubberCandidates.Length; i++)
                {
                    char current = co2ScrubberCandidates[i][j];
                    countsOfOne += current == '1' ? 1 : 0;
                }

                char expectedBit = countsOfOne >= co2ScrubberCandidates.Length - countsOfOne ? '0' : '1';

                co2ScrubberCandidates = co2ScrubberCandidates.Where(x => x[j] == expectedBit).ToArray();
            }

            int co2ScrubberRating = Convert.ToInt32(co2ScrubberCandidates.Single(), 2);
            Console.WriteLine($"CO2ScrubberRating: {co2ScrubberRating}");

            int lifeSupportRating = oxygenGeneratorRating * co2ScrubberRating;
            Console.WriteLine($"lifeSupportRating: {lifeSupportRating}");
        }
    }
}