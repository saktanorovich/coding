using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class PenLift {
            public int numTimes(string[] segments, int times) {
                  return numTimes(buildGraph(Array.ConvertAll(segments,
                        delegate(string segment) {
                              return Segment.Parse(segment);
                        })), times);
            }

            /* If a graph has an Eulerian path/circuit then the number of edge-disjoint paths is equal to 1. Assume
             * the graph has 2k (k > 1) vertices of odd degree. Then the number of edge-disjoint paths is equal
             * to k (can be proved by induction). Also we assume that the number of edges between every pair of
             * connected points is equal to <times> and each edge should be visited only one time (this is the same
             * as we are going to visit each original edge <times> times. */
            private int numTimes(List<int>[] graph, int times) {
                  int result = -1;
                  bool[] visited = new bool[graph.Length];
                  for (int curr = 0; curr < graph.Length; ++curr) {
                        if (!visited[curr]) {
                              List<int> component = dfs(graph, curr, visited);
                              if (component.Count > 1) {
                                    int numOfOddNodes = 0;
                                    foreach (int node in component) {
                                          if ((graph[node].Count * times) % 2 == 1) {
                                                numOfOddNodes = numOfOddNodes + 1;
                                          }
                                    }
                                    result = result + 1;
                                    if (numOfOddNodes > 2) {
                                          result += numOfOddNodes / 2 - 1;
                                    }
                              }
                        }
                  }
                  return result;
            }

            private List<int> dfs(List<int>[] graph, int curr, bool[] visited) {
                  visited[curr] = true;
                  List<int> result = new List<int>(new int[] { curr });
                  foreach (int next in graph[curr]) {
                        if (!visited[next]) {
                              result.AddRange(dfs(graph, next, visited));
                        }
                  }
                  return result;
            }

            private List<int>[] buildGraph(Segment[] segments) {
                  List<int> x = new List<int>();
                  List<int> y = new List<int>();
                  foreach (Segment segment in segments) {
                        if (!x.Contains(segment.x1)) x.Add(segment.x1);
                        if (!x.Contains(segment.x2)) x.Add(segment.x2);
                        if (!y.Contains(segment.y1)) y.Add(segment.y1);
                        if (!y.Contains(segment.y2)) y.Add(segment.y2);
                  }
                  x.Sort();
                  y.Sort();
                  List<int>[] graph = new List<int>[x.Count * y.Count];
                  for (int i = 0; i < graph.Length; ++i) {
                        graph[i] = new List<int>();
                  }
                  for (int i = 0; i < x.Count; ++i) {
                        for (int j = 0; j < y.Count; ++j) {
                              for (int di = -1; di <= +1; ++di) {
                                    for (int dj = -1; dj <= +1; ++dj) {
                                          if (di != 0 || dj != 0) {
                                                if (0 <= i + di && i + di < x.Count && 0 <= j + dj && j + dj < y.Count) {
                                                      foreach (Segment segment in segments) {
                                                            if (segment.Contains(x[i], y[j]) && segment.Contains(x[i + di], y[j + dj])) {
                                                                  graph[i * y.Count + j].Add((i + di) * y.Count + j + dj);
                                                                  break;
                                                            }
                                                      }
                                                }
                                          }
                                    }
                              }
                        }
                  }
                  return graph;
            }

            private class Segment {
                  public int x1, y1;
                  public int x2, y2;

                  public Segment(int x1, int y1, int x2, int y2) {
                        this.x1 = Math.Min(x1, x2);
                        this.y1 = Math.Min(y1, y2);
                        this.x2 = Math.Max(x1, x2);
                        this.y2 = Math.Max(y1, y2);
                  }

                  public bool Contains(int x, int y) {
                        return (x1 <= x && x <= x2 && y1 <= y && y <= y2);
                  }

                  public static Segment Parse(string segment) {
                        string[] splitted = segment.Split(new char[] { ' ' });
                        return new Segment(
                              int.Parse(splitted[0]),
                              int.Parse(splitted[1]),
                              int.Parse(splitted[2]),
                              int.Parse(splitted[3]));
                  }
            }
      }
}