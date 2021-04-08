package women.io2017;

import java.io.*;
import java.util.*;
import utils.io.*;

// Problem B
public class Understudies {
    public void process(int testCase, InputReader in, PrintWriter out) {
        int n = in.nextInt();
        double[] p = new double[2 * n];
        for (int i = 0; i < 2 * n; ++i) {
            p[i] = in.nextDouble();
        }
        Arrays.sort(p);
        double res = 1.0;
        for (int i = 0; i < n; ++i) {
            res *= (1 - p[i] * p[2 * n - 1 - i]);
        }
        out.format("Case #%d: %.12f\n", testCase, res);
    }
}
