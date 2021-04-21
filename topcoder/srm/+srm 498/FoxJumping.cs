using System;

public class FoxJumping {
      private const long modulo = 10007;

      public int getCount(int xsize, int ysize, int xlen, int ylen, int njumps, int[] bad) {
            long[,] c = new long[njumps + 1, njumps + 1];
            c[0, 0] = 1;
            for (int i = 1; i <= njumps; ++i) {
                  c[i, 0] = 1;
                  for (int j = 1; j <= njumps; ++j) {
                        c[i, j] = (c[i - 1, j] + c[i - 1, j - 1]) % modulo;
                  }
            }
            int[] badMoves = new int[bad.Length + 1];
            for (int i = 0; i < bad.Length; ++i) {
                  badMoves[i] = bad[i] / 10;
            }
            int total = Math.Min(xsize, ysize) / 10;
            long[,] bd = new long[total + 1, njumps + 1];
            bd[0, 0] = 1;
            for (int k = 1; k <= njumps; ++k) {
                  for (int d = 0; d <= total; ++d) {
                        for (int i = 0; i < badMoves.Length; ++i) {
                              if (d - badMoves[i] >= 0) {
                                    bd[d, k] = (bd[d, k] + bd[d - badMoves[i], k - 1]) % modulo;
                              }
                        }
                  }
            }
            long[,] xd = get(njumps, xsize, xlen);
            long[,] yd = get(njumps, ysize, ylen);
            long result = (xd[xsize, njumps] * yd[ysize, njumps]) % modulo;
            for (int k = 1, sign = -1; k <= njumps; ++k, sign = -sign) {
                  for (int d = 0; d <= total; ++d) {
                        long bad_ = (bd[d, k] * c[njumps, k]) % modulo;
                        long rem_ = (xd[xsize - d * 10, njumps - k] * yd[ysize - d * 10, njumps - k]) % modulo;

                        result = (result + sign * ((bad_ * rem_) % modulo) + modulo) % modulo;
                  }
            }
            return (int)result;
      }

      private long[,] get(int njumps, int xsize, int xlen) {
            long[,] f = new long[xsize + 1, njumps + 1];
            f[0, 0] = 1;
            for (int k = 1; k <= njumps; ++k) {
                  for (int i = 0; i <= xsize; ++i) {
                        f[i, k] = f[i, k - 1];
                        if (i > 0) {
                              f[i, k] = (f[i, k] + f[i - 1, k]) % modulo;
                        }
                        if (i - xlen - 1 >= 0) {
                              f[i, k] = (f[i, k] - f[i - xlen - 1, k - 1] + modulo) % modulo;
                        }
                  }
            }
            return f;
      }

      static void Main(string[] args) {
            Console.WriteLine(new FoxJumping().getCount(2, 2, 1, 1, 2, new int[] { }));        // 1
            Console.WriteLine(new FoxJumping().getCount(2, 2, 1, 1, 3, new int[] { }));        // 6
            Console.WriteLine(new FoxJumping().getCount(10, 10, 10, 10, 1, new int[] { }));    // 1
            Console.WriteLine(new FoxJumping().getCount(10, 10, 10, 10, 1, new int[] { 10 })); // 0
            Console.WriteLine(new FoxJumping().getCount(11, 11, 11, 11, 2, new int[] { 10 })); // 140
            Console.WriteLine(new FoxJumping().getCount(123, 456, 70, 80, 90, new int[] { 30, 40, 20, 10, 50 })); // 6732

            Console.ReadLine();
      }
}
