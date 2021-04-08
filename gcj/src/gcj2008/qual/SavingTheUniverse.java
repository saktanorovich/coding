package gcj2008.qual;

import utils.io.*;
import java.io.*;
import java.util.Arrays;

// Problem A
public class SavingTheUniverse {
    public void process(int testCase, InputReader in, PrintWriter out) {
        this.engines = new String[in.nextInt()];
        for (int i = 0; i < engines.length; ++i) {
            engines[i] = in.nextLine();
        }
        this.queries = new String[in.nextInt()];
        for (int i = 0; i < queries.length; ++i) {
            queries[i] = in.nextLine();
        }
        this.memo = new int[engines.length][queries.length + 1];
        for (int e = 0; e < engines.length; ++e) {
            Arrays.fill(memo[e], -1);
            memo[e][queries.length] = 0;
        }
        int res = (int)1e6;
        for (int e = 0; e < engines.length; ++e) {
            res = Math.min(res, doit(e, 0));
        }
        out.format("Case #%d: %d\n", testCase, res);
    }

    private int doit(int e, int q) {
        if (memo[e][q] == -1) {
            if (engines[e].equals(queries[q])) {
                memo[e][q] = (int)1e6;
                for (int k = 0; k < engines.length; ++k) {
                    memo[e][q] = Math.min(memo[e][q], doit(k, q) + 1);
                }
            } else {
                memo[e][q] = doit(e, q + 1);
            }
        }
        return memo[e][q];
    }

    private String[] engines;
    private String[] queries;
    private int[][] memo;
}
