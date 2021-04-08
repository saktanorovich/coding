package kickstart2018.practice;

import utils.io.*;
import java.io.*;
import java.util.*;

// Problem C
public class SortAScrambledItinerary {
    public void process(int testCase, InputReader in, PrintWriter out) {
        this.n = in.nextInt();
        this.t = new int[2 * n];
        this.ind = new HashMap<>();
        this.rev = new HashMap<>();
        for (int i = 0, j = 0; i < n; ++i) {
            t[j++] = get(in.next());
            t[j++] = get(in.next());
        }
        this.m = ind.size();
        this.g = new List[m];
        for (int i = 0; i < m; ++i) {
            g[i] = new ArrayList<>();
        }
        this.d = new int[m];
        for (int i = 0, j = 0; i < n; ++i) {
            int a = t[j++];
            int b = t[j++];
            g[a].add(b);
            d[b]++;
        }
        Queue<Integer> q = new ArrayDeque<>();
        for (int i = 0; i < m; ++i) {
            if (d[i] == 0) {
                q.add(i);
            }
        }
        int s[] = new int[m];
        for (int i = 0; i < m;) {
            s[i++] = q.poll();
            for (int y : g[s[i - 1]]) {
                d[y]--;
                if (d[y] == 0) {
                    q.add(y);
                }
            }
        }
        assert q.size() == 0;
        out.format("Case #%d:", testCase);
        for (int i = 0; i + 1 < m; ++i) {
            out.format(" %s-%s", rev.get(s[i]), rev.get(s[i + 1]));
        }
        out.println();
    }

    private int get(String key) {
        if (ind.containsKey(key) == false) {
            int val = ind.size();
            ind.put(key, val);
            rev.put(val, key);
        }
        return ind.get(key);
    }

    private HashMap<String, Integer> ind;
    private HashMap<Integer, String> rev;
    private List<Integer>[] g;
    private int d[];
    private int t[];
    private int n;
    private int m;
}
