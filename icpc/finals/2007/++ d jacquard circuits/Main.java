import java.io.*;
import java.math.*;
import java.util.*;

public class Main {
    private static final class JacquardCircuits {
        public boolean process(int testCase, InputReader in, PrintWriter out) {
            int n = in.nextInt();
            int m = in.nextInt();
            if (n == 0 && m == 0) {
                return false;
            }
            long[] x = new long[n + 1];
            long[] y = new long[n + 1];
            for (int i = 0; i < n; ++i) {
                x[i] = in.nextInt();
                y[i] = in.nextInt();
            }
            for (int i = 0; i < n && n > 2; ++i) {
                while (n > 2) {
                    int a = (i - 1 + n) % n;
                    int b = (i + 1 + n) % n;
                    if (collinear(x[a], y[a], x[i], y[i], x[b], y[b])) {
                        for (int j = i; j + 1 < n; ++j) {
                            x[j] = x[j + 1];
                            y[j] = y[j + 1];
                        }
                        n = n - 1;
                    } else {
                        break;
                    }
                }
            }
            out.format("Case %d: %s\n", testCase, get(x, y, n, m));
            return true;
        }

        private String get(long[] x, long[] y, int n, int m) {
            if (n < 3) {
                return "0";
            }
            x[n] = x[0];
            y[n] = y[0];
            // Pick's theorem states that 2A = 2I + B - 2 where
            //   A is lattice polygon's area,
            //   I is a number of lattice points in the interior,
            //   B is a number of lattice points on the boundary.
            long A = 0;
            long B = 0;
            long G = 0;
            for (int i = 0; i < n; ++i) {
                long dx = Math.abs(x[i] - x[i + 1]);
                long dy = Math.abs(y[i] - y[i + 1]);
                long gc = gcd(dx, dy);
                B += gc;
                A += x[i] * y[i + 1];
                A -= x[i + 1] * y[i];
                G = gcd(G, gc);
            }
            A = Math.abs(A);
            A = A / G / G;
            B = B / G;
            BigInteger R = BigInteger.valueOf(2 * m);
            BigInteger a = BigInteger.valueOf(A);
            a = a.multiply(BigInteger.valueOf(m));
            a = a.multiply(BigInteger.valueOf(m + 1));
            a = a.multiply(BigInteger.valueOf(2 * m + 1));
            a = a.divide(BigInteger.valueOf(6));
            BigInteger b = BigInteger.valueOf(B);
            b = b.multiply(BigInteger.valueOf(m));
            b = b.multiply(BigInteger.valueOf(m + 1));
            b = b.divide(BigInteger.valueOf(2));
            R = R.add(a);
            R = R.subtract(b);
            return R.divide(BigInteger.valueOf(2)).toString();
        }

        private boolean collinear(long x1, long y1, long x2, long y2, long x3, long y3) {
            return vector(x2 - x1, y2 - y1, x3 - x1, y3 - y1) == 0;
        }

        private long vector(long x1, long y1, long x2, long y2) {
            return x1 * y2 - y1 * x2;
        }

        private long gcd(long a, long b) {
            while (a != 0 && b != 0) {
                if (a > b) {
                    a %= b;
                } else {
                    b %= a;
                }
            }
            return a + b;
        }
    }

    public static void main(String[] args) throws Exception {
        InputReader in = new InputReader(new FileInputStream("input.txt"));
        PrintWriter out = new PrintWriter(new FileOutputStream("output.txt"));
        //InputReader in = new InputReader(System.in);
        //PrintWriter out = new PrintWriter(System.out);

        if (args.length > 0 && args[0].equals("-g")) {
        } else {
            System.err.println("Test Case: Elapsed time");
            boolean contd = true;
            for (int test = 1; in.hasNext() && contd; ++test) {
                long beg = System.nanoTime();
                contd = new JacquardCircuits().process(test, in, out);
                out.flush();
                long end = System.nanoTime();
                System.err.format("Test #%3d: %9.3f ms\n", test, (end - beg) / 1e6);
            }
        }

        in.close();
        out.close();
    }

    private static final class InputReader {
        private BufferedReader reader;
        private StringTokenizer tokenizer;

        public InputReader(InputStream input) {
            reader = new BufferedReader(new InputStreamReader(input), 32768);
            tokenizer = null;
        }

        public boolean hasNext() {
            while (tokenizer == null || tokenizer.hasMoreTokens() == false) {
                try {
                    String nextLine = reader.readLine();
                    if (nextLine != null) {
                        tokenizer = new StringTokenizer(nextLine);
                    } else {
                        return false;
                    }
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }
            return tokenizer.hasMoreTokens();
        }

        public String next() {
            if (hasNext()) {
                return tokenizer.nextToken();
            }
            return null;
        }

        public int nextInt() {
            return Integer.parseInt(next());
        }

        public long nextLong() {
            return Long.parseLong(next());
        }

        public double nextDouble() {
            return Double.parseDouble(next());
        }

        public String nextLine() {
            try {
                return reader.readLine();
            } catch (IOException e) {
                e.printStackTrace();
            }
            return null;
        }

        public void close() throws IOException {
            reader.close();
            reader = null;
        }
    }
}
