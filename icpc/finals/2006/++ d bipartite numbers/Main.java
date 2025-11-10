import java.io.*;
import java.util.*;

public class Main {
    private static final class BipartiteNumbers {
        public boolean process(int testCase, InputReader in, PrintWriter out) {
            this.N = in.nextInt();
            if (N == 0) {
                return false;
            }
            R10 = new int[20][N + 2];
            L10 = new int[20][N + 2];
            for (int d = -9; d <= +9; ++d) {
                for (int i = 0; i < N; ++i) {
                    L10[d + 10][i] = Integer.MAX_VALUE;
                }
                R10[d + 10][0] = 0;
                for (int i = 1; i <= N + 1; ++i) {
                    R10[d + 10][i] = R10[d + 10][i - 1];
                    R10[d + 10][i] *= 10;
                    R10[d + 10][i] += d;
                    R10[d + 10][i] %= N;
                    R10[d + 10][i] += N;
                    R10[d + 10][i] %= N;
                    if (L10[d + 10][R10[d + 10][i]] > i ){
                        L10[d + 10][R10[d + 10][i]] = i;
                    }
                }
            }
            Key K = new Key(Integer.MAX_VALUE, 0, 0, 0);
            for (int L = 2;; ++L) {
                for (int s = 1; s <= 9; ++s) {
                    for (int t = 0; t <= 9; ++t) {
                        if (s != t) {
                            if (L10[s - t + 10][R10[s + 10][L]] >= L) {
                                continue;
                            }
                            for (int n = 1; n < L; ++n) {
                                if (okay(L - n, s, n, t)) {
                                    Key k = new Key(L - n, s, n, t);
                                    if (k.compareTo(K) < 0) {
                                        K = k;
                                    }
                                }
                            }
                        }
                    }
                    if (K.m < Integer.MAX_VALUE) {
                        out.format("%d: %s\n", N, K.toString());
                        return true;
                    }
                }
            }
        }

        private boolean okay(int m, int s, int n, int t) {
            if (R10[s + 10][m + n] != R10[s - t + 10][n]) {
                return false;
            }
            if (m + n > 5) {
                return true;
            }
            int a = 0;
            for (int i = 0; i < m; ++i) {
                a = a * 10 + s;
            }
            for (int i = 0; i < n; ++i) {
                a = a * 10 + t;
            }
            return a > N;
        }

        private int R10[][];
        private int L10[][];
        private int N;

        private class Key implements Comparable<Key> {
            public int m;
            public int s;
            public int n;
            public int t;

            public Key(int m, int s, int n, int t) {
                this.m = m;
                this.s = s;
                this.n = n;
                this.t = t;
            }

            @Override
            public String toString() {
                return String.format("%d %d %d %d", m, s, n, t);
            }

            @Override
            public int compareTo(Key o) {
                if (m + n != o.m + o.n) {
                    return m + n - o.m - o.n;
                }
                if (s != o.s) {
                    return s - o.s;
                }
                if (m != o.m) {
                    if (m < o.m) {
                        return t - o.s;
                    } else {
                        return s - o.t;
                    }
                }
                return t - o.t;
            }
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
                contd = new BipartiteNumbers().process(test, in, out);
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
