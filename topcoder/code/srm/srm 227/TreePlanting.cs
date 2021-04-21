using System;
using System.Collections.Generic;

    class TreePlanting
    {
        private long[,,] dp;
        private long run(int n, int fancy, int who)
        {
            if (dp[n, fancy, who] == -1)
            {
                dp[n, fancy, who] = 0;
                if (who == 0)
                {
                    dp[n, fancy, who] += run(n - 1, fancy, who);
                    if (fancy > 0)
                    {
                        dp[n, fancy, who] += run(n - 1, fancy - 1, 1 - who);
                    }
                }
                else
                {
                    dp[n, fancy, who] += run(n - 1, fancy, 1 - who);
                }
            }
            return dp[n, fancy, who];
        }
        public long countArrangements(int total, int fancy)
        {
            dp = new long[total + 1, fancy + 1, 2];
            for (int i = 1; i <= total; ++i)
                for (int f = 0; f <= fancy; ++f)
                    for (int w = 0; w < 2; ++w)
                    {
                        dp[i, f, w] = -1;
                    }
            dp[0, 0, 0] = dp[0, 0, 1] = 1;
            return run(total, fancy, 0);
        }
        static void Main(string[] args)
        {
            TreePlanting tp = new TreePlanting();
            Console.WriteLine(tp.countArrangements(4, 2));
            Console.WriteLine(tp.countArrangements(3, 1));
            Console.WriteLine(tp.countArrangements(4, 3));
            Console.WriteLine(tp.countArrangements(10, 4));

            Console.ReadLine();
        }
    }
