package gcj2008.rd2;

import utils.io.*;
import java.io.*;
import java.util.*;

// Problem D
public class PermRLE {
    public void process(int testCase, InputReader in, PrintWriter out) {
        this.k = in.nextInt();
        this.s = in.next();
        this.n = s.length() / k;
        this.g = new int[k][k];
        // let's iterate over each block and build distance table between chars
        for (int b = 0; b < n; ++b) {
            for (int x = 0; x < k; ++x) {
                for (int y = x + 1; y < k; ++y) {
                    if (s.charAt(b * k + x) != s.charAt(b * k + y)) {
                        ++g[x][y];
                        ++g[y][x];
                    }
                }
            }
        }
        int res = (int)1e6;
        memo = new int[1 << k][k];
        for (int fn = 0; fn < k; ++fn) {
            for (int set = 0; set < 1 << k; ++set) {
                Arrays.fill(memo[set], -1);
            }
            for (int st = 0; st < k; ++st) {
                if (fn == st) {
                    continue;
                }
                // let's bind blocks to each other
                int cst = 0;
                for (int b = 0; b < n - 1; ++b) {
                    int x = fn + b * k;
                    int y = st + b * k + k;
                    if (s.charAt(x) != s.charAt(y)) {
                        ++cst;
                    }
                }
                // let's build hamilton path from st to fn
                res = Math.min(res, hamilton(st, fn) + cst);
            }
        }
        out.format("Case #%d: %d\n", testCase, res + 1);
    }

    private int hamilton(int st, int fn) {
        return hamilton(((1 << k) - 1) ^ (1 << st), st, fn);
    }

    private int hamilton(int set, int st, int fn) {
        if (st == fn) {
            return set == 0 ? 0 : (int)1e6;
        }
        if (memo[set][st] == -1) {
            memo[set][st] = (int)1e6;
            for (int nx = 0; nx < k; ++nx) {
                if ((set & (1 << nx)) != 0) {
                    memo[set][st] = Math.min(memo[set][st], hamilton(set ^ (1 << nx), nx, fn) + g[st][nx]);
                }
            }
        }
        return memo[set][st];
    }

    private int[][] memo;
    private int[][] g;
    private int n, k;
    private String s;
}
