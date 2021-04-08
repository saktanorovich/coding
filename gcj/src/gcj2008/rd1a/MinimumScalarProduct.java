package gcj2008.rd1a;

import utils.io.*;
import java.io.*;
import java.util.*;

// Problem A
public class MinimumScalarProduct {
    public void process(int testCase, InputReader in, PrintWriter out) {
        int n = in.nextInt();
        int x[] = new int[n];
        int y[] = new int[n];
        for (int i = 0; i < n; ++i) {
            x[i] = in.nextInt();
        }
        for (int i = 0; i < n; ++i) {
            y[i] = in.nextInt();
        }
        Arrays.sort(x);
        Arrays.sort(y);
        long res = 0;
        for (int i = 0; i < n; ++i) {
            res += 1L * x[i] * y[n - 1 - i];
        }
        out.format("Case #%d: %d\n", testCase, res);
    }
}
