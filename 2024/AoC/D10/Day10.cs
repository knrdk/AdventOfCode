namespace AoC.D10
{
    internal class Day10 : ISolver
    {
        private readonly string _inputFile;

        private readonly (int, int)[] _dirs = { (0, 1), (0, -1), (1, 0), (-1, 0) };

        public Day10(string inputFile)
        {
            _inputFile = inputFile;
        }

        public async Task<string> SolvePart1()
        {
            int[][] map = await ReadInput();
            long result = CalculateScore(map, singlePath: true);
            return result.ToString();
        }

        public async Task<string> SolvePart2()
        {
            int[][] map = await ReadInput();
            long result = CalculateScore(map, singlePath: false);
            return result.ToString();
        }

        private long CalculateScore(int[][] map, bool singlePath)
        {
            int rows = map.Length;
            int cols = map[0].Length;

            long result = 0;
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (map[r][c] == 0)
                    {
                        // trailhead
                        result += CalculateTrailScore(map, r, c, singlePath);
                    }
                }
            }

            return result;
        }

        private int CalculateTrailScore(int[][] map, int r, int c, bool singlePath)
        {
            HashSet<(int, int)> visited = new();
            Queue<(int, int)> q = new();
            q.Enqueue((r, c));

            int result = 0;
            while (q.Any())
            {
                (int x, int y) = q.Dequeue();
                if (visited.Contains((x, y)))
                {
                    continue;
                }

                if (singlePath)
                {
                    visited.Add((x, y));
                }

                int value = map[x][y];
                if (value == 9)
                {
                    result++;
                    continue;
                }

                foreach ((int a, int b) in _dirs)
                {
                    if (IsValid(map, x + a, y + b))
                    {
                        if (map[x + a][y + b] == value + 1)
                        {
                            q.Enqueue((x + a, y + b));
                        }
                    }
                }
            }

            return result;
        }

        private bool IsValid(int[][] map, int r, int c)
        {
            int rows = map.Length;
            int cols = map[0].Length;

            bool rowValid = r >= 0 && r < rows;
            bool colValid = c >= 0 && c < cols;

            return rowValid && colValid;
        }

        private async Task<int[][]> ReadInput()
        {
            string[] lines = await File.ReadAllLinesAsync(_inputFile);

            int[][] result = new int[lines.Length][];
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                result[i] = new int[line.Length];
                for (int j = 0; j < line.Length; j++)
                {
                    result[i][j] = line[j] - '0';
                }
            }

            return result;
        }
    }
}