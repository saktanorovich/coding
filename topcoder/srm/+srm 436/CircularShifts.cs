using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class CircularShifts {
            public int maxScore(int n, int z0, int a, int b, int m) {
                  long[] z = generate(z0, a, b, m, 2 * n);
                  long[] x = new long[n];
                  long[] y = new long[n];
                  for (int i = 0; i < n; ++i) {
                        x[i] = z[i + 0] % 100;
                        y[i] = z[i + n] % 100;
                  }
                  return (int)maxScore(x, y, n);
            }

            private long maxScore(long[] x, long[] y, int n) {
                  long[] a = new long[n];
                  long[] b = new long[n];
                  for (int i = 0; i < n; ++i) {
                        a[i] = x[i];
                        b[i] = y[n - 1 - i];
                  }
                  long[] c = multiply(a, b, n);
                  long result = 0;
                  for (int i = 0; i < n; ++i) {
                        result = Math.Max(result, c[i] + c[i + n]);
                  }
                  return result;
            }

            private long[] multiply(long[] a, long[] b, int n) {
                  Complex[] x = normalize(a);
                  Complex[] y = normalize(b);
                  FourierUtils.FFT(x, +1);
                  FourierUtils.FFT(y, +1);
                  for (int i = 0; i < x.Length; ++i) {
                        x[i] = x[i] * y[i];
                  }
                  FourierUtils.FFT(x, -1);
                  long[] result = new long[2 * n];
                  for (int i = 0; i < 2 * n; ++i) {
                        result[i] = (long)(x[i].Re + 0.5);
                  }
                  return result;
            }

            private static Complex[] normalize(long[] data) {
                  long size = data.Length;
                  if (!BitUtils.IsPowerOf2(data.Length)) {
                        size = 1 << BitUtils.BitsCount(data.Length);
                  }
                  Complex[] result = new Complex[2 * size];
                  for (int i = 0; i < data.Length; ++i) {
                        result[i] = new Complex(data[i], 0);
                  }
                  return result;
            }

            private long[] generate(int z0, int a, int b, int p, int m) {
                  long[] z = new long[m]; z[0] = z0 % p;
                  for (int i = 1; i < m; ++i) {
                        z[i] = (z[i - 1] * a + b) % p;
                  }
                  return z;
            }

            private static class FourierUtils {
                  public static void FFT(Complex[] a, int invert) {
                        int n = a.Length;
                        for (int i = 1, j = 0; i < n; ++i) {
                              int bit = n >> 1;
                              for (; j >= bit; bit >>= 1) {
                                    j -= bit;
                              }
                              j += bit;
                              if (i < j) {
                                    Complex temp = a[i];
                                    a[i] = a[j];
                                    a[j] = temp;
                              }
                        }
                        for (int len = 2; len <= n; len <<= 1) {
                              double ang = invert * 6.283185307179586476925286766559 / len;
                              Complex wlen = new Complex(Math.Cos(ang), Math.Sin(ang));
                              for (int i = 0; i < n; i += len) {
                                    Complex w = new Complex(1, 0);
                                    int len2 = len >> 1;
                                    for (int j = 0; j < len2; ++j) {
                                          Complex u = a[i + j];
                                          Complex v = a[i + j + len2] * w;
                                          a[i + j] = u + v;
                                          a[i + j + len2] = u - v;
                                          w = w * wlen;
                                    }
                              }
                        }
                        if (invert < 0) {
                              for (int i = 0; i < n; ++i) {
                                    a[i] = new Complex(a[i].Re / n, a[i].Im / n);
                              }
                        }
                  }
            }

            private static class BitUtils {
                  public static bool IsPowerOf2(int value) {
                        if (value != 0) {
                              if (value > 0) {
                                    return (value & (value - 1)) == 0;
                              }
                              return IsPowerOf2(-value);
                        }
                        return false;
                  }

                  public static int BitsCount(int set) {
                        var result = 0;
                        for (; set != 0; set = set >> 1) {
                              ++result;
                        }
                        return result;
                  }
            }

            private struct Complex {
                  public double Re;
                  public double Im;

                  public Complex(double re, double im) {
                        Re = re;
                        Im = im;
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
            }

            public static void Main(string[] args) {
                  Console.WriteLine(new CircularShifts().maxScore(5, 1, 1, 0, 13)); // 5
                  Console.WriteLine(new CircularShifts().maxScore(4, 1, 1, 1, 20)); // 70
                  Console.WriteLine(new CircularShifts().maxScore(10, 23, 11, 51, 4322)); // 28886
                  Console.WriteLine(new CircularShifts().maxScore(1000, 3252, 3458736, 233421, 111111111)); // 2585408
                  Console.WriteLine(new CircularShifts().maxScore(141, 96478, 24834, 74860, 92112)); // 419992
                  Console.WriteLine(new CircularShifts().maxScore(60000, 123121, 289347322, 231211112, 989333333)); // 149230883
                  Console.WriteLine(new CircularShifts().maxScore(39558, 661662853, 96144446, 15411304, 228498317)); // 97413529

                  Console.WriteLine("Press any key...");
                  Console.ReadLine();
            }
      }
}