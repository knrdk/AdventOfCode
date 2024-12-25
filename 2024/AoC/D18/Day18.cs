namespace AoC.D18
{
    internal class Day18 : ISolver
    {
        private const int _size = 71; // 71

        private const int _numberOfBytesFallen = 1024; // 1024

        private readonly string _inputFile;

        private readonly (int, int)[] _dirs = { (0, 1), (0, -1), (1, 0), (-1, 0) };

        public Day18(string inputFile)
        {
            _inputFile = inputFile;
        }

        public async Task<string> SolvePart1()
        {
            (int x, int y)[] input = await ReadInput();
            bool[][] board = new bool[_size][];
            for (int i = 0; i < _size; i++)
            {
                board[i] = new bool[_size];
            }

            for (int i = 0; i < input.Length && i < _numberOfBytesFallen; i++)
            {
                (int x, int y) = input[i];
                board[y][x] = true;
            }

            long result = CalculateDistance(board);
            return result.ToString();
        }

        public async Task<string> SolvePart2()
        {
            (int x, int y)[] input = await ReadInput();
            bool[][] board = new bool[_size][];
            for (int i = 0; i < _size; i++)
            {
                board[i] = new bool[_size];
            }

            for (int i = 0; i < input.Length; i++)
            {
                (int x, int y) = input[i];
                board[y][x] = true;

                if (i >= _numberOfBytesFallen && CalculateDistance(board) == -1)
                {
                    return $"{x},{y}";
                }
            }

            return "-";
        }

        private int CalculateDistance(bool[][] board)
        {
            const int INF = _size * _size + 1;
            Dictionary<(int x, int y), int> dist = new();
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    dist[(i, j)] = INF;
                }
            }

            PriorityQueue<(int x, int y), int> pq = new();
            dist[(0, 0)] = 0;
            pq.Enqueue((0, 0), 0);

            while (pq.Count > 0)
            {
                (int x, int y) = pq.Dequeue();
                foreach ((int dx, int dy) in _dirs)
                {
                    int nx = x + dx;
                    int ny = y + dy;
                    if (nx >= 0 && nx < _size && ny >= 0 && ny < _size)
                    {
                        if (!board[ny][nx])
                        {
                            int alt = dist[(x, y)] + 1;
                            if (alt < dist[(nx, ny)])
                            {
                                dist[(nx, ny)] = alt;
                                pq.Enqueue((nx, ny), alt);
                            }
                        }
                    }
                }
            }

            return dist[(_size - 1, _size - 1)] == INF
                ? -1
                : dist[(_size - 1, _size - 1)];
        }

        private async Task<(int x, int y)[]> ReadInput()
        {
            string[] lines = await File.ReadAllLinesAsync(_inputFile);
            (int, int)[] result = new (int, int)[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                string[] splitted = lines[i].Split(',');
                int x = int.Parse(splitted[0]);
                int y = int.Parse(splitted[1]);

                result[i] = (x, y);
            }

            return result;
        }
    }
}