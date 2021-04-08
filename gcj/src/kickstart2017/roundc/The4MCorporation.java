package kickstart2017.roundc;

import utils.io.*;
import java.io.*;

// Problem D
public class The4MCorporation {
    public void process(int testCase, InputReader in, PrintWriter out) {
        int min = in.nextInt();
        int max = in.nextInt();
        int avg = in.nextInt();
        int mid = in.nextInt();
        if (min <= avg && avg <= max && min <= mid && mid <= max) {
            if (sol1(min, max, mid, avg)) {
                out.format("Case #%d: %d\n", testCase, 1);
                return;
            }
            if (sol2(min, max, mid, avg)) {
                out.format("Case #%d: %d\n", testCase, 2);
                return;
            }
            int res = Math.min(E(min, max, mid, avg), O(min, max, mid, avg));
            if (res > 0) {
                out.format("Case #%d: %d\n", testCase, res);
                return;
            }
        }
        out.format("Case #%d: %s\n", testCase, "IMPOSSIBLE");
    }

    private boolean sol1(int min, int max, int mid, int avg) {
        if (min == max) {
            return true;
        }
        return false;
    }

    private boolean sol2(int min, int max, int mid, int avg) {
        int sum = 2 * avg;
        if (2 * mid == min + max) {
            if (sum == min + max) {
                return true;
            }
        }
        return false;
    }

    private int E(int min, int max, int mid, int avg) {
        int diff = min + max + 2 * mid - 4 * avg;
        if (diff == 0) {
            return 4;
        }
        return F(diff, min, max, mid, avg, 4);
    }

    private int O(int min, int max, int mid, int avg) {
        int diff = min + max + 1 * mid - 3 * avg;
        if (diff == 0) {
            return 3;
        }
        return F(diff, min, max, mid, avg, 3);
    }

    private static int F(int diff, int min, int max, int mid, int avg, int c) {
        int t;
        if (diff > 0) {
            // take the max from 2*avg-mid-max & 2*avg-mid-min
            t = 2 * avg - mid - min;
        } else {
            // take the max from mid+max-2*avg & mid+min-2*avg
            t = mid + max - 2 * avg;
        }
        if (t > 0) {
            return (Math.abs(diff) + t - 1) / t * 2 + c;
        }
        return -1;
    }
}
