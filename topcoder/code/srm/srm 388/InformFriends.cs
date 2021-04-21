using System;

    class InformFriends
    {
        private int n;
        private int[] g;
        private bool[] isDominatingSet;
        private int[] dp;

        private int run(int set)
        {
            if (dp[set] == -1)
            {
                for (int subSet = set; subSet > 0; subSet = (subSet - 1) & set)
                {
                    if (isDominatingSet[subSet])
                    {
                        dp[set] = Math.Max(dp[set], run(set ^ subSet) + 1);
                    }
                }
            }
            return dp[set];
        }

        public int maximumGroups(string[] friends)
        {
            n = friends.Length;
            g = new int[n];
            isDominatingSet = new bool[1 << n];
            for (int i = 0; i < n; ++i)
            {
                g[i] = 0;
                for (int j = 0; j < n; ++j)
                {
                    if (friends[i][j].Equals('Y'))
                    {
                        g[i] |= (1 << j);
                    }
                }
            }
            for (int i = 0; i < 1 << n; ++i)
            {
                int isReached = i;
                for (int j = 0; j < n; ++j)
                {
                    if ((i & (1 << j)) != 0)
                    {
                        isReached |= g[j];
                    }
                }
                isDominatingSet[i] = (isReached == (1 << n) - 1);
            }
            dp = new int[1 << n];
            for (int i = 0; i < 1 << n; ++i)
            {
                dp[i] = -1;
            }
            dp[0] = 0;
            return run((1 << n) - 1);
        }
        static void Main(string[] args)
        {
            InformFriends inf = new InformFriends();
            Console.WriteLine(inf.maximumGroups(new string[] { "NYYN", "YNYY", "YYNY", "NYYN" }));
            Console.WriteLine(inf.maximumGroups(new string[] { "NYYN", "YNYN", "YYNN", "NNNN" }));
            Console.WriteLine(inf.maximumGroups(new string[] { "NYNNNY", "YNYNNN", "NYNYNN", "NNYNYN", "NNNYNY", "YNNNYN" }));
            Console.WriteLine(inf.maximumGroups(new string[] { "NYNY", "YNYN", "NYNY", "YNYN" }));

            Console.ReadLine();
        }
    }
