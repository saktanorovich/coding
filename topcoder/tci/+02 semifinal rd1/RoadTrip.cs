using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithms {
      public class RoadTrip {
            public int howMany(string[] map) {
                  return howMany(Array.ConvertAll(map, delegate(string s) {
                        int[] result = new int[s.Length];
                        for (int i = 0; i < s.Length; ++i) {
                              switch (s[i]) {
                                    case 'R': result[i] = 3; break;
                                    case 'L': result[i] = 1; break;
                                    case 'B': result[i] = 2; break;
                              }
                        }
                        return result;
                  }), map.Length, map[0].Length);
            }

            private readonly int[] dx = new int[4] { -1,  0, +1,  0 };
            private readonly int[] dy = new int[4] {  0, -1,  0, +1 };

            private int howMany(int[][] map, int n, int m) {
                  int result = 1;
                  for (int i = 0; i < n; ++i) {
                        for (int j = 0; j < m; ++j) {
                              for (int k = 0; k < 4; ++k) {
                                    result = Math.Max(result, howMany(map, n, m, i, j, k));
                              }
                        }
                  }
                  return result;
            }

            private int howMany(int[][] map, int n, int m, int x, int y, int d) {
                  bool[,] where = new bool[n, m]; bool[,,] visited = new bool[n, m, 4];
                  while (!visited[x, y, d]) {
                        where[x, y] = visited[x, y, d] = true;
                        d = (d + map[x][y]) % 4;
                        x += dx[d];
                        y += dy[d];
                        if (0 <= x && x < n && 0 <= y && y < m) {
                        }
                        else {
                              break;
                        }
                  }
                  int result = 0;
                  for (int i = 0; i < n; ++i) {
                        for (int j = 0; j < m; ++j) {
                              result += where[i, j] ? 1 : 0;
                        }
                  }
                  return result;
            }

            public static void Main(string[] args) {
                  Console.WriteLine(new RoadTrip().howMany(new string[] {
 ".........R....",
 "..............",
 "...L.....R....",
 "..............",
 "...L.....B...." }));
                  Console.WriteLine(new RoadTrip().howMany(new string[] {
".........R.....R.......",
 ".......................",
 ".........B.............",
 ".......................",
 ".......................",
 "..B......R.....R......B",
 ".......................",
 ".......................",
 ".......................",
 ".........B.....B......."}));
                  Console.WriteLine(new RoadTrip().howMany(new string[] {
 "......................R",
 "R....................R.",
 ".R..................R..",
 "..R................R...",
 "...R..............R....",
 "....R.............R....",
 "...R...............R...",
 "..R.................R..",
 ".R...................R.",
 "R.....................R"}));
                  Console.WriteLine(new RoadTrip().howMany(new string[] {".B.....B"}));
                  Console.WriteLine(new RoadTrip().howMany(new string[] {
 "..............................",
 "..............................",
 "..............................",
 "..............................",
 "..............................",
 "..............................",
 "..............................",
 "..............................",
 "..............................",
 "..............................",
 "..............................",
 "..............................",
 "..............................",
 "..............................",
 "..............................",
 "..............................",
 "..............................",
 "..............................",
 "..............................",
 "..............................",
 "..............................",
 "..............................",
 "..............................",
 "..............................",
 "..............................",
 "..............................",
 "..............................",
 "..............................",
 "..............................",
 ".............................."}));

                  Console.WriteLine("Press any key to continue...");
                  Console.ReadLine();
            }
      }
}