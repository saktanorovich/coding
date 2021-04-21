using System;

public class Football
{
	const int countScores = 3;
	const int maxInput = 75;
	int[] scores = new int[countScores]{2, 3, 7};
	int[,] dp;
	int rec(int n, int m)
	{
		if (n <= 0)
		{
			return 0;
		}
		if (dp[n, m] == -1)
		{
			dp[n, m] = 0;
			for (int i = 0; i <= m; ++ i)
			{
				dp[n, m] += rec(n - scores[m], i);
			}
		}
		return dp[n, m];
	}
	public int fetchCombinations(int input)
	{
		dp = new int[maxInput + 1, countScores];
		for (int i = 0; i <= input; ++ i)
			for (int j = 0; j < countScores; ++ j)
			{
				dp[i, j] = -1;
			}
		for (int i = 0; i < countScores; ++ i)
		{
			for (int j = 0; j < scores[i]; ++ j)
			{
				dp[j, i] = 0;

			}
			dp[scores[i], i] = 1;
		}
		int res = 0;
		for (int i = 0; i < countScores; ++ i)
		{
			res += rec(input, i);
		}
		return res;
	}
}
