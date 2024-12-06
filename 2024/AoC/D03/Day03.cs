using System.Text.RegularExpressions;

namespace AoC.D03
{
    internal class Day03 : ISolver
    {
        private readonly string _inputFile;

        public Day03(string inputFile)
        {
            _inputFile = inputFile;
        }

        public async Task<string> SolvePart1()
        {
            string input = await ReadInput();
            var regex = new Regex(@"(mul\(\d+,\d+\))");

            int result = 0;
            foreach (object? match in regex.Matches(input))
            {
                string matchAsString = match.ToString();
                matchAsString = matchAsString.Remove(0, 4); // remove "mul("
                matchAsString = matchAsString.Substring(0, matchAsString.Length - 1); // remove ")"
                string[] values = matchAsString.Split(',');
                int x = Int32.Parse(values[0]);
                int y = Int32.Parse(values[1]);

                int z = x * y;
                Console.WriteLine(z);
                result += z;
            }
            return result.ToString();
        }

        public async Task<string> SolvePart2()
        {
            string input = await ReadInput();
            var regex = new Regex(@"(mul\(\d+,\d+\))|(do\(\))|(don't\(\))");

            int result = 0;
            bool include = true;
            foreach (object? match in regex.Matches(input))
            {
                string matchAsString = match.ToString();

                if (matchAsString.StartsWith("do("))
                {
                    include = true;
                }
                else if (matchAsString.StartsWith("don't("))
                {
                    include = false;
                }
                else
                {
                    matchAsString = matchAsString.Remove(0, 4); // remove "mul("
                    matchAsString = matchAsString.Substring(0, matchAsString.Length - 1); // remove ")"
                    string[] values = matchAsString.Split(',');
                    int x = Int32.Parse(values[0]);
                    int y = Int32.Parse(values[1]);

                    int z = x * y;
                    if (include)
                    {
                        result += z;
                    }
                }
            }
            return result.ToString();
        }

        private Task<string> ReadInput()
        {
            return File.ReadAllTextAsync(_inputFile);
        }
    }
}