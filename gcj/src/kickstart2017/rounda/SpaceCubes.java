package kickstart2017.rounda;

import java.io.*;
import utils.io.*;

// Problem C
public class SpaceCubes {
    public void process(int testCase, InputReader in, PrintWriter out) {
        n = in.nextInt();
        x = new int[n];
        y = new int[n];
        z = new int[n];
        r = new int[n];
        for (int i = 0; i < n; ++i) {
            x[i] = in.nextInt();
            y[i] = in.nextInt();
            z[i] = in.nextInt();
            r[i] = in.nextInt();
        }
        xmin = Integer.MAX_VALUE;
        ymin = Integer.MAX_VALUE;
        zmin = Integer.MAX_VALUE;
        xmax = Integer.MIN_VALUE;
        ymax = Integer.MIN_VALUE;
        zmax = Integer.MIN_VALUE;
        for (int i = 0; i < n; ++i) {
            xmin = Math.min(xmin, x[i] - r[i]);
            ymin = Math.min(ymin, y[i] - r[i]);
            zmin = Math.min(zmin, z[i] - r[i]);
            xmax = Math.max(xmax, x[i] + r[i]);
            ymax = Math.max(ymax, y[i] + r[i]);
            zmax = Math.max(zmax, z[i] + r[i]);
        }
        long lo = 0, hi = (long)1e+12;
        while (lo < hi) {
            long guess = (lo + hi) / 2;
            if (fit (guess)) {
                hi = guess;
            } else {
                lo = guess + 1;
            }
        }
        out.format("Case #%d: %d\n", testCase, lo);
    }

    private boolean fit(long L) {
        // let's check the 1st cube min point
        if (fit(xmin, ymin, zmin, L) == false) {

            return fit(xmin, ymin, zmax - L, L)
                || fit(xmin, ymax - L, zmin, L)
                || fit(xmax - L, ymin, zmin, L);
        }
        return true;
    }

    private boolean fit(long x1min, long y1min, long z1min, long L) {
        int x2min = Integer.MAX_VALUE;
        int y2min = Integer.MAX_VALUE;
        int z2min = Integer.MAX_VALUE;
        int x2max = Integer.MIN_VALUE;
        int y2max = Integer.MIN_VALUE;
        int z2max = Integer.MIN_VALUE;
        for (int i = 0; i < n; ++i) {
            if (fit(x1min, y1min, z1min, L, x[i], y[i], z[i], r[i])) {
                continue;
            }
            x2min = Math.min(x2min, x[i] - r[i]);
            y2min = Math.min(y2min, y[i] - r[i]);
            z2min = Math.min(z2min, z[i] - r[i]);
            x2max = Math.max(x2max, x[i] + r[i]);
            y2max = Math.max(y2max, y[i] + r[i]);
            z2max = Math.max(z2max, z[i] + r[i]);
        }
        return x2max - x2min <= L
            && y2max - y2min <= L
            && z2max - z2min <= L;
    }

    private static boolean fit(long xmin, long ymin, long zmin, long L, int x, int y, int z, int r) {
        return xmin <= x - r && x + r <= xmin + L
            && ymin <= y - r && y + r <= ymin + L
            && zmin <= z - r && z + r <= zmin + L;
    }

    private int n;
    private int x[];
    private int y[];
    private int z[];
    private int r[];
    private int xmin, ymin, zmin;
    private int xmax, ymax, zmax;
}
