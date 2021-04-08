package gcj2008.rd1c;

import utils.io.*;
import java.io.*;
import java.math.*;
import java.util.*;

// Problem B
public class UglyNumbers {
    public void process(int testCase, InputReader in, PrintWriter out) {
        char[] s = in.next().toCharArray();
        // The Chinese Remainder Theorem: Let p1,...,pn be pairwise co-prime
        // (that is gcd(pi,pj)=1, whenever i≠j). Then the system of n equations
        //   x=a1 mod p1
        //   ...
        //   x=an mod pn
        // has a unique solution for x modulo P where P=p1*..*pn. From the theorem
        // statement we can conclude that ai=x mod pi.
        BigInteger res = BigInteger.ZERO;
        BigInteger rem[] = get(s, s.length, MOD);
        for (int x = 0; x < MOD; ++x) {
            for (int pi : new int[] { 2, 3, 5, 7}) {
                if (x % pi == 0) {
                    res = res.add(rem[x]);
                    break;
                }
            }
        }
        out.format("Case #%d: %s\n", testCase, res.toString());
    }

    private BigInteger[] get(char[] s, int n, int factor) {
        int[][] c = new int[n][n];
        for (int i = 0; i < n; ++i) {
            c[i][i] = (s[i] - '0') % factor;
            for (int j = i + 1; j < n; ++j) {
                c[i][j] = (c[i][j - 1] * 10 + (s[j] - '0')) % factor;
            }
        }
        BigInteger[][] f = new BigInteger[n][factor];
        for (int i = 0; i < n; ++i) {
            Arrays.fill(f[i], BigInteger.ZERO);
        }
        for (int i = 0; i < n; ++i) {
            f[i][c[0][i]] = BigInteger.ONE;
            for (int j = 0; j < i; ++j) {
                int a = c[j + 1][i];
                for (int b = 0; b < factor; ++b) {
                    int p = (a + b + factor) % factor;
                    int q = (a - b + factor) % factor;
                    f[i][p] = f[i][p].add(f[j][b]);
                    f[i][q] = f[i][q].add(f[j][b]);
                }
            }
        }
        return f[n - 1];
    }

    private static final int MOD = 2 * 3 * 5 * 7;
}
