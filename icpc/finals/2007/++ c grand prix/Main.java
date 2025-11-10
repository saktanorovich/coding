import java.io.*;
import java.util.*;

public class Main {
    private static final class GrandPrix {
        public boolean process(int testCase, InputReader in, PrintWriter out) {
            int N = in.nextInt();
            int Q = in.nextInt();
            if (N == 0 && Q == 0) {
                return false;
            }
            int x[] = new int[N + 1];
            int y[] = new int[N + 1];
            for (int i = 1; i <= N; ++i) {
                x[i] = in.nextInt();
                y[i] = in.nextInt();
            }
            if (Q == 0 || inc(N, x, y)) {
                out.format("Case %d: Acceptable as proposed\n\n", testCase);
                return true;
            }
            // map angels to [-pi/2, +pi/2] range
            double Lmin = 0;
            double Lmax = Math.PI;
            double Rmin = 0;
            double Rmax = Math.PI;
            for (int i = 1; i <= N; ++i) {
                int dx = x[i] - x[i - 1];
                int dy = y[i] - y[i - 1];
                double ang = Math.atan2(dy, dx);
                if (MIN <= ang && ang <= MAX) {
                    Lmax = Math.min(Lmax, Math.PI / 2 - ang);
                    Rmax = Math.min(Rmax, Math.PI / 2 + ang);
                } else {
                    if (ang > 0) {
                        Lmin = Math.max(Lmin, +Math.PI / 2 - ang + Math.PI);
                        Rmin = Math.max(Rmin, -Math.PI / 2 + ang);
                    } else {
                        Lmin = Math.max(Lmin, -Math.PI / 2 - ang);
                        Rmin = Math.max(Rmin, +Math.PI / 2 + ang + Math.PI);
                    }
                }
            }
            if (sign(Lmin - Lmax) > 0 && sign(Rmin - Rmax) > 0) {
                out.format("Case %d: Unacceptable\n\n", testCase);
            } else {
                double L = Lmin;
                double R = Rmin;
                if (sign(Lmin - Lmax) > 0) {
                    L = Math.PI;
                }
                if (sign(Rmin - Rmax) > 0) {
                    R = Math.PI;
                }
                if (sign(L - R) < 0) {
                    out.format("Case %d: %s %.2f degrees\n\n", testCase, OK_L, Math.toDegrees(L));
                } else {
                    out.format("Case %d: %s %.2f degrees\n\n", testCase, OK_R, Math.toDegrees(R));
                }
            }
            return true;
        }

        private boolean inc(int N, int[] x, int[] y) {
            for (int i = 1; i <= N; ++i) {
                if (x[i] < x[i - 1]) {
                    return false;
                }
            }
            return true;
        }

        private static final String OK_L = "Acceptable after counterclockwise rotation of";
        private static final String OK_R = "Acceptable after clockwise rotation of";

        private static double MIN = -Math.PI / 2;
        private static double MAX = +Math.PI / 2;
        private static double EPS = 1e-8;

        private static int sign(double x) {
            if (x + EPS < 0) {
                return -1;
            }
            if (x - EPS > 0) {
                return +1;
            }
            return 0;
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
                contd = new GrandPrix().process(test, in, out);
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
