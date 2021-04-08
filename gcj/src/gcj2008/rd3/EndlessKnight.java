package gcj2008.rd3;

import utils.*;
import utils.io.*;
import java.io.*;
import java.util.*;

// Problem D
public class EndlessKnight {
    public void process(int testCase, InputReader in, PrintWriter out) {
        int h = in.nextInt() - 1;
        int w = in.nextInt() - 1;
        int n = in.nextInt();
        int x[] = new int[n];
        int y[] = new int[n];
        for (int i = 0; i < n; ++i) {
            x[i] = in.nextInt() - 1;
            y[i] = in.nextInt() - 1;
        }
        long res = 0;
        for (int set = 0; set < 1 << n; ++set) {
            List<Position> p = new ArrayList<>();
            p.add(new Position(0, 0));
            p.add(new Position(h, w));
            int sign = 1;
            for (int i = 0; i < n; ++i) {
                if ((set & (1 << i)) != 0) {
                    p.add(new Position(x[i], y[i]));
                    sign = -sign;
                }
            }
            res += sign * get(p);
            res += MOD;
            res %= MOD;
        }
        out.format("Case #%d: %d\n", testCase, res);
    }

    private static long get(List<Position> p) {
        p.sort(Comparator.naturalOrder());
        long res = 1;
        for (int i = 1; i < p.size(); ++i) {
            Position a = p.get(i - 1);
            Position b = p.get(i);
            res *= get(b.x - a.x, b.y - a.y);
            res %= MOD;
        }
        return res;
    }

    private static long get(int h, int w) {
        if (h < 0 || w < 0) {
            return 0;
        }
        if (h < MAX && w < MAX) {
            return F[h][w];
        } else {
            // From output for the simple case we can note that the number of
            // the knight moves are equal to C(x',y') for some x' and y'. Let's
            // transform original coordinate system so that f(x,y) = C(x',y').
            // Note that any position reachable by the knight can be described
            // as (2*a+b,a+2*b) where a,b are non-negative integers. It's not
            // difficult to discover that a=(2*x-y)/3 and b=(2*y-x)/3. The total
            // number of steps is a+b, however we need to choose a of those steps
            // in one direction. That's why f(x,y)=C(a+b,a).
            int a = 2 * h - w;
            int b = 2 * w - h;
            if (a >= 0 && b >= 0) {
                if (a % 3 == 0 && b % 3 == 0) {
                    a /= 3;
                    b /= 3;
                    return binom(a + b, b);
                }
            }
            return 0;
        }
    }

    // Find C(n,k) mod p using Lucas's theorem.
    private static final int binom(int n, int k) {
        int res = 1;
        while (n > 0 && k > 0) {
            int dn = n % MOD;
            int dk = k % MOD;
            res *= C[dn][dk];
            res %= MOD;
            n /= MOD;
            k /= MOD;
        }
        return res;
    }

    private static class Position implements Comparable<Position> {
        public final int x;
        public final int y;

        public Position(int x, int y) {
            this.x = x;
            this.y = y;
        }

        @Override
        public int compareTo(Position o) {
            if (x != o.x) {
                return x - o.x;
            }
            return y - o.y;
        }
    }

    private static final int MOD = (int)1e4 + 7;
    private static final int MAX = 200;

    private static final int F[][];
    private static final int C[][];
    static {
        F = new int[MAX][MAX];
        F[0][0] = 1;
        for (int x = 0; x < MAX; ++x) {
            for (int y = 0; y < MAX; ++y) {
                if (F[x][y] > 0) {
                    if (x + 1 < MAX && y + 2 < MAX) {
                        F[x + 1][y + 2] += F[x][y];
                        F[x + 1][y + 2] %= MOD;
                    }
                    if (x + 2 < MAX && y + 1 < MAX) {
                        F[x + 2][y + 1] += F[x][y];
                        F[x + 2][y + 1] %= MOD;
                    }
                }
            }
        }

        C = new int[MOD][MOD];
        for (int i = 0; i < MOD; ++i) {
            C[i][0] = 1;
            for (int j = 1; j <= i; ++j) {
                C[i][j] = (C[i - 1][j] + C[i - 1][j - 1]) % MOD;
            }
        }
    }
}
