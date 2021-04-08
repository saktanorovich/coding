package gcj2008.wfinals;

import utils.io.*;
import java.io.*;
import java.util.*;

// Problem A
public class Juice {
    public void process(int testCase, InputReader in, PrintWriter out) {
        int n = in.nextInt();
        int a[] = new int[n];
        int b[] = new int[n];
        int c[] = new int[n];
        for (int i = 0; i < n; ++i) {
            a[i] = in.nextInt();
            b[i] = in.nextInt();
            c[i] = in.nextInt();
        }
        int A[] = new int[MAX + 1];
        int B[] = new int[MAX + 2];
        int res = 0;
        for (int C = 0; C <= MAX; ++C) {
            Arrays.fill(A, 0);
            Arrays.fill(B, 0);
            for (int i = 0; i < n; ++i) {
                if (c[i] <= MAX - C) {
                    if (a[i] + b[i] <= C) {
                        A[a[i]]++;
                        B[b[i]]++;
                    }
                }
            }
            int has = 0;
            for (int x = 0, y = C; x <= C; ++x, --y) {
                has += A[x];
                has -= B[y + 1];
                res = Math.max(res, has);
            }
        }
        out.format("Case #%d: %d\n", testCase, res);
    }

    private final int MAX = 10_000;
}
