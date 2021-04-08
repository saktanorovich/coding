package gcj2008.semiAPAC;

import utils.io.*;
import java.io.*;

// Problem C
public class Millionaire {
    public void process(int testCase, InputReader in, PrintWriter out) {
        this.K = in.nextInt();
        this.P = in.nextDouble();
        this.X = in.nextDouble();
        out.format("Case #%d: %.8f\n", testCase, get(1e6));
    }

    private double get(double M) {
        if (X == M) {
            return 1.0;
        }
        this.F = new double[K + 1][1 << (K + 1)];
        F[0][1 << K] = 1;
        for (int i = 1; i <= K; ++i) {
            F[i][1 << K] = 1;
            for (int j = 0; j < 1 << K; ++j) {
                for (int k = 0; k <= j; ++k) {
                    F[i][j] = Math.max(F[i][j], P * F[i - 1][j + k] + (1 - P) * F[i - 1][j - k]);
                }
            }
        }

        for (int j = 1 << K; j >= 0; --j) {
            if (X * (1 << K) >= j * M) {
                return F[K][j];
            }
        }
        throw new RuntimeException();
    }

    private double F[][];
    private double P;
    private double X;
    private int K;
}
