using System;

    class DiceGames
    {
        private int n;
        private long[,] dp;
        private int[] sides;
        private long run(int k, int what)
        {
            if (dp[k, what] == -1)
            {
                dp[k, what] = 0;
                for (int i = what; i <= sides[k + 1]; ++i)
                {
                    dp[k, what] += run(k + 1, i);
                }
            }
            return dp[k, what];
        }
        long countFormations(int[] sides)
        {
            Array.Sort(sides);
            n = sides.Length;
            this.sides = new int[n];
            this.sides = sides;
            dp = new long[n, sides[n - 1] + 1];
            for (int i = 0; i < n; ++i)
                for (int j = 0; j <= sides[n - 1]; ++j)
                {
                    dp[i, j] = -1;
                }
            for (int i = 1; i <= sides[n - 1]; ++i)
            {
                dp[n - 1, i] = 1;
            }
            long result = 0;
            for (int i = 1; i <= sides[0]; ++i)
            {
                result += run(0, i);
            }
            return result;
        }
        static void Main(string[] args)
        {
            DiceGames dg = new DiceGames();
            Console.WriteLine(dg.countFormations(new int[] { 4 }));
            Console.WriteLine(dg.countFormations(new int[] { 2, 2 }));
            Console.WriteLine(dg.countFormations(new int[] { 4, 4 }));
            Console.WriteLine(dg.countFormations(new int[] { 3, 4 }));
            Console.WriteLine(dg.countFormations(new int[] { 4, 5, 6 }));

            Console.ReadLine();
        }
    }
