package blitz2017.qual;

import utils.io.*;
import java.io.*;
import java.util.*;

// All except one
public class Problem10 {
    public boolean process(int testCase, InputReader in, PrintWriter out) {
        int n = in.nextInt();
        int m = in.nextInt();
        int s[][] = new int[m][n];
        for (int i = 0; i < m; ++i) {
            for (int j = 0; j < n; ++j) {
                s[i][j] = in.nextInt() - 1;
            }
        }
        int k = in.nextInt();
        int Q[][] = new int[k][];
        for (int i = 0; i < k; ++i) {
            Q[i] = s[in.nextInt() - 1];
        }
        int P[][] = new int[k + 2][];
        int S[][] = new int[k + 2][];
        Arrays.fill(P, identity(n));
        Arrays.fill(S, identity(n));
        for (int i = 1; i <= k; ++i) {
            P[i] = apply(P[i], P[i - 1], n);
            P[i] = apply(P[i], Q[i - 1], n);
        }
        for (int i = k; i >= 1; --i) {
            S[i] = apply(S[i], Q[i - 1], n);
            S[i] = apply(S[i], S[i + 1], n);
        }
        for (int i = 1; i <= k; ++i) {
            int a[] = identity(n);
            a = apply(a, P[i - 1], n);
            a = apply(a, S[i + 1], n);
            for (int j = 0; j < n; ++j) {
                if (a[j] == 0) {
                    out.println(j + 1);
                }
            }
        }
        return true;
    }

    private int[] apply(int[] a, int[] p, int n) {
        int[] r = new int[n];
        for (int i = 0; i < n; ++i) {
            r[p[i]] = a[i];
        }
        return r;
    }

    private int[] identity(int n) {
        int[] a = new int[n];
        for (int i = 0; i < n; ++i) {
            a[i] = i;
        }
        return a;
    }
}
