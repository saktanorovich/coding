using System;
using System.Collections;
using System.Collections.Generic;

public class MountainMap {
      private const long modulo = 12345678;

      private static class BitUtils {
            public static int cardinality(int set) {
                  int result = 0;
                  for (; set > 0; set -= (set & (-set))) {
                        ++result;
                  }
                  return result;
            }

            public static bool contains(int set, int x) {
                  return ((set & makeSet(x)) == makeSet(x));
            }

            public static int makeSet(int x) {
                  return (1 << x);
            }

            public static int include(int set, int x) {
                  return (set | makeSet(x));
            }

            public static IEnumerable subSets(int set) {
                  for (int subset = set; subset > 0; subset = (subset - 1) & set) {
                        yield return subset;
                  }
                  yield return 0;
            }
      }

      private class Processor {
            private bool[][] _map;
            private int _nrows, _ncols;
            private long _count;

            public Processor(bool[][] map, int nrows, int ncols) {
                  _map = map;
                  _nrows = nrows;
                  _ncols = ncols;
            }

            public long process() {
                  _count = 0;
                  generate(1, 1, +1);
                  return _count;
            }

            private void generate(int row, int col, int sign) {
                  if (row == _nrows + 1) {
                        _count = (_count + sign * calculate() + modulo) % modulo;
                  }
                  else if (col == _ncols + 1)
                        generate(row + 1, 1, sign);
                  else {
                        generate(row, col + 1, sign);
                        bool found = false;
                        for (int i = -1; i <= +1; ++i)
                              for (int j = -1; j <= +1; ++j)
                                    if (_map[row + i][col + j]) {
                                          found = true;
                                          break;
                                    }
                        if (!found) {
                              _map[row][col] = true;
                              generate(row, col + 1, -sign);
                              _map[row][col] = false;
                        }
                  }
            }

            private long calculate() {
                  int set = 0, level = 0, nlocals = 0;
                  int[,] neighbours = new int[_nrows + 2, _ncols + 2];
                  for (int i = 1; i <= _nrows; ++i)
                        for (int j = 1; j <= _ncols; ++j)
                              if (_map[i][j]) {
                                    set = BitUtils.include(set, nlocals);
                                    for (int di = -1; di <= +1; ++di)
                                          for (int dj = -1; dj <= +1; ++dj)
                                                if (di != 0 || dj != 0) {
                                                      neighbours[i + di, j + dj] = BitUtils.include(neighbours[i + di, j + dj], nlocals);
                                                }
                                    ++nlocals;
                              }
                  int[] possible = new int[set + 1];
                  for (int i = 1; i <= _nrows; ++i)
                        for (int j = 1; j <= _ncols; ++j)
                              if (!_map[i][j]) {
                                    ++possible[neighbours[i, j]];
                              }
                  foreach (int superset in BitUtils.subSets(set))
                        foreach (int subset in BitUtils.subSets(superset))
                              if (subset != superset) {
                                    possible[superset] += possible[subset];
                              }
                  long[,] dp = new long[2, set + 1];
                  dp[level, 0] = 1;
                  for (int h = 0; h < _nrows * _ncols; ++h) {
                        for (int subset = 0; subset <= set; ++subset) {
                              dp[1 - level, subset] = 0;
                        }
                        for (int subset = 0; subset <= set; ++subset) {
                              if (dp[level, subset] > 0) {
                                    int nlocations = possible[subset] - (h - BitUtils.cardinality(subset));
                                    if (nlocations > 0) {
                                          dp[1 - level, subset] = (dp[1 - level, subset] + dp[level, subset] * nlocations) % modulo;
                                    }
                                    for (int x = 0; x < nlocals; ++x) {
                                          if (!BitUtils.contains(subset, x)) {
                                                int ext = BitUtils.include(subset, x);
                                                dp[1 - level, ext] = (dp[1 - level, ext] + dp[level, subset]) % modulo;
                                          }
                                    }
                              }
                        }
                        level = 1 - level;
                  }
                  return dp[level, set];
            }
      }

      public int count(string[] data) {
            int nrows = data.Length, ncols = data[0].Length;
            string[] extdata = extend(data, nrows, ncols);
            bool[][] map = new bool[extdata.Length][];
            for (int i = 0; i < extdata.Length; ++i) {
                  map[i] = Array.ConvertAll<char, bool>(extdata[i].ToCharArray(), delegate(char c) { return (c.Equals('X')); });
            }
            if (isValid(map, nrows, ncols)) {
                  return (int)(new Processor(map, nrows, ncols).process());
            }
            return 0;
      }

      private string[] extend(string[] data, int nrows, int ncols) {
            List<string> map = new List<string>();
            map.Add(string.Empty.PadLeft(ncols + 2, '.'));
            map.AddRange(Array.ConvertAll<string, string>(data, delegate(string s) { return string.Format(".{0}.", s); }));
            map.Add(string.Empty.PadLeft(ncols + 2, '.'));
            return map.ToArray();
      }

      private bool isValid(bool[][] map, int nrows, int ncols) {
            bool found = false;
            for (int i = 1; i <= nrows; ++i)
                  for (int j = 1; j <= ncols; ++j)
                        if (map[i][j]) {
                              found = true;
                              for (int di = -1; di <= +1; ++di)
                                    for (int dj = -1; dj <= +1; ++dj)
                                          if (di != 0 || dj != 0)
                                                if (map[i + di][j + dj]) {
                                                      return false;
                                                }
                        }
            return found;
      }

      public static void Main(string[] args) {
            DateTime _beg = DateTime.Now;
            Console.WriteLine(new MountainMap().count(new string[] { ".X." }));// == 2);
            Console.WriteLine(new MountainMap().count(new string[] { "X.", ".X" }));// == 0);
            Console.WriteLine(new MountainMap().count(new string[] { "...", "..." }));// == 0);
            Console.WriteLine(new MountainMap().count(new string[] { "X.", "..", ".X" }));// == 60);
            Console.WriteLine(new MountainMap().count(new string[] { "X.X", "...", "X.X" }));// == 4800);
            Console.WriteLine(new MountainMap().count(new string[] { "..X.." })); // == 6);
            Console.WriteLine(new MountainMap().count(new string[] { "....X.", "......", ".X...X" }));// == 869490);
            Console.WriteLine(new MountainMap().count(new string[] { "X......", ".......", ".......", "......." }));
            DateTime _end = DateTime.Now;
            Console.WriteLine("Elapsed time: {0}ms", (_end - _beg).TotalMilliseconds);
            Console.ReadKey();
      }
}
