using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class SplittingFoxes2 {

            /* We can note that a[k] = sum{p[i] * p[k - i], i=[0, n)}. So the resulting array a is a circular convolution (p * p)
             * and we can conclude that DFT(a) = DFT(p) * DFT(p). */

            public int[] getPattern(int[] amount) {
                  int n = amount.Length;
                  Complex[] sqrtFromDFTAmount = Array.ConvertAll(FourierUtils.DFT(amount, +1), delegate(Complex value) {
                        return value.Sqrt();
                  });
                  int[] result = null;
                  for (int set = 0; set < (1 << ((n >> 1) + 1)); ++set) {
                        Complex[] sqrt = (Complex[])sqrtFromDFTAmount.Clone();
                        for (int i = 0; i < n; ++i) {
                              if ((set & (1 << Math.Min(i, n - i))) > 0) {
                                    sqrt[i] = sqrt[i].Negate();
                              }
                        }
                        int[] pattern = Array.ConvertAll(FourierUtils.DFT(sqrt, -1), delegate(Complex value) {
                              return Math.Max(0, (int)(value.Re + 0.5));
                        });
                        if (isValid(pattern, amount)) {
                              if (result == null || CompareTo(pattern, result) < 0) {
                                    result = pattern;
                              }
                        }
                  }
                  if (result != null) {
                        return result;
                  }
                  return new int[] { -1 };
            }

            private int CompareTo(int[] a, int[] b) {
                  if (a.Length == b.Length) {
                        for (int i = 0; i < a.Length; ++i) {
                              if (a[i] != b[i]) {
                                    return a[i].CompareTo(b[i]);
                              }
                        }
                        return 0;
                  }
                  return a.Length.CompareTo(b.Length);
            }

            private bool isValid(int[] pattern, int[] amount) {
                  int n = pattern.Length;
                  for (int i = 1; i < n; ++i) {
                        if (pattern[i] != pattern[n - i]) {
                              return false;
                        }
                  }
                  for (int k = 0; k < n; ++k) {
                        int convolution = 0;
                        for (int i = 0; i < n; ++i) {
                              convolution += pattern[i] * pattern[(k - i + n) % n];
                        }
                        if (convolution != amount[k]) {
                              return false;
                        }
                  }
                  return true;
            }

            public static class FourierUtils {
                  public static Complex[] DFT(int[] a, int invert) {
                        return DFT(Array.ConvertAll(a, delegate(int value) {
                              return new Complex(value, 0);
                        }), invert);
                  }

                  public static Complex[] DFT(Complex[] a, int invert) {
                        int n = a.Length;
                        double ang = -invert * 2 * 3.1415926535897932384626433832795 / n;
                        Complex[] result = new Complex[n];
                        for (int k = 0; k < n; ++k) {
                              result[k] = new Complex(0, 0);
                              for (int j = 0; j < n; ++j) {
                                    result[k] += a[j] * new Complex(Math.Cos(ang * k * j), Math.Sin(ang * k * j));
                              }
                              if (invert == -1) {
                                    result[k] = new Complex(result[k].Re / n, result[k].Im / n);
                              }
                        }
                        return result;
                  }
            }

            public class Complex {
                  public double Re { get; private set; }
                  public double Im { get; private set; }

                  public Complex(double re, double im) {
                        Re = re;
                        Im = im;
                        if (Math.Abs(Re) < 1e-12) Re = 0.0;
                        if (Math.Abs(Im) < 1e-12) Im = 0.0;
                  }

                  public static Complex operator +(Complex a, Complex b) {
                        return new Complex(a.Re + b.Re, a.Im + b.Im);
                  }

                  public static Complex operator -(Complex a, Complex b) {
                        return new Complex(a.Re - b.Re, a.Im - b.Im);
                  }

                  public static Complex operator *(Complex a, Complex b) {
                        return new Complex(a.Re * b.Re - a.Im * b.Im, a.Im * b.Re + a.Re * b.Im);
                  }

                  public Complex Sqrt() {
                        double mag = Math.Sqrt(Math.Sqrt(Re * Re + Im * Im));
                        double ang = Math.Atan2(Im, Re) / 2.0;
                        return new Complex(mag * Math.Cos(ang), mag * Math.Sin(ang)); ;
                  }

                  public Complex Negate() {
                        return new Complex(-Re, -Im);
                  }

                  public override string ToString() {
                        return string.Format("({0}, {1})", Re, Im);
                  }
            }

            internal static string ToString(int[] a) {
                  string result = string.Empty;
                  for (int i = 0; i < a.Length; ++i) {
                        result += a[i].ToString();
                        if (i + 1 < a.Length) {
                              result += " ";
                        }
                  }
                  return "{" + result + "}";
            }

            internal static void Main(string[] args) {
                  new SplittingFoxes2().getPattern(new int[] { 0, 59, 90, 76, 22, 76, 90, 59 });
                  Console.WriteLine(ToString(new SplittingFoxes2().getPattern(new int[] { 2, 0, 1, 1, 0 })));
                  Console.WriteLine(ToString(new SplittingFoxes2().getPattern(new int[] { 1, 0, 0, 0, 0, 0 })));
                  Console.WriteLine(ToString(new SplittingFoxes2().getPattern(new int[] { 2, 0, 0, 0, 0, 0 })));
                  Console.WriteLine(ToString(new SplittingFoxes2().getPattern(new int[] { 10, 0, 8, 0, 10, 0, 8, 0 })));
                  Console.WriteLine(ToString(new SplittingFoxes2().getPattern(new int[] {35198,27644,22185,26896,34136,26896,22185,27644})));
                  Console.WriteLine(ToString(new SplittingFoxes2().getPattern(new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 })));

                  Console.WriteLine("\nPress any key to continue...");
                  Console.ReadKey();
            }
      }
}
