using System;
using System.Collections;
using System.Collections.Generic;

      public class TheCitiesAndRoadsDivTwo {
            public int find(string[] map) {
                  return (int)find(Array.ConvertAll(map, delegate(string s) {
                        List<int> adjacency = new List<int>();
                        for (int i = 0; i < s.Length; ++i) {
                              if (s[i].Equals('Y')) {
                                    adjacency.Add(i);
                              }
                        }
                        return adjacency;
                  }), map.Length);
            }

            private long find(List<int>[] graph, int numOfVertices) {
                  List<List<int>> connectedComponents = getConnectedComponents(graph, numOfVertices);
                  return find(Array.ConvertAll(connectedComponents.ToArray(),
                        delegate(List<int> connectedComponent) {
                              return connectedComponent.Count;
                  }), connectedComponents.Count);
            }

            private long find(int[] particles, int numOfParticles) {
                  if (numOfParticles > 1) {
                        long sum = 0;
                        long mul = 1;
                        foreach (int particle in particles) {
                              sum += particle;
                              mul *= particle;
                        }
                        return mul * pow(sum, numOfParticles - 2);
                  }
                  return 1;
            }

            private long pow(long x, int k) {
                  if (k == 0) {
                        return 1;
                  }
                  else if (k % 2 == 0) {
                        return pow(x * x, k >> 1);
                  }
                  else {
                        return x * pow(x, k - 1);
                  }
            }

            private List<List<int>> getConnectedComponents(List<int>[] graph, int numOfVertices) {
                  List<List<int>> result = new List<List<int>>();
                  bool[] visited = new bool[numOfVertices];
                  for (int curr = 0; curr < numOfVertices; ++curr) {
                        if (!visited[curr]) {
                              result.Add(dfs(graph, curr, visited));
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
      }
