using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class Gems {
            public int numMoves(string[] board) {
                  return numMoves(Array.ConvertAll(board,
                        delegate(string s) {
                              return s.ToCharArray();
                  }), board.Length, board[0].Length);
            }

            private int numMoves(char[][] board, int n, int m) {
                  int result = 0;
                  for (int i = 0; i < n; ++i) {
                        for (int j = 0; j < m; ++j) {
                              if (i > 0) {
                                    swap(board, i, j, i - 1, j);
                                    result += disappear(board, n, m, i, j);
                                    swap(board, i, j, i - 1, j);
                              }
                              if (j > 0) {
                                    swap(board, i, j, i, j - 1);
                                    result += disappear(board, n, m, i, j);
                                    swap(board, i, j, i, j - 1);
                              }
                        }
                  }
                  return result;
            }

            private static readonly int[] di = new int[5] { 0, -1,  0, +1,  0 };
            private static readonly int[] dj = new int[5] { 0,  0, +1,  0, -1 };

            private int disappear(char[][] board, int n, int m, int i, int j) {
                  for (int k = 0; k < 5; ++k) {
                        int x = i + di[k];
                        int y = j + dj[k];
                        if (0 <= x && x < n &&
                              0 <= y && y < m) {
                                    int x0 = disappear(board, n, m, x, y, -1, 0);
                                    int x1 = disappear(board, n, m, x, y, +1, 0);
                                    if (x0 + x1 >= 4) {
                                          return 1;
                                    }
                                    int y0 = disappear(board, n, m, x, y, 0, -1);
                                    int y1 = disappear(board, n, m, x, y, 0, +1);
                                    if (y0 + y1 >= 4) {
                                          return 1;
                                    }
                        }
                  }
                  return 0;
            }

            private int disappear(char[][] board, int n, int m, int x, int y, int dx, int dy) {
                  int gem = board[x][y], match = 0;
                  while (true) {
                        if(0 <= x && x < n &&
                              0 <= y && y < m) {
                                    if (board[x][y] == gem && match < 3) {
                                          x += dx;
                                          y += dy;
                                          ++match;
                                          continue;
                                    }
                        }
                        break;
                  }
                  return match;
            }

            private void swap(char[][] board, int i1, int j1, int i2, int j2) {
                  char temp = board[i1][j1];
                  board[i1][j1] = board[i2][j2];
                  board[i2][j2] = temp;
            }
      }
}