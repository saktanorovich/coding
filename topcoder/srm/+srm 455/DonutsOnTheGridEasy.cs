using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class DonutsOnTheGridEasy {
            public int calc(string[] grid) {
                  return calc(grid.Length, grid[0].Length, Array.ConvertAll(grid, delegate(string s) {
                        return Array.ConvertAll(s.ToCharArray(), delegate(char c) {
                              return c.Equals('0');
                        });
                  }));
            }

            private int calc(int n, int m, bool[][] grid) {
                  isSequentialRow = new bool[n, m, m];
                  isSequentialCol = new bool[m, n, n];
                  for (int i = 0; i < n; ++i) {
                        for (int j1 = 0; j1 < m; ++j1) {
                              for (int j2 = j1; j2 < m; ++j2) {
                                    isSequentialRow[i, j1, j2] = true;
                                    for (int j = j1; j <= j2; ++j) {
                                          isSequentialRow[i, j1, j2] &= grid[i][j];
                                    }
                              }
                        }
                  }
                  for (int j = 0; j < m; ++j) {
                        for (int i1 = 0; i1 < n; ++i1) {
                              for (int i2 = i1; i2 < n; ++i2) {
                                    isSequentialCol[j, i1, i2] = true;
                                    for (int i = i1; i <= i2; ++i) {
                                          isSequentialCol[j, i1, i2] &= grid[i][j];
                                    }
                              }
                        }
                  }
                  cache = new int[n, m, n, m];
                  for (int i1 = 0; i1 < n; ++i1) {
                        for (int j1 = 0; j1 < m; ++j1) {
                              for (int i2 = i1 + 2; i2 < n; ++i2) {
                                    for (int j2 = j1 + 2; j2 < m; ++j2) {
                                          cache[i1, j1, i2, j2] = -1;
                                    }
                              }
                        }
                  }
                  return calc(0, 0, n - 1, m - 1);
            }

            private bool[,,] isSequentialRow;
            private bool[,,] isSequentialCol;
            private int[,,,] cache;

            private int calc(int i1, int j1, int i2, int j2) {
                  if (cache[i1, j1, i2, j2] == -1) {
                        if (isSequentialRow[i1, j1, j2] && isSequentialRow[i2, j1, j2] && isSequentialCol[j1, i1, i2] && isSequentialCol[j2, i1, i2]) {
                              cache[i1, j1, i2, j2] = 1 + calc(i1 + 1, j1 + 1, i2 - 1, j2 - 1);
                        }
                        else {
                              int result = 0;
                              if (i1 < i2) result = Math.Max(result, calc(i1 + 1, j1, i2, j2));
                              if (i1 < i2) result = Math.Max(result, calc(i1, j1, i2 - 1, j2));
                              if (j1 < j2) result = Math.Max(result, calc(i1, j1 + 1, i2, j2));
                              if (j1 < j2) result = Math.Max(result, calc(i1, j1, i2, j2 - 1));
                              cache[i1, j1, i2, j2] = result;
                        }
                  }
                  return cache[i1, j1, i2, j2];
            }

            public static void Main() {
                  Console.WriteLine(new DonutsOnTheGridEasy().calc(new string[] {
                        "0000000",
                        "0.....0",
                        "0.00000",
                        "0.0..00",
                        "0.00000",
                        "0.....0",
                        "0000000"}));
                  Console.WriteLine(new DonutsOnTheGridEasy().calc(new string[] {
                        "000",
                        "0.0",
                        "000"}));
                  Console.WriteLine(new DonutsOnTheGridEasy().calc(new string[] {
                        "...",
                        "...",
                        "..."}));
                  Console.WriteLine(new DonutsOnTheGridEasy().calc(new string[] {
                        "00.000",
                        "00.000",
                        "0.00.0",
                        "000.00"}));
                  Console.WriteLine(new DonutsOnTheGridEasy().calc(new string[] {
                        "0000000....",
                        "0000000....",
                        "0000000.000",
                        "0000000.0.0",
                        "0000000.000",
                        "0000000....",
                        "0000000...."}));

                  Console.WriteLine("\nPress any key to continue...");
                  Console.ReadKey();
            }
      }
}