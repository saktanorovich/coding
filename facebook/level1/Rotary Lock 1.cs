using System;

class Solution {
  public long getMinCodeEntryTime(int N, int M, int[] C) {
    var res = 0L;
    var num = 1;
    for (var i = 0; i < M; ++i) {
      var a = num - C[i] + N;
      var b = C[i] - num + N;
      a %= N;
      b %= N;
      res = res + Math.Min(a, b);
      num = C[i];
    }
    return res;
  }
}
