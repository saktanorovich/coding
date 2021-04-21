using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithms {
      public class PastingPaintingDivOne {
            public long[] countColors(string[] clipboard, int T) {
                  return countColors(Array.ConvertAll(clipboard, delegate(string s) {
                        return Array.ConvertAll(s.ToCharArray(), delegate(char c) {
                              return ".RGB".IndexOf(c);
                        });
                  }), clipboard.Length, clipboard[0].Length, T);
            }

            private long[] countColors(int[][] clipboard, int n, int m, int T) {
                  long[] previus = new long[4];
                  long[] current = new long[4];
                  int iterations = 2 * Math.Max(n, m);
                  int[,] canvas = new int[2 * iterations, 2 * iterations];
                  for (int t = 0; t < Math.Min(iterations, T); ++t) {
                        previus = (long[])current.Clone();
                        for (int i = 0; i < n; ++i) {
                              for (int j = 0; j < m; ++j) {
                                    if (clipboard[i][j] > 0) {
                                          --current[canvas[t + i, t + j]];
                                          canvas[t + i, t + j] = clipboard[i][j];
                                          ++current[canvas[t + i, t + j]];
                                    }
                              }
                        }
                  }
                  if (T > iterations) {
                        for (int i = 1; i <= 3; ++i) {
                              current[i] += (current[i] - previus[i]) * (T - iterations);
                        }
                  }
                  return new long[] { current[1], current[2], current[3] };
            }

            private static string ToString(long[] a) {
                  string res = string.Empty;
                  for (int i = 0; i < a.Length; ++i) {
                        res += a[i];
                        if (i + 1 < a.Length) {
                              res += ' ';
                        }
                  }
                  return res;
            }

            public static void Main(string[] args) {
                  Console.WriteLine(ToString(new PastingPaintingDivOne().countColors(new string[] {
                        "..G", "R..", "BG.", }, 3)));
                  Console.WriteLine(ToString(new PastingPaintingDivOne().countColors(new string[] {
"..........G..........",
".........G.G.........",
"........G...G........",
".......G.....G.......",
"......G..B.B..G......",
".....G...B.B...G.....",
"....G...........G....",
"...G...R.....R...G...",
"..G.....RRRRRR....G..",
".G..........RR.....G.",
"GGGGGGGGGGGGGGGGGGGGG" }, 1000000000)));
 

                  Console.WriteLine("Press any key to continue...");
                  Console.ReadLine();
            }
      }
}