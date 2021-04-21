using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class BridgeCrossing {
            public int minTime(int[] times) {
                  Array.Sort(times);
                  int[] dp = (int[])times.Clone();
                  if (times.Length > 2) {
                        for (int i = 2; i < dp.Length; ++i) {
                              dp[i] = times[i] + Math.Min(dp[i - 1] + times[0], dp[i - 2] + times[0] + 2 * times[1]);
                        }
                  }
                  return dp[dp.Length - 1];
            }
      }
}