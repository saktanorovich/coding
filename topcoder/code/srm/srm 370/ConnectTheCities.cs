using System;

class ConnectTheCities
{
    private int helper(int range, int distance, int[] position)
    {
        int n = position.Length;
        int[,] dp = new int[n, distance + 1];
        for (int i = 0; i < n; ++ i)
            for (int j = 0; j <= distance; ++ j)
            {
                dp[i, j] = 1000000000;
            }
        for (int j = 0; j <= range; ++ j)
        {
            dp[0, j] = Math.Abs(position[0] - j);
        }
        for (int i = 1; i < n; ++ i)
            for (int j = 0; j <= distance; ++ j)
                for (int k = 0; k <= range; ++ k)
                    if (j - k >= 0)
                    {
                        dp[i, j] = Math.Min(dp[i, j], dp[i - 1, j - k] + Math.Abs(position[i] - j));
                    }
        int cost = 1000000000;
        for (int j = 0; j <= range; ++j)
        {
            cost = Math.Min(cost, dp[n - 1, distance - j]);
        }
        return cost;
    }
    public int minimalRange(int distance, int funds, int[] position)
    {
        Array.Sort(position);
        int left = 0, right = distance + 1;
        while (right - left > 1)
        {
            int mid = (left + right) / 2;
            int cost = helper(mid, distance, position);
            if (cost <= funds)
                right = mid;
            else
                left = mid;
        }
        if (helper(left, distance, position) <= funds)
            return left;
        return right;
    }
}
