using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TopCoder.Algorithm {
      public class ThePriceIsRight {
            public int[] howManyReveals(int[] prices) {
                  int best = 1, count = 0;
                  int[] f = new int[prices.Length];
                  int[] g = new int[prices.Length];
                  for (int i = 0; i < prices.Length; ++i) {
                        f[i] = 1;
                        g[i] = 1;
                        for (int j = 0; j < i; ++j) {
                              if (prices[j] < prices[i]) {
                                    if (f[i] < f[j] + 1) {
                                          f[i] = f[j] + 1;
                                          g[i] = g[j];
                                          best = Math.Max(best, f[i]);
                                    }
                                    else if (f[i] == f[j] + 1) {
                                          g[i] += g[j];
                                    }
                              }
                        }
                  }
                  for (int i = 0; i < prices.Length; ++i) {
                        if (f[i] == best) {
                              count = count + g[i];
                        }
                  }
                  return new int[] { best, count };
            }
      }
}