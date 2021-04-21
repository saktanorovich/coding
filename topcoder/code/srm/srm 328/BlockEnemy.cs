using System;
using System.Collections.Generic;

    class BlockEnemy
    {
        struct Edge
        {
            public readonly int u, v;
            public readonly int effort;

            public Edge(int u, int v, int effort)
            {
                this.u = u;
                this.v = v;
                this.effort = effort;
            }
        }

        private const int oo = int.MaxValue / 2;
        private List<Edge>[] g;
        private bool[] occupied;
        private int[,] dp;

        private void dfs(int node, int parent)
        {
            int sum = 0;
            foreach (Edge edge in g[node])
            {
                if (edge.v != parent)
                {
                    dfs(edge.v, node);
                    sum += Math.Min(dp[edge.v, 0], dp[edge.v, 1] + edge.effort);
                }
            }
            if (occupied[node])
            {
                dp[node, 0] = +oo;
                dp[node, 1] = sum;
            }
            else
            {
                dp[node, 0] = sum;
                dp[node, 1] = +oo;
                for (int i = 0; i < g[node].Count; ++i)
                {
                    Edge edge = g[node][i];
                    if (edge.v != parent && dp[edge.v, 1] != +oo)
                    {
                        dp[node, 1] = Math.Min(dp[node, 1], 
                            sum - Math.Min(dp[edge.v, 0], dp[edge.v, 1] + edge.effort) + dp[edge.v, 1]);
                    }
                }
            }
        }

        public int minEffort(int n, string[] roads, int[] occupiedTowns)
        {
            g = new List<Edge>[n];
            for (int i = 0; i < n; ++i)
            {
                g[i] = new List<Edge>();
            }
            for (int i = 0; i < roads.Length; ++i)
            {
                int[] r = Array.ConvertAll<string, int>(roads[i].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries),
                        delegate(string item)
                        {
                            return int.Parse(item);
                        });
                g[r[0]].Add(new Edge(r[0], r[1], r[2]));
                g[r[1]].Add(new Edge(r[1], r[0], r[2]));
            }
            occupied = new bool[n];
            for (int i = 0; i < occupiedTowns.Length; ++i)
            {
                occupied[occupiedTowns[i]] = true;
            }
            dp = new int[n, 2];
            dfs(0, -1);
            return Math.Min(dp[0, 0], dp[0, 1]);
        }
        static void Main()
        {
            Console.WriteLine(new BlockEnemy().minEffort(5,
                new string[] { "1 0 1", "1 2 2", "0 3 3", "4 0 4" },
                new int[] { 3, 2, 4 }));

            Console.WriteLine(new BlockEnemy().minEffort(5,
                new string[] { "1 0 1", "1 2 2", "0 3 3", "4 0 4" },
                new int[] { 3 }));

            Console.WriteLine(new BlockEnemy().minEffort(12,
                new string[] { "0 1 3", "2 0 5", "1 3 1", "1 4 8", "1 5 4", "2 6 2", "4 7 11", "4 8 10", "6 9 7", "6 10 9", "6 11 6" }, 
                new int[] { 1, 2, 6, 8 }));

            Console.WriteLine(new BlockEnemy().minEffort(12,
                new string[] { "0 1 3", "2 0 5", "1 3 1", "1 4 8", "1 5 4", "2 6 2", "4 7 11", "4 8 10", "6 9 7", "6 10 9", "6 11 6" },
                new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 }));

            Console.ReadKey();
        }
    }
