using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class PathFinding {
            public int minTurns(string[] board) {
                  int ax = 0, ay = 0;
                  int bx = 0, by = 0;
                  for (int x = 0; x < board.Length; ++x) {
                        for (int y = 0; y < board[x].Length; ++y) {
                              if (board[x][y].Equals('A')) {
                                    ax = x;
                                    ay = y;
                              }
                              if (board[x][y].Equals('B')) {
                                    bx = x;
                                    by = y;
                              }
                        }
                  }
                  return minTurns(Array.ConvertAll(board, delegate(string s) {
                        return Array.ConvertAll(s.ToCharArray(), delegate(char c) {
                              return c.Equals('X') ? 1 : 0;
                        });
                  }), board.Length, board[0].Length, ax, ay, bx, by);
            }

            private int minTurns(int[][] board, int n, int m, int ax, int ay, int bx, int by) {
                  int[] turns = new int[encrypt(n, m, n, m)];
                  for (int state = 0; state < encrypt(n, m, n, m); ++state) {
                        turns[state] = int.MaxValue / 2;
                  }
                  Queue<int> queue = new Queue<int>();
                  for (turns[encrypt(ax, ay, bx, by)] = 0, queue.Enqueue(encrypt(ax, ay, bx, by)); queue.Count > 0; ) {
                        int ax1, ay1;
                        int bx1, by1;
                        decrypt(queue.Dequeue(), out ax1, out ay1, out bx1, out by1);
                        for (int k1 = 0; k1 < 9; ++k1) {
                              for (int k2 = 0; k2 < 9; ++k2) {
                                    int ax2 = ax1 + dx[k1];
                                    int ay2 = ay1 + dy[k1];
                                    int bx2 = bx1 + dx[k2];
                                    int by2 = by1 + dy[k2];
                                    if (isvalid(board, n, m, ax1, ay1, bx1, by1, ax2, ay2, bx2, by2)) {
                                          int curr = encrypt(ax1, ay1, bx1, by1);
                                          int next = encrypt(ax2, ay2, bx2, by2);
                                          if (turns[next] > turns[curr] + 1) {
                                                turns[next] = turns[curr] + 1;
                                                queue.Enqueue(next);
                                          }
                                    }
                              }
                        }
                  }
                  if (turns[encrypt(bx, by, ax, ay)] < int.MaxValue / 2) {
                        return turns[encrypt(bx, by, ax, ay)];
                  }
                  return -1;
            }

            private bool isvalid(int[][] board, int n, int m, int ax1, int ay1, int bx1, int by1, int ax2, int ay2, int bx2, int by2) {
                  if (0 <= ax2 && ax2 < n && 0 <= ay2 && ay2 < m &&
                        0 <= bx2 && bx2 < n && 0 <= by2 && by2 < m) {
                              if (board[ax2][ay2] + board[bx2][by2] == 0) {
                                    if (ax2 != bx2 || ay2 != by2) {
                                          if (ax1 != bx2 || ay1 != by2 || bx1 != ax2 || by1 != ay2) {
                                                return true;
                                          }
                                    }
                              }
                  }
                  return false;
            }

            private int encrypt(int ax, int ay, int bx, int by) {
                  int state = 0;
                  state |= (ax << 15);
                  state |= (ay << 10);
                  state |= (bx << 05);
                  state |= (by << 0);
                  return state;
            }

            private void decrypt(int state, out int ax, out int ay, out int bx, out int by) {
                  ax = (state >> 15) & 0x1F;
                  ay = (state >> 10) & 0x1F;
                  bx = (state >> 05) & 0x1F;
                  by = (state >> 00) & 0x1F;
            }

            private static readonly int[] dx = new int[9] { -1, -1, -1,  0, 0,  0, +1, +1, +1 };
            private static readonly int[] dy = new int[9] { -1,  0, +1, -1, 0, +1, -1, 0,  +1 };
      }
}