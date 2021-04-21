using System;
using System.Collections.Generic;

public class AvoidFour
{
      private const long oo = 1000000000000000000L;
      private const long modulo = 1000000007;

      private static class BitUtils
      {
            public static int cardinality(int set)
            {
                  int result = 0;
                  for (; set > 0; set -= set & (-set))
                        ++result;
                  return result;
            }

            public static bool contains(int set, int x)
            {
                  return ((set & makeSet(x)) == makeSet(x));
            }

            public static int makeSet(int x)
            {
                  return (1 << x);
            }
      }

      private static class MathUtils
      {
            public static long gcd(long a, long b)
            {
                  while (a != 0 && b != 0)
                        if (a > b)
                              a %= b;
                        else
                              b %= a;
                  return (a + b);
            }

            public static long lcm(long a, long b)
            {
                  return a * (b / gcd(a, b));
            }
      }

      private static class MatrixUtils
      {
            public static long[,] sumofPowers(long[,] a, int n, long k)
            {
                  if (k == 0)
                        return new long[n, n];
                  else if (k % 2 == 0)
                  {
                        return mul(sumofPowers(a, n, k >> 1), sum(identity(n), pow(a, n, k >> 1), n), n);
                  }
                  else
                        return sum(sumofPowers(a, n, k - 1), pow(a, n, k), n);
            }

            public static long[,] sum(long[,] a, long[,] b, int n)
            {
                  long[,] result = new long[n, n];
                  for (int i = 0; i < n; ++i)
                        for (int j = 0; j < n; ++j)
                              result[i, j] = (a[i, j] + b[i, j]) % modulo;
                  return result;
            }

            public static long[,] sub(long[,] a, long[,] b, int n)
            {
                  long[,] result = new long[n, n];
                  for (int i = 0; i < n; ++i)
                        for (int j = 0; j < n; ++j)
                        {
                              result[i, j] = (a[i, j] - b[i, j] + modulo) % modulo;
                        }
                  return result;
            }

            public static long[,] pow(long[,] a, int n, long k)
            {
                  if (k == 0)
                        return identity(n);
                  else if (k % 2 == 0)
                        return pow(mul(a, a, n), n, k >> 1);
                  else
                        return mul(a, pow(a, n, k - 1), n);
            }

            public static long[,] mul(long[,] a, long[,] b, int n)
            {
                  long[,] result = new long[n, n];
                  for (int i = 0; i < n; ++i)
                        for (int j = 0; j < n; ++j)
                              for (int k = 0; k < n; ++k)
                                    result[i, j] = (result[i, j] + (a[i, k] * b[k, j]) % modulo) % modulo;
                  return result;
            }

            public static long[] mul(long[,] a, long[] b, int n)
            {
                  long[] result = new long[n];
                  for (int i = 0; i < n; ++i)
                        for (int k = 0; k < n; ++k)
                              result[i] = (result[i] + (a[i, k] * b[k]) % modulo) % modulo;
                  return result;
            }

            public static long[,] identity(int n)
            {
                  long[,] result = new long[n, n];
                  for (int i = 0; i < n; ++i)
                        result[i, i] = 1;
                  return result;
            }
      }

