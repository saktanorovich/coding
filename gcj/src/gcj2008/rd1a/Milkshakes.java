package gcj2008.rd1a;

import utils.io.*;
import java.io.*;
import java.util.*;

// Problem B
public class Milkshakes {
    public void process(int testCase, InputReader in, PrintWriter out) {
        int n = in.nextInt();
        int m = in.nextInt();
        int[][] xs = new int[m][];
        int[][] ys = new int[m][];
        for (int i = 0; i < m; ++i) {
            int t = in.nextInt();
            xs[i] = new int[t];
            ys[i] = new int[t];
            for (int j = 0; j < t; ++j) {
                xs[i][j] = in.nextInt();
                ys[i][j] = in.nextInt();
            }
        }
        out.format("Case #%d:", testCase, 1);
        SAT solver = new SAT(n);
        if (solver.solve(sat(xs, ys))) {
            for (int i = 0; i < n; ++i) {
                out.format(" %d", solver.get(i));
            }
        } else {
            out.print(" IMPOSSIBLE");
        }
        out.println();
    }

    // Get 2-SAT problem expressed in CNF.
    private static List<HornClause> sat(int[][] xs, int[][] ys) {
        List<HornClause> res = new ArrayList<>();
        for (int i = 0; i < xs.length; ++i) {
            Set<Integer> positive = new HashSet<>();
            Set<Integer> negative = new HashSet<>();
            for (int j = 0; j < xs[i].length; ++j) {
                int x = xs[i][j] - 1;
                if (ys[i][j] == 1) {
                    positive.add(+x);
                } else {
                    negative.add(~x);
                }
            }
            res.add(new HornClause(positive, negative));
        }
        return res;
    }

    // Solver for Horn-satisfiability problem.
    private static class SAT {
        private final int[] solution;
        private final int size;

        public SAT(int size) {
            this.solution = new int[size];
            Arrays.fill(solution, -1);
            this.size = size;
        }

        public boolean solve(List<HornClause> sat) {
            Deque<HornClause> queue = new LinkedList<>();
            for (int i = 0; i < sat.size(); ++i) {
                if (sat.get(i).unit()) {
                    queue.addFirst(sat.get(i));
                } else {
                    queue.addLast(sat.get(i));
                }
            }
            while (queue.size() > 0) {
                HornClause clause = queue.getFirst();
                if (clause.unit() == false) {
                    break;
                }
                int x = clause.head();
                if (set(x)) {
                    Deque<HornClause> next = new LinkedList<>();
                    for (HornClause c : queue) {
                        if (c.has(+x)) {
                            continue;
                        }
                        if (c.has(~x) && c.unit() == false) {
                            c.del(~x);
                        }
                        if (c.unit()) {
                            next.addFirst(c);
                        } else {
                            next.addLast(c);
                        }
                    }
                    queue = next;
                } else {
                    return false;
                }
            }
            for (int x = 0; x < size; ++x) {
                if (solution[x] == -1) {
                    solution[x] = 0;
                }
            }
            return true;
        }

        public int get(int x) {
            return solution[x];
        }

        private boolean set(int x) {
            return set(x, x < 0 ? 0 : 1);
        }

        private boolean set(int x, int value) {
            x = Math.max(x, ~x);
            if (solution[x] == -1) {
                solution[x] = value;
            }
            return solution[x] == value;
        }
    }

    private static class HornClause {
        public final Set<Integer> positive;
        public final Set<Integer> negative;

        public HornClause(Set<Integer> positive, Set<Integer> negative) {
            this.positive = positive;
            this.negative = negative;
        }

        public boolean has(int x) {
            return positive.contains(x) || negative.contains(x);
        }

        public boolean del(int x) {
            if (positive.contains(x)) {
                return positive.remove(x);
            } else {
                return negative.remove(x);
            }
        }

        public boolean unit() {
            return positive.size() + negative.size() == 1;
        }

        public int head() {
            if (positive.size() > 0) {
                return positive.iterator().next();
            } else {
                return negative.iterator().next();
            }
        }
    }
}
