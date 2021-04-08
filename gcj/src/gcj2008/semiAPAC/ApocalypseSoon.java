package gcj2008.semiAPAC;

import utils.io.*;
import java.io.*;

// Problem B
public class ApocalypseSoon {
    public void process(int testCase, InputReader in, PrintWriter out) {
        int m = in.nextInt();
        int n = in.nextInt();
        int c = in.nextInt();
        int r = in.nextInt();
        int a[][] = new int[n + 2][m + 2];
        for (int i = 1; i <= n; ++i) {
            for (int j = 1; j <= m; ++j) {
                a[i][j] = in.nextInt();
            }
        }
        int days = doit(a, n, m, r, c);
        if (days < +oo) {
            out.format("Case #%d: %d day(s)\n", testCase, days);
        } else {
            out.format("Case #%d: %s\n", testCase, "forever");
        }
    }

    private int doit(int[][] a, int n, int m, int r, int c) {
        int b[][] = make(a, n, m, r, c);
        if (b != null) {
            if (a[r][c] - b[r][c] > 0) {
                return 1 + eval(a, b, n, m, r, c);
            }
            return 0;
        }
        return +oo;
    }

    private int eval(int[][] a, int[][] b, int n, int m, int r, int c) {
        int R = 0;
        int z[][];
        for (int k = 0; k < 4; ++k) {
            int x = r + dx[k];
            int y = c + dy[k];
            if (a[x][y] > 0) {
                z = fire(a, b, n, m);
                z[x][y] -= a[r][c];
                R = Math.max(R, doit(z, n, m, r, c));
            }
        }
        z = fire(a, b, n, m);
        return Math.max(R, doit(z, n, m, r, c));
    }

    private int[][] make(int[][] a, int n, int m, int r, int c) {
        int b[][] = null;
        for (int x = 1; x <= n; ++x) {
            for (int y = 1; y <= m; ++y) {
                if (a[x][y] > 0) {
                    if (x == r && y == c) {
                        continue;
                    }
                    int ex = -1;
                    int ey = -1;
                    for (int k = 0; k < 4; ++k) {
                        int nx = x + dx[k];
                        int ny = y + dy[k];
                        if (a[nx][ny] > 0) {
                            if (ex == -1 || a[ex][ey] < a[nx][ny]) {
                                ex = nx;
                                ey = ny;
                            }
                        }
                    }
                    if (ex != -1) {
                        if (b == null) {
                            b = new int[n + 2][m + 2];
                        }
                        b[ex][ey] += a[x][y];
                    }
                }
            }
        }
        return b;
    }

    private int[][] fire(int[][] a, int[][] b, int n, int m) {
        int[][] z = new int[n + 2][m + 2];
        for (int x = 1; x <= n; ++x) {
            for (int y = 1; y <= m; ++y) {
                z[x][y] = a[x][y] - b[x][y];
            }
        }
        return z;
    }

    private static final int dx[] = { -1, 0, 0, +1 };
    private static final int dy[] = { 0, -1, +1, 0 };
    private static final int oo = (int)1e6;
}
