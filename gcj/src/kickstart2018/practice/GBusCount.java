package kickstart2018.practice;

import utils.io.*;
import java.io.*;

// Problem A
public class GBusCount {
    public void process(int testCase, InputReader in, PrintWriter out) {
        int n = in.nextInt();
        int t[] = new int[MAX + 2];
        for (int i = 0; i < n; ++i) {
            int a = in.nextInt();
            int b = in.nextInt();
            ++t[a];
            --t[b + 1];
        }
        for (int i = 1; i <= MAX; ++i) {
            t[i] += t[i - 1];
        }
        int p = in.nextInt();
        out.format("Case #%d:", testCase);
        for (int i = 0; i < p; ++i) {
            int q = in.nextInt();
            out.format(" %d", t[q]);
        }
        out.println();
    }

    private static final int MAX = 5000;
}
