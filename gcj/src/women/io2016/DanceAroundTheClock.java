package women.io2016;

import utils.io.*;
import java.io.*;

// Problem B
public class DanceAroundTheClock {
    public void process(int testCase, InputReader in, PrintWriter out) {
        int d = in.nextInt();
        int k = in.nextInt();
        int n = in.nextInt();
        int p = pos(k - 1, d, n);
        int l = p + 1;
        int r = p - 1;
        l = (l + d) % d;
        r = (r + d) % d;
        out.format("Case #%d: %d %d\n", testCase, idx(l, d, n) + 1, idx(r, d, n) + 1);
    }

    // Consider a sample
    //  0: 0 1 2 3 4 5
    //  1: 1 0 3 2 5 4
    //  2: 4 3 0 5 2 1
    //  3: 3 4 5 0 1 2
    //  4: 2 5 4 1 0 3
    //  5: 5 2 1 4 3 0
    //  6: 0 1 2 3 4 6
    private static int pos(int k, int d, int n) {
        n = n % d;
        if (k % 2 == 0) {
            return (k + n + d) % d;
        } else {
            return (k - n + d) % d;
        }
    }

    private static int idx(int p, int d, int n) {
        n = n % d;
        if (p % 2 == 0) {
            if (n % 2 == 0) {
                return (p - n + d) % d;
            } else {
                return (p + n + d) % d;
            }
        } else {
            if (n % 2 == 0) {
                return (p + n + d) % d;
            } else {
                return (p - n + d) % d;
            }
        }
    }
}
