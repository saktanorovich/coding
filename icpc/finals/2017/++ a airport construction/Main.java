import java.io.*;
import java.util.*;

public class Main {
    private static final class AirportConstruction {
        public void process(int testCase, InputReader in, PrintWriter out) {
            this.n = in.nextInt();
            this.p = new Point[n + 1];
            for (int i = 0; i < n; ++i) {
                int x = in.nextInt();
                int y = in.nextInt();
                p[i] = new Point(x, y);
            }
            this.p[n] = p[0];
            double res = 0;
            for (int i = 0; i < n; ++i) {
                for (int j = 0; j < i; ++j) {
                    res = get(p[i], p[j], res);
                }
            }
            out.format("%.9f\n", res);
        }

        private double get(Point a, Point b, double best) {
            Point z[] = cut(a, b);
            int m = z.length - 1;
            if (z[0].distance(z[m]) > best) {
                for (int i = 0; i <= m; ++i) {
                    if (z[i].equals(a) || z[i].equals(b)) {
                        Point pt0 = z[i];
                        Point pt1 = z[i];
                        for (int j = i - 1; j >= 0; --j) {
                            if (z[j].equals(z[j + 1]) == false) {
                                Point c = z[j].add(z[j + 1]).mul(0.5);
                                if (has(c) == false) {
                                    break;
                                }
                                pt0 = z[j];
                            }
                        }
                        for (int j = i + 1; j <= m; ++j) {
                            if (z[j].equals(z[j - 1]) == false) {
                                Point c = z[j].add(z[j - 1]).mul(0.5);
                                if (has(c) == false) {
                                    break;
                                }
                                pt1 = z[j];
                            }
                        }
                        best = Math.max(best, pt0.distance(pt1));
                    }
                }
            }
            return best;
        }

        // Let's find intersection point using vector arithmetic
        // to minimize number of arithmetic operations. Consider
        // two vectors A=(a,b) and U=(u,v). We can write
        //   a + α * A = u + β * U
        // or crossing with U (UxU=0)
        //   a x U + α * A x U = u x U
        // From the last equation we can find that
        //   α = (u-a) x U / A x U
        // If we denote na=(a-u)xU and nb=(b-u)xU then the last
        // equation can be rewritten as
        //   α = -na / (nb-na) = na / (na-nb).
        // We do not use line intersection due to presicion errors.
        private Point[] cut(Point u, Point v) {
            List<Point> res = new ArrayList<>();
            for (int i = 0; i < n; ++i) {
                Segment s = new Segment(p[i], p[i + 1]);
                double na = MathUtils.vector(s.a.sub(u), v.sub(u));
                double nb = MathUtils.vector(s.b.sub(u), v.sub(u));
                if (MathUtils.sign(na * nb) <= 0) {
                    if (MathUtils.sign(na - nb) != 0) {
                        res.add(s.a.add(s.b.sub(s.a).mul(na / (na - nb))));
                    }
                }
            }
            res.sort((a, b) -> {
                Double sa = MathUtils.scalar(a.sub(u), v.sub(u));
                Double sb = MathUtils.scalar(b.sub(u), v.sub(u));
                return sa.compareTo(sb);
            });
            return res.toArray(new Point[res.size()]);
        }

        private boolean has(Point t) {
            boolean res = false;
            for (int i = 0; i < n; ++i) {
                Segment s = new Segment(p[i], p[i + 1]);
                if (s.contains(t)) {
                    return true;
                }
                int a = MathUtils.sign(s.b.y - t.y);
                int b = MathUtils.sign(s.a.y - t.y);
                if (a >= 0 != b >= 0) {
                    if (s.a.y < s.b.y) {
                        res ^= MathUtils.sign(MathUtils.vector(s.b.sub(s.a), t.sub(s.a))) > 0;
                    } else {
                        res ^= MathUtils.sign(MathUtils.vector(s.a.sub(s.b), t.sub(s.a))) > 0;
                    }
                }
            }
            return res;
        }

        private Point p[];
        private int n;
    }

    public static void main(String[] args) throws Exception {
        InputReader in = new InputReader(new FileInputStream("input.txt"));
        PrintWriter out = new PrintWriter(new FileOutputStream("output.txt"));
        //InputReader in = new InputReader(System.in);
        //PrintWriter out = new PrintWriter(System.out);

        System.err.println("Test Case: Elapsed time");
        for (int test = 1; in.hasNext(); ++test) {
            long beg = System.nanoTime();
            new AirportConstruction ().process(test, in, out);
            out.flush();
            long end = System.nanoTime();
            System.err.format("Test #%3d: %9.3f ms\n", test, (end - beg) / 1e6);
        }

        in.close();
        out.close();
    }

    private static final class MathUtils {
        private static double EPS = 1e-12;

        public static int sign(double x) {
            if (x + EPS < 0) {
                return -1;
            }
            if (x - EPS > 0) {
                return +1;
            }
            return 0;
        }

        public static double vector(Point a, Point b) {
            return a.x * b.y - a.y * b.x;
        }

        public static double scalar(Point a, Point b) {
            return a.x * b.x + a.y * b.y;
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

        public boolean equals(Point p) {
            if (p != null) {
                return MathUtils.sign(x - p.x) == 0
                    && MathUtils.sign(y - p.y) == 0;
            }
            return false;
        }

        public Point add(Point p) {
            return new Point(x + p.x, y + p.y);
        }

        public Point sub(Point p) {
            return new Point(x - p.x, y - p.y);
        }

        public Point mul(double a) {
            return new Point(a * x, a * y);
        }

        public double distance(Point p) {
            return Math.sqrt((x - p.x) * (x - p.x) + (y - p.y) * (y - p.y));
        }
    }

    private static final class Segment {
        public final Point a;
        public final Point b;

        public Segment(Point a, Point b) {
            this.a = a;
            this.b = b;
        }

        public boolean contains(Point p) {
            double v = MathUtils.vector(p.sub(a), b.sub(a));
            double s = MathUtils.scalar(a.sub(p), b.sub(p));
            if (MathUtils.sign(v) == 0) {
                return MathUtils.sign(s) <= 0;
            }
            return false;
        }

        public Line line() {
            return new Line(a, b);
        }
    }

    private static final class Line {
        private final double A, B, C;

        public Line(Point a, Point b) {
            A = a.y - b.y;
            B = b.x - a.x;
            C = a.x * b.y - a.y * b.x;
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

        public boolean equals(Line line) {
            return MathUtils.sign(MathUtils.det(this.A, this.B, line.A, line.B)) == 0
                && MathUtils.sign(MathUtils.det(this.A, this.C, line.A, line.C)) == 0
                && MathUtils.sign(MathUtils.det(this.B, this.C, line.B, line.C)) == 0;
        }

        public Point normal() {
            double x = +A / Math.sqrt(A * A + B * B);
            double y = +B / Math.sqrt(A * A + B * B);
            return new Point(x, y);
        }

        public Point direct() {
            double x = -B / Math.sqrt(A * A + B * B);
            double y = +A / Math.sqrt(A * A + B * B);
            return new Point(x, y);
        }
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
