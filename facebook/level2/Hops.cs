using System;
using System.Linq;

class Solution {
  public long getSecondsRequired(long N, int F, long[] P) {
    return N - P.Min();
  }
}
