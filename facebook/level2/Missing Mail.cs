using System;
using System.Linq;

class Solution {
  public double getMaxExpectedProfit(int N, int[] V, int C, double S) {
    var best = new double[N + 1];
    for (var i = 1; i <= N; ++i) {
      var suma = 0.0;
      var prob = 1.0;
      for (var j = i - 1;  j >= 0; --j) {
        suma += V[j] * prob;
        best[i] = Math.Max(best[i], best[j] + suma);
        prob *= 1 - S;
      }
      best[i] -= C;
    }
    return best.Max();
  }
}
