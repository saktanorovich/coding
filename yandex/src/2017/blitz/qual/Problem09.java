package blitz2017.qual;

import utils.io.*;
import java.io.*;
import java.util.Arrays;

// Red-black tree
public class Problem09 {
    public boolean process(int testCase, InputReader in, PrintWriter out) {
        int n = in.nextInt();
        if (n > 2) {
            this.memo = new long[2][n + 1][25];
            for (int k = 3; k <= n; ++k) {
                Arrays.fill(memo[0][k], -1);
                Arrays.fill(memo[1][k], -1);
            }
            memo[0][1][1] = 1;
            long res = 0;
            // h - the number of black nodes on a path from the root to a leaf
            for (int h = 2; h < 25; ++h) {
                res += B(n, h);
                res %= MOD;
            }
            out.println(res);
        } else {
            out.println(0);
        }
        return true;
    }

    private long B(int n, int h) {
        if (memo[0][n][h] != -1) {
            return memo[0][n][h];
        }
        long res = 0;
        if (1 < h && h < n) {
            for (int le = 1, ri = n - 2; le <= ri; ++le, --ri) {
                long L = 0;
                L += B(le, h - 1);
                L += R(le, h - 1);
                L %= MOD;
                long R = 0;
                R += B(ri, h - 1);
                R += R(ri, h - 1);
                R %= MOD;
                res += L * R % MOD;
                res %= MOD;
            }
        }
        memo[0][n][h] = res;
        return res;
    }

    private long R(int n, int h) {
        if (memo[1][n][h] != -1) {
            return memo[1][n][h];
        }
        long res = 0;
        for (int le = 1, ri = n - 2; le <= ri; ++le, --ri) {
            long L = B(le, h);
            long R = B(ri, h);
            res += L * R % MOD;
            res %= MOD;
        }
        memo[1][n][h] = res;
        return res;
    }

    private long[][][] memo;

    private static final long MOD = (long) 1e9 + 7;
}
