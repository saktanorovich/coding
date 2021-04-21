import java.io.*;
import java.util.*;

public class Main {
    private static final class InsecureInPrague {
        public boolean process(int testCase, InputReader in, PrintWriter out) {
            String encoded = in.next();
            if (encoded.equals("X")) {
                return false;
            }
            this.text = encoded.toCharArray();
            this.m = text.length;
            this.f = new int[128];
            this.r = new int[m];
            this.u = new boolean[m];
            for (int i = 0; i < m; ++i) {
                ++f[text[i]];
            }
            for (int n = m / 2; n > 0; --n) {
                String result = process(n);
                if (result != null) {
                    if (result.isEmpty() == false) {
                        out.format("Code %d: %s\n", testCase, result);
                    } else {
                        out.format("Code %d: %s\n", testCase, "Codeword not unique");
                    }
                    return true;
                }
            }
            throw new RuntimeException();
        }

        private String process(int n) {
            HashSet<String> res = new HashSet<>();
            for (int s = 0; s < m; ++s) {
                if (f[text[s]] < 2) {
                    continue;
                }
                pass1: for (int i = 0; i < m; ++i) {
                    int[] c = new int[128];
                    int[] p = scan(m, n, s, i);
                    for (int x : p) {
                        int z = text[x];
                        ++c[z];
                        if (2 * c[z] > f[z]) {
                            continue pass1;
                        }
                    }
                    String word = make(n, p);
                    for (int t = 0; t < m - n; ++t) {
                        if (text[p[0]] != text[r[t]]) {
                            continue;
                        }
                        pass2: for (int j = i + 1; j < m; ++j) {
                            int[] q = scan(m - n, n, t, j);
                            for (int x = 0; x < n; ++x) {
                                q[x] = r[q[x]];
                                if (text[p[x]] != text[q[x]]) {
                                    continue pass2;
                                }
                            }
                            res.add(word);
                        }
                    }
                }
            }
            if (res.size() > 0) {
                if (res.size() == 1) {
                    return res.iterator().next();
                }
                return "";
            }
            return null;
        }

        private String make(int n, int[] p) {
            Arrays.fill(u, false);
            StringBuilder word = new StringBuilder();
            for (int x = 0; x < n; ++x) {
                word.append(text[p[x]]);
                u[p[x]] = true;
            }
            for (int x = 0, k = 0; x < m; ++x) {
                if (u[x] == false) {
                    r[k++] = x;
                }
            }
            return word.toString();
        }

        private char[] text;
        private boolean[] u;
        private int r[];
        private int f[];
        private int m;

        private static int PERM[][][];
        private static int MAXM = 40;
        static {
            PERM = new int[MAXM + 1][MAXM + 1][MAXM];
            for (int m = 2; m <= MAXM; ++m) {
                for (int i = 0; i < MAXM; ++i) {
                    PERM[m][i] = init(m, m, 0, i);
                }
            }
        }

        private static int[] init(int m, int n, int s, int i) {
            boolean u[] = new boolean[m];
            u[0] = true;
            int[] p = new int[n];
            for (int x = 1; x < n; ++x) {
                for (int k = i % (m - x); k >= 0; --k) {
                    s = move(m, s + 1, u);
                }
                p[x] = s;
                u[s] = true;
            }
            return p;
        }

        private static int[] scan(int m, int n, int s, int i) {
            int[] p = new int[n];
            for (int x = 0; x < n; ++x) {
                p[x] = PERM[m][i][x];
                p[x] += s;
                p[x] %= m;
            }
            return p;
        }

        private static int move(int m, int s, boolean[] u) {
            s %= m;
            while (u[s]) {
                s = s + 1;
                s = s % m;
            }
            return s;
        }
    }

    public static void main(String[] args) throws Exception {
        InputReader in = new InputReader(new FileInputStream("input.txt"));
        PrintWriter out = new PrintWriter(new FileOutputStream("output.txt"));
        //InputReader in = new InputReader(System.in);
        //PrintWriter out = new PrintWriter(System.out);

        System.err.println("Test Case: Elapsed time");
        boolean contd = true;
        for (int test = 1; in.hasNext() && contd; ++test) {
            long beg = System.nanoTime();
            contd = new InsecureInPrague().process(test, in, out);
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
