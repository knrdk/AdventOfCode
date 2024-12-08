using System.Text;

namespace AoC.D08
{
    internal class Day08 : ISolver
    {
        private readonly string _inputFile;

        public Day08(string inputFile)
        {
            _inputFile = inputFile;
        }

        public async Task<string> SolvePart1()
        {
            (Dictionary<char, List<(int x, int y)>> input, int rows, int cols) = await ReadInput();

            HashSet<(int x, int y)> set = new();
            foreach (char c in input.Keys)
            {
                List<(int x, int y)> list = input[c];
                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = i + 1; j < list.Count; j++)
                    {
                        int mX = list[i].x - list[j].x;
                        int mY = list[i].y - list[j].y;

                        int firstX = list[i].x + mX;
                        int firstY = list[i].y + mY;

                        if (firstX >= 0 && firstX < cols && firstY >= 0 && firstY < rows)
                        {
                            set.Add((firstX, firstY));
                        }

                        int secondX = list[j].x - mX;
                        int secondY = list[j].y - mY;

                        if (secondX >= 0 && secondX < cols && secondY >= 0 && secondY < rows)
                        {
                            set.Add((secondX, secondY));
                        }
                    }
                }
            }

            int result = set.Count;
            return result.ToString();
        }

        public async Task<string> SolvePart2()
        {
            (Dictionary<char, List<(int x, int y)>> input, int rows, int cols) = await ReadInput();

            HashSet<(int x, int y)> set = new();
            foreach (char c in input.Keys)
            {
                List<(int x, int y)> list = input[c];
                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = i + 1; j < list.Count; j++)
                    {
                        set.Add((list[i].x, list[i].y));
                        set.Add((list[j].x, list[j].y));

                        int mX = list[i].x - list[j].x;
                        int mY = list[i].y - list[j].y;
                        int gcd = GCD(Math.Abs(mX), Math.Abs(mY));
                        mX /= gcd;
                        mY /= gcd;

                        int x = list[i].x + mX;
                        int y = list[i].y + mY;
                        do
                        {
                            if (x >= 0 && x < cols && y >= 0 && y < rows)
                            {
                                set.Add((x, y));
                            }

                            x += mX;
                            y += mY;
                        } while (x >= 0 && x < cols);

                        x = list[i].x - mX;
                        y = list[i].y - mY;
                        do
                        {
                            if (x >= 0 && x < cols && y >= 0 && y < rows)
                            {
                                set.Add((x, y));
                            }

                            x -= mX;
                            y -= mY;
                        } while (x >= 0 && x < cols);
                    }
                }
            }

            int result = set.Count;
            return result.ToString();
        }

        private static int GCD(int a, int b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }

            return a | b;
        }

        private async Task<(Dictionary<char, List<(int x, int y)>> input, int rows, int cols)> ReadInput()
        {
            string[] lines = await File.ReadAllLinesAsync(_inputFile);
            int rows = lines.Length;
            int cols = lines[0].Length;

            Dictionary<char, List<(int, int)>> result = new();
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    char c = lines[i][j];
                    if (c != '.')
                    {
                        if (!result.ContainsKey(c))
                        {
                            result[c] = [];
                        }

                        result[c].Add((j, i));
                    }
                }
            }
            return (result, rows, cols);
        }
    }
}