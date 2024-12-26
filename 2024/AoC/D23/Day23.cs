using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace AoC.D23
{
    internal class Day23 : ISolver
    {
        private readonly string _inputFile;

        public Day23(string inputFile)
        {
            _inputFile = inputFile;
        }

        public async Task<string> SolvePart1()
        {
            (string, string)[] edges = await ReadInput();
            Dictionary<string, HashSet<string>> adj = GetAdjacencyMap(edges);
            HashSet<string> set = new();

            foreach ((string a, string b) in edges)
            {
                foreach (string c in adj[a])
                {
                    bool connedted = adj[b].Contains(c);
                    bool uniques = a != b && a != c && b != c;
                    bool startWithT = a.StartsWith('t') || b.StartsWith('t') || c.StartsWith('t');
                    if (uniques && connedted && startWithT)
                    {
                        string label = string.Join(',', new[] { a, b, c }.OrderBy(x => x));
                        set.Add(label);
                    }
                }
            }

            long result = set.Count;
            return result.ToString();
        }

        public async Task<string> SolvePart2()
        {
            (string, string)[] edges = await ReadInput();
            Dictionary<string, HashSet<string>> adj = GetAdjacencyMap(edges);

            IEnumerable<HashSet<string>> BronKerbosch(HashSet<string> R, HashSet<string> P, HashSet<string> X)
            {
                if (P.Count == 0 && X.Count == 0)
                {
                    yield return R;
                }

                foreach (string v in P)
                {
                    HashSet<string> nR = new(R);
                    nR.Add(v);

                    HashSet<string> nP = new(P);
                    nP.IntersectWith(adj[v]);

                    HashSet<string> nX = new(X);
                    nX.IntersectWith(adj[v]);

                    foreach (var z in BronKerbosch(nR, nP, nX))
                    {
                        yield return z;
                    }

                    P.Remove(v);
                    X.Add(v);
                }
            }

            IEnumerable<HashSet<string>> maximalCliques = BronKerbosch(new HashSet<string>(), new HashSet<string>(adj.Keys), new HashSet<string>());
            HashSet<string>? maximumClique = maximalCliques.OrderByDescending(x => x.Count).FirstOrDefault();
            if(maximumClique==null){
                maximumClique = new HashSet<string>();
            }

            string pass = string.Join(',', maximumClique.OrderBy(x=>x));
            return pass;
        }



        private Dictionary<string, HashSet<string>> GetAdjacencyMap((string, string)[] edges)
        {
            Dictionary<string, HashSet<string>> map = new();
            foreach ((string a, string b) in edges)
            {
                if (!map.ContainsKey(a))
                {
                    map[a] = new HashSet<string>();
                }
                map[a].Add(b);

                if (!map.ContainsKey(b))
                {
                    map[b] = new HashSet<string>();
                }
                map[b].Add(a);
            }

            return map;
        }

        private async Task<(string, string)[]> ReadInput()
        {
            string[] lines = await File.ReadAllLinesAsync(_inputFile);
            (string, string)[] result = new (string, string)[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                string[] splitted = lines[i].Split('-');
                result[i] = (splitted[0], splitted[1]);
            }

            return result;
        }
    }
}