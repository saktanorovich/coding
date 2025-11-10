using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class HillHike {
            public long numPaths(int distance, int maxHeight, int[] landmarks) {
                  /* memo[d, h, l, {0, 1}] -- the num of ways to reach position (d, h) visiting l landmarks
                   * on the distances [0, d) assuming that the maxHeight was reach at the last parameter... */
                  long[,,,] memo = new long[distance + 1, maxHeight + 1, landmarks.Length + 1, 2];
                  memo[1, 1, 0, 0] = 1;
                  for (int dist = 1; dist < distance; ++dist) {
                        for (int height = 1; height <= maxHeight; ++height) {
                              for (int dh = -1; dh <= +1; ++dh) {
                                    int nextHeight = height + dh;
                                    if (0 <= nextHeight && nextHeight <= maxHeight) {
                                          for (int mark = 0; mark <= landmarks.Length; ++mark) {
                                                int nextMark = mark;
                                                if (nextMark + 1 <= landmarks.Length) {
                                                      nextMark += reach(height, landmarks[mark]);
                                                }
                                                for (int seen = 0; seen < 2; ++seen) {
                                                      memo[dist + 1, nextHeight, nextMark, Math.Max(reach(height, maxHeight), seen)] += memo[dist, height, mark, seen];
                                                }
                                          }
                                    }
                              }
                        }
                  }
                  return memo[distance, 0, landmarks.Length, 1];
            }

            private int reach(int height, int target) {
                  if (height == target) {
                        return 1;
                  }
                  return 0;
            }
      }
}