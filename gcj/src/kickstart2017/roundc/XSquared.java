package kickstart2017.roundc;

import utils.io.*;
import java.io.*;

// Problem B
public class XSquared {
    public void process(int testCase, InputReader in, PrintWriter out) {
        int n = in.nextInt();
        int f[][] = new int[n][n];
        for (int i = 0; i < n; ++i) {
            String s = in.next();
            for (int j = 0; j < n; ++j) {
                f[i][j] = ".X".indexOf(s.charAt(j));
            }
        }
        if (doit(f, n)) {
            out.format("Case #%d: %s\n", testCase, "POSSIBLE");
        } else {
            out.format("Case #%d: %s\n", testCase, "IMPOSSIBLE");
        }
    }

    private static boolean doit(int[][] f, int n) {
        int r[] = new int[n];
        int c[] = new int[n];
        for (int i = 0; i < n; ++i) {
            for (int j = 0; j < n; ++j) {
                r[i] += f[i][j];
                c[j] += f[i][j];
            }
        }
        for (int i = 0; i < n; ++i) {
            if (0 < r[i] && r[i] < 3 && 0 < c[i] && c[i] < 3) {
            } else {
                return false;
            }
        }
        int one = 0, x = 0, y = 0;
        for (int i = 0; i < n; ++i) {
            for (int j = 0; j < n; ++j) {
                if (f[i][j] == 1) {
                    if (r[i] == 1 && c[j] == 1) {
                        one = one + 1;
                        x = i;
                        y = j;
                    } else if (r[i] == 1) {
                        return false;
                    } else if (c[j] == 1) {
                        return false;
                    }
                }
            }
        }
        if (one != 1) {
            return false;
        }
        rswap(f, n, x, n / 2);
        cswap(f, n, y, n / 2);
        for (int t = 0, b = n - 1; t <= n / 2; ++t, --b) {
            for (int k = 0; k < 2; ++k) {
                int z = t * (1 - k) + k * b;
                if (f[t][z] != 1) {
                    for (int j = t + 1; j < n; ++j) {
                        if (f[t][j] == 1) {
                            cswap(f, n, z, j);
                            break;
                        }
                    }
                }
            }
            for (int k = 0; k < 2; ++k) {
                int z = t * (1 - k) + k * b;
                if (f[b][z] != 1) {
                    for (int j = t + 1; j < n; ++j) {
                        if (f[j][z] == 1) {
                            rswap(f, n, j, b);
                            break;
                        }
                    }
                }
            }
            if (f[t][t] != f[b][t]) return false;
            if (f[t][b] != f[b][b]) return false;
        }
        return true;
    }

    private static void rswap(int[][] f, int n, int r1, int r2) {
        int[] r = f[r1];
        f[r1] = f[r2];
        f[r2] = r;
    }

    private static void cswap(int[][] f, int n, int c1, int c2) {
        for (int i = 0; i < n; ++i) {
            int c = f[i][c1];
            f[i][c1] = f[i][c2];
            f[i][c2] = c;
        }
    }
}
