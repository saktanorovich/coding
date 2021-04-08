package gcj2008.rd2;

import java.io.*;
import utils.io.*;

// Problem B
public class TriangleAreas {
    public void process(int testCase, InputReader in, PrintWriter out) {
        int n = in.nextInt();
        int m = in.nextInt();
        int a = in.nextInt();
        int p[] = get(n, m, a);
        if (p != null) {
            out.format("Case #%d: %d %d %d %d %d %d\n", testCase, p[0], p[1], p[2], p[3], p[4], p[5]);
        } else {
            out.format("Case #%d: %s\n", testCase, "IMPOSSIBLE");
        }
    }

    private static int[] get(int n, int m, int a) {
        if (a > n * m) {
            return null;
        }
        if (n * m < 1e4) {
            for (int x1 = 1; x1 <= n; ++x1) {
                for (int y1 = m; y1 >= 0; ++y1) {
                    for (int x2 = 0; x2 <= n; ++x2) {
                        for (int k = -1; k < 2; k += 2) {
                            int d = a + k * x2 * y1;
                            if (d % x1 == 0) {
                                int y2 = d / (k * x1);
                                if (y2 >= 0 && y2 <= m) {
                                    return new int[]{0, 0, x1, y1, x2, y2};
                                }
                            }
                        }
                    }
                }
            }
        } else {
            // We need to solve the equation x2*y1-x1*y2=a in integers. Let's
            // consider x1=1 and y1=m, then x2*m-y2=a.
            int p = a / m;
            int q = a % m;
            if (q == 0) {
                p = p - 1;
                q = q + m;
            }
            int x2 = p + 1;
            int y2 = m - q;
            return new int[]{0, 0, 1, m, x2, y2};
        }
        throw new RuntimeException();
    }
}
