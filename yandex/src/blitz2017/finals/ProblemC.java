package blitz2017.finals;

import java.io.*;
import utils.io.*;

public class ProblemC {
    public boolean process(int testCase, InputReader in, PrintWriter out) {
        this.n = in.nextInt();
        this.a = new double[n][n];
        for (int i = 0; i < n - 1; ++i) {
            for (int j = 0; j < n; ++j) {
                a[i][j] = in.nextDouble();
            }
        }
        this.s = new double[n];
        for (int j = 0; j < n; ++j) {
            for (int i = 0; i < n - 1; ++i) {
                s[j] += a[i][j] * a[i][j];
            }
            s[j] = 1 - s[j];
        }
        for (int k = 0; k < n; ++k) {
            if (Math.abs(s[k]) > EPS) {
                for (int p = -1; p <= +1; p += 2) {
                    if (orthogonal(k, p * Math.sqrt(s[k]))) {
                        for (int j = 0; j < n; ++j) {
                            out.format("%.12f", a[n - 1][j]);
                            if (j + 1 < n) {
                                out.print(" ");
                            }
                        }
                        out.println();
                        return true;
                    }
                }
            }
        }
        throw new RuntimeException();
    }

    private double a[][];
    private double s[];
    private int n;

    private boolean orthogonal(int k, double x) {
        for (int j = 0; j < n; ++j) {
            if (j == k) {
                a[n - 1][j] = x;
            } else {
                a[n - 1][j] = 0;
                for (int i = 0; i < n - 1; ++i) {
                    a[n - 1][j] += a[i][k] * a[i][j];
                }
                a[n - 1][j] = -a[n - 1][j] / x;
            }
        }
        double detA = det(a, n);
        if (Math.abs(detA - 1) > EPS) {
            return false;
        }
        return true;
    }

    /**/
    private static double det(double[][] a, int n) {
        double[][] b = new double[n][n];
        for (int i = 0; i < n; ++i) {
            for (int j = 0; j < n; ++j) {
                b[i][j] = a[i][j];
            }
        }
        return gauss(b, n);
    }

    private static double gauss(double[][] a, int n) {
        double res = 1;
        for (int i = 0, r; i < n; ++i) {
            r = i;
            for (int k = i + 1; k < n; ++k) {
                if (Math.abs(a[k][i]) > Math.abs(a[r][i])) {
                    r = k;
                }
            }
            if (Math.abs(a[r][i]) <= EPS) {
                return 0;
            }
            double[] t = a[i];
            a[i] = a[r];
            a[r] = t;
            if (i != r) {
                res = -res;
            }
            res *= a[i][i];
            for (r = i + 1; r < n; ++r) {
                double z = a[r][i] / a[i][i];
                for (int j = i; j < n; ++j) {
                    a[r][j] -= a[i][j] * z;
                }
            }
        }
        return res;
    }
    /*
    private static double det(double[][] a, int n) {
        if (n > 2) {
            double res = 0;
            for (int c = 0; c < n; ++c) {
                double[][] minor = new double[n - 1][n - 1];
                for (int i = 0; i < n - 1; ++i) {
                    for (int j = 0; j < n - 1; ++j) {
                        if (j < c) {
                            minor[i][j] = a[i + 1][j];
                        } else {
                            minor[i][j] = a[i + 1][j + 1];
                        }
                    }
                }
                if (c % 2 == 0) {
                    res += a[0][c] * det(minor, n - 1);
                } else {
                    res -= a[0][c] * det(minor, n - 1);
                }
            }
            return res;
        }
        return n > 1 ? a[0][0] * a[1][1] - a[0][1] * a[1][0] : a[0][0];
    }
    /**/

    private static final double EPS = 1e-9;
}
