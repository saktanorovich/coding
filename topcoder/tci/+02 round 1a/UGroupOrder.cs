using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithms {
      public class UGroupOrder {
            public int[] findOrders(int n) {
                  List<int> result = new List<int>();
                  for (int i = 1; i < n; ++i) {
                        if (gcd(n, i) == 1) {
                              for (int e = 1, x = 1; true; ++e) {
                                    x = (x * i) % n;
                                    if (x == 1) {
                                          result.Add(e);
                                          break;
                                    }
                              }
                        }
                  }
                  return result.ToArray();
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
                  Console.WriteLine(ToString(new UGroupOrder().findOrders(9)));

                  Console.WriteLine("Press any key to continue...");
                  Console.ReadLine();
            }
      }
}