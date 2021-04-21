using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithms {
      public class Decimal {
            public int[] find(int lower, int upper, int lowerLength, int upperLength) {
                  List<int> result = new List<int>();
                  for (int x = lower; x <= upper; ++x) {
                        int p = periodLength(x);
                        if (lowerLength <= p && p <= upperLength) {
                              result.Add(x);
                        }
                  }
                  return result.ToArray();
            }

            private int periodLength(int x) {
                  x = reduceBy(x, 2);
                  x = reduceBy(x, 5);
                  if (x > 1) {
                        int result = 1;
                        for (int i = 3; i * i < x; i += 2) {
                              if (x % i == 0) {
                                    int p = 1;
                                    while (x % i == 0) {
                                          p *= i;
                                          x /= i;
                                    }
                                    result = lcm(result, primePeriodLength(p));
                              }
                        }
                        if (x > 1) {
                              result = lcm(result, primePeriodLength(x));
                        }
                        return result;
                  }
                  return 1;
            }

            private int primePeriodLength(int p) {
                  int prev = pow(10, p + 1, p);
                  int curr = pow(10, p + 1, p);
                  int result = 0;
                  do {
                        curr = (curr * 10) % p;
                        result = result + 1;
                  } while (curr != prev);
                  return result;
            }

            private int pow(int x, int k, int p) {
                  if (k == 0) {
                        return 1;
                  }
                  else if ((k & 1) == 0) {
                        return pow((x * x) % p, k >> 1, p);
                  }
                  else {
                        return (x * pow(x, k - 1, p)) % p;
                  }
            }

            private int lcm(int a, int b) {
                  return a * (b / gcd(a, b));
            }

            private int gcd(int a, int b) {
                  while (a > 0 && b > 0) {
                        if (a > b) {
                              a %= b;
                        }
                        else {
                              b %= a;
                        }
                  }
                  return a + b;
            }

            private int reduceBy(int x, int by) {
                  if (x > 0) {
                        while (x % by == 0) {
                              x /= by;
                        }
                  }
                  return x;
            }

            private static string ToString<T>(T[] a) {
                  string result = string.Empty;
                  for (int i = 0; i < a.Length; ++i) {
                        result += a[i].ToString();
                        if (i + 1 < a.Length) {
                              result += ' ';
                        }
                  }
                  return result + Environment.NewLine;
            }

            public static void Main(string[] args) {
                  //Console.WriteLine(ToString(new Decimal().find(1000000 - 100, 1000000, 1, 1000000)));
                  Console.WriteLine(ToString(new Decimal().find(204, 300, 10, 20)));

                  Console.WriteLine("Press any key to continue...");
                  Console.ReadLine();
            }
      }
}