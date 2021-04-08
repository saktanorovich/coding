package kickstart2017.roundb;

import utils.io.*;
import java.io.*;

// Problem A
public class MathEncoder {
    public void process(int testCase, InputReader in, PrintWriter out) {
        int n = in.nextInt();
        int a[] = new int[n];
        for (int i = 0; i < n; ++i) {
            a[i] = in.nextInt();
        }
        long res = 0;
        for (int i = 0; i < n; ++i) {
            long count = pow(2, i) - pow(2, n - 1 - i);
            count += MOD;
            count %= MOD;
            count *= a[i];
            res += count;
            res %= MOD;
        }
        out.format("Case #%d: %d\n", testCase, res);
    }

    private static long pow(long x, int k) {
        if (k == 0) {
            return 1;
        } else if (k % 2 == 0) {
            return pow(x * x % MOD, k / 2);
        } else {
            return x * pow(x, k - 1) % MOD;
        }
    }
    private static final long MOD = (long)1e9 + 7;
}
