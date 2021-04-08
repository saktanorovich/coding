package kickstart2017.roundg;

import utils.io.*;
import java.io.*;
import java.util.*;

// Problem B
public class CardsGame {
    public void process(int testCase, InputReader in, PrintWriter out) {
        int n = in.nextInt();
        int r[] = new int[n];
        int b[] = new int[n];
        for (int i = 0; i < n; ++i) {
            r[i] = in.nextInt();
        }
        for (int i = 0; i < n; ++i) {
            b[i] = in.nextInt();
        }
        List<Edge> es = new ArrayList<>();
        for (int i = 0; i < n; ++i) {
            for (int j = i + 1; j < n; ++j) {
                es.add(new Edge(i, j, Math.min(r[i] ^ b[j], b[i] ^ r[j])));
            }
        }
        out.format("Case #%d: %d\n", testCase, mst(es, n));
    }

    private long mst(List<Edge> es, int n) {
        es.sort((x, y) -> {
            return x.c - y.c;
        });
        Sets dss = new Sets(n);
        long res = 0;
        for (int i = 0; dss.size() > 1; ++i) {
            Edge e = es.get(i);
            if (dss.find(e.a) != dss.find(e.b)) {
                dss.union(e.a, e.b);
                res += e.c;
            }
        }
        return res;
    }

    private class Edge {
        public final int a;
        public final int b;
        public final int c;

        public Edge(int a, int b, int c) {
            this.a = a;
            this.b = b;
            this.c = c;
        }
    }

    private class Sets {
        private int leader[];
        private int size;

        public Sets(int n) {
            this.leader = new int[n];
            for (int i = 0; i < n; ++i) {
                leader[i] = i;
            }
            this.size = n;
        }

        public int size() {
            return size;
        }

        public int find(int a) {
            return leader[a];
        }

        public void union(int a, int b) {
            int as = find(a);
            int bs = find(b);
            if (as != bs) {
                for (int i = 0; i < leader.length; ++i) {
                    if (leader[i] == bs) {
                        leader[i] = as;
                    }
                }
                size = size - 1;
            }
        }
    }
}
