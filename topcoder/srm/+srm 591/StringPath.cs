using System;

public class StringPath {
      private static readonly long modulo = 1000000009;
      private static readonly long undefined = -1;

      private long[,,,] dp;
      private int n, m;
      private string a, b;

      public int countBoards(int n, int m, string a, string b) {
            dp = new long[n, m, 1 << m, 1 << m];
            for (int i = 0; i < n; ++i) {
                  for (int j = 0; j < m; ++j) {
                        for (int aset = 0; aset < (1 << m); ++aset) {
                              for (int bset = 0; bset < (1 << m); ++bset) {
                                    dp[i, j, aset, bset] = undefined;
                              }
                        }
                  }
            }
            this.n = n;
            this.m = m;
            this.a = a;
            this.b = b;
            return (int)run(0, 0, 1, 1);
      }

      private long run(int ix, int iy, int acurr, int bcurr) {
            if (ix == n) {
                  /* if we reach the last cell with 1 for both strings then return 1 as a result. */
                  if (BitUtils.Contains(acurr, m - 1) && BitUtils.Contains(bcurr, m - 1)) {
                        return 1;
                  }
                  return 0;
            }
            if (iy == m) {
                  return run(ix + 1, 0, acurr, bcurr);
            }
            if (dp[ix, iy, acurr, bcurr] == undefined) {
                  dp[ix, iy, acurr, bcurr] = 0;
                  for (char c = 'A'; c <= 'Z'; ++c) {
                        int anext = BitUtils.Exclude(acurr, iy);
                        int bnext = BitUtils.Exclude(bcurr, iy);
                        /* check if we can move to the [ix, iy] either from [ix, iy - 1] or [ix - 1, iy] for A string. */
                        if ((iy > 0 && BitUtils.Contains(acurr, iy - 1)) || BitUtils.Contains(acurr, iy)) {
                              if (a[ix + iy].Equals(c)) {
                                    anext = BitUtils.Include(anext, iy);
                              }
                        }
                        /* check if we can move to the [ix, iy] either from [ix, iy - 1] or [ix - 1, iy] for B string. */
                        if ((iy > 0 && BitUtils.Contains(bcurr, iy - 1)) || BitUtils.Contains(bcurr, iy)) {
                              if (b[ix + iy].Equals(c)) {
                                    bnext = BitUtils.Include(bnext, iy);
                              }
                        }
                        dp[ix, iy, acurr, bcurr] += run(ix, iy + 1, anext, bnext);
                  }
                  dp[ix, iy, acurr, bcurr] %= modulo;
            }
            return dp[ix, iy, acurr, bcurr];
      }

      public static class BitUtils {
            #region Public Methods

            public static bool IsPowerOf2(int value) {
                  if (value != 0) {
                        if (value > 0) {
                              return (value & (value - 1)) == 0;
                        }
                        return IsPowerOf2(-value);
                  }
                  return false;
            }

            public static int BitsCount(int set) {
                  int result = 0;
                  for (; set != 0; set = set >> 1) {
                        ++result;
                  }
                  return result;
            }

            public static int Cardinality(int set) {
                  int result = 0;
                  for (; set > 0; set -= (set & (-set))) {
                        ++result;
                  }
                  return result;
            }

            public static bool Contains(int set, int x) {
                  return ((set & MakeSet(x)) == MakeSet(x));
            }

            public static int MakeSet(int x) {
                  return (1 << x);
            }

            public static int Include(int set, int x) {
                  return (set | MakeSet(x));
            }

            public static int Exclude(int set, int x) {
                  if (Contains(set, x)) {
                        return (set ^ MakeSet(x));
                  }
                  return set;
            }

            #endregion
      }

      internal static void Main(string[] args) {
            Console.WriteLine(new StringPath().countBoards(2, 2, "AAA", "ABA")); // 2
            Console.WriteLine(new StringPath().countBoards(2, 2, "ABC", "ADC")); // 2
            Console.WriteLine(new StringPath().countBoards(2, 2, "ABC", "ABD")); // 0
            Console.WriteLine(new StringPath().countBoards(3, 4, "ABCDDE", "ACCBDE")); // 1899302
            Console.WriteLine(new StringPath().countBoards(8, 8, "ZZZZZZZZZZZZZZZ", "ZABCDEFGHIJKLMZ")); // 120390576
            Console.WriteLine(new StringPath().countBoards(8, 8, "MPDRPMKFGOZPILO", "MKDSPMKTQOJOILO")); // 120246903

            Console.WriteLine("Press any key...");
            Console.ReadKey();
      }
}
