package gcj2008.rd2;

import java.io.*;
import utils.io.*;

// Problem A
public class CheatingBooleanTree {
    public void process(int testCase, InputReader in, PrintWriter out) {
        int n = in.nextInt();
        int v = in.nextInt();
        int g[] = new int[n];
        int c[] = new int[n];
        int x[] = new int[n];
        int m = (n - 1) / 2;
        int f[][] = new int[2][n];
        for (int i = 0; i < m; ++i) {
            g[i] = in.nextInt();
            c[i] = in.nextInt();
            f[0][i] = (int)1e+6;
            f[1][i] = (int)1e+6;
        }
        for (int i = m; i < n; ++i) {
            x[i] = in.nextInt();
            if (x[i] == 0) {
                f[1][i] = (int)1e+6;
            } else {
                f[0][i] = (int)1e+6;
            }
        }
        for (int i = m - 1; i >= 0; --i) {
            for (int l = 0; l < 2; ++l) {
                for (int r = 0; r < 2; ++r) {
                    int a = f[l][2 * i + 1]; 
                    int b = f[r][2 * i + 2];
                    int w = g[i];
                    for (int k = 0; k <= c[i]; ++k) {
                        int d = apply(w, l, r);
                        f[d][i] = Math.min(f[d][i], a + b + k);
                        w = 1 - w;
                    }
                }
            }
        }
        if (f[v][0] < (int)1e+6) {
            out.format("Case #%d: %d\n", testCase, f[v][0]);
        } else {
            out.format("Case #%d: %s\n", testCase, "IMPOSSIBLE");
        }
    }

    private static int apply(int f, int a, int b) {
        if (f == 1) {
            return a & b;
        } else {
            return a | b;
        }
    }
}
