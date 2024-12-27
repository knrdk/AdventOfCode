using System.Runtime.Intrinsics.X86;

namespace AoC.D14
{
    internal class Day14 : ISolver
    {
        private const int ROWS = 103;
        private const int COLS = 101;
        private const int SECONDS = 100;

        private readonly string _inputFile;

        public Day14(string inputFile)
        {
            _inputFile = inputFile;
        }

        public async Task<string> SolvePart1()
        {
            (int x, int y, int mx, int my)[] input = await ReadInput();

            int[][] map = new int[ROWS][];
            for (int i = 0; i < ROWS; i++)
            {
                map[i] = new int[COLS];
            }

            foreach ((int x, int y, int mx, int my) in input)
            {
                int nX = x;
                int nY = y;
                for (int i = 0; i < SECONDS; i++)
                {
                    nX = (COLS + nX + mx) % COLS;
                    nY = (ROWS + nY + my) % ROWS;
                }

                map[nY][nX]++;
            }

            int q1 = 0, q2 = 0, q3 = 0, q4 = 0;
            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLS; j++)
                {
                    if (i < ROWS / 2)
                    {
                        if (j < COLS / 2)
                        {
                            q1 += map[i][j];
                        }
                        else if (j > COLS / 2)
                        {
                            q2 += map[i][j];
                        }
                    }
                    else if (i > ROWS / 2)
                    {
                        if (j < COLS / 2)
                        {
                            q3 += map[i][j];
                        }
                        else if (j > COLS / 2)
                        {
                            q4 += map[i][j];
                        }
                    }
                }
            }

            long result = ((long)q1) * q2 * q3 * q4;
            return result.ToString();
        }

        public async Task<string> SolvePart2()
        {
            (int x, int y, int mx, int my)[] input = await ReadInput();

            for (int s = 0; s < 10_403; s++)
            {
                int[][] map = new int[ROWS][];
                for (int i = 0; i < ROWS; i++)
                {
                    map[i] = new int[COLS];
                }

                foreach ((int x, int y, int mx, int my) in input)
                {
                    int nX = x;
                    int nY = y;
                    for (int i = 0; i < s; i++)
                    {
                        nX = (COLS + nX + mx) % COLS;
                        nY = (ROWS + nY + my) % ROWS;
                    }

                    map[nY][nX]++;
                }

                if (s % 101 == 83 || s % 103 == 60) // there is a cycle in the output
                {
                    Console.WriteLine($"Second: {s}");
                    for (int i = 0; i < ROWS; i++)
                    {
                        Console.WriteLine(string.Join("", map[i].Select(x => x == 0 ? ' ' : '#')));
                    }

                    Console.WriteLine("----");
                }
            }

            return "Check output";
        }

        private async Task<(int x, int y, int mx, int my)[]> ReadInput()
        {
            string[] lines = await File.ReadAllLinesAsync(_inputFile);
            (int x, int y, int mx, int my)[] result = new (int, int, int, int)[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                string[] splitted = lines[i].Split(' ');
                (int x, int y) = Parse(splitted[0]);
                (int mx, int my) = Parse(splitted[1]);

                result[i] = (x, y, mx, my);
            }

            return result;
        }

        private (int, int) Parse(string segment) // p=0,4
        {
            segment = segment.Substring(2);
            string[] splitted = segment.Split(',');
            int a = int.Parse(splitted[0]);
            int b = int.Parse(splitted[1]);
            return (a, b);
        }
    }
}