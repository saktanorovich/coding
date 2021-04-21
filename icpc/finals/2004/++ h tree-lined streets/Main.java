import java.io.*;
import java.util.*;

public class Main {
    private static final class TreeLinedStreets {
        public boolean process(int testCase, InputReader in, PrintWriter out) {
            int n = in.nextInt();
            if (n == 0) {
                return false;
            }
            Segment s[] = new Segment[n];
            for (int i = 0; i < n; ++i) {
                int x1 = in.nextInt();
                int y1 = in.nextInt();
                int x2 = in.nextInt();
                int y2 = in.nextInt();
                s[i] = new Segment(new Point(x1, y1), new Point(x2, y2));
            }
            int res = 0;
            for (int i = 0; i < n; ++i) {
                List<Double> d = new ArrayList<>();
                for (int j = 0; j < n; ++j) {
                    if (i == j) {
                        continue;
                    }
                    Point p = s[i].intersect(s[j]);
                    if (p != null) {
                        if (p.equals(s[i].a) == false && p.equals(s[i].b) == false) {
                            d.add(p.distance(s[i].a));
                        }
                    }
                }
                if (d.size() == 0) {
                    res += (int)Math.floor(s[i].length() / 50) + 1;
                } else {
                    d.add(0.0);
                    d.sort(Comparator.naturalOrder());
                    d.add(s[i].length());
                    for (int k = 1; k < d.size(); ++k) {
                        double dd = d.get(k) - d.get(k - 1);
                        if (1 < k && k + 1 < d.size()) {
                            if (MathUtils.sign(dd - 50) >= 0) {
                                res += (int)Math.floor((dd - 50) / 50) + 1;
                            }
                        } else {
                            if (MathUtils.sign(dd - 25) >= 0) {
                                res += (int)Math.floor((dd - 25) / 50) + 1;
                            }
                        }
                    }
                }
            }
            out.format("Map %d\nTrees = %d\n", testCase, res);
            return true;
        }

        private static final class MathUtils {
            private static double EPS = 1e-4;

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
                return Math.hypot(x - p.x, y - p.y);
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

            public Point intersect(Segment s) {
                Point p = this.line().intersect(s.line());
                if (p != null) {
                    if (this.contains(p) && s.contains(p)) {
                        return p;
                    }
                }
                return null;
            }

            public Line line() {
                return new Line(a, b);
            }

            public double length() {
                return a.distance(b);
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
                contd = new TreeLinedStreets().process(test, in, out);
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
