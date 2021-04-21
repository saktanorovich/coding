using System;
using System.Collections.Generic;

class Solution {
  public long getArtisticPhotographCount(int N, string C, int X, int Y) {
    var apos = new List<int>();
    var pcnt = new int[N + 1];
    var bcnt = new int[N + 1];
    for (var i = 1; i <= N; ++i) {
      if (C[i - 1] == 'A') {
          apos.Add(i);
      }
      pcnt[i] = pcnt[i - 1] + (C[i - 1] == 'P' ? 1 : 0);
      bcnt[i] = bcnt[i - 1] + (C[i - 1] == 'B' ? 1 : 0);
    }
    var answ = 0L;
    foreach (var a in apos) {
      var pab = count(a - Y, a - X, pcnt) * count(a + X, a + Y, bcnt);
      var bap = count(a - Y, a - X, bcnt) * count(a + X, a + Y, pcnt);
      answ += pab;
      answ += bap;
    }
    return answ;
  }

  private long count(int lo, int hi, int[] sum) {
    var L = 1;
    var H = sum.Length - 1;
    lo = Math.Max(lo, L);
    hi = Math.Min(hi, H);
    if (hi >= lo) {
      return sum[hi] - sum[lo - 1];
    }
    return 0;
  }
}
