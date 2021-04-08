package gcj2008.rd1c;

import utils.io.*;
import java.io.*;
import java.util.*;

// Problem C
public class IncreasingSpeedLimits {
    public void process(int testCase, InputReader in, PrintWriter out) {
        this.n = in.nextInt();
        this.m = in.nextInt();
        long X = in.nextInt();
        long Y = in.nextInt();
        long Z = in.nextInt();
        long A[] = new long[m];
        for (int i = 0; i < m; ++i) {
            A[i] = in.nextInt();
        }
        int[] s = new int[n];
        for (int i = 0; i < n; ++i) {
            int j = i % m;
            s[i] = (int)A[j];
            A[j] = (X * A[j] + Y * (i + 1)) % Z;
        }
        out.format("Case #%d: %d\n", testCase, get(s));
    }

    private long get(int[] s) {
        int[] a = Arrays.copyOf(s, n);
        Arrays.sort(a);
        this.set = new HashMap<>();
        for (int i = 0, k = 0; i < n; ++i) {
            if (set.containsKey(a[i]) == false) {
                set.put(a[i], ++k);
            }
        }
        for (int i = 0; i < n; ++i) {
            s[i] = set.get(s[i]);
        }
        this.cumul = new long[n + 1];
        long res = 0;
        for (int i = 0; i < n; ++i) {
            long add = sum(s[i] - 1);
            res += add;
            res %= MOD;
            inc(s[i], add);
        }
        return res;
    }

    private long sum(int x) {
        long res = 1;
        for (; x > 0; x -= x & (-x)) {
            res += cumul[x];
            res %= MOD;
        }
        return res;
    }

    private void inc(int x, long add) {
        for (; x <= n; x += x & (-x)) {
            cumul[x] += add;
            cumul[x] %= MOD;
        }
    }

    private Map<Integer, Integer> set;
    private long[] cumul;
    private int n, m;

    private static final long MOD = (long)1e9 + 7;
}
