package gcj2008.semiAPAC;

import utils.io.*;
import java.io.*;
import java.util.*;

// Problem D
public class ModernArtPlagiarism {
    public void process(int testCase, InputReader in, PrintWriter out) {
        this.G = Tree.read(in);
        this.H = Tree.read(in);
        out.format("Case #%d: ", testCase);
        if (isomorphic()) {
            out.println("YES");
        } else {
            out.println("NO");
        }
    }

    private boolean isomorphic() {
        for (int g = 0; g < G.size(); ++g) {
            if (isomorphic(g, -1, 0, -1)) {
                return true;
            }
        }
        return false;
    }

    private boolean isomorphic(int groot, int gfrom, int hroot, int hfrom) {
        List<Integer> gnext = next(G, groot, gfrom);
        List<Integer> hnext = next(H, hroot, hfrom);
        if (hnext.size() == 0) {
            return true;
        }
        int gn = gnext.size();
        int hn = hnext.size();
        List<Integer>[] graph = new List[hn];
        for (int i = 0; i < hn; ++i) {
            graph[i] = new ArrayList<>();
        }
        for (int h = 0; h < hn; ++h) {
            for (int g = 0; g < gn; ++g) {
                if (isomorphic(gnext.get(g), groot, hnext.get(h), hroot)) {
                    graph[h].add(g);
                }
            }
        }
        return kuhn(graph, hn, gn) == hn;
    }

    private int kuhn(List<Integer>[] g, int n1, int n2) {
        int m1[] = new int[n1];
        int m2[] = new int[n2];
        Arrays.fill(m1, -1);
        Arrays.fill(m2, -1);
        int cardinality = 0;
        while (augment(g, m1, m2)) {
            cardinality = cardinality + 1;
        }
        return cardinality;
    }

    private boolean augment(List<Integer>[] g, int[] m1, int[] m2) {
        boolean[] was = new boolean[m1.length];
        for (int u1 = 0; u1 < m1.length; ++u1) {
            if (m1[u1] == -1) {
                if (dfs(g, u1, m1, m2, was)) {
                    return true;
                }
            }
        }
        return false;
    }

    private boolean dfs(List<Integer>[] g, int u1, int[] m1, int[] m2, boolean[] was) {
        if (was[u1]) {
            return false;
        }
        was[u1] = true;
        for (int u2 : g[u1]) {
            if (m2[u2] == -1 || dfs(g, m2[u2], m1, m2, was)) {
                m1[u1] = u2;
                m2[u2] = u1;
                return true;
            }
        }
        return false;
    }

    private List<Integer> next(Tree T, int root, int from) {
        List<Integer> next = new ArrayList<>();
        for (int node : T.adj(root)) {
            if (node != from) {
                next.add(node);
            }
        }
        return next;
    }

    private Tree G;
    private Tree H;

    private static class Tree {
        private List<Integer>[] tree;
        private int size;

        public Tree(int n) {
            this.size = n;
            this.tree = new List[n];
            for (int i = 0; i < n; ++i) {
                tree[i] = new ArrayList<>();
            }
        }

        public int size() {
            return size;
        }

        public void add(int a, int b) {
            tree[a].add(b);
            tree[b].add(a);
        }

        public List<Integer> adj(int a) {
            return tree[a];
        }

        public static Tree read(InputReader in) {
            int size = in.nextInt();
            Tree tree = new Tree(size);
            for (int i = 0; i + 1 < size; ++i) {
                int a = in.nextInt() - 1;
                int b = in.nextInt() - 1;
                tree.add(a, b);
            }
            return tree;
        }
    }
}
