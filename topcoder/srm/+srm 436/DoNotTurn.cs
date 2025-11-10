using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class DoNotTurn {
            public int minimumTurns(int n, int x0, int a, int b, int y0, int c, int d, int p, int m) {
                  bool[,] field = new bool[n, n];
                  if (m > 0) {
                        long[] x = generate(x0, a, b, p, m);
                        long[] y = generate(y0, c, d, p, m);
                        for (int i = 0; i < m; ++i) {
                              field[x[i] % n, y[i] % n] = true;
                        }
                  }
                  field[0, 0] = field[n - 1, n - 1] = false;
                  return minimumTurns(field, n);
            }

            private const int oo = +1000000000;

            private int[] dx = new int[] {  0, -1,  0, +1 };
            private int[] dy = new int[] { +1,  0, -1,  0 };

            private int minimumTurns(bool[,] field, int n) {
                  int[,,] dist = new int[n, n, 4];
                  for (int i = 0; i < n; ++i) {
                        for (int j = 0; j < n; ++j) {
                              if (i + j != 0) {
                                    for (int k = 0; k < 4; ++k) {
                                          dist[i, j, k] = +oo;
                                    }
                              }
                        }
                  }
                  Queue<QueueEntry> queue = new Queue<QueueEntry>();
                  for (int k = 0; k < 4; ++k) {
                        queue.Enqueue(new QueueEntry(0, 0, k));
                  }
                  while (queue.Count > 0) {
                        QueueEntry curr = queue.Dequeue();
                        for (int k = 0; k < 4; ++k) {
                              int turnPerformed = 0;
                              QueueEntry next = getNext(curr, field, n, k, out turnPerformed);
                              if (next != null) {
                                    if (dist[next.XPos, next.YPos, next.Dir] > dist[curr.XPos, curr.YPos, curr.Dir] + turnPerformed) {
                                          dist[next.XPos, next.YPos, next.Dir] = dist[curr.XPos, curr.YPos, curr.Dir] + turnPerformed;
                                          queue.Enqueue(next);
                                    }
                              }
                        }
                  }
                  int result = +oo;
                  for (int k = 0; k < 4; ++k) {
                        result = Math.Min(result, dist[n - 1, n - 1, k]);
                  }
                  return result < +oo ? result : -1;
            }

            private QueueEntry getNext(QueueEntry curr, bool[,] field, int n, int dir, out int turnPerformed) {
                  turnPerformed = 0;
                  int xpos = curr.XPos + dx[dir];
                  int ypos = curr.YPos + dy[dir];
                  if (0 <= xpos && xpos < n && 0 <= ypos && ypos < n && !field[xpos, ypos]) {
                        turnPerformed = curr.Dir != dir ? 1 : 0;
                        return new QueueEntry(xpos, ypos, dir);
                  }
                  return null;
            }

            private long[] generate(int x0, int a, int b, int p, int m) {
                  long[] x = new long[m]; x[0] = x0 % p;
                  for (int i = 1; i < m; ++i) {
                        x[i] = (x[i - 1] * a + b) % p;
                  }
                  return x;
            }

            private class QueueEntry {
                  public int XPos;
                  public int YPos;
                  public int Dir;

                  public QueueEntry(int xpos, int ypos, int dir) {
                        XPos = xpos;
                        YPos = ypos;
                        Dir = dir;
                  }
            }

            public static void Main(string[] args) {
                  Console.WriteLine(new DoNotTurn().minimumTurns(2, 0, 0, 1, 0, 0, 1, 10, 2)); // 1
                  Console.WriteLine(new DoNotTurn().minimumTurns(3, 0, 1, 1, 1, 1, 0, 3, 3)); // -1
                  Console.WriteLine(new DoNotTurn().minimumTurns(3, 0, 1, 1, 1, 1, 1, 3, 3)); // 3
                  Console.WriteLine(new DoNotTurn().minimumTurns(10, 911111, 845499, 866249, 688029, 742197, 312197, 384409, 40)); // 12
                  Console.WriteLine(new DoNotTurn().minimumTurns(5, 23, 2, 3, 35, 5, 7, 9, 3)); // 2
                  Console.WriteLine(new DoNotTurn().minimumTurns(2, 0, 0, 0, 0, 0, 0, 1, 0)); // 1

                  Console.WriteLine("Press any key...");
                  Console.ReadLine();
            }
      }
}