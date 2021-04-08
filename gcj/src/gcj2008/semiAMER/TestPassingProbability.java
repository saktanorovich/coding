package gcj2008.semiAMER;

import utils.io.*;
import java.io.*;
import java.util.*;

// Problem C
public class TestPassingProbability {
    public void process(int testCase, InputReader in, PrintWriter out) {
        this.n = in.nextInt();
        this.m = in.nextInt();
        this.p = new double[m][4];
        for (int i = 0; i < m; ++i) {
            p[i][0] = in.nextDouble();
            p[i][1] = in.nextDouble();
            p[i][2] = in.nextDouble();
            p[i][3] = in.nextDouble();
            Arrays.sort(p[i]);
        }
        int s[] = new int[m];
        Arrays.fill(s, 3);
        out.format("Case #%d: %.9f\n", testCase, bfs(s));
    }

    private double bfs(int[] s) {
        this.q = new PriorityQueue<>();
        this.v = new HashSet<>();
        add(new State(s, score(s)));
        double res = 0.0;
        for (int i = 0; i < n; ++i) {
            if (q.isEmpty() == false) {
                State a = q.poll();
                res += a.score;
                for (int[] b : a.next()) {
                    add(new State(b, score(b)));
                }
            } else {
                break;
            }
        }
        return res;
    }

    private void add(State s) {
        if (v.add(s)) {
            q.add(s);
        }
    }

    private Double score(int[] s) {
        double res = 1.0;
        for (int i = 0; i < m; ++i) {
            res *= p[i][s[i]];
        }
        return res;
    }

    private PriorityQueue<State> q;
    private Set<State> v;
    private double p[][];
    private int n;
    private int m;

    private class State implements Comparable<State> {
        public int value[];
        public Double score;

        public State(int[] value, double score) {
            this.value = value;
            this.score = score;
        }

        public List<int[]> next() {
            List<int[]> res = new ArrayList<>();
            for (int i = 0; i < value.length; ++i) {
                int[] s = value.clone();
                if (s[i] > 0) {
                    s[i]--;
                    res.add(s);
                }
            }
            return res;
        }

        @Override
        public boolean equals(Object o) {
            return Arrays.equals(value, ((State)o).value);
        }

        @Override
        public int hashCode() {
            return Arrays.hashCode(value);
        }

        @Override
        public int compareTo(State o) {
            return -1 * score.compareTo(o.score);
        }
    }
}
