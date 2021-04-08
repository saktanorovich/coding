package practice.contest1;

import java.io.*;
import java.util.*;
import utils.io.*;

// Problem C
public class Cycles {
    public void process(int testCase, InputReader in, PrintWriter out) {
        int n = in.nextInt();
        int k = in.nextInt();
        int u[] = new int[k];
        int v[] = new int[k];
        for (int i = 0; i < k; ++i) {
            u[i] = in.nextInt() - 1;
            v[i] = in.nextInt() - 1;
        }
        int res = 0;
        for (int set = 0; set < 1 << k; ++set) {
            List<Integer>[] g = new ArrayList[n];
            for (int i = 0; i < n; ++i) {
                g[i] = new ArrayList<>();
            }
            int sign = 1, e = 0;
            for (int i = 0; i < k; ++i) {
                if ((set & (1 << i)) != 0) {
                    g[u[i]].add(v[i]);
                    g[v[i]].add(u[i]);
                    sign = -sign;
                    e = e + 1;
                }
            }
            res += sign * get(g, n, e);
            res += MOD;
            res %= MOD;
        }
        out.format("Case #%d: %d\n", testCase, res);
    }

    // Number of Hamiltonian cycles in a complete undirected graph
    // with n nodes is equal to (n-1)! / 2.
    private static int get(List<Integer>[] g, int n, int e) {
        int cnt[] = new int[3];
        for (int i = 0; i < n; ++i) {
            if (g[i].size() > 2) {
                return 0;
            }
            ++cnt[g[i].size()];
        }
        int chains = cnt[1] / 2;
        int cycles = 0;
        for (int i = 0; i < n; ++i) {
            if (g[i].size() > 0) {
                cycles += dfs(g, i, i, new boolean[n]);
            }
        }
        if (cycles > 0) {
            if (cycles == 1) {
                return cnt[2] / n;
            } else {
                return 0;
            }
        }
        if (chains > 0) {
            // Number of positions to put chains is equal to r=n-e-1. Number of
            // arrangements of k chains into r positions is r!/k!. So number
            // of permutations is 2^k*k!*r!/k! = 2^k*r!.
            int res = pow2[chains];
            res *= fact[n - e - 1];
            res %= MOD;
            res *= inv(2);
            res %= MOD;
            return res;
        }
        return div2[n - 1];
    }

    private static int dfs(List<Integer>[] g, int curr, int from, boolean[] was) {
        was[curr] = true;
        int cycle = 0;
        for (int next : g[curr]) {
            if (was[next] == false) {
                cycle |= dfs(g, next, curr, was);
            } else if (next != from){
                cycle |= 1;
            }
        }
        g[curr].clear();
        return cycle;
    }

    private static int inv(int x) {
        return pow(x, MOD - 2);
    }

    private static int pow(int x, int k) {
        if (k == 0) {
            return 1;
        } else if (k % 2 == 0) {
            return pow(x * x % MOD, k / 2) % MOD;
        } else {
            return x * pow(x, k - 1) % MOD;
        }
    }

    private static final int MOD = 9901;
    private static final int MAX = 300;

    private static final int[] pow2;
    private static final int[] fact;
    private static final int[] div2;
    static {
        pow2 = new int[MAX + 1];
        fact = new int[MAX + 1];
        div2 = new int[MAX + 1];
        pow2[0] = 1;
        pow2[1] = 2;
        fact[0] = 1;
        fact[1] = 1;
        div2[1] = 1;
        for (int i = 2, inv2 = inv(2); i <= MAX; ++i) {
            pow2[i] = pow2[i - 1] * 2 % MOD;
            fact[i] = fact[i - 1] * i % MOD;
            div2[i] = fact[i];
            div2[i] *= inv2;
            div2[i] %= MOD;
        }
    }
}
