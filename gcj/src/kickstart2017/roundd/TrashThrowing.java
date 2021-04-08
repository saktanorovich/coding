package kickstart2017.roundd;

import utils.*;
import utils.io.*;
import java.io.*;
import java.util.*;

// Problem C
public class TrashThrowing {
    public void process(int testCase, InputReader in, PrintWriter out) {
        this.n = in.nextInt();
        this.P = in.nextInt();
        this.H = in.nextInt();
        this.x = new int[n];
        this.y = new int[n];
        for (int i = 0; i < n; ++i) {
            x[i] = in.nextInt();
            y[i] = in.nextInt();
        }
        double lo = 0, hi = H, R = 0;
        for (int it = 0; it < MAX; ++it) {
            R = (lo + hi) / 2;
            if (okay(R)) {
                lo = R;
            } else {
                hi = R;
            }
        }
        out.format("Case #%d: %.6f\n", testCase, R);
    }

    private boolean okay(double R) {
        List<Pair<Double>> invalid = new ArrayList<>();
        for (int i = 0; i < n; ++i) {
            Double a = above(R, x[i], y[i]);
            Double b = below(R, x[i], y[i]);
            if (b != null && a != null) {
                invalid.add(new Pair(b, a));
            } else if (b != null) {
                invalid.add(new Pair(b, U(R)));
            } else if (a != null) {
                invalid.add(new Pair(0.0, a));
            }
        }
        invalid.sort((x, y) -> {
            if (x.item1.compareTo(y.item1) != 0) {
                return +x.item1.compareTo(y.item1);
            }
            return -1 * x.item2.compareTo(y.item2);
        });
        if (invalid.size() > 0) {
            Double lo = null;
            Double hi = null;
            for (Pair<Double> z : invalid) {
                if (lo == null && hi == null) {
                    lo = z.item1;
                    hi = z.item2;
                } else if (sign(z.item1 - hi) <= 0) {
                    hi = Math.max(hi, z.item2);
                } else {
                    return true;
                }
            }
            return 0 < sign(lo) || sign(hi - U(R)) < 0;
        }
        return true;
    }

    private Double above(double R, int x, int y) {
        double lo = A(x, y);
        double hi = U(R);
        if (lo > hi) {
            return null;
        }
        for (int it = 0; it < MAX; ++it) {
            double a = (lo + hi) / 2;
            if (sign(eval(a, x, y) - R) < 0) {
                lo = a;
            } else {
                hi = a;
            }
        }
        return sign(eval(lo, x, y) - R) < 0 ? lo : null;
    }

    private Double below(double R, int x, int y) {
        double lo = 0;
        double hi = Math.min(A(x, y), U(R));
        for (int it = 0; it < MAX; ++it) {
            double a = (lo + hi) / 2;
            if (sign(eval(a, x, y) - R) < 0) {
                hi = a;
            } else {
                lo = a;
            }
        }
        return sign(eval(hi, x, y) - R) < 0 ? hi : null;
    }

    private double eval(double a, int x, int y) {
        double lo;
        double hi;
        if (2 * x <= P) {
            lo = 0;
            hi = P / 2.0;
        } else {
            lo = P / 2.0;
            hi = P;
        }
        for (int it = 0; it < MAX; ++it) {
            double lo3 = lo + (hi - lo) / 3;
            double hi3 = hi - (hi - lo) / 3;
            double loV = D(lo3 - x, F(a, lo3) - y);
            double hiV = D(hi3 - x, F(a, hi3) - y);
            if (loV > hiV) {
                lo = lo3;
            } else {
                hi = hi3;
            }
        }
        return D(lo - x, F(a, lo) - y);
    }

    private double D(double x, double y) {
        return Math.sqrt(x * x + y * y);
    }

    private double A(double x, double y) {
        return y / x / (P - x);
    }

    private double F(double a, double x) {
        return a * x * (P - x);
    }

    private double U(double R) {
        return 4 * (H - R) / P / P;
    }

    private int[] x;
    private int[] y;
    private int P;
    private int H;
    private int n;

    private static final Integer MAX = 50;
    private static final Double EPS = 1e-12;

    private static int sign(double x) {
        if (x + EPS < 0) {
            return -1;
        }
        if (x - EPS > 0) {
            return +1;
        }
        return 0;
    }
}
