using System;

	class Find3Cheaters
	{
		private int equal(string a, int ia, string b, int ib)
		{
			if (ia >= 0 && ib >= 0)
			{
				if (a[ia] == b[ib])
				{
					return 1;
				}
			}
			return 0;
		}
		public int shortest(string a, string b, string c)
		{
			int[,,] dp = new int[a.Length + 1, b.Length + 1, c.Length + 1];
			for (int ia = 0; ia <= a.Length; ++ia)
				for (int ib = 0; ib <= b.Length; ++ib)
					for (int ic = 0; ic <= c.Length; ++ic)
					{
						if (ia + ib + ic == 0)
						{
							continue;
						}
						dp[ia, ib, ic] = int.MaxValue / 2;
						if (ia > 0)
						{
							dp[ia, ib, ic] = Math.Min(dp[ia, ib, ic],
								dp[ia - 1, ib - equal(a, ia - 1, b, ib - 1), ic - equal(a, ia - 1, c, ic - 1)] + 1);
						}
						if (ib > 0)
						{
							dp[ia, ib, ic] = Math.Min(dp[ia, ib, ic],
								dp[ia - equal(b, ib - 1, a, ia - 1), ib - 1, ic - equal(b, ib - 1, c, ic - 1)] + 1);
						}
						if (ic > 0)
						{
							dp[ia, ib, ic] = Math.Min(dp[ia, ib, ic],
								dp[ia - equal(c, ic - 1, a, ia - 1), ib - equal(c, ic - 1, b, ib - 1), ic - 1] + 1);
						}
					}
			return dp[a.Length, b.Length, c.Length];
		}
		static void Main(string[] args)
		{
			Find3Cheaters f3c = new Find3Cheaters();
			Console.WriteLine(f3c.shortest("aagaaa", "ataatg", "ctggg"));
			Console.WriteLine(f3c.shortest("wowwowwow", "wowwowwow", "badbadbad"));
			Console.WriteLine(f3c.shortest("a", "aaaaaaa", "aaaaaaaaaaaaaa"));

			Console.ReadLine();
		}
	}

