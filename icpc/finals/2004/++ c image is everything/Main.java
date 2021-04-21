import java.io.*;
import java.util.*;

public class Main {
    private static final class ImageIsEverything {
        public boolean process(int testCase, InputReader in, PrintWriter out) {
            int n = in.nextInt();
            if (n == 0) {
                return false;
            }
            char v[][][] = new char[6][n][n];
            for (int i = 0; i < n; ++i) {
                for (int k = 0; k < 6; ++k) {
                    v[k][i] = in.next().toCharArray();
                }
            }
            char[][][] c = new char[n][n][n];
            for (int i = 0; i < n; ++i) {
                for (int j = 0; j < n; ++j) {
                    Arrays.fill(c[i][j], '?');
                }
            }
            for (int k = 0; k < 6; ++k) {
                for (int i = 0; i < n; ++i) {
                    for (int j = 0; j < n; ++j) {
                        if (v[k][i][j] != '.') {
                            continue;
                        }
                        for (int d = 0; d < n; ++d) {
                            XYZ P = get(n, k, i, j, d);
                            int x = P.x;
                            int y = P.y;
                            int z = P.z;
                            c[x][y][z] = '.';
                        }
                    }
                }
            }
            for (boolean fill = true; fill;) {
                fill = false;
                for (int k = 0; k < 6; ++k) {
                    for (int i = 0; i < n; ++i) {
                        for (int j = 0; j < n; ++j) {
                            if (v[k][i][j] == '.') {
                                continue;
                            }
                            char b = v[k][i][j];
                            for (int d = 0; d < n; ++d) {
                                XYZ P = get(n, k, i, j, d);
                                int x = P.x;
                                int y = P.y;
                                int z = P.z;
                                if (c[x][y][z] == '.') {
                                    continue;
                                }
                                if (c[x][y][z] == '?') {
                                    c[x][y][z] = b;
                                    fill= true;
                                    break;
                                }
                                if (c[x][y][z] == b) {
                                    break;
                                } else {
                                    // we can see different colors only through space
                                    c[x][y][z] = '.';
                                    fill = true;
                                }
                            }
                        }
                    }
                }
            }
            verify(c, v, n);
            int res = 0;
            for (int i = 0; i < n; ++i) {
                for (int j = 0; j < n; ++j) {
                    for (int k = 0; k < n; ++k) {
                        // ? blocks can be colored any color
                        if (c[i][j][k] != '.') {
                            res = res + 1;
                        }
                    }
                }
            }
            out.format("Maximum weight: %d gram(s)\n", res);
            return true;
        }

        public void generate(PrintWriter out, int testCases) {
            Random rand = new Random(50847534);
            for (int testCase = 1; testCase <= testCases; ++testCase) {
                int n = rand.nextInt(10) + 1;
                char c[][][] = new char[n][n][n];
                for (int i = 0; i < n; ++i) {
                    for (int j = 0; j < n; ++j) {
                        for (int k = 0; k < n; ++k) {
                            int x = rand.nextInt(100) % 27;
                            if (x == 0) {
                                c[i][j][k] = '.';
                            } else {
                                c[i][j][k] = (char)('A' + x - 1);
                            }
                        }
                    }
                }
                if (testCase % 5 == 0) {
                    for (int t = 0; t < n * n; ++t) {
                        int k = rand.nextInt(6);
                        int i = rand.nextInt(n);
                        int j = rand.nextInt(n);
                        for (int d = 0; d < n; ++d) {
                            XYZ P = get(n, k, i, j, d);
                            int x = P.x;
                            int y = P.y;
                            int z = P.z;
                            c[x][y][z] = '.';
                        }
                    }
                }
                output(out, view(c, n), n);
            }
        }

        private static void verify(char[][][] c, char[][][] v, int n) {
            char[][][] w = view(c, n);
            for (int k = 0; k < 6; ++k) {
                for (int i = 0; i < n; ++i) {
                    for (int j = 0; j < n; ++j) {
                        if (v[k][i][j] != w[k][i][j]) {
                            output(new PrintWriter(System.err), v, n);
                            output(new PrintWriter(System.err), w, n);
                            String message = String.format("Diff in view # %d: (%d %d)", k, i, j);
                            throw new RuntimeException(message);
                        }
                    }
                }
            }
        }

        private static void output(PrintWriter out, char[][][] v, int n) {
            out.println(n);
            for (int i = 0; i < n; ++i) {
                for (int k = 0; k < 6; ++k) {
                    for (int j = 0; j < n; ++j) {
                        out.print(v[k][i][j]);
                    }
                    out.print(' ');
                }
                out.println();
            }
            out.flush();
        }

        private static char[][][] view(char[][][] c, int n) {
            char[][][] v = new char[6][n][n];
            for (int k = 0; k < 6; ++k) {
                for (int i = 0; i < n; ++i) {
                    for (int j = 0; j < n; ++j) {
                        v[k][i][j] = '.';
                        for (int d = 0; d < n; ++d) {
                            XYZ P = get(n, k, i, j, d);
                            int x = P.x;
                            int y = P.y;
                            int z = P.z;
                            if (c[x][y][z] == '?') {
                                throw new RuntimeException();
                            }
                            if (c[x][y][z] != '.') {
                                v[k][i][j] = c[x][y][z];
                                break;
                            }
                        }
                    }
                }
            }
            return v;
        }

        private static XYZ get(int n, int v, int i, int j, int d) {
            // the order is: front, left, back, right, top, bottom
            int m = n - 1;
            int x1 = j;
            int y1 = d;
            int z1 = i;
            int x2 = m - j;
            int y2 = m - d;
            int z2 = m - i;
            switch (v) {
                case 0: return new XYZ(x1, z2, y1);
                case 1: return new XYZ(y1, z2, x2);
                case 2: return new XYZ(x2, z2, y2);
                case 3: return new XYZ(y2, z2, x1);
                case 4: return new XYZ(x1, y2, z2);
                case 5: return new XYZ(x1, y1, z1);
            }
            return null;
        }

        private static class XYZ {
            public final int x;
            public final int y;
            public final int z;

            public XYZ(int x, int y, int z) {
                this.x = x;
                this.y = y;
                this.z = z;
            }
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
            contd = new ImageIsEverything().process(test, in, out);
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
