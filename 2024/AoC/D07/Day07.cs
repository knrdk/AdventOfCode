namespace AoC.D07
{
    internal class Day07 : ISolver
    {
        private readonly string _inputFile;

        public Day07(string inputFile)
        {
            _inputFile = inputFile;
        }

        public async Task<string> SolvePart1()
        {
            List<(long expectedResult, long[] numbers)> input = await ReadInput();

            long result = 0;
            foreach ((long expectedResult, long[] numbers) in input)
            {
                if (IsValid(expectedResult, numbers))
                {
                    result += expectedResult;
                }
            }

            return result.ToString();
        }

        public async Task<string> SolvePart2()
        {
            List<(long expectedResult, long[] numbers)> input = await ReadInput();

            long result = 0;
            foreach ((long expectedResult, long[] numbers) in input)
            {
                if (IsValid2(expectedResult, numbers))
                {
                    result += expectedResult;
                }
            }

            return result.ToString();
        }

        private bool IsValid(long expectedResult, long[] numbers)
        {
            if (numbers.Length == 1)
            {
                long num = numbers[0];
                return expectedResult == num;
            }
            else
            {
                long first = numbers[0];
                long second = numbers[1];

                long sum = first + second;
                long[] firstNumbers = new[] { sum }.Concat(numbers.Skip(2)).ToArray();
                if (IsValid(expectedResult, firstNumbers))
                {
                    return true;
                }

                long product = first * second;
                long[] secondNumbers = new[] { product }.Concat(numbers.Skip(2)).ToArray();
                if (IsValid(expectedResult, secondNumbers))
                {
                    return true;
                }

                return false;
            }
        }

        private bool IsValid2(long expectedResult, long[] numbers)
        {
            if (numbers.Length == 1)
            {
                long num = numbers[0];
                return expectedResult == num;
            }
            else
            {
                long first = numbers[0];
                long second = numbers[1];

                long sum = first + second;
                long[] firstNumbers = new[] { sum }.Concat(numbers.Skip(2)).ToArray();
                if (IsValid2(expectedResult, firstNumbers))
                {
                    return true;
                }

                long product = first * second;
                long[] secondNumbers = new[] { product }.Concat(numbers.Skip(2)).ToArray();
                if (IsValid2(expectedResult, secondNumbers))
                {
                    return true;
                }

                long concatenation = long.Parse(first.ToString() + second.ToString());
                long[] thirdNumbers = new[] { concatenation }.Concat(numbers.Skip(2)).ToArray();
                if (IsValid2(expectedResult, thirdNumbers))
                {
                    return true;
                }

                return false;
            }
        }

        private async Task<List<(long result, long[] numbers)>> ReadInput()
        {
            string[] lines = await File.ReadAllLinesAsync(_inputFile);
            List<(long expectedResult, long[] numbers)> result = new();
            foreach (string line in lines)
            {
                string[] splitted = line.Split(':', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                long expectedResult = long.Parse(splitted[0]);

                long[] numbers = splitted[1]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(x => long.Parse(x))
                .ToArray();

                result.Add((expectedResult, numbers));
            }
            return result;
        }
    }
}