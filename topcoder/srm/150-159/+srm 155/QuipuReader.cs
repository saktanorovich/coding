using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class QuipuReader {
            public int[] readKnots(string[] knots) {
                  for (int i = 0; i < knots.Length; ++i) {
                        knots[i] = string.Format("-{0}-", knots[i]);
                  }
                  return readKnots(knots, knots.Length, knots[0].Length);
            }

            private int[] readKnots(string[] knots, int n, int m) {
                  List<int>[] numbers = new List<int>[n];
                  for (int i = 0; i < n; ++i) {
                        numbers[i] = new List<int>();
                  }
                  bool[][] marked = Array.ConvertAll(knots, delegate(string knot) {
                        return Array.ConvertAll(knot.ToCharArray(), delegate(char c) {
                              return c.Equals('-');
                        });
                  });
                  List<Range> ranges = new List<Range>();
                  while (true) {
                        /* find the largest group of consecutive X in some knot... */
                        Range largest = null;
                        for (int i = 0; i < n; ++i) {
                              for (int j = 0; j < m; ++j) {
                                    if (!marked[i][j]) {
                                          if (knots[i][j].Equals('X') && knots[i][j - 1].Equals('-')) {
                                                Range current = new Range(j, j);
                                                while (knots[i][current.hi + 1].Equals('X')) {
                                                      current.hi = current.hi + 1;
                                                }
                                                if (largest == null || largest.length() < current.length()) {
                                                      largest = current;
                                                }
                                          }
                                    }
                              }
                        }
                        if (largest != null) {
                              ranges.Add(largest);
                              for (int i = 0; i < n; ++i) {
                                    for (int j = largest.lo; j <= largest.hi; ++j) {
                                          marked[i][j] = true;
                                    }
                              }
                        }
                        else break;
                  }
                  ranges.Sort();
                  for (int r = 0; r < ranges.Count; ++r) {
                        List<int> digits = new List<int>();
                        int totalSum = 0;
                        for (int i = 0; i < n; ++i) {
                              int digit = knots[i].Substring(ranges[r].lo, ranges[r].length()).Replace("-", "").Length;
                              digits.Add(digit);
                              totalSum += digit;
                        }
                        if (totalSum > 0) {
                              for (int i = 0; i < n; ++i) {
                                    numbers[i].Add(digits[i]);
                              }
                        }
                  }
                  return Array.ConvertAll(numbers, delegate(List<int> digits) {
                        int result = 0;
                        foreach (int digit in digits) {
                              result = result * 10 + digit;
                        }
                        return result;
                  });
            }

            private class Range : IComparable<Range> {
                  public int lo;
                  public int hi;

                  public Range(int lo, int hi) {
                        this.lo = lo;
                        this.hi = hi;
                  }

                  public int length() {
                        return (hi - lo + 1);
                  }

                  public int CompareTo(Range other) {
                        return this.lo.CompareTo(other.lo);
                  }
            }
      }
}