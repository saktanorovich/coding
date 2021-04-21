using System;
using System.Collections.Generic;

    class PrimeSums
    {
        private long[] dp;

        public long getCount(int[] bag)
        {
            Dictionary<int, int> map = new Dictionary<int, int>();
            int sum = 0;
            foreach (int weight in bag)
            {
                sum += weight;
                if (!map.ContainsKey(weight))
                {
                    map.Add(weight, 1);
                }
                else
                {
                    map[weight]++;
                }
            }
            bool[] isComposite = new bool[sum + 1];
            isComposite[0] = true;
            if (sum >= 1)
            {
                isComposite[1] = true;
            }
            for (int i = 2; i <= sum; ++i)
            {
                if (!isComposite[i])
                {
                    for (int j = i + i; j <= sum; j += i)
                    {
                        isComposite[j] = true;
                    }
                }
            }
            dp = new long[sum + 1];
            dp[0] = 1;
            foreach (int weight in map.Keys)
            {
                if (weight != 0)
                {
                    for (int j = sum; j >= 0; --j)
                        for (int count = 1; count <= map[weight]; ++count)
                        {
                            if (j - weight * count >= 0)
                            {
                                dp[j] += dp[j - weight * count];
                            }
                            else
                            {
                                break;
                            }
                        }
                }
            }
            long result = 0;
            int zeros = (map.ContainsKey(0) ? map[0] : 0);
            for (int i = 2; i <= sum; ++i)
            {
                if (!isComposite[i])
                {
                    result += dp[i] * (zeros + 1);
                }
            }
            return result;
        }
        static void Main(string[] args)
        {
            PrimeSums ps = new PrimeSums();
            Console.WriteLine(ps.getCount(new int[] { 1, 1, 2, 7 }));
            Console.WriteLine(ps.getCount(new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }));
            Console.WriteLine(ps.getCount(new int[] { 4, 6, 8, 10, 12, 14 }));
            Console.WriteLine(ps.getCount(new int[] { 1, 2, 4, 8, 16, 32, 64, 128 }));
            Console.WriteLine(ps.getCount(new int[] { 1234, 5678, 9012, 3456, 7890, 2345, 6789, 123, 4567, 8901 }));
            Console.WriteLine(ps.getCount(new int[] { 0, 0, 7 }));
            Console.WriteLine(ps.getCount(new int[] { 0}));

            Console.ReadLine();
        }
    }
