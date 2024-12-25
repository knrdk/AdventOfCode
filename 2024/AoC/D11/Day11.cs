namespace AoC.D11
{
    internal class Day11 : ISolver
    {
        private readonly string _inputFile;

        private readonly Dictionary<(long number, int blinks), long> memo = new();

        public Day11(string inputFile)
        {
            _inputFile = inputFile;
        }

        public async Task<string> SolvePart1()
        {
            await Task.CompletedTask;
            long[] input = ReadInput();

            for (int i = 0; i < 25; i++)
            {
                input = BlinkPart1(input);
            }

            long result = input.Length;
            return result.ToString();
        }

        public async Task<string> SolvePart2()
        {
            await Task.CompletedTask;
            long[] input = ReadInput();

            long result = BlinkPart2(input, 75);
            return result.ToString();
        }

        private long[] BlinkPart1(long[] input)
        {
            List<long> output = new();
            foreach (long x in input)
            {
                string xAsString = x.ToString();

                if (x == 0)
                {
                    output.Add(1);
                }
                else if (xAsString.Length % 2 == 0)
                {
                    int newLength = xAsString.Length / 2;
                    string first = xAsString.Substring(0, newLength);
                    string second = xAsString.Substring(newLength, newLength);

                    output.Add(long.Parse(first));
                    output.Add(long.Parse(second));
                }
                else
                {
                    output.Add(2024 * x);
                }
            }

            return output.ToArray();
        }

        private long BlinkPart2(long[] input, int blinks)
        {
            if (blinks == 0)
            {
                return input.Length;
            }

            long result = 0;
            foreach (long x in input)
            {
                if (!memo.ContainsKey((x, blinks)))
                {
                    long[] blinkResult = Blink(x);
                    long current = BlinkPart2(blinkResult, blinks - 1);
                    memo[(x, blinks)] = current;
                }

                result += memo[(x, blinks)];
            }

            return result;
        }

        private long[] Blink(long x)
        {
            string xAsString = x.ToString();

            if (x == 0)
            {
                return new long[] { 1 };
            }
            else if (xAsString.Length % 2 == 0)
            {
                int newLength = xAsString.Length / 2;
                string first = xAsString.Substring(0, newLength);
                string second = xAsString.Substring(newLength, newLength);

                return new long[] { long.Parse(first), long.Parse(second) };
            }
            else
            {
                return new long[] { 2024 * x };
            }
        }

        private long[] ReadInput()
        {
            return _inputFile
                .Split(' ')
                .Select(long.Parse)
                .ToArray();
        }
    }
}