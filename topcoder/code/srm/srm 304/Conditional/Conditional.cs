using System;

	class Conditional
	{
		private int n, m, val;
		private double[,] dp, dpv;
		private double run(int k, int sum)
		{
			if (dp[k, sum] == -1)
			{
				dp[k, sum] = 0;
				for (int i = 1; i <= Math.Min(sum, m); ++i)
				{
					dp[k, sum] += run(k - 1, sum - i);
				}	
			}
			return dp[k, sum];
		}
		private double runV(int k, int sum)
		{
			if (dpv[k, sum] == -1)
			{
				dpv[k, sum] = 0;
				for (int i = 1; i <= Math.Min(sum, m); ++i)
				{
					if (i == val)
					{
						dpv[k, sum] += run(k - 1, sum - val);
					}
					else
					{
						dpv[k, sum] += runV(k - 1, sum - i);
					}
				}
			}
			return dpv[k, sum];
		}
		public double probability(int nDice, int maxSide, int v, int theSum)
		{
			n = nDice;
			m = maxSide;
			val = v;
			dp = new double[n, n * m + 1];
			dpv = new double[n, n * m + 1];
			for (int i = 0; i < n; ++i)
				for (int j = 0; j <= n * m; ++j)
				{
					dp[i, j] = -1;
					dpv[i, j] = -1;
				}
			for (int s = 1; s <= n * m; ++s)
			{
				dp[0, s] = (s > m ? 0 : 1);
				dpv[0, s] = (s != val ? 0 : 1);
			}
			for (int i = 0; i < n; ++i)
			{
				dp[i, 0] = 0;
				dpv[i, 0] = 0;
			}
			double ok = 0, all = 0;
			for (int s = n; s <= n * m; ++s)
			{
				if (s >= theSum)
				{
					ok += runV(n - 1, s);
				}
				all += runV(n - 1, s);
			}
			return ok / all;
		}
		static void Main(string[] args)
		{
			Conditional c = new Conditional();
			Console.WriteLine(c.probability(2, 6, 6, 12));
			Console.WriteLine(c.probability(2, 6, 6, 6));
			Console.WriteLine(c.probability(1, 9, 3, 3));
			Console.WriteLine(c.probability(2, 3, 2, 4));
			Console.WriteLine(c.probability(50, 30, 1, 1300));

			Console.ReadLine();
		}
	}

