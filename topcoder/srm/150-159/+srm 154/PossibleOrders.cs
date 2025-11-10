using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class PossibleOrders {
            public long howMany(int num, string[] facts) {
                  bool[,] equivalent = new bool[num, num]; /* the equivalent numbers can be replaced by anyone... */
                  foreach (string fact in facts) {
                        string[] items = fact.Split('=');
                        equivalent[int.Parse(items[0]), int.Parse(items[1])] = true;
                        equivalent[int.Parse(items[1]), int.Parse(items[0])] = true;
                  }
                  int numOfGroups = 0;
                  bool[] visited = new bool[num];
                  for (int curr = 0; curr < num; ++curr) {
                        if (!visited[curr]) {
                              dfs(equivalent, num, curr, visited);
                              numOfGroups = numOfGroups + 1;
                        }
                  }
                  return howMany(numOfGroups);
            }

            private long howMany(int num) {
                  /* Stirling number of the second kind i.e. the number of ways to partition a set of n objects into k non-empty subsets... */
                  long[,] s = new long[num + 1, num + 1];
                  for (int n = 0; n <= num; ++n) {
                        s[n, n] = 1;
                        for (int k = 1; k < n; ++k) {
                              s[n, k] = s[n - 1, k - 1] + k * s[n - 1, k];
                        }
                  }
                  long res = 0;
                  for (int numOfSets = 1; numOfSets <= num; ++numOfSets) {
                        res += s[num, numOfSets] * f(numOfSets);
                  }
                  return res;
            }

            private long f(int n) {
                  if (n > 0) {
                        return n * f(n - 1);
                  }
                  return 1;
            }

            private void dfs(bool[,] graph, int num, int curr, bool[] visited) {
                  visited[curr] = true;
                  for (int next = 0; next < num; ++next) {
                        if (graph[curr, next]) {
                              if (!visited[next]) {
                                    dfs(graph, num, next, visited);
                              }
                        }
                  }
            }
      }
}