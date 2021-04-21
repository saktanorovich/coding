using System;

class Solution {
  public long getMaxAdditionalDinersCount(long N, long K, int M, long[] S) {
    Array.Sort(S);
    var L = S[0];
    var H = S[M - 1];
    var count = 0L;    
    count += calc(1, L, N, K);
    count += calc(H, N, N, K);
    for (var i = 1; i < M; ++i) {
      count += calc(S[i - 1], S[i], N, K);
    }
    return count;
  }

  private long calc(long lower, long upper, long N, long K) {
    if (lower >= upper) {
      return 0;
    }
    if (lower == 1) return (upper - lower) / (K + 1);
    if (upper == N) return (upper - lower) / (K + 1);
    return (upper - lower) / (K + 1) - 1;
  }
}
