using System;
using System.Collections.Generic;

    class AutoMarket
    {
        public int maxSet(int[] cost, int[] features, int[] fixTimes)
        {
            int n = cost.Length;
            for (int i = 0; i < n; ++i)
                for (int j = i + 1; j < n; ++j)
                {
                    if (cost[j] < cost[i])
                    {
                        int temp = cost[i]; cost[i] = cost[j]; cost[j] = temp;
                        temp = features[i]; features[i] = features[j]; features[j] = temp;
                        temp = fixTimes[i]; fixTimes[i] = fixTimes[j]; fixTimes[j] = temp;
                    }
                }
            int result = 0;
            int[] dp = new int[n];
            for (int i = 0; i < n; ++i)
            {
                dp[i] = 1;
                for (int j = 0; j < i; ++j)
                {
                    if (cost[i] > cost[j] && features[i] < features[j] && fixTimes[i] > fixTimes[j])
                    {
                        dp[i] = Math.Max(dp[i], dp[j] + 1);
                    }
                }
                result = Math.Max(dp[i], result);
            }
            return result;
        }
        static void Main(string[] args)
        {
            AutoMarket am = new AutoMarket();
            Console.WriteLine(am.maxSet(new int[] { 10000, 14000, 8000, 12000 }, 
                new int[] { 1, 2, 4, 3 }, 
                new int[] { 17, 15, 8, 11 }));
            Console.WriteLine(am.maxSet(new int[] { 1, 2, 3, 4, 5 },
                new int[] { 1, 2, 3, 4, 5 },
                new int[] { 1, 2, 3, 4, 5 }));
            Console.WriteLine(am.maxSet(new int[] { 9000, 6000, 5000, 5000, 7000 },
                new int[] { 1, 3, 4, 5, 2 },
                new int[] { 10, 6, 6, 5, 9 }));
            Console.WriteLine(am.maxSet(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 },
                new int[] { 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 },
                new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 }));
            Console.WriteLine(am.maxSet(new int[] { 1000, 1000, 1000, 1000, 2000 },
                new int[] { 3, 3, 4, 3, 3 },
                new int[] { 3, 3, 3, 4, 3 }));

            Console.ReadLine();
        }
    }
