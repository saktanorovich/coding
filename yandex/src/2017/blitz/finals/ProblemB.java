package blitz2017.finals;

import java.io.*;
import java.util.*;
import utils.io.*;

public class ProblemB {
    public boolean process(int testCase, InputReader in, PrintWriter out) {
        this.n = in.nextInt();
        this.k = in.nextInt();
        this.a = new int[n];
        this.t = new ArrayList[n];
        String s = in.next();
        for (int i = 0; i < n; ++i) {
            a[i] = "-0+.".indexOf(s.charAt(i)) - 1;
        }
        for (int i = 0; i < n; ++i) {
            t[i] = new ArrayList<>();
        }
        for (int i = 0; i < n - 1; ++i) {
            int x = in.nextInt() - 1;
            int y = in.nextInt() - 1;
            t[x].add(y);
            t[y].add(x);
        }
        int res = doit(0, 0, 1, 0);
        if (res == 0) {
            out.println("Draw");
        } else if (res > 0) {
            out.println("First");
        } else if (res < 0) {
            out.println("Second");
        }
        return true;
    }

    private int doit(int node, int from, int rev, int dep) {
        int res = -1;
        if (dep == k && rev > 0) {
            res = doit(node, from, -rev, dep);
        }
        for (int next : t[node]) {
            if (next != from) {
                res = Math.max(res, eval(next, node, rev, dep));
            }
        }
        return res;
    }

    private int eval(int node, int from, int rev, int dep) {
        if (a[node] < 2) {
            return a[node] * rev;
        }
        return -doit(node, from, rev, dep + 1);
    }

    private List<Integer>[] t;
    private int a[];
    private int n;
    private int k;
}
