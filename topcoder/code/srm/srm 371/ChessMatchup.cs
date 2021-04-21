using System;

class ChessMatchup
{
	public int maximumScore(int[] us, int[] them)
	{
		Array.Sort(us);
		Array.Sort(them);
		int n = us.Length;
		int[,] dp = new int[n + 1, n + 1];
		for (int i = 0; i < n; ++ i)
			for (int j = 0; j < n; ++j)
			{
				dp[i + 1, j + 1] = Math.Max(dp[i, j + 1], dp[i + 1, j]);
				if (us[i] > them[j])
					dp[i + 1, j + 1] = Math.Max(dp[i + 1, j + 1], dp[i, j] + 2);
				else if (us[i] == them[j])
					dp[i + 1, j + 1] = Math.Max(dp[i + 1, j + 1], dp[i, j] + 1);
			}
		return dp[n, n];
	}
	static void Main(string[] args)
	{
		ChessMatchup cm = new ChessMatchup();
		Console.WriteLine(cm.maximumScore(new int[]{5, 8}, new int[]{7, 3}));
		Console.WriteLine(cm.maximumScore(new int[] { 7, 3 }, new int[] { 5, 8 }));
		Console.WriteLine(cm.maximumScore(new int[] { 10, 5, 1 }, new int[] { 10, 5, 1 }));
		Console.WriteLine(cm.maximumScore(new int[] { 1, 10, 7, 4 }, new int[] { 15, 3, 8, 7 }));

		Console.ReadLine();
	}
}
