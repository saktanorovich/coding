using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class Gauss {
            public string[] whichSums(string target) {
                  return Array.ConvertAll(whichSums(long.Parse(target)),
                        delegate(Pair pair) {
                              return pair.ToString();
                  });
            }

            private Pair[] whichSums(long target) {
                  List<Pair> result = new List<Pair>();
                  long bound = 2 * target;
                  for (long k = 2; k * k <= bound; ++k) {
                        if (bound % k == 0) {
                              long x = (bound / k) - k + 1;
                              if (x % 2 == 0) {
                                    x /= 2;
                                    result.Add(new Pair(x, x + k - 1));
                              }
                        }
                  }
                  result.Sort();
                  return result.ToArray();
            }

            private struct Pair : IComparable<Pair> {
                  public long lo;
                  public long hi;

                  public Pair(long lo, long hi) {
                        this.lo = lo;
                        this.hi = hi;
                  }

                  public override string ToString() {
                        return string.Format("[{0}, {1}]", lo, hi);
                  }

                  public int CompareTo(Pair other) {
                        return this.lo.CompareTo(other.lo);
                  }
            }
      }
}