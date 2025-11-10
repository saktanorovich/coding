import java.io.*;
import java.util.*;

public class Main {
    private static final class Replicate {
        public void process(int testCase, InputReader in, PrintWriter out) {
            int m = in.nextInt();
            int n = in.nextInt();
            int f[][] = new int[n][m];
            for (int i = 0; i < n; ++i) {
                String s = in.next();
                for (int j = 0; j < m; ++j) {
                    f[i][j] = ".#".indexOf(s.charAt(j));
                }
            }
            out.print(get(new Pattern(f, n, m)).toString());
        }

        private Pattern get(Pattern f) {
            Pattern r = f.rollback();
            for ( ; r != null; r = f.rollback()) {
                f = r.compress();
            }
            return f;
        }

        private class Pattern {
            private final int f[][];
            private final int n;
            private final int m;

            public Pattern(int[][] f, int n, int m) {
                this.f = f;
                this.n = n;
                this.m = m;
            }

            public Pattern replicate() {
                int r[][] = new int[n + 2][m + 2];
                for (int i = 1; i <= n; ++i) {
                    for (int j = 1; j <= m; ++j) {
                        for (int di = -1; di <= +1; ++di) {
                            for (int dj = -1; dj <= +1; ++dj) {
                                r[i + di][j + dj] ^= f[i - 1][j - 1];
                            }
                        }
                    }
                }
                return new Pattern(r, n + 2, m + 2);
            }

            public Pattern rollback() {
                if (n > 2 && m > 2) {
                    // Let's find spontaneous pixel(s). Observe that each
                    // pixel y(i,j) can be computed as
                    //     c(i,j-2) ^ c(i,j) ^ c(i,j-1)
                    // where c(i,j-1) is defined as
                    //     x(i-1,j) ^ x(i,j) ^ x(i+1,j)
                    // So we can scan each row to the left starting at -2
                    // and to the right starting at +1. Both scans use the
                    // following scan pattern: ..1,2,1,2.. with proper sign.
                    Integer x = null;
                    Integer y = null;
                    for (int i = 0; i < n; ++i) {
                        if (analyze(f[i], m)) {
                            x = i;
                            break;
                        }
                    }
                    for (int j = 0; j < m; ++j) {
                        int[] col = new int[n];
                        for (int i = 0; i < n; ++i) {
                            col[i] = f[i][j];
                        }
                        if (analyze(col, n)) {
                            y = j;
                            break;
                        }
                    }
                    return roll(x, y);
                }
                return null;
            }

            private boolean analyze(int[] pix, int k) {
                int p[][] = new int[k][2];
                int s[][] = new int[k][2];
                p[1][1] = pix[0];
                for (int j = 2; j < k; ++j) {
                    p[j][0] ^= pix[j - 2] ^ p[j - 2][1];
                    p[j][1] ^= pix[j - 1] ^ p[j - 1][0];
                }
                s[k - 2][1] = pix[k - 1];
                for (int j = k - 3; j >= 0; --j) {
                    s[j][0] ^= pix[j + 2] ^ s[j + 2][1];
                    s[j][1] ^= pix[j + 1] ^ s[j + 1][0];
                }
                for (int j = 0; j < k; ++j) {
                    int xor = 0;
                    xor ^= p[j][0];
                    xor ^= s[j][1];
                    if (xor != pix[j]) {
                        return true;
                    }
                }
                return false;
            }

            private Pattern roll(Integer x, Integer y) {
                this.flip(x, y);
                Pattern r = roll();
                Pattern g = r.replicate();
                if (this.equals(g)) {
                    this.flip(x, y);
                    return r;
                }
                this.flip(x, y);
                return null;
            }

            private Pattern roll() {
                int r[][] = new int[n - 2][m - 2];
                for (int i = 1; i < n - 1; ++i) {
                    for (int j = 1; j < m - 1; ++j) {
                        r[i - 1][j - 1] = f[i - 1][j - 1];
                        for (int di = -2; di <= 0; ++di) {
                            for (int dj = -2; dj <= 0; ++dj) {
                                if (di + dj != 0) {
                                    int x = i + di;
                                    int y = j + dj;
                                    if (0 < x && 0 < y) {
                                        r[i - 1][j - 1] ^= r[x - 1][y - 1];
                                    }
                                }
                            }
                        }
                    }
                }
                return new Pattern(r, n - 2, m - 2);
            }

            private void flip(Integer x, Integer y) {
                if (x != null && y != null) {
                    f[x][y] ^= 1;
                }
            }

            public Pattern compress() {
                int imin = n, imax = 0;
                int jmin = m, jmax = 0;
                for (int i = 0; i < n; ++i) {
                    for (int j = 0; j < m; ++j) {
                        if (f[i][j] > 0) {
                            imin = Math.min(imin, i);
                            imax = Math.max(imax, i);
                            jmin = Math.min(jmin, j);
                            jmax = Math.max(jmax, j);
                        }
                    }
                }
                if (imin <= imax && jmin <= jmax) {
                    int a = imax - imin + 1;
                    int b = jmax - jmin + 1;
                    int r[][] = new int[a][b];
                    for (int i = 0; i < a; ++i) {
                        for (int j = 0; j < b; ++j) {
                            r[i][j] = f[imin + i][jmin + j];
                        }
                    }
                    return new Pattern(r, a, b);
                }
                return this;
            }

            public boolean equals(Pattern other) {
                if (this.n == other.n && this.m == other.m) {
                    for (int i = 0; i < n; ++i) {
                        for (int j = 0; j < m; ++j) {
                            if (this.f[i][j] != other.f[i][j]) {
                                return false;
                            }
                        }
                    }
                    return true;
                }
                return false;
            }

            public String toString() {
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < n; ++i) {
                    for (int j = 0; j < m; ++j) {
                        builder.append(".#".charAt(f[i][j]));
                    }
                    builder.append("\n");
                }
                return builder.toString();
            }
        }
    }

    public static void main(String[] args) throws Exception {
        InputReader in = new InputReader(new FileInputStream("input.txt"));
        PrintWriter out = new PrintWriter(new FileOutputStream("output.txt"));
        //InputReader in = new InputReader(System.in);
        //PrintWriter out = new PrintWriter(System.out);

        System.err.println("Test Case: Elapsed time");
        for (int test = 1; in.hasNext(); ++test) {
            long beg = System.nanoTime();
            new Replicate().process(test, in, out);
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
