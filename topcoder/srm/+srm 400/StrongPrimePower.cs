using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TopCoder.Algorithm {
      public class StrongPrimePower {
            public int[] baseAndExponent(string n) {
                  return baseAndExponent(long.Parse(n));
            }

            private int[] baseAndExponent(long n) {
                  for (int q = 64; q > 1; --q) {
                        long x = (long)Math.Pow(n, 1.0 / q);
                        for (long p = x - 1; p <= x + 1; ++p) {
                              if (p > 1 && isPrime(p)) {
                                    long m = n, k = 0;
                                    while (m % p == 0) {
                                          m = m / p;
                                          k = k + 1;
                                    }
                                    if (m == 1 && k == q) {
                                          return new int[2] { (int)p, (int)q };
                                    }
                              }
                        }
                  }
                  return new int[] { };
            }

            bool isPrime(long n) {
                  if (n > 2) {
                        for (long i = 2; i * i < n; ++i) {
                              if (n % i == 0) {
                                    return false;
                              }
                        }
                        return true;
                  }
                  return (n == 2);
            }

            private static string ToString(int[] a) {
                  string result = string.Empty;
                  for (int i = 0; i < a.Length; ++i) {
                        result += a[i].ToString();
                        if (i + 1 < a.Length) {
                              result += " ";
                        }
                  }
                  return result;
            }

            public static void Main() {
                  Console.WriteLine(ToString(new StrongPrimePower().baseAndExponent("27")));
                  Console.WriteLine(ToString(new StrongPrimePower().baseAndExponent("10")));
                  Console.WriteLine(ToString(new StrongPrimePower().baseAndExponent("1296")));
                  Console.WriteLine(ToString(new StrongPrimePower().baseAndExponent("576460752303423488")));
                  Console.WriteLine(ToString(new StrongPrimePower().baseAndExponent("999999874000003969")));
                  Console.WriteLine(ToString(new StrongPrimePower().baseAndExponent("639558602475808609")));

                  Console.WriteLine("\nPress any key to continue...");
                  Console.ReadKey();
            }
      }
}