package kickstart2017.roundc;

import utils.io.*;
import java.io.*;
import java.util.*;

// Problem C
public class MagicalThinking {
    public void process(int testCase, InputReader in, PrintWriter out) {
        this.n = in.nextInt();
        this.q = in.nextInt();
        this.answer = new int[n + 1][q];
        this.scores = new int[n];
        for (int i = 0; i <= n; ++i) {
            String s = in.next();
            for (int j = 0; j < q; ++j) {
                answer[i][j] = "FT".indexOf(s.charAt(j));
            }
        }
        for (int i = 0; i < n; ++i) {
            scores[i] = in.nextInt();
        }
        this.memory = new int[q + 1][(q + 1) * (q + 1)];
        for (int i = 0; i < q; ++i) {
            Arrays.fill(memory[i], -1);
        }
        Arrays.fill(memory[q], -100);
        memory[q][0] = 0;
        out.format("Case #%d: %d\n", testCase, doit(0));
    }

    private int doit(int z) {
        int s = state();
        if (memory[z][s] == -1) {
            int res = -100;
            for (int a = 0; a < 2; ++a) {
                int val = answer[n][z] ^ a ^ 1;
                update(z, val, -1);
                if (valid()) {
                    res = Math.max(res, a + doit(z + 1));
                }
                update(z, val, +1);
            }
            memory[z][s] = res;
        }
        return memory[z][s];
    }

    private void update(int z, int val, int inc) {
        for (int i = 0; i < n; ++i) {
            if (answer[i][z] == val) {
                scores[i] += inc;
            }
        }
    }

    private boolean valid() {
        for (int i = 0; i < n; ++i) {
            if (scores[i] < 0) {
                return false;
            }
        }
        return true;
    }

    private int state() {
        int res = 0;
        for (int x : scores) {
            res *= q + 1;
            res += x;
        }
        return res;
    }

    private int memory[][];
    private int answer[][];
    private int scores[];
    private int n;
    private int q;
}
