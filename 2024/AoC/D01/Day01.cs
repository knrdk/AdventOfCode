namespace AoC.D01
{
    internal class Day01 : ISolver
    {
        private readonly string _inputFile;

        public Day01(string inputFile)
        {
            _inputFile = inputFile;
        }

        public async Task<string> SolvePart1()
        {
            (var first, var second) = await ReadInput();

            first.Sort();
            second.Sort();

            long result = first.Zip(second, (a, b) => Math.Abs(a - b)).Sum();
            return result.ToString();
        }

        public async Task<string> SolvePart2()
        {
            (var first, var second) = await ReadInput();

            Dictionary<int, int> histogram = CreateHistogram(second);
            long result = 0;
            foreach(int num in first){
                int count = histogram.ContainsKey(num) ? histogram[num] : 0;
                result += num * count;
            }

            return result.ToString();
        }

        private Dictionary<int, int> CreateHistogram(List<int> nums){
            Dictionary<int, int> result = new();
            foreach(int num in nums){
                if(!result.ContainsKey(num)){
                    result[num] = 0;
                }

                result[num]++;
            }
            return result;
        }

        private async Task<(List<int> first, List<int> second)> ReadInput()
        {
            string[] lines = await File.ReadAllLinesAsync(_inputFile);
            List<int> first = new();
            List<int> second = new();
            foreach (string line in lines)
            {
                string[] numbersToParse = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                int num1 = int.Parse(numbersToParse[0]);
                int num2 = int.Parse(numbersToParse[1]);
                first.Add(num1);
                second.Add(num2);
            }

            return (first, second);
        }
    }
}