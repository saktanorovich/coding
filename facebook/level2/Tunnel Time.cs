using System;
using System.Linq;

class Solution {
  public long getSecondsElapsed(long C, int N, long[] A, long[] B, long K) {
    Array.Sort(A);
    Array.Sort(B);
    var asum = A.Sum();
    var bsum = B.Sum();
    var laps = K / (bsum - asum);
    var need = K % (bsum - asum);
    var answ = C * laps;
    if (need == 0) {
        answ -= C;
        answ += B.Last();
    } else {
      for (var i = 0; i < N; ++i) {
        need -= B[i] - A[i];
        if (need <= 0) {
          answ += B[i];
          answ += need;
          break;
        }
      }
    }
    return answ;
  }
}