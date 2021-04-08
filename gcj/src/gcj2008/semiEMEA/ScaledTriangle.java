package gcj2008.semiEMEA;

import utils.io.*;
import java.io.*;

// Problem A
public class ScaledTriangle {
    public void process(int testCase, InputReader in, PrintWriter out) {
        int t[][][] = new int[2][3][2];
        for (int i = 0; i < 2; ++i) {
            t[i][0][0] = in.nextInt();
            t[i][0][1] = in.nextInt();
            t[i][1][0] = in.nextInt();
            t[i][1][1] = in.nextInt();
            t[i][2][0] = in.nextInt();
            t[i][2][1] = in.nextInt();
        }
        double fixed[] = get(t[0], t[1]);
        if (fixed != null) {
            out.format("Case #%d: %.6f %.6f\n", testCase, fixed[0], fixed[1]);
        } else {
            out.format("Case #%d: %s\n", testCase, "No Solution");
        }
    }

    // apply TxRxS transforms in this order
    private static double[] get(int[][] T1, int[][] T2) {
        int A1[] = T1[0];
        int B1[] = T1[1];
        int C1[] = T1[2];
        int A2[] = T2[0];
        int B2[] = T2[1];
        int C2[] = T2[2];
        // we have a system of equations
        //   a * A1[x] - b * A1[y] + Dx = A2[x]
        //   b * A1[x] + a * A1[y] + Dy = A2[y]
        //   a * B1[x] - b * B1[y] + Dx = B2[x]
        //   b * B1[x] + a * B1[y] + Dy = B2[y]
        //   a * C1[x] - b * C1[y] + Dx = C2[x]
        //   b * C1[x] + a * C1[y] + Dy = C2[y]
        int x = A1[0] - B1[0];
        int y = A1[1] - B1[1];
        int X = A2[0] - B2[0];
        int Y = A2[1] - B2[1];

        double a = 1.0 * (x * X + y * Y) / (x * x + y * y);
        double b = 1.0 * (x * Y - y * X) / (x * x + y * y);

        double dx = A2[0] - a * A1[0] + b * A1[1];
        double dy = A2[1] - b * A1[0] - a * A1[1];

        assert Math.abs(a * A1[0] - b * A1[1] + dx - A2[0]) < 1e-8;
        assert Math.abs(b * A1[0] + a * A1[1] + dy - A2[1]) < 1e-8;
        assert Math.abs(a * B1[0] - b * B1[1] + dx - B2[0]) < 1e-8;
        assert Math.abs(b * B1[0] + a * B1[1] + dy - B2[1]) < 1e-8;
        assert Math.abs(a * C1[0] - b * C1[1] + dx - C2[0]) < 1e-8;
        assert Math.abs(b * C1[0] + a * C1[1] + dy - C2[1]) < 1e-8;

        // we have a system for a fixed point
        //   x*(a-1) - y*( b ) = -dx
        //   x*( b ) + y*(a-1) = -dy
        double fx = det(-dx, -b, -dy, a - 1) / det(a - 1, -b, b, a - 1);
        double fy = det(a - 1, +b, -dx, -dy) / det(a - 1, -b, b, a - 1);

        return new double[] { fx, fy };
    }

    private static double det(double a, double b, double c, double d) {
        return a * d - c * b;
    }
}
