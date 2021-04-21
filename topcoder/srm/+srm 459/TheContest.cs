using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithms {
      public class TheContest {
            public string[] getSchedule(int n, int m) {
                  return Array.ConvertAll(getScheduleOfFights(n, m), delegate(int[] list) {
                        string result = string.Empty;
                        for (int i = 0; i < m; ++i) {
                              result += alphabet[list[i]];
                        }
                        return result;
                  });
            }

            private const string alphabet = " 123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmno";
            private const int undefined = -1;

            private int num1, num2;
            private int numOfRounds, numOfFights;
            private int[] roundOccurence;
            private bool[,] competeInTheRound1;
            private bool[,] competeInTheRound2;

            /* The main idea is to assign the fighters from T2 to the rounds i.e. color the edges (f2, r) where color
             * belongs to the [1..n] i.e. the number of colors is equal to |T1|. Consider three separate cases:
             * 
             * (1) n < m: the number of rounds is m. Assume d fighters from T1 were analyzed. So the m fighters from T2 should be
             *            assigned to the m fights. Because each fighter from T2 was competed in d rounds and each round was occured
             *            d times we have got that the degree of each fighter from T2 and the degree of each round is equal to n - d.
             *            We should color this graph to n - d colors which is possible by constructing (n - d) non disjoint perfect matchings.
             * 
             * (2) n = m: the number of rounds is m. This case is similar to the previous one.
             * 
             * (3) n > m: the number of rounds is n. Assume d fighters from T1 were analyzed. So the m fighters from T2 should be
             *            assigned to the m fights. Because each fighter from T2 was competed in d rounds we have got that the degree
             *            of each fighter from T2 is equal to n - d but the degrees of the rounds may differ. Consider three separate cases:
             *                (a) r < n - d: in this case the rounds may be included or not to the matching.
             *                (b) r = n - d: in this case the rounds with degree n - d must be included to the matching because
             *                               otherwise the next step will result to the non-colored edges.
             *                (c) r > n - d: in this case it is impossible to complete the problem because some edges would not be
             *                               colored (there are n - d colors at all which is not enough to color all edges). */
            private int[][] getScheduleOfFights(int n, int m) {
                  num1 = n;
                  num2 = m;
                  /* schedule[f1][f2] -- the number of the round where fighter f1 encounters fighter f2. */
                  int[][] schedule = Array.ConvertAll(new int[n], delegate(int x) {
                        return new int[m];
                  });
                  /* we should process the schedule row by row, column by column because lexicographically earliest schedule is required. */
                  numOfRounds = Math.Max(n, m);
                  numOfFights = Math.Min(n, m);
                  roundOccurence = new int[numOfRounds];
                  competeInTheRound1 = new bool[n, numOfRounds];
                  competeInTheRound2 = new bool[m, numOfRounds];
                  for (int f1 = 0; f1 < n; ++f1) {
                        for (int f2 = 0; f2 < m; ++f2) {
                              for (int round = 0; round < numOfRounds; ++round) {
                                    if (possible(f1, f2, round)) {
                                          schedule[f1][f2] = round + 1;
                                          goto next;
                                    }
                              }
                              throw new Exception();
                              next:;
                        }
                  }
                  return schedule;
            }

            private bool possible(int f1, int f2, int xround) {
                  if (competeInTheRound1[f1, xround]) {
                        return false;
                  }
                  if (competeInTheRound2[f2, xround]) {
                        return false;
                  }
                  competeInTheRound1[f1, xround] = true;
                  competeInTheRound2[f2, xround] = true;
                  roundOccurence[xround] = roundOccurence[xround] + 1;
                  /* build bipartite graph for the fighters from T2 not assigned to the fighter f1 from T1... */
                  int numOfFighters = num2 - f2 - 1;
                  int numOfVertices = numOfFighters + numOfRounds;
                  bool[,] bipartite = new bool[numOfVertices, numOfVertices];
                  for (int f = 0; f2 + 1 + f < num2; ++f) {
                        for (int r = 0; r < numOfRounds; ++r) {
                              if (competeInTheRound1[f1, r] || competeInTheRound2[f2 + 1 + f, r]) {
                                  continue;
                              }
                              bipartite[f, r + numOfFighters] = true;
                              bipartite[r + numOfFighters, f] = true;
                        }
                  }
                  /* apply kuhn algorithm in order to build maximum matching... */
                  int[] matching = Array.ConvertAll(new int[numOfVertices],
                        delegate(int x) {
                              return undefined;
                  });
                  int cardinality = 0;
                  for (int round = 0; round < numOfRounds; ++round) {
                        int roundDegree = numOfFights - roundOccurence[round];
                        if (roundDegree == num1 - f1) {
                              if (dfs(bipartite, numOfVertices, round + numOfFighters, matching, new bool[numOfVertices])) {
                                    cardinality = cardinality + 1;
                              }
                              else goto exit;
                        }
                  }
                  for (int round = 0; round < numOfRounds; ++round) {
                        int roundDegree = numOfFights - roundOccurence[round];
                        if (roundDegree < num1 - f1) {
                              if (dfs(bipartite, numOfVertices, round + numOfFighters, matching, new bool[numOfVertices])) {
                                    cardinality = cardinality + 1;
                              }
                        }
                  }
                  if (cardinality == numOfFighters) {
                        return true;
                  }
                  exit: {
                        competeInTheRound1[f1, xround] = false;
                        competeInTheRound2[f2, xround] = false;
                        roundOccurence[xround] = roundOccurence[xround] - 1;
                  }
                  return false;
            }

            private bool dfs(bool[,] bipartite, int numOfVertices, int curr, int[] matching, bool[] visited) {
                  if (visited[curr]) {
                        return false;
                  }
                  visited[curr] = true;
                  for (int next = 0; next < numOfVertices; ++next) {
                        if (bipartite[curr, next]) {
                              if (matching[next] == undefined || dfs(bipartite, numOfVertices, matching[next], matching, visited)) {
                                    matching[curr] = next;
                                    matching[next] = curr;
                                    return true;
                              }
                        }
                  }
                  return false;
            }

            private static string ToString(string[] s) {
                  string result = string.Empty;
                  for (int i = 0; i < s.Length; ++i) {
                        result += s[i] + Environment.NewLine;
                  }
                  return result;
            }

            public static void Main(string[] args) {
                  Console.WriteLine(ToString(new TheContest().getSchedule(3, 3)));
                  Console.WriteLine(ToString(new TheContest().getSchedule(4, 4)));
                  Console.WriteLine(ToString(new TheContest().getSchedule(4, 6)));
                  Console.WriteLine(ToString(new TheContest().getSchedule(5, 3)));
                  Console.WriteLine(ToString(new TheContest().getSchedule(28, 40)));
                  Console.WriteLine(ToString(new TheContest().getSchedule(20, 10)));
                  Console.WriteLine(ToString(new TheContest().getSchedule(50, 50)));

                  Console.WriteLine("Press any key to continue...");
                  Console.ReadLine();
            }
      }
}