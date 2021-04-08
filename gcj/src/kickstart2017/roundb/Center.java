package kickstart2017.roundb;

import utils.io.*;
import java.io.*;
import java.util.function.*;

// Problem B
public class Center {
    public void process(int testCase, InputReader in, PrintWriter out) {
        this.n = in.nextInt();
        this.x = new double[n];
        this.y = new double[n];
        this.w = new double[n];
        for (int i = 0; i < n; ++i) {
            x[i] = in.nextDouble();
            y[i] = in.nextDouble();
            w[i] = in.nextDouble();
        }
        out.format("Case #%d: %f\n", testCase,  ternary(x -> ternary(y -> eval(x, y), MIN, MAX), MIN, MAX));
    }

    private double eval(double x, double y) {
        double res = 0;
        for (int i = 0; i < this.x.length; ++i) {
            res += Math.max(Math.abs(this.x[i] - x), Math.abs(this.y[i] - y)) * this.w[i];
        }
        return res;
    }

    private double[] x;
    private double[] y;
    private double[] w;
    private int n;

    private static double ternary(DoubleFunction<Double> f, double lo, double hi) {
        double φ = 2 - (1 + Math.sqrt(5)) / 2;
        double lo3 = lo + φ * (hi - lo);
        double hi3 = hi - φ * (hi - lo);
        double flo = f.apply(lo3);
        double fhi = f.apply(hi3);
        for (int it = 0; it < 100; ++it) {
            if (flo - EPS > fhi) {
                lo = lo3;
                lo3 = hi3;
                hi3 = hi - φ * (hi - lo);
                flo = fhi;
                fhi = f.apply(hi3);
            } else {
                hi = hi3;
                hi3 = lo3;
                lo3 = lo + φ * (hi - lo);
                fhi = flo;
                flo = f.apply(lo3);
            }
        }
        return f.apply((lo + hi) / 2);
    }

    private static final double MIN = -1e+4;
    private static final double MAX = +1e+4;
    private static final double EPS = +1e-8;
}
