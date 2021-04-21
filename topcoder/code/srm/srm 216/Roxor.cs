using System;

	class Roxor
	{
		private int n;
		private int[] dp;
		private int canWin(int state)
		{
			if (dp[state] != -1)
			{
				return dp[state];
			}
			else
			{
				for (int i = 0; i < n; ++i)
				{
					if ((state & (1 << i)) != 0)
					{
						for (int j = i + 1; j < n; ++j)
							for (int k = j; k < n; ++k)
							{
								if (canWin(state ^ (1 << i) ^ (1 << j) ^ (1 << k)) == 0)
								{
									return (dp[state] = 1);
								}
							}
					}
				}
			}
			return (dp[state] = 0);
		}
		public int[] play(int[] piles)
		{
			n = piles.Length;
			dp = new int[1 << n];
			for (int i = 0; i < (1 << n); ++i)
			{
				dp[i] = -1;
			}
			for (int i = 0; i < n; ++i)
			{
				if (piles[i] > 0)
				{
					for (int j = i + 1; j < n; ++j)
						for (int k = j; k < n; ++k)
						{
							piles[i]--;
							piles[j]++;
							piles[k]++;
							int state = 0;
							for (int q = 0; q < n; ++q)
							{
								if ((piles[q] & 1) == 1)
								{
									state |= 1 << q;
								}
							}
							if (canWin(state) == 0)
							{
								return new int[3]{i, j, k};
							}
							piles[i]++;
							piles[j]--;
							piles[k]--;
						}
				}
			}
			return new int[]{ };
		}
		static void Main(string[] args)
		{
			Roxor r = new Roxor();
			Console.WriteLine(r.play(new int[]{ 0, 0, 1, 0, 1, 100 }));
			Console.WriteLine(r.play(new int[]{ 1000, 1000, 1000, 1000, 1000 }));
			Console.WriteLine(r.play(new int[]{ 2, 1, 1, 1, 5 }));
			Console.WriteLine(r.play(new int[]{ 14, 301, 391, 410, 511, 681, 58, 259, 981, 81, 5, 42, 251, 401, 120 }));

			Console.ReadLine();
		}
	}
