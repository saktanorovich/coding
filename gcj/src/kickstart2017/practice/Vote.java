package kickstart2017.practice;

import java.io.*;
import utils.io.*;

// Problem B
public class Vote {
    public void process(int testCase, InputReader in, PrintWriter out) {
        int n = in.nextInt();
        int m = in.nextInt();
        assert n > m : "incorrect input";

        // we need to find all such permutations p that for any prefix
        // p[i] the a(p[i]) > b(p[i]) where a(p[i]) and b(p[i]) is the
        // number of as and bs in the prefix p[1..i], i = 1..n+m.
        double prob[][] = new double[n + 1][m + 1];
        prob[1][0] = 1.0 * n / (n + m);
        for (int i = 2; i <= n + m; ++i) {
            for (int j = 1; j <= i; ++j) {
                int a = j;
                int b = i - j;
                if (a <= n && b <= m && a > b) {
                    if (a > b - 1 && b > 0) {
                        prob[a][b] += prob[a][b - 1] * (m - b + 1) / (n + m - a - b + 1);
                    }
                    if (a > b + 1 && a > 0) {
                        prob[a][b] += prob[a - 1][b] * (n - a + 1) / (n + m - a - b + 1);
                    }
                }
            }
        }
        out.format("Case #%d: %.8f\n", testCase, prob[n][m]);
    }
}
