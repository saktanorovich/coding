using System;

namespace topcoder.algorithm {
      public class BigBurger {
            public int maxWait(int[] arrival, int[] service) {
                  int[] leave = new int[arrival.Length]; leave[0] = arrival[0] + service[0];
                  for (int i = 1; i < arrival.Length; ++i) {
                        leave[i] = Math.Max(leave[i - 1], arrival[i]) + service[i];
                  }
                  int result = 0;
                  for (int i = 1; i < arrival.Length; ++i) {
                        result = Math.Max(result, leave[i - 1] - arrival[i]);
                  }
                  return result;

            }
      }
}