public class Solution {
    public int RearrangeSticks(int n, int k) {
        var dp = new long[n + 1, k + 1];
        for (var i = 0; i <= k; ++i) {
            dp[i, i] = 1;
        }
        for (var i = 1; i <= n; ++i) {
            for (var j = 1; j <= k; ++j) {
                dp[i, j] = dp[i - 1, j - 1] + (i - 1) * dp[i - 1, j];
                if (dp[i, j] >= mod) {
                    dp[i, j] %= mod;
                }
            }
        }
        return (int)dp[n, k];
    }

    private const long mod = (long)1e9 + 7;
}