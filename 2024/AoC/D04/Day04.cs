using System.Text.RegularExpressions;

namespace AoC.D04
{
    internal class Day04 : ISolver
    {
        private readonly string _inputFile;

        public Day04(string inputFile)
        {
            _inputFile = inputFile;
        }

        public async Task<string> SolvePart1()
        {
            char[][] input = await ReadInput();

            IEnumerable<string> lines = EnumerateAllLines(input);
            int result = 0;
            foreach (string line in lines)
            {
                int xmas = Regex.Matches(line, "XMAS").Count;
                int samx = Regex.Matches(line, "SAMX").Count;

                result += xmas + samx;
            }

            return result.ToString();
        }

        public async Task<string> SolvePart2()
        {
            char[][] input = await ReadInput();
            int ROWS = input.Length;
            int COLS = input[0].Length;

            bool IsMOrS(int row, int col){
                return input[row][col] == 'M' || input[row][col] == 'S';
            }

            bool IsMatch(int row, int col)
            {
                if(input[row+1][col+1] != 'A'){
                    return false;
                }

                if(!IsMOrS(row, col) || !IsMOrS(row+2, col+2)){
                    return false;
                }

                if(input[row][col] == input[row+2][col+2]){
                    return false;
                }

                if(!IsMOrS(row, col+2) || !IsMOrS(row+2, col)){
                    return false;
                }

                if(input[row][col+2] == input[row+2][col]){
                    return false;
                }

                return true;
            }

            int result = 0;
            for (int i = 0; i < ROWS - 2; i++)
            {
                for (int j = 0; j < COLS - 2; j++)
                {
                    if (IsMatch(i, j))
                    {
                        result++;
                    }
                }
            }

            return result.ToString();
        }

        private IEnumerable<string> EnumerateAllLines(char[][] input)
        {
            return EnumerableHorizontal(input)
                .Concat(EnumerableVertical(input))
                .Concat(EnumerableDiagonal(input));
        }

        private IEnumerable<string> EnumerableHorizontal(char[][] input)
        {
            foreach (char[] x in input)
            {
                yield return new string(x);
            }
        }

        private IEnumerable<string> EnumerableVertical(char[][] input)
        {
            int ROWS = input.Length;
            int COLS = input[0].Length;
            for (int i = 0; i < COLS; i++)
            {
                char[] line = new char[ROWS];
                for (int j = 0; j < ROWS; j++)
                {
                    line[j] = input[j][i];
                }

                yield return new string(line);
            }
        }

        private IEnumerable<string> EnumerableDiagonal(char[][] input)
        {
            int ROWS = input.Length;
            int COLS = input[0].Length;

            for (int i = 0; i < COLS; i++)
            {
                List<char> lineA = new();
                List<char> lineB = new();
                for (int j = 0; j < ROWS; j++)
                {
                    int row = j;
                    int col = i + j;

                    if (row < ROWS && col < COLS)
                    {
                        lineA.Add(input[row][col]);
                    }

                    col = i - j;
                    if (row < ROWS && col >= 0)
                    {
                        lineB.Add(input[row][col]);
                    }
                }

                yield return new string(lineA.ToArray());
                yield return new string(lineB.ToArray());
            }

            for (int i = 1; i < ROWS; i++)
            {
                List<char> lineA = new();
                List<char> lineB = new();
                for (int j = 0; j < COLS; j++)
                {
                    int row = i + j;
                    int col = j;

                    if (row < ROWS && col < COLS)
                    {
                        lineA.Add(input[row][col]);
                    }

                    col = COLS - 1 - j;

                    if (row < ROWS && col >= 0)
                    {
                        lineB.Add(input[row][col]);
                    }
                }

                yield return new string(lineA.ToArray());
                yield return new string(lineB.ToArray());
            }
        }

        private async Task<char[][]> ReadInput()
        {
            string[] lines = await File.ReadAllLinesAsync(_inputFile);
            char[][] result = new char[lines.Length][];
            for (int i = 0; i < lines.Length; i++)
            {
                result[i] = lines[i].ToCharArray();
            }

            return result;
        }
    }
}