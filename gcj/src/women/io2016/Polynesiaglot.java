package women.io2016;

import utils.io.*;
import java.io.*;

// Problem C
public class Polynesiaglot {
    public void process(int testCase, InputReader in, PrintWriter out) {
        int c = in.nextInt();
        int v = in.nextInt();
        int l = in.nextInt();
        out.format("Case #%d: %d\n", testCase, get(c, v, l));
    }

    private static long get(int c, int v, int l) {
        long[] C = new long[l + 1];
        long[] V = new long[l + 1];
        C[1] = c;
        V[1] = v;
        for (int i = 2; i <= l; ++i) {
            C[i] = c * V[i - 1];
            V[i] = v * V[i - 1] + v * C[i - 1];
            C[i] %= MOD;
            V[i] %= MOD;
        }
        return V[l];
    }

    private static final long MOD = (long)1e9+7;
}
