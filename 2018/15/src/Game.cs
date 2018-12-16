using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Priority_Queue;

namespace src
{
    class Game
    {
        private Map _map;
        private List<Unit> _units;
        private int _width;
        private int _height;
        public int Turn { get; private set; }
        public bool IsCompleted { get; private set; }
        public bool NoEnemiesLeft =>
            _units.All(x => x.Type == UnitType.Elf)
            || _units.All(x => x.Type == UnitType.Goblin);

        public int Score => Turn * _units.Select(x => x.HitPoints).Sum();

        public Game(Map map, List<Unit> units)
        {
            _map = map;
            _width = _map.Width;
            _height = _map.Height;
            _units = units;
        }

        public void NextTurn()
        {
            List<Unit> sortedUnits = _units.OrderBy(x => x.X).OrderBy(x => x.Y).ToList();
            foreach (Unit currentUnit in sortedUnits.Where(x => x.IsAlive))
            {

                if (NoEnemiesLeft)
                {
                    IsCompleted = true;
                    return;
                }

                Unit unitToAttack = GetTarget(currentUnit);
                if (unitToAttack == null)
                {
                    MoveTowardsTarget(currentUnit);
                    unitToAttack = GetTarget(currentUnit);
                }
                if (unitToAttack != null)
                {
                    Attack(currentUnit, unitToAttack);
                }
            }
            Turn++;
        }

        private void MoveTowardsTarget(Unit currentUnit)
        {
            (int x, int y) = GetNextMove(currentUnit);
            if (x < 0 || y < 0)
            {
                return;
            }
            if (_units.Any(u => u.X == x && u.Y == y))
            {
                return;
            }

            currentUnit.Move(x, y);
        }

        private void Attack(Unit currentUnit, Unit target)
        {
            currentUnit.Attack(target);
            if (!target.IsAlive)
            {
                _units.Remove(target);
            }
        }

        private Unit GetTarget(Unit currentUnit)
        {
            return _units
                .Where(x => x.Type != currentUnit.Type)
                .Where(x => AreUnitsAdjacent(currentUnit, x))
                .OrderBy(x => x.X)
                .OrderBy(x => x.Y)
                .OrderBy(x => x.HitPoints)
                .FirstOrDefault();
        }

        private bool AreUnitsAdjacent(Unit a, Unit b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y) == 1;
        }

