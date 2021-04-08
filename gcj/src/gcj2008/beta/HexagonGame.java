package gcj2008.beta;

import utils.io.*;
import java.io.*;
import java.util.*;

// Problem D
public class HexagonGame {
    public void process(int testCase, InputReader in, PrintWriter out) {
        String[] ps = in.nextLine().split(" ");
        String[] vs = in.nextLine().split(" ");
        int n = ps.length;
        int m = (n + 1) / 2;
        int k = (m - 1) * (m - 1) + n * m;
        int l = -m + 1;
        int r = +m - 1;
        this.fx = new int[k + 1];
        this.fy = new int[k + 1];
        this.fz = new int[n][n];
        for (int x = l, t = 1; x <= r; ++x) {
            int c = n - Math.abs(x);
            for (int y = x <= 0 ? l : l + x; c > 0; ++y) {
                fz[x + r][y + r] = t;
                fx[t] = x;
                fy[t] = y;
                t = t + 1;
                c = c - 1;
            }
        }
        assert fx[(k + 1) / 2] == 0;
        assert fy[(k + 1) / 2] == 0;
        int p[] = new int[n];
        int v[] = new int[n];
        for (int i = 0; i < n; ++i) {
            p[i] = Integer.parseInt(ps[i]);
            v[i] = Integer.parseInt(vs[i]);
        }
        out.format("Case #%d: %d\n", testCase, get(p, v, n, m, k));
    }

    private int get(int[] p, int[] v, int n, int m, int k) {
        int res = (int)1e+6;
        int mid = (k + 1) / 2;
        res = Math.min(res, get(p, v, axis(n, m, mid, new int[] { 0, 3 }), n, m, k));
        res = Math.min(res, get(p, v, axis(n, m, mid, new int[] { 1, 4 }), n, m, k));
        res = Math.min(res, get(p, v, axis(n, m, mid, new int[] { 2, 5 }), n, m, k));
        return res;
    }

    private int get(int[] p, int[] v, int[] f, int n, int m, int k) {
        int[][] g = new int[n][n];
        for (int i = 0; i < n; ++i) {
            int[] d = bfs(m, k, p[i]);
            for (int j = 0; j < n; ++j) {
                g[i][j] = -v[i] * d[f[j]];
            }
        }
        int result = -hungarian(g, n);
        return result;
    }

    private int hungarian(int[][] g, int n) {
        this.mat1 = new int[n];
        this.mat2 = new int[n];
        this.par1 = new int[n];
        this.par2 = new int[n];
        this.pot1 = new int[n];
        this.pot2 = new int[n];
        for (int i = 0; i < n; ++i) {
            mat1[i] = -1;
            mat2[i] = -1;
            for (int j = 0; j < n; ++j) {
                pot1[i] = Math.max(pot1[i], g[i][j]);
            }
        }
        for (int cardinality = 0; cardinality < n;) {
            int unmatched = -1;
            for (int i = 0; i < n; ++i) {
                par1[i] = -1;
                par2[i] = -1;
                if (mat1[i] == -1) {
                    unmatched = i;
                }
            }
            par1[unmatched] = unmatched;
            int at = kuhn(g, n, unmatched);
            if (at != -1) {
                ++cardinality;
                for (int u2 = at, u1; true;) {
                    u1 = par2[u2];
                    mat1[u1] = u2;
                    mat2[u2] = u1;
                    if (u1 == unmatched) {
                        break;
                    }
                    u2 = par1[u1];
                }
            } else {
                int delta = (int)1e+6;
                for (int i = 0; i < n; ++i) {
                    if (par1[i] != -1) {
                        for (int j = 0; j < n; ++j) {
                            if (par2[j] == -1) {
                                delta = Math.min(delta, pot1[i] + pot2[j] - g[i][j]);
                            }
                        }
                    }
                }
                for (int i = 0; i < n; ++i) {
                    if (par1[i] != -1) {
                        pot1[i] -= delta;
                    }
                    if (par2[i] != -1) {
                        pot2[i] += delta;
                    }
                }
            }
        }
        int res = 0;
        for (int i = 0; i < n; ++i) {
            res += g[i][mat1[i]];
        }
        return res;
    }

    private int kuhn(int[][] g, int n, int u1) {
        for (int u2 = 0; u2 < n; ++u2) {
            if (pot1[u1] + pot2[u2] == g[u1][u2]) {
                if (par2[u2] == -1) {
                    par2[u2] = u1;
                    if (mat2[u2] == -1) {
                        return u2;
                    }
                    par1[mat2[u2]] = u2;
                    int result = kuhn(g, n, mat2[u2]);
                    if (result != -1) {
                        return result;
                    }
                }
            }
        }
        return -1;
    }

    private int[] bfs(int m, int k, int p) {
        int[] d = new int[k + 1];
        Arrays.fill(d, (int)1e6);
        Queue<Integer> q = new LinkedList<>();
        for (d[p] = 0, q.add(p); q.size() > 0;) {
            p = q.poll();
            int x = fx[p];
            int y = fy[p];
            for (int t = 0; t < 6; ++t) {
                int z = val(m, x + dx[t], y + dy[t]);
                if (z > 0) {
                    if (d[z] > d[p] + 1) {
                        d[z] = d[p] + 1;
                        q.add(z);
                    }
                }
            }
        }
        return d;
    }

    private int[] axis(int n, int m, int p, int[] d) {
        int[] res = new int[n];
        for (int k = 0; k < 2; ++k) {
            int x = fx[p] + k * dx[d[k]];
            int y = fy[p] + k * dy[d[k]];
            int z = val(m, x, y);
            do {
                res[--n] = z;
                x += dx[d[k]];
                y += dy[d[k]];
                z = val(m, x, y);
            } while (z > 0);
        }
        assert n == 0;
        return res;
    }

    private int val(int m, int x, int y) {
        if (-m < x && x < m && -m < y && y < m) {
            return fz[x + m - 1][y + m - 1];
        }
        return 0;
    }

    private int[] mat1;
    private int[] mat2;
    private int[] par1;
    private int[] par2;
    private int[] pot1;
    private int[] pot2;
    private int[][] fz;
    private int[] fx;
    private int[] fy;

    private static final int[] dx = {  0, -1, -1,  0, +1, +1 };
    private static final int[] dy = { -1, -1,  0, +1, +1,  0 };
}
