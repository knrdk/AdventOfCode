namespace AoC.D06
{
    internal class Day06 : ISolver
    {
        private const int FREE = 10;

        private const int OBSTACLE = 100;

        private readonly string _inputFile;

        // UP, RIGHT, DOWN, LEFT
        private readonly (int x, int y)[] directions = { (0, -1), (1, 0), (0, 1), (-1, 0) };

        public Day06(string inputFile)
        {
            _inputFile = inputFile;
        }

        public async Task<string> SolvePart1()
        {
            (short[][] map, (int x, int y) guard) = await ReadInput();
            (HashSet<(int x, int y)> guardPositions, bool _) = GetAllGuardPositions(map, guard);
            int result = guardPositions.Count;
            return result.ToString();
        }

        public async Task<string> SolvePart2()
        {
            (short[][] map, (int x, int y) guard) = await ReadInput();
            (HashSet<(int x, int y)> guardPositions, bool _) = GetAllGuardPositions(Copy(map), guard);
            int result = 0;
            foreach ((int x, int y) in guardPositions)
            {
                if (x == guard.x && y == guard.y)
                {
                    continue; // skip the initial position
                }

                short[][] modifiedMap = Copy(map);
                modifiedMap[y][x] = OBSTACLE;

                (HashSet<(int x, int y)> _, bool hasLoop) = GetAllGuardPositions(modifiedMap, guard);
                if (hasLoop)
                {
                    result++;
                }
            }

            return result.ToString();
        }

        private (HashSet<(int x, int y)>, bool hasLoop) GetAllGuardPositions(short[][] map, (int x, int y) guard)
        {
            HashSet<(int x, int y)> pos = new();
            HashSet<(int x, int y, int d)> positionsWithDirection = new();
            int rows = map.Length;
            int cols = map[0].Length;

            while (guard.x >= 0 && guard.x < cols && guard.y >= 0 && guard.y < rows)
            {
                short dir = map[guard.y][guard.x];
                if (positionsWithDirection.Contains((guard.x, guard.y, dir)))
                {
                    return (pos, true);
                }

                pos.Add((guard.x, guard.y));
                positionsWithDirection.Add((guard.x, guard.y, dir));

                int nextX = guard.x + directions[dir].x;
                int nextY = guard.y + directions[dir].y;

                if (!(nextX >= 0 && nextX < cols && nextY >= 0 && nextY < rows))
                {
                    break; // end of map
                }

                if (map[nextY][nextX] == FREE)
                {
                    map[guard.y][guard.x] = FREE;
                    map[nextY][nextX] = dir;
                    guard.x = nextX;
                    guard.y = nextY;
                }
                else // obstacle
                {
                    short nextDir = (short)((map[guard.y][guard.x] + 1) % 4);
                    map[guard.y][guard.x] = nextDir; // change direction
                }
            }

            return (pos, false);
        }
        private T[][] Copy<T>(T[][] arr)
        {
            T[][] result = new T[arr.Length][];
            for (int i = 0; i < arr.Length; i++)
            {
                result[i] = (T[])arr[i].Clone();
            }

            return result;
        }

        private async Task<(short[][], (int x, int y))> ReadInput()
        {
            string[] lines = await File.ReadAllLinesAsync(_inputFile);
            int rows = lines.Length;
            int cols = lines[0].Length;

            int guardX = -1;
            int guardY = -1;
            short[][] map = new short[rows][];
            for (int i = 0; i < rows; i++)
            {
                map[i] = new short[cols];

                for (int j = 0; j < cols; j++)
                {
                    char c = lines[i][j];

                    if (c == '.')
                    {
                        map[i][j] = FREE;
                    }
                    else if (c == '#')
                    {
                        map[i][j] = OBSTACLE;
                    }
                    else
                    {
                        guardY = i;
                        guardX = j;
                        map[i][j] = 0; // first direction - UP
                    }
                }
            }

            return (map, (guardX, guardY));
        }
    }
}