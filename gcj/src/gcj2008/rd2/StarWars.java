package gcj2008.rd2;

import utils.io.*;
import java.io.*;

// Problem С
public class StarWars {
    public void process(int testCase, InputReader in, PrintWriter out) {
        int n = in.nextInt();
        int x[] = new int[n];
        int y[] = new int[n];
        int z[] = new int[n];
        int p[] = new int[n];
        for (int i = 0; i < n; ++i) {
            x[i] = in.nextInt();
            y[i] = in.nextInt();
            z[i] = in.nextInt();
            p[i] = in.nextInt();
        }
        // It's obvious that if a power P is a solution then any
        // value greater than P is a solution as well. So we can
        // apply binary search to find minimum power.
        double lo = 0;
        double hi = 1e9;
        for (int it = 0; it < 100; ++it) {
            double P = (lo + hi) / 2;
            // any feasible solution will satisfy (|xi-x|+|yi-y|+|zi-z|)/bi <= P
            if (feasible(x, y, z, mul(p, P))) {
                hi = P;
            } else {
                lo = P;
            }
        }
        out.format("Case #%d: %f\n", testCase, (lo + hi) / 2);
    }

    // Each inequality |xi-x|+|yi-y|+|zi-z| <= bi can be transformed to
    //   +xi - x + yi - y + zi - z <= bi
    //   +xi - x + yi - y - zi + z <= bi
    //   +xi - x - yi + y + zi - z <= bi
    //   +xi - x - yi + y - zi + z <= bi
    //   -xi + x + yi - y + zi - z <= bi
    //   -xi + x + yi - y - zi + z <= bi
    //   -xi + x - yi + y + zi - z <= bi
    //   -xi + x - yi + y - zi + z <= bi
    // or
    //   -bi + xi + yi + zi <= x + y + z <= bi + xi + yi + zi
    //   -bi + xi + yi - zi <= x + y - z <= bi + xi + yi - zi
    //   -bi + xi - yi + zi <= x - y + z <= bi + xi - yi + zi
    //   -bi + xi - yi - zi <= x - y - z <= bi + xi - yi - zi
    // or
    //   L1 <= x + y + z <= R1
    //   L2 <= x + y - z <= R2
    //   L3 <= x - y + z <= R3
    //   L4 <= x - y - z <= R4
    private static boolean feasible(int[] x, int[] y, int[] z, double[] b) {
        double L1 = -1e9;
        double R1 = +1e9;
        double L2 = -1e9;
        double R2 = +1e9;
        double L3 = -1e9;
        double R3 = +1e9;
        double L4 = -1e9;
        double R4 = +1e9;
        for (int i = 0; i < x.length; ++i) {
            L1 = Math.max(L1, x[i] + y[i] + z[i] - b[i]);
            R1 = Math.min(R1, x[i] + y[i] + z[i] + b[i]);
            L2 = Math.max(L2, x[i] + y[i] - z[i] - b[i]);
            R2 = Math.min(R2, x[i] + y[i] - z[i] + b[i]);
            L3 = Math.max(L3, x[i] - y[i] + z[i] - b[i]);
            R3 = Math.min(R3, x[i] - y[i] + z[i] + b[i]);
            L4 = Math.max(L4, x[i] - y[i] - z[i] - b[i]);
            R4 = Math.min(R4, x[i] - y[i] - z[i] + b[i]);
        }
        return L1 <= R1 && L2 <= R2 && L3 <= R3 && L4 <= R4;
    }

    private static double[] mul(int[] p, double a) {
        double[] b = new double[p.length];
        for (int i = 0; i < p.length; ++i) {
            b[i] = a * p[i];
        }
        return b;
    }
}
