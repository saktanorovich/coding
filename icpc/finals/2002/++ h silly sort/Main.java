import java.io.*;
import java.util.*;

public class Main {
    private static final class SillySort {
        public boolean process(int testCase, InputReader in, PrintWriter out) {
            int n = in.nextInt();
            if (n == 0) {
                return false;
            }
            Integer a[] = new Integer[n];
            Integer o[] = new Integer[n];
            boolean v[] = new boolean[n];
            for (int i = 0; i < n; ++i) {
                a[i] = in.nextInt();
                o[i] = i;
            }
            Arrays.sort(o, (x, y) -> {
                return a[x] - a[y];
            });
            long M = a[o[0]];
            long R = 0;
            for (int i = 0; i < n; ++i) {
                if (v[i] == false) {
                    long m = 1000;
                    long s = 0;
                    long k = 0;
                    for (int x = i; !v[x]; x = o[x]) {
                        v[x] = true;
                        s += a[x];
                        if (m > a[x]) {
                            m = a[x];
                        }
                        ++k;
                    }
                    s += m * (k - 2);
                    if (k > 1) {
                        R += Math.min(s, s - (m - M) * (k - 1) + 2 * (m + M));
                    }
                }
            }
            out.format("Case %d: %d\n\n", testCase, R);
            return true;
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
            //int T = in.nextInt();
            for (int test = 1; in.hasNext() && contd; ++test) {
                long beg = System.nanoTime();
                contd = new SillySort().process(test, in, out);
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
