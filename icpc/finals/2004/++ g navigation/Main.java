import java.io.*;
import java.util.*;

public class Main {
    private static final class Navigation {
        public boolean process(int testCase, InputReader in, PrintWriter out) throws Exception {
            int N = in.nextInt();
            double T = in.nextDouble();
            double X = in.nextDouble();
            double Y = in.nextDouble();
            if (N == 0) {
                return false;
            }
            List<Satellite> S = new ArrayList<>();
            for (int i = 0; i < N; ++i) {
                double x = in.nextDouble();
                double y = in.nextDouble();
                double d = in.nextDouble();
                double t = in.nextDouble();
                if (T >= t) {
                    S.add(new Satellite(x, y, d, t));
                }
            }
            PointF target = new PointF(X, Y);
            try {
                PointF source = gps(S, T);
                if (!MathUtils.same(source, target)) {
                    out.format("Trial %d: %d degrees\n", testCase, source.ang(target));
                } else {
                    out.format("Trial %d: Arrived\n", testCase);
                }
            } catch (InconclusiveException e) {
                out.format("Trial %d: Inconclusive\n", testCase);
            } catch (InconsistentException e) {
                out.format("Trial %d: Inconsistent\n", testCase);
            }
            return true;
        }

        private PointF gps( List<Satellite> S, double T) throws Exception {
            if (S.size() == 1) {
                return gps1(S, T);
            }
            if (S.size() == 2) {
                return gps2(S, T);
            }
            List<PointF> pts = new ArrayList<>();
            for (int i = 0; i < S.size(); ++i) {
                for (int j = 0; j < S.size(); ++j) {
                    if (i == j) {
                        continue;
                    }
                    Zone z1 = S.get(i).zone(T);
                    Zone z2 = S.get(j).zone(T);
                    PointF[] at = MathUtils.intersect(z1, z2);
                    loop:
                    for (PointF p : at) {
                        for (Satellite s : S) {
                            if (s.has(p, T) == false) {
                                continue loop;
                            }
                        }
                        add(pts, p);
                    }
                }
            }
            if (pts.size() > 0) {
                if (pts.size() == 1) {
                    return pts.get(0);
                } else {
                    throw new InconclusiveException();
                }
            }
            throw new InconsistentException();
        }

        private PointF gps1(List<Satellite> S, double T) throws Exception {
            Zone z = S.get(0).zone(T);
            if (MathUtils.sign(z.radius) == 0) {
                return z.center;
            }
            throw new InconclusiveException();
        }

        private PointF gps2(List<Satellite> S, double T) throws Exception {
            Zone z1 = S.get(0).zone(T);
            Zone z2 = S.get(1).zone(T);
            PointF[] at = MathUtils.intersect(z1, z2);
            if (at.length > 0) {
                if (at.length == 1) {
                    return at[0];
                }
                throw new InconclusiveException();
            }
            throw new InconsistentException();
        }

        private void add(List<PointF> list, PointF p) {
            for (PointF t : list) {
                if (MathUtils.same(p, t)) {
                    return;
                }
            }
            list.add(p);
        }

        private class InconclusiveException extends Exception { }
        private class InconsistentException extends Exception { }

        private static final class Satellite {
            public final PointF origin;
            public final double look;
            public final double time;
            public final PointF pos;

            public Satellite(double x, double y, double look, double time) {
                this.origin = new PointF(x, y);
                this.look = look;
                this.time = time;
                PointF vect = new PointF(1, 0);
                vect = vect.rot(look);
                vect = vect.mul(time * 100.0);
                this.pos = origin.add(vect);
            }

            public boolean has(PointF p, double T) {
                if (T >= time) {
                    return zone(T).has(p);
                }
                return false;
            }

            public Zone zone(double T) {
                return new Zone(pos, (T - time) * 350.0);
            }
        }
    }

    private static final class PointF {
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

        public PointF mul(double f) {
            return new PointF(x * f, y * f);
        }

        public PointF rot(double ang) {
            double phi = MathUtils.compass2Cartesian(ang);
            double nx = x * Math.cos(phi) - y * Math.sin(phi);
            double ny = x * Math.sin(phi) + y * Math.cos(phi);
            return new PointF(nx, ny);
        }

        public int ang(PointF p) {
            PointF look = p.sub(this);
            double res = MathUtils.ang(look);
            return (int)(res + 0.5);
        }
    }

    private static final class Zone {
        public final double radius;
        public final PointF center;

        public Zone(PointF center, double radius) {
            this.center = center;
            this.radius = radius;
        }

        public boolean has(PointF p) {
            double dist = MathUtils.dist(p, center);
            if (MathUtils.sign(dist - radius) == 0) {
                return true;
            }
            return false;
        }
    }

    private static final class MathUtils {
        public static final double EPS = 0.1;

