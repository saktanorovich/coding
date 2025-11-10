using System;
using System.Collections.Generic;

public class SplittingFoxes {
      private static readonly long modulo = 1000000007;

      private static class MatrixUtils {
            public static long[][] pow(long[][] a, int n, long k) {
                  if (k == 0) {
                        return identity(n);
                  }
                  else if (k % 2 == 0) {
                        return pow(mul(a, a, n), n, k >> 1);
                  }
                  else {
                        return mul(a, pow(a, n, k - 1), n);
                  }
            }

            public static long[][] mul(long[][] a, long[][] b, int n) {
                  long[][] result = new long[n][];
                  for (int i = 0; i < n; ++i) {
                        result[i] = new long[n];
                  }
                  for (int i = 0; i < n; ++i) {
                        for (int j = 0; j < n; ++j) {
                              for (int k = 0; k < n; ++k) {
                                    result[i][j] = (result[i][j] + (a[i][k] * b[k][j]) % modulo) % modulo;
                              }
                        }
                  }
                  return result;
            }

            public static long[] mul(long[][] a, long[] b, int n) {
                  long[] result = new long[n];
                  for (int i = 0; i < n; ++i) {
                        for (int k = 0; k < n; ++k) {
                              result[i] = (result[i] + (a[i][k] * b[k]) % modulo) % modulo;
                        }
                  }
                  return result;
            }

            public static long[][] identity(int n) {
                  long[][] result = new long[n][];
                  for (int i = 0; i < n; ++i) {
                        result[i] = new long[n];
                        result[i][i] = 1;
                  }
                  return result;
            }
      }

