using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class Jumper {
            public int minTime(string[] patterns, int[] speeds, string rows) {
                  List<string> board = new List<string>();
                  board.Add("".PadRight(20, '#'));
                  board.AddRange(Array.ConvertAll(rows.ToCharArray(),
                        delegate(char c) {
                              string result = string.Empty;
                              for (int i = 0; i < 4; ++i) {
                                    result += patterns[c - '0'];
                              }
                              return result;
                  }));
                  List<int> rspeeds = new List<int>();
                  rspeeds.Add(0);
                  rspeeds.AddRange(Array.ConvertAll(rows.ToCharArray(),
                        delegate(char c) {
                              return speeds[c - '0'];
                  }));
                  return minTime(board, rspeeds, board.Count, 20);
            }


            private int minTime(List<string> board, List<int> speeds, int n, int m) {
                  /* because each row will reach its initial state during 5 seconds the
                   * total number of states is 21x20x5=2'100.. */
                  bool[,,] visited = new bool[n, m, 5];
                  for (int xpos = 0; xpos < n; ++xpos) {
                        for (int ypos = 0; ypos < m; ++ypos) {
                              for (int face = 0; face < 5; ++face) {
                                    visited[xpos, ypos, face] = false;
                              }
                        }
                  }

                  visited[0, 0, 0] = true;
                  Queue<State> queue = new Queue<State>();
                  for (queue.Enqueue(new State(0, 0, 0)); queue.Count > 0; ) {
                        State curr = queue.Dequeue();
                        foreach (State next in curr.next()) {
                              int xpos = next.xpos;
                              int ypos = next.ypos;
                              int time = next.time;
                              if (0 <= xpos && xpos < n) {
                                    if (0 <= ypos && ypos < m) {
                                          int ymov = (ypos - speeds[xpos] * curr.time) % m;
                                          if (ymov < 0) {
                                                ymov += m;
                                          }
                                          if (board[xpos][ymov] == '#') {
                                                ypos += speeds[xpos];
                                                if (0 <= ypos && ypos < m) {
                                                      if (xpos == n - 1) {
                                                            return time + 1;
                                                      }
                                                      int face = time % 5;
                                                      if (!visited[xpos, ypos, face]) {
                                                            visited[xpos, ypos, face] = true;
                                                            queue.Enqueue(new State(xpos, ypos, time));
                                                      }
                                                }
                                          }
                                    }
                              }
                        }
                  }
                  return -1;
            }

            private class State {
                  public int xpos;
                  public int ypos;
                  public int time;

                  public State(int xpos, int ypos, int time) {
                        this.xpos = xpos;
                        this.ypos = ypos;
                        this.time = time;
                  }

                  public IEnumerable<State> next() {
                        List<State> result = new List<State>();
                        for (int x = -1; x <= +1; ++x) {
                              for (int y = -1; y <= +1; ++y) {
                                    if (Math.Abs(x) + Math.Abs(y) < 2) {
                                          result.Add(new State(xpos + x, ypos + y, time + 1));
                                    }
                              }
                        }
                        return result;
                  }
            }
      }
}