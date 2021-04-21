using System;

class PalindromePartition
{
    public int partition(string[] s)
    {
        string str = string.Empty;
        for (int i = 0; i < s.Length; ++i)
        {
            str += s[i];
        }
        int n = str.Length;
        bool[,] isPalindrome = new bool[n, n];
        for (int i = 0; i < n; ++i)
        {
            isPalindrome[i, i] = true;
        }
        for (int len = 2; len <= n; ++len)
            for (int i = 0; i <= n - len; ++i)
            {
                int j = i + len - 1;
                if (len == 2)
                {
                    isPalindrome[i, j] = str[i].Equals(str[j]);
                }
                else
                {
                    isPalindrome[i, j] = str[i].Equals(str[j]) && isPalindrome[i + 1, j - 1];
                }
            }
        int[] dp = new int[n + 1];
        for (int i = 1; i <= n; ++i)
        {
            dp[i] = int.MaxValue / 2;
        }
        dp[0] = 0;
        for (int i = 1; i <= n; ++i)
            for (int j = i; j >= 1; --j)
            {
                if (isPalindrome[j - 1, i - 1])
                {
                    dp[i] = Math.Min(dp[i], dp[j - 1] + 1);
                }
            }
        return dp[n];
    }
}
