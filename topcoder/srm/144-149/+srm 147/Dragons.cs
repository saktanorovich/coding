using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class Dragons {
            public string snaug(int[] initialFood, int rounds) {
                  return snaug(Array.ConvertAll(initialFood, delegate(int x) {
                        return new Fraction(x);
                  }), rounds);
            }

            private string snaug(Fraction[] food, int rounds) {
                  for (int round = 0; round < rounds; ++round) {
                        Fraction[] next = new Fraction[6];
                        for (int i = 0; i < 6; ++i) {
                              next[i] = new Fraction(0);
                              for (int j = 0; j < 4; ++j) {
                                    next[i] += food[neighbors[i, j]] / 4;
                              }
                        }
                        food = next;
                  }
                  return food[2].ToString();
            }

            private static readonly int[,] neighbors = new int[6, 4] {
                  { 2, 3, 4, 5 },
                  { 2, 3, 4, 5 },
                  { 0, 1, 4, 5 },
                  { 0, 1, 4, 5 },
                  { 0, 1, 2, 3 },
                  { 0, 1, 2, 3 },
            };

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