package gcj2008.rd1a;

import java.io.*;
import java.math.*;
import utils.io.*;

// Problem C
public class Numbers {
    public void process(int testCase, InputReader in, PrintWriter out) {
        out.format("Case #%d: %03d\n", testCase, get(in.nextInt()));
    }

    // Let's assume that x=3+√5 and y=3-√5. Note that z=x^n+y^n is
    // an integer number. We can conclude that integer part of x^n
    // is equal to z-1 because y<1. To quickly find z let's construct
    // a recurrence relation for z(n) in the form
    //   z(n)=a*z(n-1)+b*z(n-2).
    // It can be shown that a = 6 and b = -4.
    private static int get(int n) {
        if (n <= (int)1e2) {
            return new Solver1().get(n);
        }
        if (n <= (int)1e4) {
            return new Solver2().get(n);
        }
        if (n % 2 == 0) {
            return new Solver3().one(n);
        } else {
            return new Solver3().two(n);
        }
    }

    private static final int MOD = 1000;

    private static final class Solver3 {
        public int one(int n) {
            int[][] m = new int[][] {
                new int[] {  0, 1 },
                new int[] { -4, 6 },
            };
            m[0][0] += MOD;
            m[0][1] += MOD;
            m[1][0] += MOD;
            m[1][1] += MOD;
            m = pow(m, n);
            int z = 2 * m[0][0] + 6 * m[0][1];
            z += MOD - 1;
            z %= MOD;
            return z;
        }

        public int two(int n) {
            int[][] m = new int[][] {
                new int[] { 3, 5 },
                new int[] { 1, 3 },
            };
            m = pow(m, n);
            int z = 2 * m[0][0];
            z += MOD - 1;
            z %= MOD;
            return z;
        }

        private static int[][] pow(int[][] m, int n) {
            if (n == 0) {
                return new int[][] {
                    new int[] { 1, 0 },
                    new int[] { 0, 1 },
                };
            } else if (n % 2 == 0) {
                return pow(mul(m, m), n / 2);
            } else {
                return mul(m, pow(m, n - 1));
            }
        }

        private static int[][] mul(int[][] x, int[][] y) {
            int[][] c = new int[2][2];
            c[0][0] = (x[0][0] * y[0][0] + x[0][1] * y[1][0]) % MOD;
            c[0][1] = (x[0][0] * y[0][1] + x[0][1] * y[1][1]) % MOD;
            c[1][0] = (x[1][0] * y[0][0] + x[1][1] * y[1][0]) % MOD;
            c[1][1] = (x[1][0] * y[0][1] + x[1][1] * y[1][1]) % MOD;
            return c;
        }
    }

    private static final class Solver2 {
        public int get(int n) {
            int z = 0;
            int x = 6;
            int y = 2;
            for (int i = 2; i <= n; ++i) {
                z = 6 * x - 4 * y;
                z = z % MOD;
                y = x;
                x = z;
            }
            z += MOD - 1;
            z %= MOD;
            return z;
        }
    }

    private static final class Solver1 {
        public int get(int n) {
            return pow(new Form(3, 1), n).eval();
        }

        private static Form pow(Form f, int n) {
            if (n == 0) {
                return new Form(1, 0);
            } else if (n % 2 == 0) {
                return pow(mul(f, f), n / 2);
            } else {
                return mul(f, pow(f, n - 1));
            }
        }

        private static Form mul(Form x, Form y) {
            BigInteger a = BigInteger.ZERO;
            BigInteger b = BigInteger.ZERO;
            a = a.add(x.a.multiply(y.a));
            a = a.add(x.b.multiply(y.b.multiply(BigInteger.valueOf(5))));
            b = b.add(x.a.multiply(y.b));
            b = b.add(x.b.multiply(y.a));
            return new Form(a, b);
        }

        private static final class Form {
            public final BigInteger a;
            public final BigInteger b;

            public Form(int a, int b) {
                this.a = BigInteger.valueOf(a);
                this.b = BigInteger.valueOf(b);
            }

            public Form(BigInteger a, BigInteger b) {
                this.a = a;
                this.b = b;
            }

            public int eval() {
                BigInteger x = BigInteger.valueOf(5);
                x = x.multiply(b);
                x = x.multiply(b);
                x = sqrt(x);
                x = x.add(a);
                x = x.mod(BigInteger.valueOf(MOD));
                return x.intValue();
            }

            private BigInteger sqrt(BigInteger a) {
                BigInteger _1 = BigInteger.valueOf(1);
                BigInteger _2 = BigInteger.valueOf(2);
                BigInteger lo = BigInteger.ZERO;
                BigInteger hi = BigInteger.ZERO.add(a);
                while (lo.add(_1).compareTo(hi) < 0) {
                    BigInteger x = lo.add(hi).divide(_2);
                    BigInteger m = x.multiply(x);
                    if (a.compareTo(m) < 0) {
                        hi = x;
                    } else {
                        lo = x;
                    }
                }
                return lo;
            }
        }
    }
}
