package kickstart2017.rounda;

import java.io.*;
import utils.io.*;

// Problem A
public class SquareCounting {
    public void process(int testCase, InputReader in, PrintWriter out) {
        int n = in.nextInt();
        int m = in.nextInt();
        // The answer is sum[(n - d) * (m - d) * d, d = 1..n-1, n <= m]. Here is why.
        // Assume we have a strip of fixed width d (1 <= d < n). The number of squares
        // that can be put in a square of dxd is equal to d (you can consider a triangle
        // with sides a and b inside square such that a+b=d). Summing up all squares through
        // all strips will result to the above sum.
        out.format("Case #%d: %d\n", testCase, get(n, m));
    }

    private static long get(long n, long m) {
        if (n > m) {
            return get(m, n);
        }
        long r = n * m % MOD * pow1(n - 1);
        r %= MOD;
        r += pow3(n - 1);
        r %= MOD;
        r -= pow2(n - 1) * (m + n) % MOD;
        r += MOD;
        r %= MOD;
        return r;
    }

    // 1^1+2^1+...+n^1
    private static long pow1(long n) {
        long res = n;
        res *= n + 1;
        res %= MOD;
        res *= inv(2);
        res %= MOD;
        return res;
    }

    // 1^2+2^2+...+n^2
    private static long pow2(long n) {
        long res = n;
        res *= n + 1;
        res %= MOD;
        res *= 2 * n + 1;
        res %= MOD;
        res *= inv(6);
        res %= MOD;
        return res;
    }

    // 1^3+2^3+...+n^3
    private static long pow3(long n) {
        long res = 0;
        res += pow1(n);
        res *= pow1(n);
        res %= MOD;
        return res;
    }

    private static long inv(long x) {
        return pow(x, MOD - 2);
    }

    private static long pow(long x, long k) {
        if (k == 0) {
            return 1;
        } else if (k % 2 == 0) {
            return pow(x * x % MOD, k / 2) % MOD;
        } else {
            return x * pow(x, k - 1) % MOD;
        }
    }

    private static final long MOD = (long)1e+9 + 7;
}
