using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace src
{
    public class Area
    {
        private static readonly Dictionary<AcreTypes, char> _acreTypesToCharMapping = new Dictionary<AcreTypes, char>()
        {
            [AcreTypes.Unknown] = '?',
            [AcreTypes.OpenGround] = '.',
            [AcreTypes.Trees] = '|',
            [AcreTypes.Lumberyard] = '#'
        };

        private AcreTypes[,] _area;
        private int TimeInSeconds { get; }
        private int Width { get; }
        private int Height { get; }

        public int WoodedAcres => (from AcreTypes item in _area where item == AcreTypes.Trees select item).Count();

        public int Lumberyards => (from AcreTypes item in _area where item == AcreTypes.Lumberyard select item).Count();

        public int ResourceValues => WoodedAcres * Lumberyards;

        private Area(int timeInSeconds, AcreTypes[,] initialArea)
        {
            TimeInSeconds = timeInSeconds;
            _area = initialArea;
            Height = _area.GetLength(1);
            Width = _area.GetLength(0);
        }

        public Area GetNext()
        {
            var newArea = new AcreTypes[Width, Height];

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    newArea[x, y] = GetNewValue(x, y);
                }
            }

            return new Area(TimeInSeconds + 1, newArea);
        }

        private AcreTypes GetNewValue(int x, int y)
        {
            AcreTypes current = _area[x, y];
            List<AcreTypes> neighbours = GetNeighbours(x, y).ToList();
            int numberOfNeighboursWithTrees = neighbours
                                .Where(n => n == AcreTypes.Trees)
                                .Count();
            int numberOfNeighboursWithLumberyards = neighbours
                            .Where(n => n == AcreTypes.Lumberyard)
                            .Count();

            if (current == AcreTypes.OpenGround)
            {
                return numberOfNeighboursWithTrees >= 3
                    ? AcreTypes.Trees
                    : AcreTypes.OpenGround;
            }
            else if (current == AcreTypes.Trees)
            {
                return numberOfNeighboursWithLumberyards >= 3
                    ? AcreTypes.Lumberyard
                    : AcreTypes.Trees;
            }
            else if (current == AcreTypes.Lumberyard)
            {
                return (numberOfNeighboursWithLumberyards >= 1 && numberOfNeighboursWithTrees >= 1)
                    ? AcreTypes.Lumberyard
                    : AcreTypes.OpenGround;
            }
            else
            {
                throw new Exception(); // TODO 
            }
        }

        private IEnumerable<AcreTypes> GetNeighbours(int x, int y)
        {
            int[] neigboursIndexShifts = new[] { -1, 0, 1 };
            IEnumerable<(int nx, int ny)> neighboursIndexes =
                (from xShift in neigboursIndexShifts
                 from yShift in neigboursIndexShifts
                 select (xShift, yShift))
                .Where(shift => shift.xShift != 0 || shift.yShift != 0)
                .Select(shift => (x + shift.xShift, y + shift.yShift));
            IEnumerable<(int nx, int ny)> validNeighboursIndexes = neighboursIndexes
                .Where(index => index.nx >= 0 && index.nx < Width)
                .Where(index => index.ny >= 0 && index.ny < Height);

            return validNeighboursIndexes.Select(indexes => _area[indexes.nx, indexes.ny]);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Time: {TimeInSeconds}");
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    sb.Append(_acreTypesToCharMapping[_area[x, y]]);
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

        public class AreaBuilder
        {
            private AcreTypes[,] _area;
            public AreaBuilder(int width, int height)
            {
                _area = new AcreTypes[width, height];
            }

            public void SetValue(int x, int y, AcreTypes type)
            {
                _area[x, y] = type;
            }

            public Area Build()
            {
                return new Area(0, _area);
            }
        }
    }
}
