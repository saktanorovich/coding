using System;

    class CodeSet
    {
        private int[] c;
        private int[,] dp;
        private int get(int n, int k)
        {
            if (dp[n, k] == -1)
            {
                dp[n, k] = 0;
                if (k <= n - 1)
                {
                    for (int i = 1; i <= n - 1; ++i)
                    {
                        dp[n, k] += get(i, k - 1) * c[n - i];
                    }
                }
            }
            return dp[n, k];
        }
        public int numSets(int n, string favorite)
        {
            c = new int[n + 1];
            c[1] = 1;
            for (int i = 2; i <= n; ++i)
            {
                c[i] = 0;
                for (int j = 1; j <= i - 1; ++j)
                {
                    c[i] += c[j] * c[i - j];
                }
            }
            dp = new int[n + 1, favorite.Length + 1];
            for (int i = 0; i <= n; ++i)
            {
                dp[i, 0] = 0;
                for (int j = 1; j <= favorite.Length; ++j)
                {
                    dp[i, j] = -1;
                }
            }
            dp[1, 0] = 1;
            return get(n, favorite.Length);
        }
        static void Main(string[] args)
        {
            CodeSet cs = new CodeSet();
            Console.WriteLine(cs.numSets(4, "01"));
            Console.WriteLine(cs.numSets(4, "0101"));
            Console.WriteLine(cs.numSets(20, "011001"));
            Console.WriteLine(cs.numSets(15, "100001"));

            Console.ReadLine();
        }
    }
