package practice.contest2;

import java.io.*;
import utils.io.*;

// Problem C
public class EggDrop {
    public void process(int testCase, InputReader in, PrintWriter out) {
        int F = in.nextInt();
        int D = in.nextInt();
        int B = in.nextInt();
        out.format("Case #%d: %d %d %d\n", testCase, Fmax(F, D, B), Dmin(F, D, B), Bmin(F, D, B));
    }

    private static long Fmax(int F, int D, int B) {
        long res = solve(D, B);
        if (res < INF) {
            return res;
        }
        return -1;
    }

    private static long Dmin(int F, int D, int B) {
        int lo = 1, hi = D;
        while (lo < hi) {
            int d = (lo + hi) / 2;
            if (solve(d, B) >= F) {
                hi = d;
            } else {
                lo = d + 1;
            }
        }
        return lo;
    }

    private static long Bmin(int F, int D, int B) {
        int lo = 1, hi = B;
        while (lo < hi) {
            int b = (lo + hi) / 2;
            if (solve(D, b) >= F) {
                hi = b;
            } else {
                lo = b + 1;
            }
        }
        return lo;
    }

    private static long solve(int D, int B) {
        if (B == 0) return 1L * 0;
        if (B == 1) return 1L * D;
        if (B == 2) return 1L * D * (D + 1) / 2;

        D = Math.min(D, MAX_D);
        B = Math.min(B, MAX_B);

        return FLOOR[D][Math.min(B, D)];
    }

    private static int MAX_D = 3000;
    private static int MAX_B = 3000;
    private static long INF = 1L << 32;

    // let's FLOOR[D][B] is defined as the largest value F such that
    // Solvable(F, D, B) is true. Consider two cases:
    //   (*) the egg has been broken: in this case we need to determine
    //       the number of floors downstairs using D-1 eggs where B-1
    //       eggs can be broken.
    //   (*) the egg has not been broken: in this case we need to determine
    //       the number of floors upstairs. Because the floors below current
    //       do not result to the break we can assume that we are on the
    //       ground having D-1 eggs where B eggs can be broken.
    private static long FLOOR[][];
    static {
        FLOOR = new long[MAX_D + 1][MAX_B + 1];
        for (int d = 1; d <= MAX_D; ++d) {
            FLOOR[d][1] = d;
            for (int b = 2; b <= d; ++b) {
                FLOOR[d][b] = FLOOR[d - 1][b - 1] + FLOOR[d - 1][b] + 1;
                if (FLOOR[d][b] > INF) {
                    FLOOR[d][b] = INF;
                }
            }
            if (d + 1 <= MAX_D) {
                FLOOR[d][d + 1] = FLOOR[d][d];
            }
        }
    }
}
