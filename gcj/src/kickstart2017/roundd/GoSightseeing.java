package kickstart2017.roundd;

import utils.io.*;
import java.io.*;
import java.util.*;

// Problem A
public class GoSightseeing {
    public void process(int testCase, InputReader in, PrintWriter out) {
        int n = in.nextInt();
        int ts = in.nextInt();
        int tf = in.nextInt();
        int s[] = new int[n - 1];
        int f[] = new int[n - 1];
        int d[] = new int[n - 1];
        for (int i = 0; i < n - 1; ++i) {
            s[i] = in.nextInt();
            f[i] = in.nextInt();
            d[i] = in.nextInt();
        }
        long dp[][] = new long[n][n];
        for (int i = 0; i < n; ++i) {
            Arrays.fill(dp[i], Long.MAX_VALUE);
        }
        dp[0][0] = 0;
        for (int i = 0; i < n - 1; ++i) {
            for (int j = 0; j <= i; ++j) {
                if (dp[i][j] < Long.MAX_VALUE) {
                    for (int k = 0; k < 2; ++k) {
                        dp[i + 1][j + k] = Math.min(dp[i + 1][j + k], get(dp[i][j] + k * ts, s[i], f[i]) + d[i]);
                    }
                }
            }
        }
        for (int k = n - 1; k >= 0; --k) {
            if (dp[n - 1][k] <= tf) {
                out.format("Case #%d: %d\n", testCase, k);
                return;
            }
        }
        out.format("Case #%d: %s\n", testCase, "IMPOSSIBLE");
    }

    private static long get(long t, int s, int f) {
        if (t > s) {
            return s + f * (long)Math.ceil(1.0 * (t - s) / f);
        }
        return s;
    }
}
