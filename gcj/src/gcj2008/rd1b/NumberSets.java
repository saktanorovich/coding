package gcj2008.rd1b;

import utils.io.*;
import java.io.*;
import java.util.*;

// Problem B
public class NumberSets {
    public void process(int testCase, InputReader in, PrintWriter out) {
        long A = in.nextLong();
        long B = in.nextLong();
        long P = in.nextLong();
        out.format("Case #%d: %d\n", testCase, get(A, B, P));
    }

    private int get(long a, long b, long p) {
        int n = (int)(b - a + 1);
        if (p > b - p) {
            return n;
        } else {
            // a minimum number which has a prime factor p is 2 * p
            // that means the distance between these numbers is p;
            // in general minimum distance between any two numbers
            // which have a factor p is equal to p.. in our case
            // the distance should not be greater than n
            this.set = new int[n];
            Arrays.fill(set, -1);
            boolean sieve[] = new boolean[n + 1];
            for (int q = 2; q <= n; ++q) {
                if (sieve[q] == false) {
                    for (int z = q + q; z <= n; z += q) {
                        sieve[z] = true;
                    }
                    if (q >= p) {
                        long x = (a + q - 1) / q * q;
                        for (long y = x; y <= b; y += q) {
                            link((int)(x - a), (int)(y - a));
                        }
                    }
                }
            }
            int res = 0;
            for (int i = 0; i < n; ++i) {
                if (set[i] == -1) {
                    ++res;
                }
            }
            return res;
        }
    }

    private void link(int a, int b) {
        a = find(a);
        b = find(b);
        if (a != b) {
            set[a] = b;
        }
    }

    private int find(int a) {
        if (set[a] == -1) {
            return a;
        }
        return set[a] = find(set[a]);
    }

    private int[] set;
}
