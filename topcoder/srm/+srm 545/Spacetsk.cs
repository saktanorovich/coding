using System;
using System.Collections.Generic;

public class Spacetsk {
      private const long modulo = (long)1e9 + 7;

      public int countsets(int length, int height, int signalsCount) {
            long result = 0;
            if (signalsCount == 1) {
                  result = (length + 1) * (height + 1);
            }
            else {
                  long[,] c = CombinatoricUtils.buildCombinationTable(Math.Max(length, height) + 1, modulo);
                  for (int dx = 1; dx <= length; ++dx) {
                        for (int dy = 1; dy <= height; ++dy) {
                              long count = MathUtils.gcd(dx, dy) + 1;
                              if  (count >= signalsCount) {
                                    result = (result + c[count - 1, signalsCount - 1] * (length - dx + 1)) % modulo;
                              }
                        }
                  }
                  result = (2 * result) % modulo;
                  if (height + 1 >= signalsCount) {
                        for (int x = 0; x <= length; ++x) {
                              result = (result + c[height + 1, signalsCount]) % modulo;
                        }
                  }
            }
            return (int)result;
      }

      private static class CombinatoricUtils {
            public static long[,] buildCombinationTable(int n, long modulo) {
                  long[,] c = new long[n + 1, n + 1];
                  for (int i = 0; i <= n; ++i) {
                        c[i, 0] = 1;
                        for (int j = 1; j <= i; ++j) {
                              c[i, j] = c[i - 1, j] + c[i - 1, j - 1];
                              if (c[i, j] >= modulo) {
                                    c[i, j] -= modulo;
                              }
                        }
                  }
                  return c;
            }
      }

      private static class MathUtils {
            public static long gcd(long a, long b) {
                  while (a != 0 && b != 0) {
                        if (a > b) {
                              a %= b;
                        }
                        else {
                              b %= a;
                        }
                  }
                  return (a + b);
            }

            public static long lcm(long a, long b) {
                  return a * (b / gcd(a, b));
            }
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
      private void test_case_0() { int Arg0 = 1; int Arg1 = 1; int Arg2 = 2; int Arg3 = 4; verify_case(0, Arg3, countsets(Arg0, Arg1, Arg2)); }
      private void test_case_1() { int Arg0 = 1; int Arg1 = 1; int Arg2 = 1; int Arg3 = 4; verify_case(1, Arg3, countsets(Arg0, Arg1, Arg2)); }
      private void test_case_2() { int Arg0 = 2; int Arg1 = 2; int Arg2 = 1; int Arg3 = 9; verify_case(2, Arg3, countsets(Arg0, Arg1, Arg2)); }
      private void test_case_3() { int Arg0 = 2; int Arg1 = 2; int Arg2 = 2; int Arg3 = 23; verify_case(3, Arg3, countsets(Arg0, Arg1, Arg2)); }
      private void test_case_4() { int Arg0 = 5; int Arg1 = 5; int Arg2 = 3; int Arg3 = 202; verify_case(4, Arg3, countsets(Arg0, Arg1, Arg2)); }
      private void test_case_5() { int Arg0 = 561; int Arg1 = 394; int Arg2 = 20; int Arg3 = 786097180; verify_case(5, Arg3, countsets(Arg0, Arg1, Arg2)); }

      // END CUT HERE

      // BEGIN CUT HERE
      [STAThread]
      public static void Main(string[] args) {
            Spacetsk item = new Spacetsk();
            item.run_test(-1);
            Console.ReadLine();
      }
      // END CUT HERE
}
