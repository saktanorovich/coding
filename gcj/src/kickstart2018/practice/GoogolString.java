package kickstart2018.practice;

import utils.io.*;
import java.io.*;

// Problem B
public class GoogolString {
    public void process(int testCase, InputReader in, PrintWriter out) {
        out.format("Case #%d: %d\n", testCase, get(in.nextLong()));
    }

    private static int get(long k) {
        int n = 1;
        while ((1L << n) - 1 < k) {
            n = n + 1;
        }
        return get(n, k, 0);
    }

    private static int get(int n, long k, int b) {
        if (n == 1) {
            return b;
        }
        long m = (1L << (n - 1));
        if (k < m) {
            return get(n - 1, k, b);
        }
        if (k > m) {
            k = k - m;
            k = m - k;
            b = 1 - b;
            return get(n - 1, k, b);
        }
        return b;
    }
}
