import java.io.*;
import java.util.*;

public class Main {
    private static final class Posterize {
        public void process(int testCase, InputReader in, PrintWriter out) {
            this.d = in.nextInt();
            this.k = in.nextInt();
            this.r = new long[d];
            this.p = new long[d];
            for (int i = 0; i < d; ++i) {
                r[i] = in.nextInt();
                p[i] = in.nextInt();
            }
            long[][] z = new long[d][d];
            for (int a = 0; a < d; ++a) {
                for (int b = a; b < d; ++b) {
                    z[a][b] = get(a, b);
                }
            }
            long[][] f = new long[d][k + 1];
            for (int a = 0; a < d; ++a) {
                Arrays.fill(f[a], Long.MAX_VALUE);
            }
            for (int a = 0; a < d; ++a) {
                f[a][1] = get(0, a);
            }
            for (int b = 0; b < d; ++b) {
                for (int i = 2; i <= k; ++i) {
                    f[b][i] = z[0][b];
                    for (int a = 0; a < b; ++ a) {
                        f[b][i] = Math.min(f[b][i], f[a][i - 1] + z[a + 1][b]);
                    }
                }
            }
            out.format("%d\n", f[d - 1][k]);
        }

        private long get(int a, int b) {
            long res = Long.MAX_VALUE;
            for (int c = (int)r[a]; c <= r[b]; ++c) {
                res = Math.min(res, get(a, b, c));
            }
            return res;
        }

        private long get(int a, int b, long c) {
            long res = 0;
            for (int i = a; i <= b; ++i) {
                res += p[i] * (r[i] - c) * (r[i] - c);
            }
            return res;
        }

        private long z[][];
        private long r[];
        private long p[];
        private int d;
        private int k;
    }

    public static void main(String[] args) throws Exception {
        InputReader in = new InputReader(new FileInputStream("input.txt"));
        PrintWriter out = new PrintWriter(new FileOutputStream("output.txt"));
        //InputReader in = new InputReader(System.in);
        //PrintWriter out = new PrintWriter(System.out);

        System.err.println("Test Case: Elapsed time");
        for (int test = 1; in.hasNext(); ++test) {
            long beg = System.nanoTime();
            new Posterize().process(test, in, out);
            out.flush();
            long end = System.nanoTime();
            System.err.format("Test #%3d: %9.3f ms\n", test, (end - beg) / 1e6);
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

        public void close() throws IOException {
            reader.close();
            reader = null;
        }
    }
}
