package kickstart2017.rounde;

import utils.io.*;
import java.io.*;
import java.util.*;

// Problem A
public class CopyPaste {
    public void process(int testCase, InputReader in, PrintWriter out) {
        this.s = in.next();
        this.n = s.length();
        this.e = new boolean[n][n][n];
        for (int i = 0; i < n; ++i) {
            for (int j = i + 1; j < n; ++j) {
                e[i][j][1] = s.charAt(i) == s.charAt(j);
            }
        }
        for (int k = 2; k < n; ++k) {
            for (int i = 0; i + k <= n; ++i) {
                for (int j = i + k; j + k <= n; ++j) {
                    if (e[i][j][k - 1]) {
                        e[i][j][k] = e[i + k - 1][j + k - 1][1];
                    }
                }
            }
        }
        this.f = new int[n][n][n + 1];
        for (int i = 0; i < n; ++i) {
            for (int j = 0; j < n; ++j) {
                Arrays.fill(f[i][j], Integer.MAX_VALUE);
            }
        }
        f[0][0][0] = 1;
        for (int i = 0; i < n; ++i) {
            int best = Integer.MAX_VALUE;
            for (int j = 0; j <= i; ++j) {
                for (int k = 0; j + k - 1 <= i; ++k) {
                    best = Math.min(best, f[i][j][k]);
                }
            }
            if (i + 1 == n) {
                out.format("Case #%d: %d\n", testCase, best);
                return;
            }
            for (int j = 0; j <= i; ++j) {
                for (int k = 1; j + k - 1 <= i; ++k) {
                    f[i][j][k] = Math.min(f[i][j][k], best + 1);
                }
            }
            for (int j = 0; j <= i; ++j) {
                for (int c = 0; j + c - 1 <= i; ++c) {
                    if (f[i][j][c] < Integer.MAX_VALUE) {
                        if (e[j][i + 1][c]) {
                            f[i + c][j][c] = Math.min(f[i + c][j][c], f[i][j][c] + 1);
                        }
                        f[i + 1][j][c] = Math.min(f[i + 1][j][c], f[i][j][c] + 1);
                    }
                }
            }
        }
    }

    private String s;
    private boolean e[][][];
    private int f[][][];
    private int n;
}
