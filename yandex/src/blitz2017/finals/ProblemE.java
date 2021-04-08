package blitz2017.finals;

import java.io.*;
import java.util.*;
import utils.io.*;

public class ProblemE {
    public boolean process(int testCase, InputReader in, PrintWriter out) {
        Solver solver = new Solver(in.next());
        int k = in.nextInt();
        for (int i = 0; i < k; ++i) {
            int cmd = in.nextInt();
            int pos = in.nextInt();
            if (cmd == 1) {
                solver.set(pos, in.next().charAt(0));
            } else {
                if (solver.get(pos)) {
                    out.println("YES");
                } else {
                    out.println("NO");
                }
            }
        }
        return true;
    }

    private class Solver {
        private int next[][] = {
            new int[] { 0, 1, 0 }, // I
            new int[] { 0, 1, 2 }, // /
            new int[] { 2, 2, 3 }, // /*
            new int[] { 2, 4, 3 }, // *
            new int[] { 0, 1, 0 }, // */
        };
        private int mark[][] = {
            new int[] { 0, 0, 0 }, // I
            new int[] { 0, 0, 1 }, // /
            new int[] { 1, 1, 1 }, // /*
            new int[] { 1, 1, 1 }, // *
            new int[] { 1, 1, 1 }, // */
        };
        private int state[][][];
        private int chars[];

        public Solver(String s) {
            chars = new int[s.length() + 2];
            for (int i = 0; i < s.length(); ++i) {
                chars[i + 1] = "./*".indexOf(s.charAt(i));
            }
            state = new int[4 * chars.length][][];
            for (int i = 0; i < state.length; ++i) {
                state[i] = new int[5][];
                for (int j = 0; j < 5; ++j) {
                    state[i][j] = new int[2];
                }
            }
            build(0, 0, chars.length - 1);
        }

        public void set(int pos, char c) {
            set(0, 0, chars.length - 1, pos, c);
        }

        private void set(int node, int lo, int hi, int pos, char c) {
            if (lo == hi) {
                chars[pos] = "./*".indexOf(c);
            } else {
                int xx = (lo + hi) / 2;
                if (pos <= xx) {
                    set(2 * node + 1, lo, xx, pos, c);
                } else {
                    set(2 * node + 2, xx + 1, hi, pos, c);
                }
                update(node, xx);
            }
        }

        public boolean get(int pos) {
            return mark[get(0, 0, 0, chars.length - 1, pos)][chars[pos + 1]] > 0;
        }

        private int get(int node, int init, int lo, int hi, int pos) {
            if (lo == pos) {
                return state[node][init][0];
            }
            if (hi == pos) {
                return state[node][init][1];
            } else {
                int xx = (lo + hi) / 2;
                if (pos <= xx) {
                    return get(2 * node + 1, init, lo, xx, pos);
                } else {
                    int l[] = state[2 * node + 1][init];
                    int t = next[l[1]][chars[xx + 1]];
                    int r[] = state[2 * node + 2][t];
                    return get(2 * node + 2, r[0], xx + 1, hi, pos);
                }
            }
        }

        private void build(int node, int lo, int hi) {
            if (lo == hi) {
                for (int s = 0; s < 5; ++s) {
                    state[node][s][0] = s;
                    state[node][s][1] = s;
                }
            } else {
                int xx = (lo + hi) / 2;
                build(2 * node + 1, lo, xx);
                build(2 * node + 2, xx + 1, hi);
                update(node, xx);
            }
        }

        private void update(int node, int xx) {
            for (int s = 0; s < 5; ++s) {
                int l[] = state[2 * node + 1][s];
                int t = next[l[1]][chars[xx + 1]];
                int r[] = state[2 * node + 2][t];
                state[node][s][0] = s;
                state[node][s][1] = r[1];
            }
        }
    }

    public void generate(PrintWriter out, int testCases) {
        final int MAX_N = 100000;
        final int MAX_K = 100000;
        Random rand = new Random(50847534);
        for (int testCase = 1; testCase <= testCases; ++testCase) {
            int n = rand.nextInt(MAX_N) + 1;
            int k = rand.nextInt(MAX_K) + 1;
            for (int i = 0; i < n; ++i) {
                out.print("/*.".charAt(rand.nextInt(3)));
            }
            out.println();
            out.println(k);
            for (int i = 0; i < k; ++i) {
                int cmd = rand.nextInt(2);
                int pos = rand.nextInt(n);
                out.format("%d %d", cmd + 1, pos + 1);
                if (cmd == 0) {
                    out.println(" " + "/*.".charAt(rand.nextInt(3)));
                } else {
                    out.println();
                }
            }
        }
        out.flush();
    }
}