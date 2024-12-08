namespace AoC.D05
{
    internal class Day05 : ISolver
    {
        private readonly string _inputFile;

        public Day05(string inputFile)
        {
            _inputFile = inputFile;
        }

        public async Task<string> SolvePart1()
        {
            (List<(int, int)> orders, List<List<int>> updates) = await ReadInput();
            Dictionary<int, List<int>> rules = CreateRulesDictionary(orders);

            int result = 0;
            foreach (List<int> update in updates)
            {
                bool isValid = ValidateUpdate(rules, update);
                if (isValid)
                {
                    int index = update.Count / 2;
                    result += update[index];
                }
            }

            return result.ToString();
        }

        public async Task<string> SolvePart2()
        {
            (List<(int, int)> orders, List<List<int>> updates) = await ReadInput();
            Dictionary<int, List<int>> rules = CreateRulesDictionary(orders);

            var comparer = new CustomComparer(rules);
            int result = 0;
            foreach (List<int> update in updates)
            {
                bool isValid = ValidateUpdate(rules, update);
                if (!isValid)
                {
                    update.Sort(comparer);
                    int index = update.Count / 2;
                    result += update[index];
                }
            }

            return result.ToString();
        }

        private bool ValidateUpdate(Dictionary<int, List<int>> rules, List<int> update)
        {
            var set = new HashSet<int>();
            foreach (int x in update)
            {
                if (rules.ContainsKey(x))
                {
                    foreach (int y in rules[x])
                    {
                        if (set.Contains(y))
                        {
                            return false;
                        }
                    }
                }

                set.Add(x);
            }
            return true;
        }

        private Dictionary<int, List<int>> CreateRulesDictionary(List<(int, int)> orders)
        {
            Dictionary<int, List<int>> result = new();
            foreach ((int a, int b) in orders)
            {
                if (!result.ContainsKey(a))
                {
                    result[a] = new List<int>();
                }

                result[a].Add(b);
            }

            return result;
        }

        private async Task<(List<(int, int)> orders, List<List<int>> updates)> ReadInput()
        {
            string[] lines = await File.ReadAllLinesAsync(_inputFile);
            bool firstPhase = true;

            List<(int, int)> orders = new();
            List<List<int>> updates = new();
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    firstPhase = false;
                    continue;
                }

                if (firstPhase)
                {
                    string[] splitted = line.Split('|');
                    int a = int.Parse(splitted[0]);
                    int b = int.Parse(splitted[1]);
                    orders.Add((a, b));
                }
                else
                {
                    var list = new List<int>();
                    string[] splitted = line.Split(',');
                    foreach (string value in splitted)
                    {
                        list.Add(int.Parse(value));
                    }
                    updates.Add(list);
                }
            }

            return (orders, updates);
        }

        private class CustomComparer : IComparer<int>
        {
            Dictionary<int, List<int>> _rules;

            public CustomComparer(Dictionary<int, List<int>> rules)
            {
                _rules = rules;
            }

            public int Compare(int x, int y)
            {
                if (x == y)
                {
                    return 0;
                }
                else if (_rules.ContainsKey(x) && _rules[x].Contains(y))
                {
                    return -1;
                }else{
                    return 1;
                }
            }
        }
    }
}