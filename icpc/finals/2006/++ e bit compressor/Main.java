import java.io.*;
import java.util.*;

public class Main {
    private static final class BitCompressor {
        public boolean process(int testCase, InputReader in, PrintWriter out) {
            int L = in.nextInt();
            int N = in.nextInt();
            if (L == 0 && N == 0) {
                return false;
            }
            String S = in.next() + '0';
            out.format("Case #%d: ", testCase);
            int res = doit(S, L + 1, N, 0, 0, 0);
            if (res == 0) {
                out.println("NO");
            } else if (res == 1) {
                out.println("YES");
            } else {
                out.println("NOT UNIQUE");
            }
            return true;
        }

        private int doit(String S, int L, int N, int bits, int ones, int at) {
            if (at >= S.length()) {
                if (bits == L && ones == N) {
                    return 1;
                } else {
                    return 0;
                }
            } else if (bits > L || ones > N) {
                return 0;
            }
            if (S.charAt(at) == '0') {
                return doit(S, L, N, bits + 1, ones, at + 1);
            } else {
                return eval(S, L, N, bits, ones, at);
            }
        }

        private int eval(String S, int L, int N, int bits, int ones, int at) {
            int res = 0;
            int val = 1;
            for (int x = at + 1; x < S.length(); val = 2 * val + S.charAt(x) - '0', ++x) {
                if (val < 1 << 17) {
                    if (S.charAt(x) == '0') {
                        if (x - at == 2) {
                            if (val == 3) {
                                res += doit(S, L, N, bits + 2, ones + 2, x);
                                res += doit(S, L, N, bits + 3, ones + 3, x);
                            }
                        } else {
                            res += doit(S, L, N, bits + val, ones + val, x);
                        }
                    }
                } else break;
            }
            return res;
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
                contd = new BitCompressor().process(test, in, out);
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
