using System;
using System.Collections.Generic;

class RaceOrdering
{
    private const int MODULO = 1000003;
    private int n;
    private bool[,] g;
    private long[,] c;
    private bool[] used;
    private int[] outDegree;
    private Dictionary<int, int> dp;

    private void dfs(int src, ref int set, ref int size)
    {
        used[src] = true;
        set |= 1 << src;
        size++;
        for (int i = 0; i < n; ++i)
        {
            if ((g[src, i] || g[i, src]) && !used[i])
            {
                dfs(i, ref set, ref size);
            }
        }
    }

    private int f(int set)
    {
        if (!dp.ContainsKey(set))
        {
            dp[set] = 0;
            for (int i = 0; i < n; ++i)
            {
                if ((set & (1 << i)) != 0)
                {
                    if (outDegree[i] == 0)
                    {
                        for (int j = 0; j < n; ++j)
                        {
                            if (g[j, i])
                            {
                                outDegree[j]--;
                            }
                        }
                        dp[set] = (dp[set] + f(set ^ (1 << i))) % MODULO;
                        for (int j = 0; j < n; ++j)
                        {
                            if (g[j, i])
                            {
                                outDegree[j]++;
                            }
                        }
                    }
                }
            }
        }
        return dp[set];
    }

    public int countOrders(int n, int[] first, int[] second)
    {
        this.n = n;
        g = new bool[n, n];
        outDegree = new int[n];
        for (int i = 0; i < first.Length; ++i)
        {
            g[second[i], first[i]] = true;
        }
        for (int i = 0; i < n; ++i)
        {
            outDegree[i] = 0;
            for (int j = 0; j < n; ++j)
            {
                outDegree[i] += g[i, j] ? 1 : 0;
            }
        }
        c = new long[n + 1, n + 1];
        c[0, 0] = 1;
        for (int i = 1; i <= n; ++i)
        {
            c[i, 0] = 1;
            for (int j = 1; j <= n; ++j)
            {
                c[i, j] = (c[i - 1, j - 1] + c[i - 1, j]) % MODULO;
            }
        }
        used = new bool[n];
        dp = new Dictionary<int, int>();
        for (int i = 0; i < n; ++i)
        {
            dp[1 << i] = 1;
        }
        long result = 1;
        int count = 0;
        for (int i = 0; i < n; ++i)
        {
            if (!used[i])
            {
                int set = 0, size = 0;
                dfs(i, ref set, ref size);
                result = ((result * f(set)) % MODULO * c[count + size, size]) % MODULO;
                count += size;
            }
        }
        return (int)result;
    }

    static void Main(string[] args)
    {
        RaceOrdering ro = new RaceOrdering();
        //Console.WriteLine(ro.countOrders(3, new int[] { 1 }, new int[] { 2 }));
        //Console.WriteLine(ro.countOrders(4, new int[] { 0, 0 }, new int[] { 1, 2 }));
        //Console.WriteLine(ro.countOrders(10, new int[] { 1, 2, 3 }, new int[] { 2, 3, 1 }));
        //Console.WriteLine(ro.countOrders(30, new int[] { }, new int[] { }));
        //Console.WriteLine(ro.countOrders(15, new int[] { 0, 9, 2, 1, 0, 2, 1, 9 }, 
            //new int[] { 1, 4, 5, 6, 1, 5, 6, 4 }));

        Console.WriteLine(ro.countOrders(27,
            new int[] { 0, 0, 3, 3, 6, 6,  9,  9, 12, 12, 15, 15, 18, 18, 20 },
            new int[] { 1, 2, 4, 5, 7, 8, 10, 11, 13, 14, 16, 17, 19, 20, 24 }));
        Console.ReadLine();
    }
}
