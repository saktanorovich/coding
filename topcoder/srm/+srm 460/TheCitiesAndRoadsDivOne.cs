using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithms {
      public class TheCitiesAndRoadsDivOne {
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

            private readonly long modulo = 1234567891;
            private readonly long[] sum = new long[1 << 20];
            private readonly long[] mul = new long[1 << 20];
            private readonly long[,] memo = new long[1 << 20, 2];
            private readonly SortedDictionary<long, long> cache = new SortedDictionary<long, long>();

            /* note that in order to satisfy the property of the desired graph, it should contain at most one cycle... */
            private long find(List<int>[] graph, int numOfVertices) {
                  sum[0] = 0;
                  mul[0] = 1;
                  int[] particles = getParticles(graph, numOfVertices);
                  int particlesSet = (1 << particles.Length) - 1;
                  for (int set = 0; set <= particlesSet; ++set) {
                        memo[set, 0] = -1;
                        memo[set, 1] = -1;
                        for (int i = 0; i < particles.Length; ++i) {
                              if ((set & (1 << i)) != 0) {
                                    sum[set] = sum[set ^ (1 << i)] + particles[i];
                                    mul[set] = mul[set ^ (1 << i)] * particles[i];
                                    break;
                              }
                        }
                  }
                  long result = find(particles, particlesSet, 1);
                  if (!hasCycle(graph, numOfVertices)) {
                        result += find(particles, particlesSet, 0);
                        result %= modulo;
                  }
                  return result;
            }

            private long find(int[] particles, int set, int extra) {
                  if (memo[set, extra] == -1) {
                        if (extra > 0) {
                              memo[set, extra] = 1;
                              if (numOfParticels(set) > 1) {
                                    memo[set, extra] = (mul[set] * pow(sum[set], numOfParticels(set) - 2)) % modulo;
                              }
                        }
                        else {
                              memo[set, extra] = (sum[set] * (sum[set] - 1) / 2 - (sum[set] - 1)) % modulo;
                              if (numOfParticels(set) > 1) {
                                    long result = 0, hash = 1;
                                    for (int i = 0; i < particles.Length; ++i) {
                                          if ((set & (1 << i)) != 0) {
                                                hash = hash * 97 + particles[i];
                                          }
                                    }
                                    if (!cache.TryGetValue(hash, out result)) {
                                          /* split the set into two subsets with fixed vertex in one subset and another subset... */
                                          int xset = set ^ last(set) ^ last(set - last(set));
                                          for (int subset = xset; subset >= 0; subset = (subset - 1) & xset) {
                                                int sub = subset ^ last(set);

                                                long edg = (sum[sub] * sum[last(set - last(set))]); /* the number of ways to connect one subset to the vertex from another subset. */
                                                long r01 = (find(particles, sub, 0) * find(particles, sub ^ set, 1)) % modulo;
                                                long r10 = (find(particles, sub, 1) * find(particles, sub ^ set, 0)) % modulo;
                                                long r11 = (find(particles, sub, 1) * find(particles, sub ^ set, 1)) % modulo;

                                                result = (result + r01 * edg) % modulo;
                                                result = (result + r10 * edg) % modulo;
                                                result = (result + r11 * edg * (edg - 1) / 2) % modulo;

                                                if (subset == 0) break;
                                          }
                                          cache[hash] = result;
                                    }
                                    memo[set, extra] = result;
                              }
                        }
                  }
                  return memo[set, extra];
            }

            private int last(int set) {
                  return set & (-set);
            }

            private int numOfParticels(int set) {
                  return set > 0 ? 1 + numOfParticels(set & (set - 1)) : 0;
            }

            private long pow(long x, int k) {
                  if (k == 0) {
                        return 1;
                  }
                  else if (k % 2 == 0) {
                        return pow((x * x) % modulo, k >> 1);
                  }
                  else {
                        return (x * pow(x, k - 1)) % modulo;
                  }
            }

            private int[] getParticles(List<int>[] graph, int numOfVertices) {
                  List<int> result = new List<int>();
                  bool[] visited = new bool[numOfVertices];
                  for (int curr = 0; curr < numOfVertices; ++curr) {
                        if (!visited[curr]) {
                              result.Add(dfs(graph, curr, visited));
                        }
                  }
                  result.Sort();
                  return result.ToArray();
            }

            private int dfs(List<int>[] graph, int curr, bool[] visited) {
                  visited[curr] = true;
                  int result = 1;
                  foreach (int next in graph[curr]) {
                        if (!visited[next]) {
                              result += dfs(graph, next, visited);
                        }
                  }
                  return result;
            }

            private bool hasCycle(List<int>[] graph, int numOfVertices) {
                  bool[] visited = new bool[numOfVertices];
                  for (int curr = 0; curr < numOfVertices; ++curr) {
                        if (hasCycle(graph, curr, -1, new bool[numOfVertices])) {
                              return true;
                        }
                  }
                  return false;
            }

            private bool hasCycle(List<int>[] graph, int curr, int prev, bool[] visited) {
                  if (visited[curr]) {
                        return true;
                  }
                  visited[curr] = true;
                  foreach (int next in graph[curr]) {
                        if (next != prev) {
                              if (hasCycle(graph, next, curr, visited)) {
                                    return true;
                              }
                        }
                  }
                  return false;
            }

            public static void Main(string[] args) {
                  Console.WriteLine(new TheCitiesAndRoadsDivOne().find(new string[] { "NNN", "NNN", "NNN" })); // 4
                  Console.WriteLine(new TheCitiesAndRoadsDivOne().find(new string[] { "NN", "NN" })); // 1
                  Console.WriteLine(new TheCitiesAndRoadsDivOne().find(new string[] { "NNY", "NNN", "YNN" })); // 3
                  Console.WriteLine(new TheCitiesAndRoadsDivOne().find(new string[] { "NY", "YN" })); // 1
                  Console.WriteLine(new TheCitiesAndRoadsDivOne().find(new string[] { "NYNN", "YNNN", "NNNY", "NNYN" })); // 10
                  Console.WriteLine(new TheCitiesAndRoadsDivOne().find(new string[] {
                        "NNNNNNNNNNNN", "NNNNNNNNNNNN", "NNNNNNNNNNNN", "NNNNNNNNNNNN",
                        "NNNNNNNNNNNN", "NNNNNNNNNNNN", "NNNNNNNNNNNN", "NNNNNNNNNNNN",
                        "NNNNNNNNNNNN", "NNNNNNNNNNNN", "NNNNNNNNNNNN", "NNNNNNNNNNNN"})); // 1137797187
                  Console.WriteLine(new TheCitiesAndRoadsDivOne().find(new string[] { "N" })); // 1
                  Console.WriteLine(new TheCitiesAndRoadsDivOne().find(new string[] { "NYYNN", "YNYNN", "YYNNN", "NNNNY", "NNNYN" })); // 6
                  Console.WriteLine(new TheCitiesAndRoadsDivOne().find(new string[] {
                        "NNNNNNNNNNNNNNNNNNNN", "NNNNNNNNNNNNNNNNNNNN", "NNNNNNNNNNNNNNNNNNNN", "NNNNNNNNNNNNNNNNNNNN",
                        "NNNNNNNNNNNNNNNNNNNN", "NNNNNNNNNNNNNNNNNNNN", "NNNNNNNNNNNNNNNNNNNN", "NNNNNNNNNNNNNNNNNNNN",
                        "NNNNNNNNNNNNNNNNNNNN", "NNNNNNNNNNNNNNNNNNNN", "NNNNNNNNNNNNNNNNNNNN", "NNNNNNNNNNNNNNNNNNNN",
                        "NNNNNNNNNNNNNNNNNNNN", "NNNNNNNNNNNNNNNNNNNN", "NNNNNNNNNNNNNNNNNNNN", "NNNNNNNNNNNNNNNNNNNN",
                        "NNNNNNNNNNNNNNNNNNNN", "NNNNNNNNNNNNNNNNNNNN", "NNNNNNNNNNNNNNNNNNNN", "NNNNNNNNNNNNNNNNNNNN" }));

                  Console.WriteLine("Press any key to continue...");
                  Console.ReadLine();
            }
      }
}