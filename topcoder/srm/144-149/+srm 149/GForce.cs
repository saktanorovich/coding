using System;

namespace Topcoder.Algorithm {
      public class GForce {
            public double avgAccel(int period, int[] accel, int[] time) {
                  return maxSquare(time, accel, accel.Length, period) / period;
            }

            private double maxSquare(int[] x, int[] y, int n, int period) {
                  double result = 0;
                  for (int i = 0; i < n; ++i) {
                        result = Math.Max(result, square(x, y, x[i] - period, x[i]));
                        result = Math.Max(result, square(x, y, x[i], x[i] + period));
                  }
                  for (int i = 0; i + 1 < n; ++i) {
                        Line linei = new Line(x[i], y[i], x[i + 1], y[i + 1]);
                        for (int j = i + 1; j + 1 < n; ++j) {
                              Line linej = new Line(x[j], y[j], x[j + 1], y[j + 1]);
                              if (linei.slope() != linej.slope()) {
                                    double ki = linei.slope();
                                    double kj = linej.slope();
                                    double bi = linei.inter();
                                    double bj = linej.inter();
                                    double xx = (bi - bj - kj * period) / (kj - ki);
                                    result = Math.Max(result, square(x, y, xx, xx + period));
                              }
                        }
                  }
                  return result;
            }

            private double square(int[] x, int[] y, double a, double b) {
                  double result = 0;
                  for (int i = 0; i + 1 < x.Length; ++i) {
                        if (x[i] <= b && a <= x[i + 1]) {
                              Line line = new Line(x[i], y[i], x[i + 1], y[i + 1]);
                              double x1 = Math.Max(x[i + 0], a);
                              double x2 = Math.Min(x[i + 1], b);
                              double y1 = (-line.C - line.A * x1) / line.B;
                              double y2 = (-line.C - line.A * x2) / line.B;
                              result += 0.5 * (x2 - x1) * (y2 + y1);
                        }
                  }
                  return result;
            }

            private class Line {
                  public double A;
                  public double B;
                  public double C;

                  public Line(double x1, double y1, double x2, double y2) {
                        A = y1 - y2;
                        B = x2 - x1;
                        C = x1 * y2 - y1 * x2;
                  }

                  public double slope() {
                        return -A / B;
                  }

                  public double inter() {
                        return -C / B;
                  }
            }
      }
}