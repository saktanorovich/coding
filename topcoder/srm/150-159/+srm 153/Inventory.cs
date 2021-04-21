using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class Inventory {
            public int monthlyOrder(int[] sales, int[] daysAvailable) {
                  Fraction result = 0, numOfDays = 0;
                  for (int i = 0; i < sales.Length; ++i) {
                        if (daysAvailable[i] > 0) {
                              result += new Fraction(sales[i] * 30, daysAvailable[i]);
                              numOfDays += 1;
                        }
                  }
                  return (int)(result / numOfDays).ToLong();
            }

            private class Fraction {
                  public long Num;
                  public long Den;

                  public Fraction(long x)
                        : this(x, 1) {
                  }

                  public Fraction(long num, long den) {
                        long d = gcd(Math.Abs(num), Math.Abs(den));
                        Num = num / d;
                        Den = den / d;
                        if (den < 0) {
                              Num = -Num;
                              Den = -Den;
                        }
                  }

                  public override string ToString() {
                        if (Num != 0) {
                              if (Den > 1) {
                                    return string.Format("{0}/{1}", Num, Den);
                              }
                              return Num.ToString();
                        }
                        return "0";
                  }

                  public long ToLong() {
                        if (Den > 1) {
                              return (Num / Den) + 1;
                        }
                        return Num;
                  }

                  public static Fraction operator +(Fraction x, Fraction y) {
                        long d = gcd(x.Den, y.Den);
                        long num = x.Num * (y.Den / d);
                        long den = y.Num * (x.Den / d);
                        return new Fraction(num + den, x.Den * (y.Den / d));
                  }

                  public static Fraction operator /(Fraction x, Fraction y) {
                        return new Fraction(x.Num * y.Den, x.Den * y.Num);
                  }

                  public static implicit operator Fraction(long x) {
                        return new Fraction(x, 1);
                  }

                  private static long gcd(long a, long b) {
                        while (a != 0 && b != 0) {
                              if (a > b) {
                                    a %= b;
                              }
                              else {
                                    b %= a;
                              }
                        }
                        return a + b;
                  }
            }
      }
}