        public static int sign(double x) {
            if (x + EPS < 0.0) {
                return -1;
            }
            if (x - EPS > 0.0) {
                return +1;
            }
            return 0;
        }

        public static boolean same(PointF a, PointF b) {
            return sign(dist(a, b)) == 0;
        }

        public static double dist(PointF a, PointF b) {
            return Math.hypot(a.x - b.x, a.y - b.y);
        }

        public static double ang(PointF p) {
            return cartesian2Compass(Math.atan2(p.y, p.x));
        }

        public static double compass2Cartesian(double ang) {
            if (ang < 0) {
                ang = 360.0 + ang;
            }
            if (ang > 0) {
                ang = 360.0 - ang;
            }
            ang = Math.toRadians(ang);
            ang = ang + Math.PI / 2;
            if (ang >= 2 * Math.PI) {
                ang -= 2 * Math.PI;
            }
            return ang;
        }

        public static double cartesian2Compass(double phi) {
            phi = phi - Math.PI / 2;
            if (phi < 0) {
                phi = 2 * Math.PI + phi;
            }
            if (phi > 0) {
                phi = 2 * Math.PI - phi;
            }
            phi = Math.toDegrees(phi);
            return phi;
        }

        public static PointF[] intersect(Zone z1, Zone z2) {
            double d = dist(z1.center, z2.center);
            double a = Math.abs(z1.radius - z2.radius);
            double b = Math.abs(z1.radius + z2.radius);
            if (sign(a - d) <= 0 && sign(d - b) <= 0 && d > 0) {
                double r = (z1.radius * z1.radius - z2.radius * z2.radius + d * d) / (2 * d);
                double cos = (z2.center.x - z1.center.x) / d;
                double sin = (z2.center.y - z1.center.y) / d;
                if (sign(a - d) < 0 && sign(d - b) < 0) {
                    double h = Math.sqrt(z1.radius * z1.radius - r * r);
                    double x1 = z1.center.x + (r * cos - (+h) * sin);
                    double y1 = z1.center.y + (r * sin + (+h) * cos);
                    double x2 = z1.center.x + (r * cos - (-h) * sin);
                    double y2 = z1.center.y + (r * sin + (-h) * cos);
                    return new PointF[] {
                        new PointF(x1, y1),
                        new PointF(x2, y2)
                    };
                } else {
                    double x = z1.center.x + r * cos;
                    double y = z1.center.y + r * sin;
                    return new PointF[] {
                        new PointF(x, y)
                    };
                }
            }
            return new PointF[0];
        }

        static {
            assert MathUtils.ang(new PointF(+0, +1)) ==   0;
            assert MathUtils.ang(new PointF(+1, +1)) ==  45;
            assert MathUtils.ang(new PointF(+1, +0)) ==  90;
            assert MathUtils.ang(new PointF(+1, -1)) == 135;
            assert MathUtils.ang(new PointF(+0, -1)) == 180;
            assert MathUtils.ang(new PointF(-1, -1)) == 225;
            assert MathUtils.ang(new PointF(-1, +0)) == 270;
            assert MathUtils.ang(new PointF(-1, +1)) == 315;

            assert MathUtils.compass2Cartesian(  0) == 1 * Math.PI / 2;
            assert MathUtils.compass2Cartesian( 45) == 1 * Math.PI / 4;
            assert MathUtils.compass2Cartesian( 90) == 0 * Math.PI;
            assert MathUtils.compass2Cartesian(135) == 7 * Math.PI / 4;
            assert MathUtils.compass2Cartesian(180) == 3 * Math.PI / 2;
            assert MathUtils.compass2Cartesian(225) == 5 * Math.PI / 4;
            assert MathUtils.compass2Cartesian(270) == 1 * Math.PI;
            assert MathUtils.compass2Cartesian(315) == 3 * Math.PI / 4;

            assert MathUtils.compass2Cartesian(-315) == 1 * Math.PI / 4;
            assert MathUtils.compass2Cartesian(-270) == 0 * Math.PI;
            assert MathUtils.compass2Cartesian(-225) == 7 * Math.PI / 4;
            assert MathUtils.compass2Cartesian(-180) == 3 * Math.PI / 2;
            assert MathUtils.compass2Cartesian(-135) == 5 * Math.PI / 4;
            assert MathUtils.compass2Cartesian( -90) == 1 * Math.PI;
            assert MathUtils.compass2Cartesian( -45) == 3 * Math.PI / 4;
        }
    }

    public static void main(String[] args) throws Exception {
        InputReader in = new InputReader(System.in);
        PrintWriter out = new PrintWriter(System.out);

        if (args.length > 0 && args[0].equals("-g")) {
        } else {
            System.err.println("Test Case: Elapsed time");
            boolean contd = true;
            for (int test = 1; in.hasNext() && contd; ++test) {
                long beg = System.nanoTime();
                contd = new Navigation().process(test, in, out);
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
