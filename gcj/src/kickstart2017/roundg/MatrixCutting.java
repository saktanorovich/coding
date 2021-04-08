package kickstart2017.roundg;

import utils.io.*;
import java.io.*;

// Problem C
public class MatrixCutting {
    public void process(int testCase, InputReader in, PrintWriter out) {
        this.n = in.nextInt();
        this.m = in.nextInt();
        this.a = new int[n][m];
        for (int i = 0; i < n; ++i) {
            for (int j = 0; j < m; ++j) {
                a[i][j] = in.nextInt();
            }
        }
        this.qmin = new int[n][m][n][m];
        for (int i0 = 0; i0 < n; ++i0) {
            for (int j0 = 0; j0 < m; ++j0) {
                for (int i1 = i0; i1 < n; ++i1) {
                    for (int j1 = j0; j1 < m; ++j1) {
                        qmin[i0][j0][i1][j1] = a[i1][j1];
                        if (j1 > j0) {
                            qmin[i0][j0][i1][j1] = Math.min(qmin[i0][j0][i1][j1], qmin[i0][j0][i1][j1 - 1]);
                        }
                        if (i1 > i0) {
                            qmin[i0][j0][i1][j1] = Math.min(qmin[i0][j0][i1][j1], qmin[i0][j0][i1 - 1][j1]);
                        }
                    }
                }
            }
        }
        this.memo = new int[n][m][n][m];
        for (int i0 = 0; i0 < n; ++i0) {
            for (int j0 = 0; j0 < m; ++j0) {
                for (int i1 = i0; i1 < n; ++i1) {
                    for (int j1 = j0; j1 < m; ++j1) {
                        memo[i0][j0][i1][j1] = -1;
                    }
                }
                memo[i0][j0][i0][j0] = 0;
            }
        }
        out.format("Case #%d: %d\n", testCase, doit(0, 0, n - 1, m - 1));
    }

    private int doit(int i0, int j0, int i1, int j1) {
        if (memo[i0][j0][i1][j1] == - 1) {
            int best = 0;
            for (int ix = i0; ix < i1; ++ix) {
                best = Math.max(best, doit(i0, j0, ix, j1) + doit(ix + 1, j0, i1, j1));
            }
            for (int jx = j0; jx < j1; ++jx) {
                best = Math.max(best, doit(i0, j0, i1, jx) + doit(i0, jx + 1, i1, j1));
            }
            memo[i0][j0][i1][j1] = best + qmin[i0][j0][i1][j1];
        }
        return memo[i0][j0][i1][j1];
    }

    private int memo[][][][];
    private int qmin[][][][];
    private int a[][];
    private int n, m;
}
