import java.io.*;
import java.util.*;

public class Main {
    private static final class SimplifiedGSMNetwork {
        public boolean process(int testCase, InputReader in, PrintWriter out) {
            int B = in.nextInt();
            int C = in.nextInt();
            int R = in.nextInt();
            int Q = in.nextInt();
            if (B == 0 && C == 0 && R == 0 && Q == 0) {
                return false;
            }
            Point[] towers = new Point[B];
            Point[] cities = new Point[C];
            for (int i = 0; i < B; ++i) {
                double x = in.nextDouble();
                double y = in.nextDouble();
                towers[i] = new Point(x, y);
            }
            for (int i = 0; i < C; ++i) {
                double x = in.nextDouble();
                double y = in.nextDouble();
                cities[i] = new Point(x, y);
            }
            int[][] graph = new int[C][];
            for (int i = 0; i < C; ++i) {
                graph[i] = new int[C];
                Arrays.fill(graph[i], 1000);
                graph[i][i] = 0;
            }
            for (int i = 0; i < R; ++i) {
                int a = in.nextInt() - 1;
                int b = in.nextInt() - 1;
                int c = count(towers, new Segment(cities[a], cities[b]));
                graph[a][b] = c;
                graph[b][a] = c;
            }
            for (int k = 0; k < C; ++k) {
                for (int i = 0; i < C; ++i) {
                    for (int j = 0; j < C; ++j) {
                        if (graph[i][j] > graph[i][k] + graph[k][j]) {
                            graph[i][j] = graph[i][k] + graph[k][j];
                        }
                    }
                }
            }
            out.format("Case %d:\n", testCase);
            for (int i = 0; i < Q; ++i) {
                int x = in.nextInt() - 1;
                int y = in.nextInt() - 1;
                if (graph[x][y] < 1000) {
                    out.println(graph[x][y]);
                } else {
                    out.println("Impossible");
                }
            }
            return true;
        }

        private int count(Point[] towers, Segment s) {
            int res = 0;
            for (int main = 0; main < towers.length; ++main) {
                Segment t = s;
                for (int other = 0; other < towers.length; ++other) {
                    if (main == other) {
                        continue;
                    }
                    t = cover(t, towers[main], towers[other]);
                    if (t == null) {
                        break;
                    }
                }
                if (t != null) res = res + 1;
            }
            return res - 1;
        }

        private Segment cover(Segment s, Point p1, Point p2) {
            // let's find perpendicular line to (p1, p2) through the middle point
            double a = (p1.x - p2.x);
            double b = (p1.y - p2.y);
            double c = (p2.x * p2.x + p2.y * p2.y - p1.x * p1.x - p1.y * p1.y) * 0.5;
            Line line = new Line(a, b, c);
            double eval1 = line.eval(s.p1);
            double eval2 = line.eval(s.p2);
            double evalm = line.eval(p1);
            if (MathUtils.sign(eval1 * eval2) < 0) {
                Point at = line.intersect(new Line(s.p1, s.p2));
                if (MathUtils.sign(evalm * eval1) > 0) {
                    return new Segment(s.p1, at);
                } else {
                    return new Segment(at, s.p2);
                }
            } else {
                // segment should lie in the same half-plane
                if (MathUtils.sign(evalm * eval1) > 0) {
                    return s;
                }
                if (MathUtils.sign(evalm * eval2) > 0) {
                    return s;
                } else {
                    return null;
                }
            }
        }

        private static final class MathUtils {
            private static double EPS = 1e-9;

            public static int sign(double x) {
                if (x + EPS < 0) {
                    return -1;
                }
                if (x - EPS > 0) {
                    return +1;
                }
                return 0;
            }

            public static double det(double a, double b, double c, double d) {
                return a * d - b * c;
            }
        }

        private static final class Point {
            public final double x;
            public final double y;

            public Point(double x, double y) {
                this.x = x;
                this.y = y;
            }
        }

        private static final class Segment {
            public final Point p1;
            public final Point p2;

            public Segment(Point p1, Point p2) {
                this.p1 = p1;
                this.p2 = p2;
            }
        }

        private static final class Line {
            public final double A;
            public final double B;
            public final double C;

            public Line(Point a, Point b) {
                A = a.y - b.y;
                B = b.x - a.x;
                C = a.x * b.y - a.y * b.x;
            }

            public Line(double a, double b, double c) {
                A = a;
                B = b;
                C = c;
            }

            public double eval(Point p) {
                return A * p.x + B * p.y + C;
            }

            public Point intersect(Line line) {
                if (this.parallel(line) == false) {
                    double x = -1 * MathUtils.det(this.C, this.B, line.C, line.B) / MathUtils.det(this.A, this.B, line.A, line.B);
                    double y = -1 * MathUtils.det(this.A, this.C, line.A, line.C) / MathUtils.det(this.A, this.B, line.A, line.B);
                    return new Point(x,y);
                }
                return null;
            }

            public boolean parallel(Line line) {
                return MathUtils.sign(MathUtils.det(this.A, this.B, line.A, line.B)) == 0;
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
                contd = new SimplifiedGSMNetwork().process(test, in, out);
                out.flush();
                long end = System.nanoTime();
                if (contd) {
                    System.err.format("Test #%3d: %9.3f ms\n", test, (end - beg) / 1e6);
                }
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
