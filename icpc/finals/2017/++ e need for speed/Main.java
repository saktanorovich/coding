import java.io.*;
import java.util.*;

public class Main {
    private static final class NeedForSpeed {
        public void process(int testCase, InputReader in, PrintWriter out) {
            this.n = in.nextInt();
            this.t = in.nextInt();
            this.s = new int[n];
            this.d = new int[n];
            for (int i = 0; i < n; ++i) {
                d[i] = in.nextInt();
                s[i] = in.nextInt();
            }
            double lo = -1e+9;
            double hi = +1e+9;
            while (hi - lo > 1e-9) {
                Double c = (lo + hi) / 2;
                Double z = get(c);
                if (z == null || z >= t) {
                    lo = c;
                } else {
                    hi = c;
                }
            }
            out.format("%.9f\n", (lo + hi) / 2);
        }

        private Double get(Double c) {
            Double res = 0.0;
            for (int i = 0; i < n; ++i) {
                double z = c + s[i];
                if (z >= 0) {
                    res += d[i] / z;
                } else {
                    return null;
                }
            }
            return res;
        }

        private int[] s;
        private int[] d;
        private int n;
        private int t;
    }

    public static void main(String[] args) throws Exception {
        InputReader in = new InputReader(new FileInputStream("input.txt"));
        PrintWriter out = new PrintWriter(new FileOutputStream("output.txt"));
        //InputReader in = new InputReader(System.in);
        //PrintWriter out = new PrintWriter(System.out);

        System.err.println("Test Case: Elapsed time");
        for (int test = 1; in.hasNext(); ++test) {
            long beg = System.nanoTime();
            new NeedForSpeed().process(test, in, out);
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
