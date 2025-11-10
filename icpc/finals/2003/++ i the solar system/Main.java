import java.io.*;
import java.util.*;

public class Main {
    private static class MathUtils {
        public static final double PI = 3.1415926535897932384626433832795;
        public static final double EPS = 1e-6;

        public static int sign(double x) {
            if (x + EPS < 0) {
                return -1;
            }
            if (x - EPS > 0) {
                return +1;
            }
            return 0;
        }
    }

    private static class Planet {
        private double a, b, c, e, t;

        public Planet(double a, double b, double t) {
            this.a = a;
            this.b = b;
            this.t = t;
            this.c = Math.sqrt(a * a - b * b);
            this.e = c / a;
        }

        public double get(double time) {
            time = time % t;
            /* From the 2nd Kepler's law we know that the swept square
             * is proportional to the spent time so we can write:
             *   dS/dθ ~ dt/dθ or dS/dθ = α*dt/dθ,
             * where α is a proportion coefficient. Taking integral on both
             * side at θ from 0 to 2*pi results to
             *   α = S/T, S = pi*a*b,
             * where T - planet's revolution time,
             *       S - planet's orbit square.
             */
            double area = a * b * MathUtils.PI;
            double goal = time * area / t;
            double l = 0, r = 2 * MathUtils.PI;
            double φ = 0;
            for (int it = 0; it < 40; ++it) {
                /* Let's define ellipse in parametric form:
                 *   x = a*cos(φ),
                 *   y = b*cos(φ),
                 * where φ is eccentric anomaly (not an angel from origin
                 * to the point (x,y) on the ellipse curve).
                 *
                 * In order to find swept square let's define an ellipse in the polar form:
                 *   r = a*(1-e^2)/(1+e*cos(θ)),
                 * where r - focus polar radius,
                 *       θ - focus polar angel.
                 * The element square in polar coordinates is defined as
                 *   dA = r^2*dθ/2,
                 * so A can be found using Weierstrass substitution.
                 * But the simplest way is to use simple geometry:
                 *   A(θ) = a*b*φ/2 - c*d*sin(θ)/2
                 * where φ -- eccentric anomaly,
                 *       θ -- origin polar angel.
                 */
                φ = (l + r) / 2;
                double x = a * Math.cos(φ);
                double y = b * Math.sin(φ);
                double d = Math.sqrt(x * x + y * y);
                double θ = Math.atan2(y, x);
                double swept = a * b * φ / 2 - c * d * Math.sin(θ) / 2;
                assert MathUtils.sign(swept) >= 0 : "swept should be greater or equal to zero";
                if (MathUtils.sign(swept - goal) > 0)
                    r = φ;
                else
                    l = φ;
            }
            return φ;
        }

        public static double getPeriod(double a1, double a2, double t1) {
            // from the 3rd Kepler's law we can find revolution time of the second planet
            double a = a2 / a1;
            return t1 * Math.sqrt(a * a * a);
        }
    }

    public static void main(String[] args) {
        Scanner in = new Scanner(System.in);
        PrintWriter out = new PrintWriter(System.out);
        for (int testCase = 1; true; ++testCase) {
            double a1 = in.nextInt();
            double b1 = in.nextInt();
            double t1 = in.nextInt();
            double a2 = in.nextInt();
            double b2 = in.nextInt();
            double t = in.nextInt();
            if (a1 + b1 + b2 + b2 == 0) {
                break;
            }
            assert a1 >= b1 : "planet1: major should be greater or equal to minor";
            assert a2 >= b2 : "planet2: major should be greater or equal to minor";
            double period = Planet.getPeriod(a1, a2, t1);
            Planet planet = new Planet(a2, b2, period);
            double φ = planet.get(t);
            out.printf("Solar System %d: %.3f %.3f\n", testCase,
                    a2 * Math.cos(φ),
                    b2 * Math.sin(φ));
        }
        in.close();
        out.close();
    }
}
