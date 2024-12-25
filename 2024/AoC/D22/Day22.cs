namespace AoC.D22
{
    internal class Day22 : ISolver
    {
        private readonly string _inputFile;

        public Day22(string inputFile)
        {
            _inputFile = inputFile;
        }

        public async Task<string> SolvePart1()
        {
            long[] input = await ReadInput();

            long result = 0;
            foreach (long secret in input)
            {
                long x = secret;
                for (int i = 0; i < 2000; i++)
                {
                    x = EvolveSecret(x);
                }

                result += x;
            }

            return result.ToString();
        }

        public async Task<string> SolvePart2()
        {
            long[] input = await ReadInput();

            Dictionary<(int, int, int, int), long> sum = new();
            for (int i = 0; i < input.Length; i++)
            {
                HashSet<(int, int, int, int)> set = new();
                int[] prices = new int[2001];
                long secret = input[i];

                prices[0] = (int)(secret % 10);
                for (int j = 1; j < 2001; j++)
                {
                    secret = EvolveSecret(secret);
                    prices[j] = (int)(secret % 10);

                    if (j > 3)
                    {
                        int a = prices[j - 3] - prices[j - 4];
                        int b = prices[j - 2] - prices[j - 3];
                        int c = prices[j - 1] - prices[j - 2];
                        int d = prices[j] - prices[j - 1];

                        if (!set.Contains((a, b, c, d)))
                        {
                            set.Add((a, b, c, d));

                            if (!sum.ContainsKey((a, b, c, d)))
                            {
                                sum[(a, b, c, d)] = 0;
                            }

                            sum[(a, b, c, d)] += prices[j];
                        }
                    }
                }
            }

            long max = 0;
            foreach ((int a, int b, int c, int d) in sum.Keys)
            {
                if (sum[(a, b, c, d)] > max)
                {
                    max = sum[(a, b, c, d)];
                }
            }
            return max.ToString();
        }

        long EvolveSecret(long secret)
        {
            long a = secret << 6; // *64
            secret = secret ^ a;
            secret = secret % 16777216;

            long b = secret >> 5; // /32;
            secret = secret ^ b;
            secret = secret % 16777216;

            long c = secret << 11; // *2048
            secret = secret ^ c;
            secret = secret % 16777216;

            return secret;
        }

        private async Task<long[]> ReadInput()
        {
            string[] lines = await File.ReadAllLinesAsync(_inputFile);
            long[] result = new long[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                result[i] = long.Parse(lines[i]);
            }

            return result;
        }
    }
}