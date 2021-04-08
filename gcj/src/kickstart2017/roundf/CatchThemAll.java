package kickstart2017.roundf;

import utils.io.*;
import java.io.*;
import java.util.*;

// Problem C
public class CatchThemAll {
    public void process(int testCase, InputReader in, PrintWriter out) {
        int N = in.nextInt();
        int M = in.nextInt();
        int K = in.nextInt();
        int d[][] = new int[N][N];
        for (int i = 0; i < N; ++i) {
            Arrays.fill(d[i], Integer.MAX_VALUE / 2);
            d[i][i] = 0;
        }
        for (int i = 0; i < M; ++i) {
            int a = in.nextInt() - 1;
            int b = in.nextInt() - 1;
            int c = in.nextInt();
            d[a][b] = c;
            d[b][a] = c;
        }
        for (int k = 0; k < N; ++k) {
            for (int i = 0; i < N; ++i) {
                for (int j = 0; j < N; ++j) {
                    d[i][j] = Math.min(d[i][j], d[i][k] + d[k][j]);
                }
            }
        }
        /** Let us denote by F[i,k] the expected time to catch k
         *  codejamons starting from city i. We can write that
         *    F[i,k] = sum{j!=i, p*(F[j,k-1] + d(i,j))}
         *    F[i,0] = 0
         *  where p=1/(n-1). In matrix form
         *  | F[0,k] |   | 0 p p p pS[0] |   | F[0,k-1] |
         *  | F[1,k] |   | p 0 p p pS[1] |   | F[1,k-1] |
         *  | F[2,k] | = | p p 0 p pS[2] | * | F[2,k-1] |
         *  | F[3,k] |   | p p p 0 pS[3] |   | F[3,k-1] |
         *  |    1   |   | 0 0 0 0   1   |   |    1     |
         *  where S[i] = sum{j!=i, d(i,j)}.
         */
        double F[];
        double A[][];
        F = new double[N + 1];
        A = new double[N + 1][N + 1];
        F[N] = 1;
        A[N][N] = 1;
        for (int i = 0; i < N; ++i) {
            for (int j = 0; j < N; ++j) {
                if (i != j) {
                    A[i][j] += 1.0 / (N - 1);
                    A[i][N] += d[i][j];
                }
            }
            A[i][N] /= (N - 1);
        }
        A = pow(A, N + 1, K);
        F = mul(A, F, N + 1);
        out.format("Case #%d: %.6f\n", testCase, F[0]);
    }

    private static double[][] pow(double[][] a, int n, int k) {
        if (k == 0) {
            double[][] e = new double[n][n];
            for (int i = 0; i < n; ++i) {
                e[i][i] = 1.0;
            }
            return e;
        } else if (k % 2 == 0) {
            return pow(mul(a, a, n), n, k / 2);
        } else {
            return mul(a, pow(a, n, k - 1), n);
        }
    }

    private static double[][] mul(double[][] a, double[][] b, int n) {
        double[][] c = new double[n][n];
        for (int i = 0; i < n; ++i) {
            for (int j = 0; j < n; ++j) {
                for (int k = 0; k < n; ++k) {
                    c[i][j] += a[i][k] * b[k][j];
                }
            }
        }
        return c;
    }

    private static double[] mul(double[][] a, double[] b, int n) {
        double[] c = new double[n];
        for (int i = 0; i < n; ++i) {
            for (int j = 0; j < n; ++j) {
                c[i] += a[i][j] * b[j];
            }
        }
        return c;
    }
}
