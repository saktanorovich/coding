using System;
using System.Collections.Generic;

public class TheFansAndMeetingsDivOne {
      private const int total = 1600;

      public double find(int[] jmin, int[] jmax, int[] bmin, int[] bmax, int k) {
            double[] jp = get(jmin.Length, jmin, jmax, k);
            double[] bp = get(bmin.Length, bmin, bmax, k);
            double result = 0;
            for (int i = 0; i <= total; ++i) {
                  result += jp[i] * bp[i];
            }
            return result;
      }

      private double[] get(int n, int[] min, int[] max, int k) {
            /* use accumulation technique... */
            double[,] dp = new double[k + 1, total + 1]; dp[0, 0] = 1.0;
            for (int i = 0; i < n; ++i) {
                  double prob = 1.0 / (max[i] - min[i] + 1);
                  for (int j = k - 1; j >= 0; --j) {
                        for (int visited = min[i]; visited <= max[i]; ++visited) {
                              for (int tot = visited; tot <= total; ++tot) {
                                    dp[j + 1, tot] += dp[j, tot - visited] * prob;
                              }
                        }
                  }
            }
            double ksetProb = p(n, k);
            double[] result = new double[total + 1];
            for (int i = 0; i <= total; ++i) {
                  result[i] = dp[k, i] * ksetProb;
            }
            return result;
      }

      /* p(n, k) = 1 / c(n, k) = k! / (n * (n - 1) * ... * (n - k + 1)) */
      private double p(int n, int k) {
            double result = 1.0;
            for (int i = 1; i <= k; ++i) {
                  result *= i;
                  result /= (n - i + 1);
            }
            return result;
      }

      // BEGIN CUT HERE
      public void run_test(int Case) { if ((Case == -1) || (Case == 0)) test_case_0(); if ((Case == -1) || (Case == 1)) test_case_1(); if ((Case == -1) || (Case == 2)) test_case_2(); if ((Case == -1) || (Case == 3)) test_case_3(); }
      private void verify_case(int Case, double Expected, double Received) {
            Console.Write("Test Case #" + Case + "...");
            if (Expected == Received)
                  Console.WriteLine("PASSED");
            else {
                  Console.WriteLine("FAILED");
                  Console.WriteLine("\tExpected: \"" + Expected + '\"');
                  Console.WriteLine("\tReceived: \"" + Received + '\"');
            }
      }
      private void test_case_0() { int[] Arg0 = new int[] { 1 }; int[] Arg1 = new int[] { 9 }; int[] Arg2 = new int[] { 5 }; int[] Arg3 = new int[] { 5 }; int Arg4 = 1; double Arg5 = 0.1111111111111111; verify_case(0, Arg5, find(Arg0, Arg1, Arg2, Arg3, Arg4)); }
      private void test_case_1() { int[] Arg0 = new int[] { 5, 2, 5, 1, 1, 2, 4, 1 }; int[] Arg1 = new int[] { 7, 6, 7, 3, 4, 3, 5, 1 }; int[] Arg2 = new int[] { 8, 9, 7, 11, 12, 7, 8, 40 }; int[] Arg3 = new int[] { 9, 10, 9, 33, 14, 7, 11, 40 }; int Arg4 = 2; double Arg5 = 4.724111866969009E-5; verify_case(1, Arg5, find(Arg0, Arg1, Arg2, Arg3, Arg4)); }
      private void test_case_2() { int[] Arg0 = new int[] { 4, 7, 4 }; int[] Arg1 = new int[] { 7, 7, 7 }; int[] Arg2 = new int[] { 40, 40, 40 }; int[] Arg3 = new int[] { 40, 40, 40 }; int Arg4 = 1; double Arg5 = 0.0; verify_case(2, Arg5, find(Arg0, Arg1, Arg2, Arg3, Arg4)); }
      private void test_case_3() { int[] Arg0 = new int[] { 3, 6, 2, 1, 1, 10, 3 }; int[] Arg1 = new int[] { 6, 9, 5, 6, 5, 10, 9 }; int[] Arg2 = new int[] { 1, 1, 1, 1, 8, 3, 1 }; int[] Arg3 = new int[] { 3, 9, 7, 3, 10, 6, 5 }; int Arg4 = 4; double Arg5 = 0.047082056525158976; verify_case(3, Arg5, find(Arg0, Arg1, Arg2, Arg3, Arg4)); }

      // END CUT HERE


      // BEGIN CUT HERE
      [STAThread]
      public static void Main(string[] args) {
            TheFansAndMeetingsDivOne item = new TheFansAndMeetingsDivOne();
            item.run_test(-1);
            Console.ReadLine();
      }
      // END CUT HERE
}
