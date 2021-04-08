package gcj2008.semiAMER;

import utils.*;
import utils.io.*;
import java.io.*;
import java.util.*;

// Problem B
public class CodeSequence {
    public void process(int testCase, InputReader in, PrintWriter out) {
        int n = in.nextInt();
        int b[] = new int[n];
        for (int i = 0; i < n; ++i) {
            b[i] = in.nextInt();
        }
        Integer res = get(b, n);
        if (res != null) {
            out.format("Case #%d: %d\n", testCase, res);
        } else {
            out.format("Case #%d: %s\n", testCase, "UNKNOWN");
        }
    }

    private static Integer get(int[] b, int n) {
        // let's iterate over possible suffixes of the 1st element
        Set<Integer> has = new HashSet<>();
        for (int set = 0; set < 1 << BIT; ++set) {
            int[][] a = new int[n + 1][];
            for (int i = 0; i < n + 1; ++i) {
                a[i] = bits(i + set, BIT);
            }
            int c[] = sol(a, b, n);
            if (c != null) {
                Integer next = mul(a[n], c, BIT);
                if (next == null) {
                    return null;
                }
                has.add(next);
            }
        }
        if (has.size() == 1) {
            return has.iterator().next();
        }
        return null;
    }

    private static int[] sol(int[][] a, int[] b, int n) {
        int A[][] = new int[n][BIT + 1];
        for (int i = 0; i < n; ++i) {
            System.arraycopy(a[i], 0, A[i], 0, a[i].length);
            A[i][BIT] = b[i];
        }
        return gauss(A, n, BIT);
    }

    // solve linear system of equations with n equations and m unknowns
    private static int[] gauss(int[][] a, int n, int m) {
        int[] z = new int[m];
        int[] c = new int[m];
        Arrays.fill(c, -1);
        Arrays.fill(z, -1);
        for (int row = 0, col = 0; row < n && col < m; ++col) {
            for (int ind = row; ind < n; ++ind) {
                if (a[ind][col] != 0) {
                    int[] t = a[ind];
                    a[ind] = a[row];
                    a[row] = t;
                    break;
                }
            }
            if (a[row][col] == 0) {
                continue;
            }
            z[col] = row;
            for (int ind = 0; ind < n; ++ind) {
                if (row == ind || a[ind][col] == 0) {
                    continue;
                }
                int fr = a[row][col];
                int fi = a[ind][col];
                for (int k = 0; k <= m; ++k) {
                    a[row][k] *= fi;
                    a[row][k] %= MOD;
                    a[ind][k] *= fr;
                    a[ind][k] %= MOD;
                    a[ind][k] -= a[row][k];
                    a[ind][k] += MOD;
                    a[ind][k] %= MOD;
                }
            }
            ++row;
        }
        for (int x = 0; x < m; ++x) {
            if (z[x] != -1) {
                c[x] = a[z[x]][m] * inv(a[z[x]][x]) % MOD;
            }
        }
        for (int i = 0; i < n; ++i) {
            Integer sum = mul(a[i], c, m);
            if (sum == null || sum != a[i][m]) {
                return null;
            }
        }
        return c;
    }

    private static Integer mul(int[] a, int[] x, int n) {
        Integer res = 0;
        for (int i = 0; i < n; ++i) {
            if (a[i] != 0 && x[i] == -1) {
                return null;
            }
            res += a[i] * x[i];
            res %= MOD;
        }
        return res;
    }

    private static int[] bits(int x, int count) {
        int[] res = new int[count];
        for (int i = 0; i < count; ++i) {
            res[i] = (x >> i) & 1;
        }
        return res;
    }

    private static int inv(int x) {
        return pow(x, MOD - 2);
    }

    private static int pow(int x, int k) {
        if (k == 0) {
            return 1;
        } else if (k % 2 == 0) {
            return pow(x * x % MOD, k / 2);
        } else {
            return x * pow(x, k - 1) % MOD;
        }
    }

    private static final int MOD = 10007;
    private static final int BIT = 12;
}
