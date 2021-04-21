using System;

	class BoggleScore
	{
		private const long modulo = 10000000000000;
		public long bestScore(string[] grid, string[] words)
		{
			long result = 0;

			for (int k = 0; k < words.Length; ++k)
			{
				long[,,] dp = new long[4, 4, words[k].Length];
				for (int i = 0; i < 4; ++i)
					for (int j = 0; j < 4; ++j)
					{
						if (grid[i][j].Equals(words[k][0]))
						{
							dp[i, j, 0] = 1;
						}
					}
				for (int l = 1; l < words[k].Length; ++l)
				{
					char ch = words[k][l];
					for (int i = 0; i < 4; ++i)
						for (int j = 0; j < 4; ++j)
						{
							if (grid[i][j].Equals(ch))
							{

								for (int di = -1; di <= +1; ++di)
									for (int dj = -1; dj <= +1; ++dj)
									{
										if (di == 0 && dj == 0)
										{
											continue;
										}
										if (0 <= i + di && i + di < 4 && 0 <= j + dj && j + dj < 4)
										{
											dp[i, j, l] += dp[i + di, j + dj, l - 1];
										}
										dp[i, j, l] %= modulo;
									}
							}
						}
				}
				long times = 0;
				for (int i = 0; i < 4; ++i)
					for (int j = 0; j < 4; ++j)
					{
						times += dp[i, j, words[k].Length - 1];
					}
				times = (times * words[k].Length * words[k].Length) % modulo;
				result = (result + times) % modulo;
			}
			return result;
		}
		static void Main(string[] args)
		{
			BoggleScore bs = new BoggleScore();
			Console.WriteLine(bs.bestScore(new string[]{ "XXEY", "XXXX", "XXXX", "XXXX" }, new string[]{ "EYE" }));
			Console.WriteLine(bs.bestScore(new string[]{ "XEYE", "XXXX", "XXXX", "XXXX" }, new string[]{ "EYE" }));
			Console.WriteLine(bs.bestScore(new string[]{ "TEXX", "REXX", "XXXX", "XXXX" }, new string[]{ "TREE" }));
			Console.WriteLine(bs.bestScore(new string[]{ "XXXX", "XSAX", "XDNX", "XXXX" },
				new string[]{ "SANDS", "SAND", "SAD", "AND" }));
			Console.WriteLine(bs.bestScore(new string[]{ "TREX", "XXXX", "XXXX", "XXXX" }, new string[]{ "TREE" }));

			Console.ReadLine();
		}
	}
