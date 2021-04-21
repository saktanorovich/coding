using System;
using System.Collections.Generic;

	class DeserializeSequence
	{
		private const int bound = 1000000;
		private List<int> nums;
		private int[,] dp;
		private string sequence;
		private int toInt(string str)
		{
			int result = 0;
			for (int i = 0; i < str.Length; ++i)
			{
				result = result * 10 + int.Parse(str[i].ToString());
				if (result > bound)
				{
					return -1;
				}
			}
			return result;
		}
		private int run(int k, int what)
		{
			if (dp[k, what] == -1)
			{
				dp[k, what] = 0;
				for (int i = k - 1; i >= 0; --i)
				{
					int num = toInt(sequence.Substring(i, k - i));
					if (num == -1)
					{
						break;
					}
					if (num <= nums[what] && num != 0)
					{
						dp[k, what] += run(i, nums.IndexOf(num));
					}
				}
			}
			return dp[k, what];
		}
		public int howMany(string str)
		{
			sequence = str;
			int n = str.Length;
			nums = new List<int>();
			for (int i = 0; i < n; ++i)
				for (int j = i; j < n; ++j)
				{
					int num = int.Parse(str.Substring(i, j - i + 1));
					if (!nums.Contains(num) && num != 0)
					{
						nums.Add(num);
					}
					if (num > bound)
					{
						break;
					}
				}
			if (!nums.Contains(bound))
			{
				nums.Add(bound);
			}
			nums.Sort();
			dp = new int[n + 1, nums.Count];
			for (int j = 0; j < nums.Count; ++j)
			{
				for (int i = 1; i <= n; ++i)
				{
					dp[i, j] = -1;
				}
				dp[0, j] = 1;
			}
			return run(n, nums.IndexOf(bound));
		}
	}
