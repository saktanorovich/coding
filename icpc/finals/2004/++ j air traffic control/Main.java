import java.io.*;
import java.util.*;

public class Main {
    private static final class AirTrafficControl {
        public boolean process(int testCase, InputReader in, PrintWriter out) {
            int NP = in.nextInt();
            int NC = in.nextInt();
            if (NP == 0 && NC == 0) {
                return false;
            }
            PointF P[] = new PointF[NP];
            for (int i = 0; i < NP; ++i) {
                double x = in.nextDouble();
                double y = in.nextDouble();
                P[i] = new PointF(x, y);
            }
            Center C[] = new Center[NC];
            for (int i = 0; i < NC; ++i) {
                int count = in.nextInt();
                double x1 = in.nextDouble();
                double y1 = in.nextDouble();
                double x2 = in.nextDouble();
                double y2 = in.nextDouble();
                C[i] = new Center(count, x1, y1, x2, y2);
            }
            int res[] = get(P, C);
            if (res != null) {
                out.format("Trial %d:", testCase);
                for (int x : res) {
                    out.format("  %d", x);
                }
                out.format("\n\n");
            } else {
                out.format("Trial %d:  Impossible\n\n", testCase);
            }
            return true;
        }

        private int[] get(PointF[] P, Center[] C) {
            Arrays.sort(P, (a, b) -> {
                if (sign(b.y - a.y) != 0) {
                    return sign(b.y - a.y);
                } else {
                    return sign(b.x - a.x);
                }
            });
            int[] pres = new int[P.length];
            for (Center c : C) {
                int ctrl[] = get(P, c);
                if (ctrl != null) {
                    for (int i = 0; i < P.length; ++i) {
                        pres[i] += ctrl[i];
                    }
                } else return null;
            }
            int[] cres = new int[C.length + 1];
            for (int t : pres) {
                cres[t]++;
            }
            return cres;
        }

        private int[] get(PointF[] P, Center C) {
            Span best = new Span(null, 0, 0);
            for (PointF p : P) {
                Span span = C.span(p);
                if (span != null) {
                    span.mark(P);
                    if (better(span, best)) {
                        best = span;
                    }
                }
            }
            return best.control;
        }

        private boolean better(Span span, Span best) {
            if (span.control == null) {
                return false;
            }
            if (best.control == null) {
                return true;
            }
            // We will prefer to monitor planes that are closer to the
            // control center rather than ones that are farther away.
            // Otherwise choose the span that includes the airplane that
            // is farthest to the north breaking ties by choosing the
            // airplane that is farthest to the north then to the east.
            int sign = sign(span.radius - best.radius);
            if (sign < 0) {
                return true;
            }
            if (sign > 0) {
                return false;
            }
            return better(span.control, best.control);
        }

        private boolean better(int[] span, int[] best) {
            for (int i = 0; i < span.length; ++i) {
                if (span[i] > best[i]) {
                    return true;
                }
                if (span[i] < best[i]) {
                    return false;
                }
            }
            return false;
        }

        private final class Center {
            public final PointF p1;
            public final PointF p2;
            public final int count;

            public Center(int count, double x1, double y1, double x2, double y2) {
                this.count = count;
                this.p1 = new PointF(x1, y1);
                this.p2 = new PointF(x2, y2);
            }

            public Span span(PointF p3) {
                // the center of the circle is the intersection of the
                // two lines perpendicular to and passing through the
                // midpoints of the lines p1p3 and p2p3
                PointF m1 = p1.add(p3).mul(0.5);
                PointF m2 = p2.add(p3).mul(0.5);
                Line l1 = new Line(m1, m1.add(new Line(p1, p3).normal()));
                Line l2 = new Line(m2, m2.add(new Line(p2, p3).normal()));
                PointF c = l1.intersect(l2);
                if (c != null) {
                    return new Span(c, c.dist(p3), count);
                } else {
                    return null;
                }
            }
        }

        private final class Span {
            public final PointF center;
            public final double radius;
            public final int planes;
            public int[] control;

            public Span(PointF center, double radius, int planes) {
                this.center = center;
                this.radius = radius;
                this.planes = planes;
                this.control = null;
            }

            public void mark(PointF[] P) {
                int in = 0;
                int on = 0;
                control = new int[P.length];
                for (int i = 0; i < P.length; ++i) {
                    int sign = sign(center.dist(P[i]) - radius);
                    if (sign == 0) {
                        on++;
                        control[i] = 2;
                    }
                    if (sign < 0) {
                        in++;
                        control[i] = 1;
                    }
                }
                // planes can be equal to in when planes is zero
                if (in <= planes && planes <= in + on) {
                    on  = planes - in;
                    for (int i = 0; i < P.length; ++i) {
                        if (control[i] == 2) {
                            if (on > 0) {
                                on--;
                                control[i] = 1;
                            } else {
                                control[i] = 0;
                            }
                        }
                    }
                } else {
                    control = null;
                }
            }
        }

        private final class PointF {
            public final double x;
            public final double y;

            public PointF(double x, double y) {
                this.x = x;
                this.y = y;
            }

            public PointF add(PointF p) {
                return new PointF(x + p.x, y + p.y);
            }

            public PointF sub(PointF p) {
                return new PointF(x - p.x, y - p.y);
            }

            public PointF mul(double a) {
                return new PointF(a * x, a * y);
            }

            public PointF rot(double a) {
                double nx = x * Math.cos(a) - y * Math.sin(a);
                double ny = x * Math.sin(a) + y * Math.cos(a);
                return new PointF(nx, ny);
            }

            public double dist(PointF p) {
                return Math.hypot(x - p.x, y - p.y);
            }
        }

        private final class Line {
            private final double A, B, C;

            public Line(PointF a, PointF b) {
                A = a.y - b.y;
                B = b.x - a.x;
                C = a.x * b.y - a.y * b.x;
            }

            public PointF intersect(Line line) {
                if (this.parallel(line) == false) {
                    double x = -1 * det(C, B, line.C, line.B) / det(A, B, line.A, line.B);
                    double y = -1 * det(A, C, line.A, line.C) / det(A, B, line.A, line.B);
                    return new PointF(x,y);
                }
                return null;
            }

            public boolean parallel(Line line) {
                return sign(det(A, B, line.A, line.B)) == 0;
            }

            public PointF normal() {
                double x = +A;
                double y = +B;
                return new PointF(x, y);
            }

            public PointF direct() {
                double x = -B;
                double y = +A;
                return new PointF(x, y);
            }
        }

        private static double det(double a, double b, double c, double d) {
            return a * d - b * c;
        }

        private static int sign(double x) {
            if (x + EPS < 0.0) {
                return -1;
            }
            if (x - EPS > 0.0) {
                return +1;
            }
            return 0;
        }

        private static final double EPS = 1e-5;
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
                contd = new AirTrafficControl().process(test, in, out);
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
