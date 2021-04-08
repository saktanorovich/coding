package kickstart2017.roundg;

import utils.io.*;
import java.io.*;

// Problem A
public class HugeNumbers {
    public void process(int testCase, InputReader in, PrintWriter out) {
        long a = in.nextInt();
        long n = in.nextInt();
        long p = in.nextInt();
        for (int i = 1; i <= n; ++i) {
            a = pow(a, i, p);
        }
        out.format("Case #%d: %d\n", testCase, a);
    }

    private static long pow(long x, long k, long mod) {
        if (k == 0) {
            return 1 % mod;
        } else if (k % 2 == 0) {
            return pow(x * x % mod, k / 2, mod);
        } else {
            return x * pow(x, k - 1, mod) % mod;
        }
    }
}
