using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithms {
      public class GameOfLife {
            public int alive(string[] start, string rules, int generations) {
                  return alive(Array.ConvertAll(start, delegate(string s) {
                        int[] result = new int[s.Length];
                        for (int i = 0; i < s.Length; ++i) {
                              if (s[i].Equals('X')) {
                                    result[i] = 1;
                              }
                        }
                        return result;
                  }), start.Length, start[0].Length, rules, generations);
            }

            private int alive(int[][] field, int n, int m, string rules, int generations) {
                  for (int times = 0; times < generations; ++times) {
                        int[,] count = new int[n, m];
                        for (int i = 0; i < n; ++i) {
                              for (int j = 0; j < m; ++j) {
                                    for (int di = -1; di <= +1; ++di) {
                                          for (int dj = -1; dj <= +1; ++dj) {
                                                if (di != 0 || dj != 0) {
                                                      count[i, j] += field[(i + di + n) % n][(j + dj + m) % m];
                                                }
                                          }
                                    }
                              }
                        }
                        for (int i = 0; i < n; ++i) {
                              for (int j = 0; j < m; ++j) {
                                    switch (rules[count[i, j]]) {
                                          case 'D': field[i][j] = 0; break;
                                          case 'S': break;
                                          case 'B': field[i][j] = 1; break;
                                    }
                              }
                        }
                  }
                  int result = 0;
                  for (int i = 0; i < n; ++i) {
                        for (int j = 0; j < m; ++j) {
                              result += field[i][j];
                        }
                  }
                  return result;
            }

            private static string ToString<T>(T[] a) {
                  string result = string.Empty;
                  for (int i = 0; i < a.Length; ++i) {
                        result += a[i].ToString();
                        if (i + 1 < a.Length) {
                              result += ' ';
                        }
                  }
                  return result + Environment.NewLine;
            }

            public static void Main(string[] args) {

                  Console.WriteLine("Press any key to continue...");
                  Console.ReadLine();
            }
      }
}