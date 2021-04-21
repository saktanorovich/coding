using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class BombSweeper {
            public double winPercentage(string[] board) {
                  int[] count = new int[3];
                  for (int i = 0; i < board.Length; ++i) {
                        for (int j = 0; j < board[i].Length; ++j) {
                              ++count[bomb(board, i, j)];
                        }
                  }
                  return 100.0 * count[0] / (count[0] + count[1]);
            }

            private int bomb(string[] board, int i, int j) {
                  if (board[i][j].Equals('B')) {
                        return 1;
                  }
                  else {
                        for (int di = -1; di <= +1; ++di) {
                              for (int dj = -1; dj <= +1; ++dj) {
                                    if (0 <= i + di && i + di < board.Length &&
                                          0 <= j + dj && j + dj < board[i].Length) {
                                          if (board[i + di][j + dj].Equals('B')) {
                                                return 2;
                                          }
                                    }
                              }
                        }
                        return 0;
                  }
            }
      }
}