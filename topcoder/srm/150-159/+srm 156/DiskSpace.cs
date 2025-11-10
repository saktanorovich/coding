using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class DiskSpace {
            public int minDrives(int[] used, int[] total) {
                  Array.Sort(total, used);
                  int result = 0, memory = accumulate(used);
                  for (int i = total.Length - 1; i >= 0; --i) {
                        if (memory > 0) {
                              memory = memory - total[i];
                              result = result + 1;
                        }
                        else break;
                  }
                  return result;
                  /**
                  int[] space = Array.ConvertAll(new int[accumulate(total) + 1],
                        delegate(int x) {
                              return int.MaxValue / 2;
                  }); space[0] = 0;
                  foreach (int disk in total) {
                        for (int memory = space.Length - disk; memory >= 0; --memory) {
                              if (memory + disk < space.Length) {
                                    if (space[memory + disk] > space[memory] + 1) {
                                          space[memory + disk] = space[memory] + 1;
                                    }
                              }
                        }
                  }
                  int result = total.Length;
                  for (int memory = accumulate(used); memory < space.Length; ++memory) {
                        result = Math.Min(result, space[memory]);
                  }
                  return result;
                  /**/
            }

            private int accumulate(int[] a) {
                  int result = 0;
                  foreach (int item in a) {
                        result += item;
                  }
                  return result;
            }
      }
}