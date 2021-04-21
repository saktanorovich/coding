import java.io.*;
import java.util.*;

public class Main {
    private static final class ArsLonga {
        public boolean process(int testCase, InputReader in, PrintWriter out) {
            int J = in.nextInt();
            int R = in.nextInt();
            if (J == 0 && R == 0) {
                return false;
            }
            double[] X = new double[J];
            double[] Y = new double[J];
            double[] Z = new double[J];
            for (int i = 0; i < J; ++i) {
                X[i] = in.nextDouble();
                Y[i] = in.nextDouble();
                Z[i] = in.nextDouble();
            }
            int N = 0;
            int O[] = new int[J];
            Arrays.fill(O, -1);
            for (int i = 0; i < J; ++i) {
                int z = sign(Z[i]);
                if (z > 0) {
                    O[i] = N++;
                }
            }
            double A[][] = new double[3 * N][R + 1];
            for (int i = 0; i < R; ++i) {
                int a = in.nextInt() - 1;
                int b = in.nextInt() - 1;
                int x = O[a];
                if (x >= 0) {
                    A[3 * x + 0][i] = X[a] - X[b];
                    A[3 * x + 1][i] = Y[a] - Y[b];
                    A[3 * x + 2][i] = Z[a] - Z[b];
                    A[3 * x + 0][R] = 0;
                    A[3 * x + 1][R] = 0;
                    A[3 * x + 2][R] = 1;
                }
                int y = O[b];
                if (y >= 0) {
                    A[3 * y + 0][i] = X[b] - X[a];
                    A[3 * y + 1][i] = Y[b] - Y[a];
                    A[3 * y + 2][i] = Z[b] - Z[a];
                    A[3 * y + 0][R] = 0;
                    A[3 * y + 1][R] = 0;
                    A[3 * y + 2][R] = 1;
                }
            }
            out.format("Sculpture %d: ", testCase);
            int res = solve(A, 3 * N, R);
            if (res == 0) {
                out.println("NON-STATIC");
            } else if (res == 1) {
                out.println("UNSTABLE");
            } else {
                out.println("STABLE");
            }
            return true;
        }

        private static int solve(double[][] A, int n, int m) {
            int row = 0;
            int col = 0;
            for (; col < m && row < n; ++col) {
                int max = row;
                for (int idx = row + 1; idx < n; ++idx) {
                    if (Math.abs(A[max][col]) < Math.abs(A[idx][col])) {
                        max = idx;
                    }
                }
                if (sign(A[max][col]) == 0) {
                    continue;
                }
                double[] temp = A[max];
                A[max] = A[row];
                A[row] = temp;
                for (int idx = row + 1; idx < n; ++idx) {
                    double z = A[idx][col] / A[row][col];
                    for (int cur = col; cur <= m; ++cur) {
                        A[idx][cur] -= A[row][cur] * z;
                    }
                }
                ++row;
            }
            for (int idx = row; idx < n; ++idx) {
                if (sign(A[idx][m]) != 0) {
                    return 0;
                }
            }
            return row < n ? 1 : 2;
        }

        private static final int sign(double x) {
            if (x + EPS < 0) {
                return -1;
            }
            if (x - EPS > 0) {
                return +1;
            }
            return 0;
        }

        private static final double EPS = 1e-8;
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
                contd = new ArsLonga().process(test, in, out);
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