      /**
       * f[0][i] = {S * f[0][i - 1]  + S * y[0][i - 1]} + {R * f[1][i - 1]}                   + {0 * f[2][i - 1]}                   + {L * f[3][i - 1]},                   f[0][0] = 0,
       * f[1][i] = {L * f[0][i - 1]}                    + {S * f[1][i - 1] + S * x[1][i - 1]} + {R * f[2][i - 1]}                   + {0 * f[3][i - 1]},                   f[1][0] = 0,
       * f[2][i] = {0 * f[0][i - 1]}                    + {L * f[1][i - 1]}                   + {S * f[2][i - 1] - S * y[2][i - 1]} + {R * f[3][i - 1]},                   f[2][0] = 0,
       * f[3][i] = {R * f[0][i - 1]}                    + {0 * f[1][i - 1]}                   + {L * f[2][i - 1]}                   + {S * f[3][i - 1] - S * x[3][i - 1]}, f[3][0] = 0,

       * x[0][i] = {S * x[0][i - 1] + S * t[0][i - 1]} + {R * x[1][i - 1]} + {0 * x[2][i - 1]}                   + {L * x[3][i - 1]}, x[0][0] = 0,
       * x[1][i] = {L * x[0][i - 1]}                   + {S * x[1][i - 1]} + {R * x[2][i - 1]}                   + {0 * x[3][i - 1]}, x[0][1] = 0,
       * x[2][i] = {0 * x[0][i - 1]}                   + {L * x[1][i - 1]} + {S * x[2][i - 1] - S * t[2][i - 1]} + {R * x[3][i - 1]}, x[0][2] = 0,
       * x[3][i] = {R * x[0][i - 1]}                   + {0 * x[1][i - 1]} + {L * x[2][i - 1]}                   + {S * x[3][i - 1]}, x[0][3] = 0,

       * y[0][i] = {S * y[0][i - 1]} + {R * y[1][i - 1]}                   + {0 * y[2][i - 1]} + {L * y[3][i - 1]},                   y[0][0] = 0,
       * y[1][i] = {L * y[0][i - 1]} + {S * y[1][i - 1] + S * t[1][i - 1]} + {R * y[2][i - 1]} + {0 * y[3][i - 1]},                   y[0][1] = 0,
       * y[2][i] = {0 * y[0][i - 1]} + {L * y[1][i - 1]}                   + {S * y[2][i - 1]} + {R * y[3][i - 1]},                   y[0][2] = 0,
       * y[3][i] = {R * y[0][i - 1]} + {0 * y[1][i - 1]}                   + {L * y[2][i - 1]} + {S * y[3][i - 1] - S * t[3][i - 1]}, y[0][3] = 0,

       * t[0][i] = {S * t[0][i - 1]} + {R * t[1][i - 1]} + {0 * t[2][i - 1]} + {L * t[3][i - 1]}, t[0][0] = 1,
       * t[1][i] = {L * t[0][i - 1]} + {S * t[1][i - 1]} + {R * t[2][i - 1]} + {0 * t[3][i - 1]}, t[0][1] = 0,
       * t[2][i] = {0 * t[0][i - 1]} + {L * t[1][i - 1]} + {S * t[2][i - 1]} + {R * t[3][i - 1]}, t[0][2] = 0,
       * t[3][i] = {R * t[0][i - 1]} + {0 * t[1][i - 1]} + {L * t[2][i - 1]} + {S * t[3][i - 1]}, t[0][3] = 0,
       */
      public int sum(long n, int S, int L, int R) {
            long[][] a = new long[16][];
            a[00] = new long[16] { S, R, 0, L, 0, 0, 0, 0, S, 0, 0, 0, 0, 0, 0, 0 };
            a[01] = new long[16] { L, S, R, 0, 0, S, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            a[02] = new long[16] { 0, L, S, R, 0, 0, 0, 0, 0, 0, -S, 0, 0, 0, 0, 0 };
            a[03] = new long[16] { R, 0, L, S, 0, 0, 0, -S, 0, 0, 0, 0, 0, 0, 0, 0 };

            a[04] = new long[16] { 0, 0, 0, 0, S, R, 0, L, 0, 0, 0, 0, S, 0, 0, 0 };
            a[05] = new long[16] { 0, 0, 0, 0, L, S, R, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            a[06] = new long[16] { 0, 0, 0, 0, 0, L, S, R, 0, 0, 0, 0, 0, 0, -S, 0 };
            a[07] = new long[16] { 0, 0, 0, 0, R, 0, L, S, 0, 0, 0, 0, 0, 0, 0, 0 };
            
            a[08] = new long[16] { 0, 0, 0, 0, 0, 0, 0, 0, S, R, 0, L, 0, 0, 0, 0 };
            a[09] = new long[16] { 0, 0, 0, 0, 0, 0, 0, 0, L, S, R, 0, 0, S, 0, 0 };
            a[10] = new long[16] { 0, 0, 0, 0, 0, 0, 0, 0, 0, L, S, R, 0, 0, 0, 0 };
            a[11] = new long[16] { 0, 0, 0, 0, 0, 0, 0, 0, R, 0, L, S, 0, 0, 0, -S };

            a[12] = new long[16] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, S, R, 0, L };
            a[13] = new long[16] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, L, S, R, 0 };
            a[14] = new long[16] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, L, S, R };
            a[15] = new long[16] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, R, 0, L, S };
            for (int i = 0; i < 16; ++i) {
                  for (int j = 0; j < 16; ++j) {
                        a[i][j] = (a[i][j] + modulo) % modulo;
                  }
            }
            a = MatrixUtils.pow(a, 16, n);
            long[] b = MatrixUtils.mul(a, new long[16] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0 }, 16);
            long result = 0;
            for (int i = 0; i < 4; ++i) {
                  result = (result + b[i]) % modulo;
            }
            return (int)(result);
      }

      // BEGIN CUT HERE
      public void run_test(int Case) { if ((Case == -1) || (Case == 0)) test_case_0(); if ((Case == -1) || (Case == 1)) test_case_1(); if ((Case == -1) || (Case == 2)) test_case_2(); if ((Case == -1) || (Case == 3)) test_case_3(); if ((Case == -1) || (Case == 4)) test_case_4(); if ((Case == -1) || (Case == 5)) test_case_5(); }
      private void verify_case(int Case, int Expected, int Received) {
            Console.Write("Test Case #" + Case + "...");
            if (Expected == Received)
                  Console.WriteLine("PASSED");
            else {
                  Console.WriteLine("FAILED");
                  Console.WriteLine("\tExpected: \"" + Expected + '\"');
                  Console.WriteLine("\tReceived: \"" + Received + '\"');
            }
      }
      private void test_case_0() { long Arg0 = 58l; int Arg1 = 2; int Arg2 = 0; int Arg3 = 0; int Arg4 = 0; verify_case(0, Arg4, sum(Arg0, Arg1, Arg2, Arg3)); }
      private void test_case_1() { long Arg0 = 3l; int Arg1 = 1; int Arg2 = 1; int Arg3 = 0; int Arg4 = 1; verify_case(1, Arg4, sum(Arg0, Arg1, Arg2, Arg3)); }
      private void test_case_2() { long Arg0 = 5l; int Arg1 = 1; int Arg2 = 3; int Arg3 = 2; int Arg4 = 34; verify_case(2, Arg4, sum(Arg0, Arg1, Arg2, Arg3)); }
      private void test_case_3() { long Arg0 = 5l; int Arg1 = 1; int Arg2 = 2; int Arg3 = 3; int Arg4 = 999999973; verify_case(3, Arg4, sum(Arg0, Arg1, Arg2, Arg3)); }
      private void test_case_4() { long Arg0 = 123456789l; int Arg1 = 987654321; int Arg2 = 544; int Arg3 = 544; int Arg4 = 0; verify_case(4, Arg4, sum(Arg0, Arg1, Arg2, Arg3)); }
      private void test_case_5() { long Arg0 = 65536l; int Arg1 = 1024; int Arg2 = 512; int Arg3 = 4096; int Arg4 = 371473914; verify_case(5, Arg4, sum(Arg0, Arg1, Arg2, Arg3)); }

      // END CUT HERE

      // BEGIN CUT HERE
      [STAThread]
      public static void Main(string[] args) {
            SplittingFoxes item = new SplittingFoxes();
            item.run_test(-1);
            Console.ReadLine();
      }
      // END CUT HERE
}
