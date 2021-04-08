package gcj2008.semiAMER;

import utils.io.*;
import java.io.*;
import java.util.*;

// Problem A
public class MixingBowls {
    public void process(int testCase, InputReader in, PrintWriter out) {
        this.n = in.nextInt();
        this.m = n * 10;
        this.a = new int[n][];
        this.z = new int[m];
        this.r = new HashMap<>();
        Arrays.fill(z, -1);
        for (int i = 0; i < n; ++i) {
            String s = in.next();
            int k = in.nextInt();
            this.z[id(s)] = i;
            this.a[i] = new int[k];
            for (int j = 0; j < k; ++j) {
                a[i][j] = id(in.next());
            }
        }
        out.format("Case #%d: %d\n", testCase, get(0));
    }

    private int get(int x) {
        if (z[x] != -1) {
            List<Integer> h = new ArrayList<>();
            for (int d : a[z[x]]) {
                h.add(get(d));
            }
            return mix(h);
        }
        return 0;
    }

    private int mix(List<Integer> h) {
        h.sort(Comparator.reverseOrder());
        int res = 0, have = 0;
        for (int d : h) {
            if (d > 0) {
                if (have >= d) {
                    have -= 1;
                } else {
                    res += d - have;
                    have = d - 1;
                }
            }
        }
        if (have > 0) {
            return res;
        } else {
            return res + 1;
        }
    }

    private int id(String s) {
        if (r.containsKey(s) == false) {
            r.put(s, r.size());
        }
        return r.get(s);
    }

    private Map<String, Integer> r;
    private int a[][];
    private int z[];
    private int n;
    private int m;
}
