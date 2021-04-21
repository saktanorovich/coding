using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithms {
      public class InducedSubgraphs {
            public int getCount(int[] edge1, int[] edge2, int k) {
                  List<int>[] tree = Array.ConvertAll(new int[edge1.Length + 1], delegate(int x) {
                        return new List<int>();
                  });
                  for (int i = 0; i < edge1.Length; ++i) {
                        tree[edge1[i]].Add(edge2[i]);
                        tree[edge2[i]].Add(edge1[i]);
                  }
                  return (int)getCount(tree, tree.Length, k);
            }

            private static readonly long modulo = 1000000009;
            private static readonly long[] facto = new long[50];
            private static readonly long[,] comb = new long[50, 50];

            /* subtreeSize[n][p] -- num of nodes at the subtree rooted at n having the parent p. */
            private int[][] subtreeSize = new int[50][];
            private long[][,] cumul = new long[50][,];

            private long getCount(List<int>[] tree, int numOfNodes, int induceFactor) {
                  facto[0] = 1; comb[0, 0] = 1;
                  for (int i = 1; i <= numOfNodes; ++i) {
                        comb[i, 0] = 1;
                        for (int j = 1; j <= i; ++j) {
                              comb[i, j] = (comb[i - 1, j] + comb[i - 1, j - 1]) % modulo;
                        }
                        facto[i] = (i * facto[i - 1]) % modulo;
                  }
                  if (1 < induceFactor && induceFactor < numOfNodes) {
                        for (int node = 0; node < numOfNodes; ++node) {
                              subtreeSize[node] = new int[numOfNodes];
                              foreach (int prev in tree[node]) {
                                    subtreeSize[node][prev] = getSubtreeSize(tree, node, prev);
                              }
                              subtreeSize[node][node] = getSubtreeSize(tree, node, node);
                        }
                        if (2 * induceFactor > numOfNodes) {
                              /* In this case the tree has a group of 2k-n nodes which belongs to the all induced subgraphs (call such nodes internal, the other
                               * nodes call external). We can show that internal nodes should perform connected subtree in order to prevent cycles in the original
                               * graph (proof by contradiction). Also the external nodes should have degree equal to 1 at the deletion moment and each external node
                               * should be connected to at most one internal node in order to prevent cycles in the original graph and disconnected internal groups. */
                              int anyInternalNode = -1;
                              for (int node = 0; node < numOfNodes; ++node) {
                                    bool isInternalNode = true;
                                    foreach (int child in tree[node]) {
                                          isInternalNode &= (subtreeSize[child][node] < induceFactor);
                                    }
                                    if (isInternalNode) {
                                          anyInternalNode = node;
                                          break;
                                    }
                              }
                              if (anyInternalNode != -1) {
                                    count(tree, anyInternalNode, anyInternalNode, numOfNodes - induceFactor);
                                    long result = cumul[anyInternalNode][numOfNodes - induceFactor, numOfNodes - induceFactor];
                                    result *= facto[2 * induceFactor - numOfNodes]; /* the internal nodes can be shuffled in any order... */
                                    result %= modulo;
                                    return result;
                              }
                        }
                        else {
                              /* In this case the deleted and added nodes should have degree equal to 1, i.e. the original tree
                               * has a chain structure with bushes on the ends... */
                              int[][] dist = new int[numOfNodes][];
                              int[][] mark = new int[numOfNodes][];
                              for (int node = 0; node < numOfNodes; ++node) {
                                    dist[node] = new int[numOfNodes];
                                    mark[node] = new int[numOfNodes];
                                    buildDistanceTable(tree, node, -1, dist[node], mark[node], 0);
                              }
                              int numOfValidEndings = 0, ending1 = -1, ending2 = -1;
                              for (int end1 = 0; end1 < numOfNodes; ++end1) {
                                    for (int end2 = end1 + 1; end2 < numOfNodes; ++end2) {
                                          if (dist[end1][end2] == numOfNodes - 2 * induceFactor + 1) {
                                                List<int> intermediateNodes = new List<int>();
                                                for (int node = mark[end1][end2]; node != end1; node = mark[end1][node]) {
                                                      intermediateNodes.Add(node);
                                                }
                                                if (intermediateNodes.Count == numOfNodes - 2 * induceFactor) {
                                                      bool validIntermediateNodes = true;
                                                      foreach (int node in intermediateNodes) {
                                                            if (tree[node].Count != 2) {
                                                                  validIntermediateNodes = false;
                                                                  goto next;
                                                            }
                                                      }
                                                      validIntermediateNodes &= subtreeSize[end1][mark[end2][end1]] == induceFactor;
                                                      validIntermediateNodes &= subtreeSize[end2][mark[end1][end2]] == induceFactor;
                                                      if (validIntermediateNodes) {
                                                            ending1 = end1;
                                                            ending2 = end2;
                                                            numOfValidEndings = numOfValidEndings + 1;
                                                      }
                                                }
                                                next:;
                                          }
                                    }
                              }
                              if (numOfValidEndings == 1) {
                                    return (2 * count(tree, ending1, mark[ending2][ending1]) * count(tree, ending2, mark[ending1][ending2])) % modulo;
                              }
                        }
                        return 0;
                  }
                  return facto[numOfNodes];
            }

            /* Counts the num of ways to assign small and large labels to the external nodes at subtree rooted at curr having the parent prev. */
            private void count(List<int>[] tree, int curr, int prev, int numOfExternalNodes) {
                  cumul[curr] = new long[numOfExternalNodes + 1, numOfExternalNodes + 1]; cumul[curr][0, 0] = 1;
                  foreach (int next in tree[curr]) {
                        if (next != prev) {
                              count(tree, next, curr, numOfExternalNodes);
                              /**/
                              cumul[curr] = combine(cumul[curr], cumul[next], numOfExternalNodes);
                              /* count from top to bottom in order to prevent incorrect accumulation... *
                              for (int rsmall = numOfExternalNodes; rsmall >= 0; --rsmall) {
                                    for (int rlarge = numOfExternalNodes; rlarge >= 0; --rlarge) {
                                          if (cumul[curr][rsmall, rlarge] > 0) {
                                                for (int csmall = numOfExternalNodes - rsmall; csmall >= 0; --csmall) {
                                                      for (int clarge = numOfExternalNodes - rlarge; clarge >= 0; --clarge) {
                                                            if (cumul[next][csmall, clarge] > 0 && csmall + clarge > 0) {
                                                                  long eval = cumul[curr][rsmall, rlarge];
                                                                  eval *= cumul[next][csmall, clarge];
                                                                  eval %= modulo;
                                                                  eval *= comb[rsmall + csmall, csmall];
                                                                  eval %= modulo;
                                                                  eval *= comb[rlarge + clarge, clarge];
                                                                  eval %= modulo;
                                                                  cumul[curr][rsmall + csmall, rlarge + clarge] += eval;
                                                                  cumul[curr][rsmall + csmall, rlarge + clarge] %= modulo;
                                                            }
                                                      }
                                                }
                                          }
                                    }
                              }
                              /**/
                        }
                  }
                  if (subtreeSize[curr][prev] <= numOfExternalNodes) {
                        long assignments = count(tree, curr, prev);
                        cumul[curr][subtreeSize[curr][prev], 0] = assignments;
                        cumul[curr][0, subtreeSize[curr][prev]] = assignments;
                  }
            }

            private long[,] combine(long[,] a, long[,] b, int n) {
                  long[,] result = new long[n + 1, n + 1];
                  for (int smalla = 0; smalla <= n; ++smalla) {
                        for (int largea = 0; largea <= n; ++largea) {
                              for (int smallb = 0; smallb <= n; ++smallb) {
                                    for (int largeb = 0; largeb <= n; ++largeb) {
                                          if (smalla + smallb <= n && largea + largeb <= n) {
                                                long eval = a[smalla, largea];
                                                eval *= b[smallb, largeb];
                                                eval %= modulo;
                                                eval *= comb[smalla + smallb, smallb];
                                                eval %= modulo;
                                                eval *= comb[largea + largeb, largeb];
                                                eval %= modulo;
                                                result[smalla + smallb, largea + largeb] += eval;
                                                result[smalla + smallb, largea + largeb] %= modulo;
                                          }
                                    }
                              }
                        }
                  }
                  return result;
            }

            /* Returns num of ways to assign labels to the nodes at subtree rooted at curr having the parent prev.
             * The procedure can be used in both cases when the root has > or < label then its descendants. */
            private long count(List<int>[] tree, int curr, int prev) {
                  long result = 1, numOfDescendants = 0;
                  foreach (int next in tree[curr]) {
                        if (next != prev) {
                              numOfDescendants += subtreeSize[next][curr];
                              result *= count(tree, next, curr);
                              result %= modulo;
                              result *= comb[numOfDescendants, subtreeSize[next][curr]];
                              result %= modulo;
                        }
                  }
                  return result;
            }

            private void buildDistanceTable(List<int>[] tree, int curr, int prev, int[] dist, int[] mark, int d) {
                  dist[curr] = d;
                  mark[curr] = prev;
                  foreach (int next in tree[curr]) {
                        if (next != prev) {
                              buildDistanceTable(tree, next, curr, dist, mark, d + 1);
                        }
                  }
            }

            private int getSubtreeSize(List<int>[] tree, int curr, int prev) {
                  int result = 1;
                  foreach (int next in tree[curr]) {
                        if (next != prev) {
                              result += getSubtreeSize(tree, next, curr);
                        }
                  }
                  return result;
            }

            public static void Main(string[] args) {
                  Console.WriteLine(new InducedSubgraphs().getCount(
                        new int[] { 0, 1 },
                        new int[] { 1, 2 }, 2)); // 2
                  Console.WriteLine(new InducedSubgraphs().getCount(
                        new int[] { 0, 1, 3 },
                        new int[] { 2, 2, 2 }, 3)); // 12
                  Console.WriteLine(new InducedSubgraphs().getCount(
                        new int[] { 5, 0, 1, 2, 2 },
                        new int[] { 0, 1, 2, 4, 3 }, 3)); // 4
                  Console.WriteLine(new InducedSubgraphs().getCount(
                        new int[] { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6 },
                        new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 }, 11)); // 481904640
                  Console.WriteLine(new InducedSubgraphs().getCount(
                        new int[] { 5, 9, 4, 10, 10, 0, 7, 6, 2, 1, 11, 8 },
                        new int[] { 0, 0, 10, 3, 0, 6, 1, 1, 12, 12, 7, 11 }, 6)); // 800
                  Console.WriteLine(new InducedSubgraphs().getCount(
                        new int[] { 0, 5, 1, 0, 2, 3, 5 },
                        new int[] { 4, 7, 0, 6, 7, 5, 0 }, 3)); // 0
                  Console.WriteLine(new InducedSubgraphs().getCount(
                        new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                        new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 }, 1)); // 890964601
                  Console.WriteLine(new InducedSubgraphs().getCount(
                        new int[] { 5, 4, 4, 5, 5, 8, 8, 8, 3, 3, 1, 15, 6, 6, 13, 14, 9 },
                        new int[] { 12, 10, 2, 11, 8, 4, 0, 7, 7, 1, 16, 13, 1, 17, 6, 15, 14 }, 8)); // 39200
                  Console.WriteLine(new InducedSubgraphs().getCount(
                        new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39 },
                        new int[] { 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40 }, 31));
                  // 79888617

                  Console.WriteLine("Press any key to continue...");
                  Console.ReadLine();
            }
      }
}