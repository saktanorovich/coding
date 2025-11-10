using System;

class DivisiblePermutations
{
    public long count(String sDigits, int m)
    {
        int n = sDigits.Length;
        int[] digits = new int[n];
        for (int i = 0; i < n; ++ i)
        {
            digits[i] = int.Parse(sDigits[i].ToString());
        }
        Array.Sort(digits);
        long[,] dp = new long[1 << n, m];
        dp[0, 0] = 1;
        for (int s = 0; s < (1 << n); ++ s)
            for (int r = 0; r < m; ++r)
            {
                int prev = -1;
                for (int i = 0; i < n; ++ i)
                {
                    if ((s & (1 << i)) == 0)
                    {
                        if (digits[i] != prev)
                        {
                            dp[s ^ (1 << i), ((r * 10) % m + digits[i]) % m] += dp[s, r];
                        }
                        prev = digits[i];    
                    }
                }
            }
        return dp[(1 << n) - 1, 0];
    }
    static void Main(string[] args)
    {
        DivisiblePermutations dp = new DivisiblePermutations();
        Console.WriteLine(dp.count("133", 7));
        Console.WriteLine(dp.count("987654321999999", 39));

        Console.ReadLine();
    }
}
