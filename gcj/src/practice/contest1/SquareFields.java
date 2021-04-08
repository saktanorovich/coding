package practice.contest1;

import java.io.*;
import utils.io.*;

// Problem B
public class SquareFields {
    public void process(int testCase, InputReader in, PrintWriter out) {
        n = in.nextInt();
        k = in.nextInt();
        x = new int[n];
        y = new int[n];
        for (int i = 0; i < n; ++i) {
            x[i] = in.nextInt();
            y[i] = in.nextInt();
        }
        int lo = 1, hi = 1 << 16;
        while (lo < hi) {
            int d = (lo + hi) / 2;
            if (get(d) <= k) {
                hi = d;
            } else {
                lo = d + 1;
            }
        }
        out.format("Case #%d: %d\n", testCase, lo);
    }

    private int get(int d) {
        int opt[] = new int[1 << n];
        for (int set = 1; set < 1 << n; ++set) {
            if (fit(set, d)) {
                opt[set] = 1;
                continue;
            }
            opt[set] = n;
            for (int sub = set; sub > 0; sub = (sub - 1) & set) {
                int cnt = opt[sub] + opt[set ^ sub];
                if (opt[set] > cnt) {
                    opt[set] = cnt;
                }
            }
        }
        return opt[(1 << n) - 1];
    }

    private boolean fit(int set, int d) {
        int minx = Integer.MAX_VALUE;
        int miny = Integer.MAX_VALUE;
        int maxx = Integer.MIN_VALUE;
        int maxy = Integer.MIN_VALUE;
        for (int i = 0; i < n; ++i) {
            if ((set & (1 << i)) != 0) {
                minx = Math.min(minx, x[i]);
                miny = Math.min(miny, y[i]);
                maxx = Math.max(maxx, x[i]);
                maxy = Math.max(maxy, y[i]);
            }
        }
        return maxx - minx <= d && maxy - miny <= d;
    }

    private int[] x;
    private int[] y;
    private int n;
    private int k;
}
