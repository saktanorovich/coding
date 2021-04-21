using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class RedIsGood {
            private double get_prob(int n, int k) {
                  return 1.0 * k / n;
            }

            public double getProfit(int nred, int nblack) {
                  double[,] e = new double[2, nblack + 1];
                  int level = 0;
                  for (int r = 0; r <= nred; ++r) {
                        level = 1 - level;
                        for (int b = 0; b <= nblack; ++b) {
                              double value = 0.0;
                              if (r > 0) {
                                    value += get_prob(r + b, r) * (e[1 - level, b] + 1);
                              }
                              if (b > 0) {
                                    value += get_prob(r + b, b) * (e[level, b - 1] - 1);
                              }
                              e[level, b] = Math.Max(0, value);
                        }
                  }
                  return e[level, nblack];
            }

            // BEGIN CUT HERE
            public void run_test(int Case) { if ((Case == -1) || (Case == 0)) test_case_0(); if ((Case == -1) || (Case == 1)) test_case_1(); if ((Case == -1) || (Case == 2)) test_case_2(); if ((Case == -1) || (Case == 3)) test_case_3(); if ((Case == -1) || (Case == 4)) test_case_4(); if ((Case == -1) || (Case == 5)) test_case_5(); }

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

            private void test_case_0() { int Arg0 = 0; int Arg1 = 7; double Arg2 = 0.0; verify_case(0, Arg2, getProfit(Arg0, Arg1)); }
            private void test_case_1() { int Arg0 = 4; int Arg1 = 0; double Arg2 = 4.0; verify_case(1, Arg2, getProfit(Arg0, Arg1)); }
            private void test_case_2() { int Arg0 = 5; int Arg1 = 1; double Arg2 = 4.166666666666667; verify_case(2, Arg2, getProfit(Arg0, Arg1)); }
            private void test_case_3() { int Arg0 = 2; int Arg1 = 2; double Arg2 = 0.6666666666666666; verify_case(3, Arg2, getProfit(Arg0, Arg1)); }
            private void test_case_4() { int Arg0 = 12; int Arg1 = 4; double Arg2 = 8.324175824175823; verify_case(4, Arg2, getProfit(Arg0, Arg1)); }
            private void test_case_5() { int Arg0 = 11; int Arg1 = 12; double Arg2 = 1.075642825339958; verify_case(5, Arg2, getProfit(Arg0, Arg1)); }
            // END CUT HERE

            // BEGIN CUT HERE
            [STAThread]
            public static void Main(string[] args) {
                  RedIsGood item = new RedIsGood();
                  item.run_test(-1);
                  Console.ReadLine();
            }
            // END CUT HERE
      }
}
