using System;
using System.Collections.Generic;

public class WhiteSpaceEditing {
      public int getMinimum(int[] lines) {
            int[,,] dp = new int[lines.Length, lines.Length, lines.Length];
            for (int i = 0; i < lines.Length; ++i) {
                  for (int j = 0; j < lines.Length; ++j) {
                        for (int k = 0; k < lines.Length; ++k) {
                              dp[i, j, k] = int.MaxValue;
                        }
                  }
            }
            int result = int.MaxValue;
            for (int k = 0; k < lines.Length; ++k) {
                  result = Math.Min(result, getMinimum(lines, dp, 0, lines.Length - 1, k) + lines.Length - 1);
            }
            return result;
      }

      private int getMinimum(int[] lines, int[,,] dp, int a, int b, int k) {
            if (dp[a, b, k] == int.MaxValue) {
                  if (a == b) {
                        dp[a, b, k] = lines[k] + Math.Abs(lines[k] - lines[a]);
                  }
                  else {
                        dp[a, b, k] = lines[k];
                        for (int i = a; i <= b; ++i) {
                              dp[a, b, k] += Math.Abs(lines[k] - lines[i]);
                        }
                        for (int c = a; c < b; ++c) {
                              if (c <= k && k <= b) {
                                    for (int x = a; x <= b; ++x) {
                                          dp[a, b, k] = Math.Min(dp[a, b, k], getMinimum(lines, dp, a, c, x) + getMinimum(lines, dp, c, b, k) - lines[c]);
                                    }
                              }
                        }
                  }
            }
            return dp[a, b, k];
      }

      // BEGIN CUT HERE
      public void run_test(int Case) {
            if ((Case == -1) || (Case == 0)) test_case_0();
            if ((Case == -1) || (Case == 1)) test_case_1();
            if ((Case == -1) || (Case == 2)) test_case_2();
            if ((Case == -1) || (Case == 3)) test_case_3();
            if ((Case == -1) || (Case == 4)) test_case_4();
      }
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
      private void test_case_0() { int[] Arg0 = new int[] { 3, 2, 3 }; int Arg1 = 6; verify_case(0, Arg1, getMinimum(Arg0)); }
      private void test_case_1() { int[] Arg0 = new int[] { 0 }; int Arg1 = 0; verify_case(1, Arg1, getMinimum(Arg0)); }
      private void test_case_2() {
            int[] Arg0 = new int[] { 1, 2, 4 }
                  ; int Arg1 = 6; verify_case(2, Arg1, getMinimum(Arg0));
      }
      private void test_case_3() {
            int[] Arg0 = new int[] { 250, 105, 155, 205, 350 }
                  ; int Arg1 = 499; verify_case(3, Arg1, getMinimum(Arg0));
      }
      private void test_case_4() { int[] Arg0 = new int[] { 0, 2, 1 }; int Arg1 = 4; verify_case(4, Arg1, getMinimum(Arg0)); }

      // END CUT HERE

      // BEGIN CUT HERE
      [STAThread]
      public static void Main(string[] args) {
            WhiteSpaceEditing item = new WhiteSpaceEditing();
            item.run_test(-1);
            Console.ReadLine();
      }
      // END CUT HERE
}
