using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class RoboCourier {
            public int timeToDeliver(string[] path) {
                  bool[,] graph = new bool[maxNumOfNodes, maxNumOfNodes];
                  int x = 0, y = 0;
                  for (int i = 0, d = 0; i < path.Length; ++i) {
                        for (int j = 0; j < path[i].Length; ++j) {
                              switch (path[i][j]) {
                                    case 'F':
                                          int curr = getNode(x, y);
                                          x += dx[d];
                                          y += dy[d];
                                          int next = getNode(x, y);
                                          graph[curr, next] = true;
                                          graph[next, curr] = true;
                                          break;
                                    case 'L': d = (d - 1 + 6) % 6; break;
                                    case 'R': d = (d + 1 + 6) % 6; break;
                              }
                        }
                  }
                  int[,] time = new int[maxNumOfNodes, 6];
                  for (int node = 0; node < maxNumOfNodes; ++node) {
                        for (int d = 0; d < 6; ++d) {
                              time[node, d] = int.MaxValue / 2;
                        }
                  }
                  time[0, 0] = 0;
                  Queue<QueueEntry> q = new Queue<QueueEntry>();
                  for (q.Enqueue(new QueueEntry(0, 0, 0)); q.Count > 0; q.Dequeue()) {
                        int currx = q.Peek().x;
                        int curry = q.Peek().y;
                        int currd = q.Peek().d;
                        int currn = getNode(currx, curry);
                        for (int d = 0; d < 6; ++d) {
                              int nextd = (currd + d) % 6;
                              int klength = pathLength(graph, currx, curry, nextd);
                              for (int length = 1; length <= klength; ++length) {
                                    int nextx = currx + dx[nextd] * length;
                                    int nexty = curry + dy[nextd] * length;
                                    int nextn = getNode(nextx, nexty);
                                    int dtime = getTime(length) + rotate(currd, nextd);
                                    if (time[nextn, nextd] > time[currn, currd] + dtime) {
                                          time[nextn, nextd] = time[currn, currd] + dtime;
                                          q.Enqueue(new QueueEntry(nextx, nexty, nextd));
                                    }
                              }
                        }
                  }
                  int result = int.MaxValue;
                  for (int d = 0; d < 6; ++d) {
                        result = Math.Min(result, time[getNode(x, y), d]);
                  }
                  return result;
            }

            private int pathLength(bool[,] graph, int x, int y, int d) {
                  for (int k = 1; true; ++k) {
                        if (graph[getNode(x, y), getNode(x + dx[d], y + dy[d])]) {
                              x += dx[d];
                              y += dy[d];
                        }
                        else return k - 1;
                  }
            }

            private int getTime(int length) {
                  if (length > 2) {
                        return 8 + (length - 2) * 2;
                  }
                  return Math.Min(length, 2) * 4;
            }

            private int rotate(int currd, int nextd) {
                  int a = (currd - nextd + 6) % 6;
                  int b = (nextd - currd + 6) % 6;
                  return 3 * Math.Min(a, b);
            }

            private int getNode(long x, long y) {
                  int node;
                  if (!nodeByCoord.TryGetValue((x << 32) + y, out node)) {
                        node = numOfNodes;
                        nodeByCoord[(x << 32) + y] = node;
                        numOfNodes = numOfNodes + 1;
                  }
                  return node;
            }

            private struct QueueEntry {
                  public int x;
                  public int y;
                  public int d;

                  public QueueEntry(int x, int y, int d) {
                        this.x = x;
                        this.y = y;
                        this.d = d;
                  }
            }

            private readonly int maxNumOfNodes = 2000;
            private readonly int[] dx = new int[6] { +0, +1, +1, +0, -1, -1 };
            private readonly int[] dy = new int[6] { +1, +1, +0, -1, -1, +0 };
            private readonly SortedDictionary<long, int> nodeByCoord = new SortedDictionary<long, int>();
            private int numOfNodes = 0;
      }
}