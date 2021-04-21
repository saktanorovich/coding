using System;


    class CharmingTickets
    {
        private const int MODULO = 999983;

        public int count(int k, string good)
        {
            int[,] dp = new int[k + 1, 9 * k + 1];
            dp[0, 0] = 1;
            for (int len = 0; len < k; ++len)
                for (int s = 0; s <= 9 * len; ++s)
                    foreach (char c in good)
                    {
                        int d = (int)c - 48;
                        dp[len + 1, s + d] = (dp[len + 1, s + d] + dp[len, s]) % MODULO;
                    }
            int ko = (k + 1) / 2, ke = k / 2;
            long result = 0, reso = 0, rese = 0;
            for (int s = 0; s <= 9 * k; ++s)
            {
                result = (result + (long)dp[k, s] * dp[k, s]) % MODULO;
            }
            for (int s = 0; s <= 9 * ko; ++s)
            {
                reso = (reso + (long)dp[ko, s] * dp[ko, s]) % MODULO;
            }
            for (int s = 0; s <= 9 * ke; ++s)
            {
                rese = (rese + (long)dp[ke, s] * dp[ke, s]) % MODULO;
            }
            return (int)((2 * result - reso * rese) % MODULO + MODULO) % MODULO;
        }

        static void Main(string[] args)
        {
            CharmingTickets ct = new CharmingTickets();
            Console.WriteLine(ct.count(1000, "0123456789"));

            Console.ReadLine();
        }
    }
