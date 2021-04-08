package kickstart2018.rounda;

import utils.io.*;
import java.io.*;
import java.util.*;

// Problem B
public class LuckyDip {
    public void process(int testCase, InputReader in, PrintWriter out) {
        this.n = in.nextInt();
        this.k = in.nextInt();
        this.a = new Integer[n];
        for (int i = 0; i < n; ++i) {
            a[i] = in.nextInt();
        }
        Arrays.sort(a, Comparator.reverseOrder());
        out.format("Case #%d: %.8f\n", testCase, exp(k));
    }

    private double exp(int r) {
        if (r == 0) {
            long S = 0;
            for (int x : a) {
                S += x;
            }
            return 1.0 * S / n;
        }
        double H = exp(r - 1);
        double E = 0;
        for (int i = 0; i < n; ++i) {
            E += Math.max(a[i], H);
        }
        E /= n;
        return E;
    }

    private Integer a[];
    private int n;
    private int k;
}
