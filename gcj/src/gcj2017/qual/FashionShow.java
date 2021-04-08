package gcj2017.qual;

import utils.io.*;
import java.io.*;
import java.util.*;

// Problem D
public class FashionShow {
    public void process(int testCase, InputReader in, PrintWriter out) {
        int n = in.nextInt();
        int m = in.nextInt();
        char[][] f = new char[n][n];
        for (int i = 0; i < n; ++i) {
            Arrays.fill(f[i], '.');
        }
        for (int i = 0; i < m; ++i) {
            char x = in.next().charAt(0);
            int r = in.nextInt() - 1;
            int c = in.nextInt() - 1;
            f[r][c] = x;
        }
        char[][] g = get(f, n);
        int score = 0;
        int count = 0;
        for (int i = 0 ; i < n; ++i) {
            for (int j = 0; j < n; ++j) {
                if (g[i][j] == '.') {
                    assert f[i][j] == '.';
                }

                if (g[i][j] == 'x') score = score + 1;
                if (g[i][j] == '+') score = score + 1;
                if (g[i][j] == 'o') score = score + 2;

                if (g[i][j] != f[i][j]) {
                    count = count + 1;
                }
            }
        }
        out.format("Case #%d: %d %d\n", testCase, score, count);
        for (int i = 0 ; i < n; ++i) {
            for (int j = 0; j < n; ++j) {
                if (g[i][j] != f[i][j]) {
                    out.format("%s %d %d\n", g[i][j], i + 1, j + 1);
                }
            }
        }
    }

    // 1) From problem statement we know that:
    //      * no (x, x) nor (x, o) nor (o, o) can be put in the same row/column;
    //      * no (+, +) nor (+, o) nor (o, o) can be put in the same diagonal.
    // 2) We can conclude that:
    //      * we can put only one x or o in each row/column;
    //      * we can put only one + or o in each diagonal.
    // 3) If we put o in a cell (i, j) no x can be put in corresponding
    //    row/column and no + can be put in corresponding diagonal. That
    //    means that instead of o we can put x and + at the same cell (i, j).
    //
    // From (1)-(3) we can conclude that (x,+) pair can be put into the same
    // row/column/diagonal. So we can try to find optimal x and + arrangements
    // on the initial grid and then combine the results.
    private static char[][] get(char[][] f, int n) {
        char[][] f1 = r2c(f, n, 'x');
        char[][] f2 = d2d(f, n, '+');
        char[][] f3 = new char[n][n];
        for (int i = 0; i < n; ++i) {
            for (int j = 0; j < n; ++j) {
                if (f1[i][j] != '.' && f2[i][j] != '.') {
                    f3[i][j] = 'o';
                } else if (f1[i][j] != '.') {
                    f3[i][j] = 'x';
                } else if (f2[i][j] != '.') {
                    f3[i][j] = '+';
                } else {
                    f3[i][j] = '.';
                }
            }
        }
        return f3;
    }

    private static char[][] r2c(char[][] f, int n, char w) {
        Set<Integer> g1 = new HashSet<>();
        Set<Integer> g2 = new HashSet<>();
        for (int i = 0; i < n; ++i) {
            g1.add(i);
            g2.add(i);
        }
        char[][] g = new char[n][n];
        for (int i = 0; i < n; ++i) {
            for (int j = 0; j < n; ++j) {
                g[i][j] = '.';
                if (f[i][j] == w || f[i][j] == 'o') {
                    g[i][j] = w;
                    g1.remove(i);
                    g2.remove(j);
                }
            }
        }
        Iterator<Integer> i1 = g1.iterator();
        Iterator<Integer> i2 = g2.iterator();
        while (i1.hasNext() && i2.hasNext()) {
            g[i1.next()][i2.next()] = w;
        }
        printf(g, n);
        return g;
    }

    private static char[][] d2d(char[][] f, int n, char w) {
        int m = 2 * n - 1;
        int[][] d1 = new int[n][n];
        int[][] d2 = new int[n][n];
        Set<Integer> g1 = new HashSet<>();
        Set<Integer> g2 = new HashSet<>();
        for (int d = 0; d < m; ++d) {
            for (int i = 0; i < n; ++i) {
                int j = d - i;
                if (j < n && j >= 0) {
                    int i1 = i;
                    int j1 = j;
                    int i2 = n - 1 - j;
                    int j2 = i;
                    d1[i1][j1] = d;
                    d2[i2][j2] = d;
                }
            }
            g1.add(d);
            g2.add(d);
        }
        char[][] g = new char[n][n];
        for (int i = 0; i < n; ++i) {
            for (int j = 0; j < n; ++j) {
                g[i][j] = '.';
                if (f[i][j] == w || f[i][j] == 'o') {
                    g[i][j] = w;
                    g1.remove(d1[i][j]);
                    g2.remove(d2[i][j]);
                }
            }
        }
        List<Integer>[] b = new ArrayList[m];
        for (int i = 0; i < m; ++i) {
            b[i] = new ArrayList<>();
        }
        for (int i = 0; i < n; ++i) {
            for (int j = 0; j < n; ++j) {
                int o1 = d1[i][j];
                int o2 = d2[i][j];
                if (g1.contains(o1) && g2.contains(o2)) {
                    b[o1].add(o2);
                }
            }
        }
        int[] h = kuhn(b, m, m);
        for (int k = 0; k < m; ++k) {
            contd: if (h[k] != -1) {
                for (int i = 0; i < n; ++i) {
                    for (int j = 0; j < n; ++j) {
                        if (d1[i][j] == k && d2[i][j] == h[k]) {
                            g[i][j] = w;
                            break contd;
                        }
                    }
                }

            }
        }
        printf(g, n);
        return g;
    }

    private static int[] kuhn(List<Integer>[] g, int n, int m) {
        int m1[] = new int[n];
        int m2[] = new int[m];
        Arrays.fill(m1, -1);
        Arrays.fill(m2, -1);
        while (augment(g, m1, m2)) {
            // cardinality = cardinality + 1
        }
        return m1;
    }

    private static boolean augment(List<Integer>[] g, int[] m1, int[] m2) {
        boolean[] was = new boolean[m1.length];
        for (int u1 = 0; u1 < m1.length; ++u1) {
            if (m1[u1] == -1) {
                if (dfs(g, u1, m1, m2, was)) {
                    return true;
                }
            }
        }
        return false;
    }

    private static boolean dfs(List<Integer>[] g, int u1, int[] m1, int[] m2, boolean[] was) {
        if (was[u1]) {
            return false;
        }
        was[u1] = true;
        for (int u2 : g[u1]) {
            if (m2[u2] == -1 || dfs(g, m2[u2], m1, m2, was)) {
                m1[u1] = u2;
                m2[u2] = u1;
                return true;
            }
        }
        return false;
    }

    private static void printf(char[][] f, int n) {
        for (int i = 0; i < n; ++i) {
            for (int j = 0; j < n; ++j) {
                System.out.print(f[i][j]);
            }
            System.out.println();
        }
    }
}
