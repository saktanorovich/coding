using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class CatchTheMice {
            private const double oo  = 1e+14;
            private const double eps = 1e-14;

            public double largestCage(int[] xp, int[] yp, int[] xv, int[] yv) {
                  double lo = eps, hi = +oo;
                  while (hi - lo > eps) {
                        double lo_third = (2 * lo + hi) / 3;
                        double hi_third = (lo + 2 * hi) / 3;
                        if (Math.Abs(lo - lo_third) < eps)
                              break;
                        if (Math.Abs(hi - hi_third) < eps)
                              break;
                        if (getCage(lo_third, xp, yp, xv, yv) < getCage(hi_third, xp, yp, xv, yv)) {
                              hi = hi_third;
                        }
                        else {
                              lo = lo_third;
                        }
                  }
                  return getCage((lo + hi) / 2, xp, yp, xv, yv);
            }

            private double getCage(double t, int[] xp, int[] yp, int[] xv, int[] yv) {
                  double xmin = double.MaxValue, ymin = double.MaxValue;
                  double xmax = double.MinValue, ymax = double.MinValue;
                  for (int i = 0; i < xp.Length; ++i) {
                        xmin = Math.Min(xmin, xp[i] + xv[i] * t);
                        ymin = Math.Min(ymin, yp[i] + yv[i] * t);
                        xmax = Math.Max(xmax, xp[i] + xv[i] * t);
                        ymax = Math.Max(ymax, yp[i] + yv[i] * t);
                  }
                  return Math.Max(xmax - xmin, ymax - ymin);
            }

            // BEGIN CUT HERE
            public void run_test(int Case) { if ((Case == -1) || (Case == 0)) test_case_0(); if ((Case == -1) || (Case == 1)) test_case_1(); if ((Case == -1) || (Case == 2)) test_case_2(); if ((Case == -1) || (Case == 3)) test_case_3(); if ((Case == -1) || (Case == 4)) test_case_4(); if ((Case == -1) || (Case == 5)) test_case_5(); if ((Case == -1) || (Case == 6)) test_case_6(); if ((Case == -1) || (Case == 7)) test_case_7(); }
            private void verify_case(int Case, double Expected, double Received) {
                  Console.Write("Test Case #" + Case + "...");
                  if (Math.Abs(Expected - Received) <= 1e-9)
                        Console.WriteLine("PASSED");
                  else {
                        Console.WriteLine("FAILED");
                        Console.WriteLine("\tExpected: \"" + Expected + '\"');
                        Console.WriteLine("\tReceived: \"" + Received + '\"');
                  }
            }
            private void test_case_0() { int[] Arg0 = new int[] { 0, 10 }; int[] Arg1 = new int[] { 0, 10 }; int[] Arg2 = new int[] { 10, -10 }; int[] Arg3 = new int[] { 0, 0 }; double Arg4 = 10.0; verify_case(0, Arg4, largestCage(Arg0, Arg1, Arg2, Arg3)); }
            private void test_case_1() { int[] Arg0 = new int[] { 0, 10, 0 }; int[] Arg1 = new int[] { 0, 0, 10 }; int[] Arg2 = new int[] { 1, -6, 4 }; int[] Arg3 = new int[] { 4, 5, -4 }; double Arg4 = 3.0; verify_case(1, Arg4, largestCage(Arg0, Arg1, Arg2, Arg3)); }
            private void test_case_2() { int[] Arg0 = new int[] { 50, 10, 30, 15 }; int[] Arg1 = new int[] { -10, 30, 20, 40 }; int[] Arg2 = new int[] { -5, -10, -15, -5 }; int[] Arg3 = new int[] { 40, -10, -1, -50 }; double Arg4 = 40.526315789473685; verify_case(2, Arg4, largestCage(Arg0, Arg1, Arg2, Arg3)); }
            private void test_case_3() { int[] Arg0 = new int[] { 0, 10, 10, 0 }; int[] Arg1 = new int[] { 0, 0, 10, 10 }; int[] Arg2 = new int[] { 1, 0, -1, 0 }; int[] Arg3 = new int[] { 0, 1, 0, -1 }; double Arg4 = 10.0; verify_case(3, Arg4, largestCage(Arg0, Arg1, Arg2, Arg3)); }
            private void test_case_4() { int[] Arg0 = new int[] { 13, 50, 100, 40, -100 }; int[] Arg1 = new int[] { 20, 20, -150, -40, 63 }; int[] Arg2 = new int[] { 4, 50, 41, -41, -79 }; int[] Arg3 = new int[] { 1, 1, 1, 3, -1 }; double Arg4 = 212.78688524590163; verify_case(4, Arg4, largestCage(Arg0, Arg1, Arg2, Arg3)); }
            private void test_case_5() { int[] Arg0 = new int[] { 0, 10 }; int[] Arg1 = new int[] { 0, 0 }; int[] Arg2 = new int[] { 5, 5 }; int[] Arg3 = new int[] { 3, 3 }; double Arg4 = 10.0; verify_case(5, Arg4, largestCage(Arg0, Arg1, Arg2, Arg3)); }
            private void test_case_6() { int[] Arg0 = new int[] { -49, -463, -212, -204, -557, -67, -374, -335, -590, -4 }; int[] Arg1 = new int[] { 352, 491, 280, 355, 129, 78, 404, 597, 553, 445 }; int[] Arg2 = new int[] { -82, 57, -23, -32, 89, -72, 27, 17, 100, -94 }; int[] Arg3 = new int[] { -9, -58, 9, -14, 56, 75, -32, -98, -81, -43 }; double Arg4 = 25.467532467532468; verify_case(6, Arg4, largestCage(Arg0, Arg1, Arg2, Arg3)); }
            private void test_case_7() {
                  int[] Arg0 = new int[] { 0, 0 }; int[] Arg1 = new int[] { 0, 1 };
                  int[] Arg2 = new int[] { 1000, 0 }; int[] Arg3 = new int[] { 0, 1000 }; double Arg4 = 1.0; verify_case(7, Arg4, largestCage(Arg0, Arg1, Arg2, Arg3));
            }
            // END CUT HERE

            // BEGIN CUT HERE
            [STAThread]
            public static void Main(string[] args) {
                  CatchTheMice item = new CatchTheMice();
                  item.run_test(-1);
                  Console.ReadLine();
            }
            // END CUT HERE
      }
}