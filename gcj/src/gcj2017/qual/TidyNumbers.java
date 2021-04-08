package gcj2017.qual;

import utils.io.*;
import java.io.*;

// Problem B
public class TidyNumbers {
    public void process(int testCase, InputReader in, PrintWriter out) {
        long n = in.nextLong();
        long m, k;
        if (n % 2 == 0) {
            k = num(n);
            m = get(k);
        } else {
            m = rec(n, 0, 1);
        }
        out.format("Case #%d: %d\n", testCase, m);
    }

    private static long rec(long n, long m, int d) {
        if (m > n) {
            return 0;
        }
        long res = m;
        for (; d <= 9; ++d) {
            res = Math.max(res, rec(n, m * 10 + d, d));
        }
        return res;
    }

    private static long num(long n) {
        long m = 0;
        for (int l = 1; l <= MAX; ++l) {
            m += cnt(l);
        }
        long lo = 1;
        long hi = m;
        while (lo + 1 < hi) {
            long x = (lo + hi) / 2;
            if (get(x) > n) {
                hi = x;
            } else {
                lo = x;
            }
        }
        return lo;
    }

    private static long get(long n) {
        long m = 0, k;
        for (int l = 1; ; ++l) {
            k = cnt(l);
            if (m + k >= n) {
                return get(l, n - m);
            }
            m += k;
        }
    }

    private static long cnt(int l) {
        long res = 0;
        for (int d = 1; d <= 9; ++d) {
            res += dp[l][d];
        }
        return res;
    }

    private static long get(int l, long x) {
        long res = 0;
        for (int d = 1; l > 0;) {
            long m = 0, k;
            while (true) {
                k = dp[l][d];
                if (m + k >= x) {
                    x = x - m;
                    l = l - 1;
                    res = res * 10 + d;
                    break;
                }
                m += k;
                d += 1;
            }
        }
        return res;
    }

    private static final int MAX = 20;
    private static final long[][] dp;
    static {
        dp = new long[MAX + 1][10];
        for (int d = 1; d <= 9; ++d) {
            dp[1][d] = 1;
        }
        for (int i = 1; i < MAX; ++i) {
            for (int d = 1; d <= 9; ++d) {
                for (int k = d; k <= 9; ++k) {
                    dp[i + 1][d] += dp[i][k];
                }
            }
        }
    }
}