      /**
       * f[n][0] = 9 * f[n - 1][0] + 9 * f[n - 1][1] + 9 * f[n - 1][2] + 9 * f[n - 1][3];
       * f[n][1] = 1 * f[n - 1][0] + 0 * f[n - 1][1] + 0 * f[n - 1][2] + 0 * f[n - 1][3];
       * f[n][2] = 0 * f[n - 1][0] + 1 * f[n - 1][1] + 0 * f[n - 1][2] + 0 * f[n - 1][3];
       * f[n][3] = 0 * f[n - 1][0] + 0 * f[n - 1][1] + 1 * f[n - 1][2] + 0 * f[n - 1][3];
       *
       *                             1   2      n - 1
       * F(n) = A * F(n - 1) = (E + A + A + .. A     ) * F[1].
       */
      public int count(long n)
      {
            long[,] a = new long[4, 4] {
                  {9, 9, 9, 9},
                  {1, 0, 0, 0},
                  {0, 1, 0, 0},
                  {0, 0, 1, 0}};
            long[] f = new long[4] { 8, 1, 0, 0 };

            /**
             * Inlcusion-Exclusion Principle As Is...
             */
            long[,] total = get(a, n); 
            long[] four = new long[9] { 44L, 444L, 4444L, 44444L, 444444L, 4444444L, 44444444L, 444444444L, 4444444444L };
            for (int set = 1; set < (1 << 9); ++set)
            {
                  long lcm = 1;
                  for (int i = 0; i < 9; ++i)
                        if (BitUtils.contains(set, i))
                              if (lcm * (four[i] / MathUtils.gcd(four[i], lcm)) > n)
                              {
                                    lcm = +oo;
                                    break;
                              }
                              else
                                    lcm = MathUtils.lcm(four[i], lcm);
                  if (lcm <= n)
                  {
                        /**
                         * k = n / lcm
                         *           lcm - 1   2 * lcm - 1         k * lcm - 1   lcm - 1        lcm   2 * lcm        (k - 1) * lcm
                         * P(lcm) = A       + A           + ... + A           = A       * (E + A   + A       + . . .A             ).
                         */
                        long[,] partial = MatrixUtils.mul(MatrixUtils.pow(a, 4, lcm - 1), get(MatrixUtils.pow(a, 4, lcm), n / lcm), 4);
                        int sign = 1 - 2 * (BitUtils.cardinality(set) & 1);
                        if (sign > 0)
                              total = MatrixUtils.sum(total, partial, 4);
                        else
                              total = MatrixUtils.sub(total, partial, 4);
                  }
            }

            f = MatrixUtils.mul(total, f, 4);
            long result = 0;
            for (int i = 0; i < 4; ++i)
                  result = (result + f[i]) % modulo;
            return (int)result;
      }

      private long[,] get(long[,] a, long n)
      {
            return MatrixUtils.sum(MatrixUtils.identity(4), MatrixUtils.sumofPowers(a, 4, n - 1), 4);
      }


      // BEGIN CUT HERE
      public void run_test(int Case)
      {
            if ((Case == -1) || (Case == 0)) test_case_0();
            if ((Case == -1) || (Case == 1)) test_case_1();
            if ((Case == -1) || (Case == 2)) test_case_2();
            if ((Case == -1) || (Case == 3)) test_case_3();
            if ((Case == -1) || (Case == 4)) test_case_4();
      }
      private void verify_case(int Case, int Expected, int Received)
      {
            Console.Write("Test Case #" + Case + "...");
            if (Expected == Received)
                  Console.WriteLine("PASSED");
            else
            {
                  Console.WriteLine("FAILED");
                  Console.WriteLine("\tExpected: \"" + Expected + '\"');
                  Console.WriteLine("\tReceived: \"" + Received + '\"');
            }
      }
      private void test_case_0() { long Arg0 = 4L; int Arg1 = 9998; verify_case(0, Arg1, count(Arg0)); }
      private void test_case_1() { long Arg0 = 5L; int Arg1 = 99980; verify_case(1, Arg1, count(Arg0)); }
      private void test_case_2() { long Arg0 = 87L; int Arg1 = 576334228; verify_case(2, Arg1, count(Arg0)); }
      private void test_case_3() { long Arg0 = 88L; int Arg1 = 576334228; verify_case(3, Arg1, count(Arg0)); }
      private void test_case_4() { long Arg0 = 4128L; int Arg1 = 547731225; verify_case(4, Arg1, count(Arg0)); }

      // END CUT HERE


      // BEGIN CUT HERE
      [STAThread]
      public static void Main(string[] args)
      {
            AvoidFour item = new AvoidFour();
            item.run_test(-1);
            Console.Write("Press any key...");
            Console.ReadKey();
      }
      // END CUT HERE
}
