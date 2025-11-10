using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TopCoder.Algorithm {
      public class WindowWasher {
            public int fastest(int width, int height, int[] washTimes) {
                  this.washTimes = washTimes;
                  Array.Sort(this.washTimes, delegate(int x, int y) {
                        return -x.CompareTo(y);
                  });
                  this.best = new int[width + 1, washTimes.Length];
                  for (int columns = 0; columns <= width; ++columns) {
                        for (int worker = 0; worker < washTimes.Length; ++worker) {
                              best[columns, worker] = -1;
                        }
                  }
                  for (int columns = 0; columns <= width; ++columns) {
                        best[columns, 0] = columns * washTimes[0];
                  }
                  return fastest(width, washTimes.Length - 1) * height;
            }

            private int[] washTimes;
            private int[,] best;

            private int fastest(int columns, int worker) {
                  if (best[columns, worker] == -1) {
                        best[columns, worker] = int.MaxValue;
                        for (int take = 0; take <= columns; ++take) {
                              best[columns, worker] = Math.Min(best[columns, worker],
                                    Math.Max(fastest(columns - take, worker - 1), take * washTimes[worker]));
                        }
                  }
                  return best[columns, worker];
            }
      }
}