using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TopCoder.Algorithm {
      public class CollectingBonuses {
            public double expectedBuy(string n, string k) {
                  return expectedBuy(long.Parse(n), long.Parse(k));
            }

            /* Because the codes are evenly distributed we can conclude that experiments are independent from each other
             * where the experiment's goal is to take a new code. Therefore the function E[k] - the expected number of bottles
             * to collect k prizes can be described using the following recurrence relation:
             * E(0) = 0,
             * E(k + 1) = E(k) + 1 * A + 2 * q * A + 3 * q^2 * A + 4 * q^3 * A + ... =
             *          = E(k) + n / (n - k), E(k) = n * (H(n) - H(n - k)).
             * where q = k / n, A = (n - k) / n. */
            private double expectedBuy(long n, long k) {
                  if (k <= 1000 * 1000) {
                        double result = 0.0;
                        for (long i = n; i > n - k; --i) {
                              result += (double)n / i;
                        }
                        return result;
                  }
                  if (n - k > 1000) {
                        double a = n;
                        double b = n - k;
                        double x = k / b;
                        double log = x > 0.0001 ? Math.Log(1 + x) : log1p(x);
                        return n * (log + 1.0 / 2 / a - 1.0 / 12 / a / a + 1.0 / 120 / a / a / a / a - 1.0 / 252 / a / a / a / a / a / a
                                        - 1.0 / 2 / b + 1.0 / 12 / b / b - 1.0 / 120 / b / b / b / b + 1.0 / 252 / b / b / b / b / b / b);
                  }
                  return n * (h2(n) - h1(n - k));
            }

            private double h2(long n) {
                  return Math.Log(n) + 0.577215664901532860606512090082402431042 + 1.0 / 2 / n - 1.0 / 12 / n / n + 1.0 / 120 / n / n / n / n - 1.0 / 252 / n / n / n / n / n / n;
            }

            private double h1(long n) {
                  double result = 0.0;
                  for (int i = 1; i <= n; ++i) {
                        result += 1.0 / i;
                  }
                  return result;
            }

            private double log1p(double x) {
                  double result = 0, xx = x;
                  for (int i = 1; i <= 100; ++i) {
                        result += xx / i;
                        xx *= -x;
                  }
                  return result;
            }

            public static void Main() {
                  Console.WriteLine(new CollectingBonuses().expectedBuy("1", "1"));
                  Console.WriteLine(new CollectingBonuses().expectedBuy("2", "1"));
                  Console.WriteLine(new CollectingBonuses().expectedBuy("2", "2"));
                  Console.WriteLine(new CollectingBonuses().expectedBuy("4", "3"));
                  Console.WriteLine("{0}", new CollectingBonuses().expectedBuy("999999999999999999", "999999999999999999")); //4.202374733879435E19
                  Console.WriteLine("{0}", new CollectingBonuses().expectedBuy("17247006936141401", "6006801")); //6006801.001046026
                  Console.WriteLine("{0}", new CollectingBonuses().expectedBuy("999999999999999", "1")); // 1.0
                  Console.WriteLine("{0}", new CollectingBonuses().expectedBuy("300000000", "299999999")); // 5.728952609756602E9
                  Console.WriteLine("{0}", new CollectingBonuses().expectedBuy("1337328974", "666199233")); // 9.220443660135001E8
                  Console.WriteLine("{0}", new CollectingBonuses().expectedBuy("1000000000000000000", "999999999999999997")); // 4.0190414005461025E19

                  Console.WriteLine("\nPress any key to continue...");
                  Console.ReadKey();
            }
      }
}