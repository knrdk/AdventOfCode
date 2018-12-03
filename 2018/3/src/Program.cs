using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace src
{
    class Program
    {
        private static int numberOfConflicts = 0;
        private static int[,] fabric = new int[1000, 1000];
        private static HashSet<int> nonOverlappingIds = new HashSet<int>();

        static void Main(string[] args)
        {
            string fileName = args[0];
            string[] claims = File.ReadAllLines(fileName);

            foreach (ClaimDto claim in claims.Select(ClaimParser.Parse))
            {
                ProcessClaim(claim);
            }

            Console.WriteLine($"Number of conflicts: {numberOfConflicts}");
            Console.WriteLine($"Non overlapping claim: {nonOverlappingIds.Single()}");
        }

        private static void ProcessClaim(ClaimDto claim)
        {
            nonOverlappingIds.Add(claim.Id);
            for (int i = claim.LeftEdgePosition; i < claim.LeftEdgePosition + claim.Width; i++)
            {
                for (int j = claim.TopEdgePosition; j < claim.TopEdgePosition + claim.Height; j++)
                {
                    int currentClaim = fabric[i, j];
                    if (currentClaim == 0)
                    {
                        fabric[i, j] = claim.Id;
                    }
                    else if (currentClaim > 0)
                    {
                        numberOfConflicts++;
                        nonOverlappingIds.Remove(claim.Id);
                        nonOverlappingIds.Remove(currentClaim);
                        fabric[i, j] *= -1;
                    }
                    else
                    {
                        nonOverlappingIds.Remove(claim.Id);
                    }
                }
            }
        }
    }
}