        private (int x, int y) GetNextMove(Unit currentUnit)
        {
            var queue = new SimplePriorityQueue<(int, int)>();
            int[,] distances = new int[_width, _height];
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    distances[i, j] = int.MaxValue;
                    queue.Enqueue((i, j), int.MaxValue);
                }
            }

            var predestors = new Dictionary<(int, int), List<(int, int)>>();

            distances[currentUnit.X, currentUnit.Y] = 0;
            queue.UpdatePriority((currentUnit.X, currentUnit.Y), 0);

            while (queue.Any())
            {
                (int x, int y) = queue.Dequeue();
                UpdateNeighbors(distances, predestors, queue, x, y, currentUnit.Type);
            }

            List<(int targetX, int targetY)> targets = _units
                            .Where(u => u.Type != currentUnit.Type)
                            .Select(u => GetNeighbors(u.X, u.Y))
                            .SelectMany(p => p)
                            .Select(p => (p.x, p.y, CalculateManhattanDistance((currentUnit.X, currentUnit.Y), p)))
                            .OrderBy(p => p.x)
                            .OrderBy(p => p.y)
                            .OrderBy(p => p.Item3)
                            .Select(p => (p.x, p.y))
                            .ToList();

            if (!targets.Any())
            {
                return (-1, -1);
            }
            var selectedTarget = targets.First();

            try
            {
                var solutions = new HashSet<(int x, int y)>();
                var availableTargets = new HashSet<(int x, int y)>() { selectedTarget };

                while (availableTargets.Any())
                {
                    var c = availableTargets.First();
                    availableTargets.Remove(c);
                    var preds = predestors[c];

                    foreach (var pred in preds)
                    {
                        if (pred.Item1 == currentUnit.X && pred.Item2 == currentUnit.Y)
                        {
                            solutions.Add(c);
                        }
                        else
                        {
                            availableTargets.Add(pred);
                        }
                    }
                }
                return solutions.OrderBy(p => p.x).OrderBy(p => p.y).First();
            }
            catch (Exception)
            {
                return (-1, -1);
            }
        }

        private int CalculateManhattanDistance((int x, int y) a, (int x, int y) b)
        {
            return Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
        }

        private IEnumerable<(int x, int y)> GetNeighbors(int x, int y)
        {
            if (IsTileValid(x - 1, y))
            {
                yield return (x - 1, y);
            }
            if (IsTileValid(x + 1, y))
            {
                yield return (x + 1, y);
            }
            if (IsTileValid(x, y - 1))
            {
                yield return (x, y - 1);
            }
            if (IsTileValid(x, y + 1))
            {
                yield return (x, y + 1);
            }
        }

        private bool IsTileValid(int x, int y)
        {
            bool isXInvalid = x < 0 || x >= _width;
            bool isYInvalid = y < 0 || y >= _height;
            if (isXInvalid || isYInvalid)
            {
                return false;
            }
            if (_map.HasWall(x, y))
            {
                return false;
            }
            if (_units.Any(u => u.X == x && u.Y == y))
            {
                return false;
            }
            return true;
        }

        private void UpdateNeighbors(
            int[,] distances,
            Dictionary<(int, int), List<(int, int)>> predestors,
            SimplePriorityQueue<(int, int)> queue,
            int x,
            int y,
            UnitType type)
        {
            int newDistance = distances[x, y] == int.MaxValue ? int.MaxValue : distances[x, y] + 1;
            UpdateNeighbor(distances, predestors, queue, newDistance, (x, y), (x, y - 1), type);
            UpdateNeighbor(distances, predestors, queue, newDistance, (x, y), (x, y + 1), type);
            UpdateNeighbor(distances, predestors, queue, newDistance, (x, y), (x - 1, y), type);
            UpdateNeighbor(distances, predestors, queue, newDistance, (x, y), (x + 1, y), type);
        }

        public void UpdateNeighbor(
            int[,] distances,
            Dictionary<(int, int), List<(int, int)>> predestors,
            SimplePriorityQueue<(int, int)> queue,
            int newDistance,
            (int x, int y) previous,
            (int x, int y) current,
            UnitType type)
        {
            bool isXInvalid = current.x < 0 || current.x >= _width;
            bool isYInvalid = current.y < 0 || current.y >= _height;
            if (isXInvalid || isYInvalid)
            {
                return;
            }
            if (_map.HasWall(current.x, current.y))
            {
                return;
            }
            if (_units.Any(u => u.Type == type && u.X == current.x && u.Y == current.y))
            {
                return;
            }
            if (newDistance > distances[current.x, current.y])
            {
                return;
            }
            if (newDistance == int.MaxValue)
            {
                return;
            }
            if (newDistance == distances[current.x, current.y] && predestors.ContainsKey((current.x, current.y)))
            {
                predestors[(current.x, current.y)].Add(((previous.x, previous.y)));
            }
            else
            {
                predestors[(current.x, current.y)] = new List<(int, int)>() { (previous.x, previous.y) };
            }
            distances[current.x, current.y] = newDistance;
            queue.UpdatePriority((current.x, current.y), newDistance);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int y = 0; y < _map.Height; y++)
            {
                for (int x = 0; x < _map.Width; x++)
                {
                    char c;
                    if (_map.HasWall(x, y))
                    {
                        c = '#';
                    }
                    else
                    {
                        Unit unit = _units.FirstOrDefault(u => u.X == x && u.Y == y);
                        if (unit == null)
                        {
                            c = '.';
                        }
                        else
                        {
                            c = unit.Type == UnitType.Elf ? 'E' : 'G';
                        }
                    }
                    sb.Append(c);
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}