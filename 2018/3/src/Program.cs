using System;
using System.IO;
using System.Linq;

namespace src
{
    class Program
    {
        private static int numberOfConflicts = 0;
        private static short[,] fabric = new short[1000, 1000];

        static void Main(string[] args)
        {
            string fileName = args[0];
            string[] claims = File.ReadAllLines(fileName);

            foreach (ClaimDto claim in claims.Select(ClaimParser.Parse))
            {
                ProcessClaim(claim);
            }

            Console.WriteLine(numberOfConflicts);
        }

        private static void ProcessClaim(ClaimDto claim)
        {
            for (int i = claim.LeftEdgePosition; i < claim.LeftEdgePosition + claim.Width; i++)
            {
                for (int j = claim.TopEdgePosition; j < claim.TopEdgePosition + claim.Height; j++)
                {
                    if (fabric[i, j] == 0)
                    {
                        fabric[i, j] = 1;
                    }
                    else if (fabric[i, j] == 1)
                    {
                        numberOfConflicts++;
                        fabric[i, j] = 2;
                    }
                }
            }
        }
    }
}
