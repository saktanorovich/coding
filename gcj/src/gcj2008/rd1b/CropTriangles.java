package gcj2008.rd1b;

import utils.io.*;
import java.io.*;

// Problem A
public class CropTriangles {
    public void process(int testCase, InputReader in, PrintWriter out) {
        long N = in.nextInt();
        long A = in.nextInt();
        long B = in.nextInt();
        long C = in.nextInt();
        long D = in.nextInt();
        long X = in.nextInt();
        long Y = in.nextInt();
        long M = in.nextInt();
        this.W = new long[3 * 3];
        add(X, Y);
        for (int i = 1; i <= N - 1; ++i) {
            X = (A * X + B) % M;
            Y = (C * Y + D) % M;
            add(X, Y);
        }
        long res = 0;
        for (int a = 0; a < 9; ++a) {
            res += cn(a);
            for (int b = a + 1; b < 9; ++b) {
                res += cn(a, b);
                for (int c = b + 1; c < 9; ++c) {
                    res += cn(a, b, c);
                }
            }
        }
        out.format("Case #%d: %d\n", testCase, res);
    }

    private long cn(int a, int b, int c) {
        if (okey(a, b, c)) {
            long res = 1;
            res *= W[a];
            res *= W[b];
            res *= W[c];
            return res;
        }
        return 0;
    }

    private long cn(int a, int b) {
        if (okey(a, a, b)) {
            long res = 1;
            res *= W[a];
            res *= W[a] - 1;
            res *= W[b];
            res /= 2;
            return res;
        }
        return 0;
    }

    private long cn(int a) {
        long res = 1;
        res *= W[a];
        res *= W[a] - 1;
        res *= W[a] - 2;
        res /= 6;
        return res;
    }

    private boolean okey(int a, int b, int c) {
        int[] x = new int[] { a / 3, b / 3, c / 3 };
        int[] y = new int[] { a % 3, b % 3, c % 3 };
        int mx = (x[0] + x[1] + x[2]) % 3;
        int my = (y[0] + y[1] + y[2]) % 3;
        if (mx == 0 && my == 0) {
            return true;
        }
        return false;
    }

    private void add(long x, long y) {
        int a = (int)(x % 3);
        int b = (int)(y % 3);
        ++W[a * 3 + b];
    }

    private long[] W;
}
