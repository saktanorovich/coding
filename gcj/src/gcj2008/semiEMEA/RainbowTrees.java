package gcj2008.semiEMEA;

import utils.io.*;
import java.io.*;
import java.util.*;

// Problem C
public class RainbowTrees {
    public void process(int testCase, InputReader in, PrintWriter out) {
        this.n = in.nextInt();
        this.k = in.nextInt();
        this.t = new ArrayList[n];
        for (int i = 0; i < n; ++i) {
            t[i] = new ArrayList<>();
        }
        for (int i = 0; i < n - 1; ++i) {
            int x = in.nextInt() - 1;
            int y = in.nextInt() - 1;
            t[x].add(y);
            t[y].add(x);
        }
        out.format("Case #%d: %d\n", testCase, dfs(0, 0, k, 0));
    }

    // note that all children will be assigned different colors that means
    // having c children allows to color their subtrees into k-c-a colors
    // where a equals to 1 if we have incoming edge to the root
    private long dfs(int u, int p, int h, int a) {
        long res = 1; int c = 0;
        for (int v : t[u]) {
            if (v != p) {
                res *= h - c;
                res %= MOD;
                c = c + 1;
            }
        }
        for (int v : t[u]) {
            if (v != p) {
                res *= dfs(v, u, k - c - a, 1);
                res %= MOD;
            }
        }
        return res;
    }

    private final long MOD = (long)1e9 + 9;
    private List<Integer>[] t;
    private int n;
    private int k;
}
