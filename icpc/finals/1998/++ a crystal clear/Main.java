import java.io.*;
import java.util.*;

public class Main {
    private static final class Polygon {
        private final double pi = 3.1415926535897932384626433832795;
        private final double[][] cover;
        private final int[] x;
        private final int[] y;
        private final int n;
        private int xbound;
        private int ybound;

        public Polygon(int[] x, int[] y) {
            this.x = x;
            this.y = y;
            this.n = y.length;
            xbound = 0;
            ybound = 0;
            for (int i = 0; i < n; ++i) {
                xbound = Math.max(xbound, x[i]);
                ybound = Math.max(ybound, y[i]);
            }
            this.cover = new double[xbound + 1][ybound + 1];
        }

        private double len(int x, int y) {
            return Math.sqrt(x * x + y * y);
        }

        private double angel(int x1, int y1, int x2, int y2) {
            int xx = x2 - x1;
            int yy = y1 - y2;
            double cos = yy / len(xx, yy);
            return Math.acos(cos);
        }

        private void scan(int x1, int y1, int x2, int y2) {
            int sign = x1 < x2 ? 1 : -1;
            int xmin = Math.min(x1, x2);
            int xmax = Math.max(x1, x2);
            int ymin = Math.min(y1, y2);
            int ymax = Math.max(y1, y2);
            for (int y = 0; y < ymin; ++y) {
                cover[xmin][y] += sign * pi;
                cover[xmax][y] += sign * pi;
                for (int x = xmin + 1; x <= xmax - 1; ++x) {
                    cover[x][y] += sign * 2 * pi;
                }
            }
            if (y1 == y2) {
                cover[xmin][y1] += sign * pi / 2;
                cover[xmax][y1] += sign * pi / 2;
                for (int x = xmin + 1; x <= xmax - 1; ++x) {
                    cover[x][y1] += sign * pi;
                }
            }
            else {
                cover[x1][y1] += sign * angel(x1, y1, x2, y2);
                cover[x2][y2] += sign * angel(x2, y2, x1, y1);
                for (int x = xmin; x <= xmax; ++x) {
                    for (int y = ymin; y <= ymax; ++y) {
                        if (x == x1 && y == y1) continue;
                        if (x == x2 && y == y2) continue;

                        int rotate = (x - x1) * (y2 - y1) - (y - y1) * (x2 - x1);
                        if (rotate == 0) {
                            cover[x][y] += sign * pi;
                            continue;
                        }
                        int xx = x2 - x1;
                        int yy = y2 - y1;
                        double nn = len(xx, yy);
                        double ux = xx / nn;
                        double uy = yy / nn;
                        double dd = Math.abs(ux * (y - y1) - uy * (x - x1));
                        if (dd >= 0.5) {
                            if (rotate * sign > 0) {
                                if (x == x1 || x == x2) {
                                    cover[x][y] += sign * pi;
                                } else {
                                    cover[x][y] += sign * 2 * pi;
                                }
                            }
                        } else {
                            cover[x][y] = -1e6;
                        }
                    }
                }
            }
        }

        public double area() {
            for (int i = 0; i < n; ++i) {
                int x1 = x[i];
                int y1 = y[i];
                int x2 = x[(i + 1) % n];
                int y2 = y[(i + 1) % n];
                if (x1 != x2) {
                    scan(x1, y1, x2, y2);
                }
            }
            double res = 0;
            for (int x = 0; x <= xbound; ++x) {
                for (int y = 0; y <= ybound; ++y) {
                    if (cover[x][y] > 0) {
                        res += cover[x][y];
                    }
                }
            }
            return 0.25 * res / 2;
        }
    }

    public static void main(String[] args) throws FileNotFoundException {
        Scanner in = new Scanner(System.in);
        PrintWriter out = new PrintWriter(System.out);
        for (int test_case = 1; true; ++test_case) {
            int numOfPoints = in.nextInt();
            if (numOfPoints > 0) {
                assert numOfPoints > 2 : "incorrect number of points";
                int[] x = new int[numOfPoints];
                int[] y = new int[numOfPoints];
                int xmin = 0;
                int ymin = 0;
                for (int i = 0; i < numOfPoints; ++i) {
                    x[i] = in.nextInt();
                    y[i] = in.nextInt();
                    xmin = Math.min(xmin, x[i]);
                    ymin = Math.min(ymin, y[i]);
                }
                xmin = -xmin + 1;
                ymin = -ymin + 1;
                for (int i = 0; i < numOfPoints; ++i) {
                    x[i] += xmin;
                    y[i] += ymin;
                }
                out.format("Shape %d\n", test_case);
                out.format("Insulating area = %.3f cm^2\n", new Polygon(x, y).area());
            } else {
                break;
            }
        }
        in.close();
        out.close();
    }
}
