using System;
using System.Collections.Generic;

namespace TopCoder.Algorithms {
      public class NumberPyramids {
            private const int modulo = 1000000009;

            public int count(int baseLength, int top) {
                  if (baseLength <= 20) {
                        int maxValue = 1 << (baseLength - 1);
                        if (top >= maxValue) {
                              return calculate(baseLength, top);
                        }
                  }
                  return 0;
            }

            /* top = c(n-1,0)*x[0] + c(n-1,1)*x[1] + ... + c(n-1,k)*x[k] + ... + c(n-1,n-1)*x[n-1] = 
             *       c(n-1,0)*y[0] + c(n-1,1)*y[1] + ... + c(n-1,k)*y[k] + ... + c(n-1,n-1)*y[n-1] + 2^(n-1),
             * where y[i] = x[i]-1, x[i] ≥ 1. */
            private int calculate(int n, int top) {
                  int[] c = new int[n]; c[0] = 1;
                  for (int i = 1; i < n; ++i) {
                        for (int j = i; j > 0; --j) {
                              c[j] = c[j] + c[j - 1];
                        }
                  }
                  int maxValue = 1 << (n - 1);
                  int[] dp = new int[top - maxValue + 1]; dp[0] = 1;
                  for (int i = 0; i < n; ++i) {
                        for (int j = c[i]; j <= top - maxValue; ++j) {
                              dp[j] = (dp[j] + dp[j - c[i]]) % modulo;
                        }
                  }
                  return dp[top - maxValue];
            }

            // BEGIN CUT HERE
            public void run_test(int Case) { if ((Case == -1) || (Case == 0)) test_case_0(); if ((Case == -1) || (Case == 1)) test_case_1(); if ((Case == -1) || (Case == 2)) test_case_2(); if ((Case == -1) || (Case == 3)) test_case_3(); if ((Case == -1) || (Case == 4)) test_case_4(); }
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
            private void test_case_0() { int Arg0 = 3; int Arg1 = 5; int Arg2 = 2; verify_case(0, Arg2, count(Arg0, Arg1)); }
            private void test_case_1() { int Arg0 = 5; int Arg1 = 16; int Arg2 = 1; verify_case(1, Arg2, count(Arg0, Arg1)); }
            private void test_case_2() { int Arg0 = 4; int Arg1 = 15; int Arg2 = 24; verify_case(2, Arg2, count(Arg0, Arg1)); }
            private void test_case_3() { int Arg0 = 15; int Arg1 = 31556; int Arg2 = 74280915; verify_case(3, Arg2, count(Arg0, Arg1)); }
            private void test_case_4() { int Arg0 = 150; int Arg1 = 500; int Arg2 = 0; verify_case(4, Arg2, count(Arg0, Arg1)); }

            // END CUT HERE

            // BEGIN CUT HERE
            [STAThread]
            public static void Main(string[] args) {
                  NumberPyramids item = new NumberPyramids();
                  item.run_test(-1);
                  Console.ReadLine();
            }
            // END CUT HERE
      }
}