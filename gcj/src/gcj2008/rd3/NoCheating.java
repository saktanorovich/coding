package gcj2008.rd3;

import utils.io.*;
import java.io.*;
import java.util.*;

// Problem C
public class NoCheating {
    public void process(int testCase, InputReader in, PrintWriter out) {
        this.n = in.nextInt();
        this.m = in.nextInt();
        this.k = 0;
        this.f = new int[n][m];
        for (int i = 0; i < n; ++i) {
            String s = in.next();
            for (int j = 0; j < s.length(); ++j) {
                f[i][j] = -1;
                if (s.charAt(j) == '.') {
                    f[i][j] = k++;
                }
            }
        }
        this.g = new HashSet[k];
        for (int i = 0; i < k; ++i) {
            g[i] = new HashSet<>();
        }
        for (int i = 0; i < n; ++i) {
            for (int j = 0; j < m; ++j) {
                add(i, j, i, j - 1);
                add(i, j, i, j + 1);
                add(i, j, i - 1, j - 1);
                add(i, j, i - 1, j + 1);
            }
        }
        // apply König's theorem to find maximum independent set
        out.format("Case #%d: %d\n", testCase, k - kuhn(g, k));
    }

    private void add(int i1, int j1, int i2, int j2) {
        boolean inside = true;
        inside &= 0 <= i1 && i1 < n && 0 <= j1 && j1 < m;
        inside &= 0 <= i2 && i2 < n && 0 <= j2 && j2 < m;
        if (inside) {
            int a = f[i1][j1];
            int b = f[i2][j2];
            if (a != -1 && b != -1) {
                g[a].add(b);
                g[b].add(a);
            }
        }
    }

    private Set<Integer>[] g;
    private int f[][];
    private int n;
    private int m;
    private int k;

    private static int kuhn(Set<Integer>[] g, int n) {
        //verify(g, n);
        int m[] = new int[n];
        Arrays.fill(m, -1);
        int cardinality = 0;
        while (augment(g, m, n)) {
            cardinality = cardinality + 1;
        }
        return cardinality;
    }

    private static boolean augment(Set<Integer>[] g, int[] m, int n) {
        boolean[] was = new boolean[n];
        for (int u = 0; u < n; ++u) {
            if (m[u] == -1) {
                if (dfs(g, u, m, was)) {
                    return true;
                }
            }
        }
        return false;
    }

    private static boolean dfs(Set<Integer>[] g, int u1, int[] m, boolean[] was) {
        if (was[u1]) {
            return false;
        }
        was[u1] = true;
        for (int u2 : g[u1]) {
            if (m[u2] == -1 || dfs(g, m[u2], m, was)) {
                m[u1] = u2;
                m[u2] = u1;
                return true;
            }
        }
        return false;
    }

    private static void verify(Set<Integer>[] g, int n) {
        int[] part = new int[n];
        Arrays.fill(part, -1);
        for (int i = 0; i < n; ++i) {
            if (part[i] == -1) {
                part[i] = 0;
                Queue<Integer> q = new LinkedList<>();
                for (q.add(i); q.size() > 0;) {
                    int x = q.poll();
                    int p = 1 - part[x];
                    for (int y : g[x]) {
                        if (part[y] == -1) {
                            part[y] = p;
                            q.add(y);
                        } else if (part[y] != p) {
                            throw new RuntimeException();
                        }
                    }
                }
            }
        }
    }
}
