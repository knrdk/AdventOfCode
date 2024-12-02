namespace AoC.D02
{
    internal class Day02 : ISolver
    {
        private readonly string _inputFile;

        public Day02(string inputFile)
        {
            _inputFile = inputFile;
        }

        public async Task<string> SolvePart1()
        {
            List<int[]> first = await ReadInput();
            int result = first.Count(IsReportValid);
            return result.ToString();
        }

        public async Task<string> SolvePart2()
        {
            List<int[]> first = await ReadInput();
            int result = first.Count(IsReportValidWithDampener);
            return result.ToString();
        }

        private bool IsReportValid(int[] report)
        {
            if (report.Length < 2)
            {
                return true;
            }

            bool increasing = report[1] > report[0];
            for (int i = 1; i < report.Length; i++)
            {
                int diff = increasing
                    ? report[i] - report[i - 1]
                    : report[i - 1] - report[i];

                if (diff < 1 || diff > 3)
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsReportValidWithDampener(int[] report)
        {
            if(IsReportValid(report)){
                return true;
            }

            for (int i = 0; i < report.Length; i++)
            {
                int[] demp = report.Where((x, index) => index != i).ToArray();
                if(IsReportValid(demp)){
                    return true;
                }
            }

            return false;
        }

        private async Task<List<int[]>> ReadInput()
        {
            string[] lines = await File.ReadAllLinesAsync(_inputFile);
            List<int[]> result = new();
            foreach (string line in lines)
            {
                int[] numbersToParse = line
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                result.Add(numbersToParse);
            }

            return result;
        }
    }
}