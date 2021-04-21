using System;
using System.Collections.Generic;

public class EllysRivers {
      public double getMin(int length, int walk, int[] width, int[] speed) {
            int islandsCount = width.Length;
            int[] totalWidth = new int[islandsCount + 1];
            totalWidth[0] = 0;
            for (int i = 1; i <= islandsCount; ++i) {
                  totalWidth[i] = totalWidth[i - 1] + width[i - 1];
            }
            double[,] minimumTime = new double[2, length + 1];
            int ix = 0;
            for (int x = 0; x <= islandsCount; ++x) {
                  int k = length;
                  for (int y = length; y >= 0; --y) {
                        minimumTime[ix, y] = double.MaxValue;
                        if (x + y == 0) {
                              minimumTime[ix, 0] = 0.0;
                        }
                        else {
                              if (x > 0) {
                                    minimumTime[ix, y] = double.MaxValue;
                                    while (true) {
                                          if (k > y) {
                                                k = k - 1;
                                          }
                                          else {
                                                double valuation = minimumTime[1 - ix, k] + distance(totalWidth[x], y, totalWidth[x - 1], k) / speed[x - 1];
                                                if (valuation < minimumTime[ix, y]) {
                                                      minimumTime[ix, y] = valuation;
                                                      k = Math.Max(k - 1, 0);
                                                }
                                                else {
                                                      k = k + 1;
                                                      break;
                                                }
                                          }
                                    }
                                    /*
                                    int lo = 0, hi = y;
                                    while (hi - lo > 12) {
                                          int lo_third = (2 * lo + hi) / 3;
                                          int hi_third = (lo + 2 * hi) / 3;
                                          double lo_value = minimumTime[1 - ix, lo_third] + distance(totalWidth[x], y, totalWidth[x - 1], lo_third) / speed[x - 1];
                                          double hi_value = minimumTime[1 - ix, hi_third] + distance(totalWidth[x], y, totalWidth[x - 1], hi_third) / speed[x - 1];
                                          if (lo_value < hi_value) {
                                                hi = hi_third;
                                          }
                                          else {
                                                lo = lo_third;
                                          }
                                    }
                                    for (int k = lo; k <= hi; ++k) {
                                          minimumTime[ix, y] = Math.Min(minimumTime[ix, y], minimumTime[1 - ix, k] + distance(totalWidth[x], y, totalWidth[x - 1], k) / speed[x - 1]);
                                    }
                                    */
                              }
                        }
                  }
                  for (int y = 1; y <= length; ++y) {
                        minimumTime[ix, y] = Math.Min(minimumTime[ix, y], minimumTime[ix, y - 1] + 1.0 / walk);
                  }
                  ix = 1 - ix;
            }
            return minimumTime[1 - ix, length];
      }

      private double distance(int x1, int y1, int x2, int y2) {
            return Math.Sqrt(1.0 * (x2 - x1) * (x2 - x1) + 1.0 * (y2 - y1) * (y2 - y1));
      }

      // BEGIN CUT HERE
      public void run_test(int Case) { if ((Case == -1) || (Case == 0)) test_case_0(); if ((Case == -1) || (Case == 1)) test_case_1(); if ((Case == -1) || (Case == 2)) test_case_2();
            if ((Case == -1) || (Case == 3)) test_case_3(); if ((Case == -1) || (Case == 4)) test_case_4();
            if ((Case == -1) || (Case == 5)) test_case_5();
            if ((Case == -1) || (Case == 6)) test_case_6();
      }
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
      private void test_case_0() { int Arg0 = 10; int Arg1 = 3; int[] Arg2 = new int[] { 5, 2, 3 }; int[] Arg3 = new int[] { 5, 2, 7 }; double Arg4 = 3.231651964071508; verify_case(0, Arg4, getMin(Arg0, Arg1, Arg2, Arg3)); }
      private void test_case_1() { int Arg0 = 10000; int Arg1 = 211; int[] Arg2 = new int[] { 911 }; int[] Arg3 = new int[] { 207 }; double Arg4 = 48.24623664712219; verify_case(1, Arg4, getMin(Arg0, Arg1, Arg2, Arg3)); }
      private void test_case_2() { int Arg0 = 1337; int Arg1 = 2; int[] Arg2 = new int[] { 100, 200, 300, 400 }; int[] Arg3 = new int[] { 11, 12, 13, 14 }; double Arg4 = 128.57830549575695; verify_case(2, Arg4, getMin(Arg0, Arg1, Arg2, Arg3)); }
      private void test_case_3() { int Arg0 = 77; int Arg1 = 119; int[] Arg2 = new int[] { 11, 12, 13, 14 }; int[] Arg3 = new int[] { 100, 200, 300, 400 }; double Arg4 = 0.3842077071089629; verify_case(3, Arg4, getMin(Arg0, Arg1, Arg2, Arg3)); }
      private void test_case_4() { int Arg0 = 7134; int Arg1 = 1525; int[] Arg2 = new int[] { 11567, 19763, 11026, 10444, 24588, 22263, 17709, 11181, 15292, 28895, 15039, 18744, 19985, 13795, 26697, 18812, 25655, 13620, 28926, 12393 }; int[] Arg3 = new int[] { 1620, 1477, 2837, 2590, 1692, 2270, 1655, 1078, 2683, 1475, 1383, 1153, 1862, 1770, 1671, 2318, 2197, 1768, 1979, 1057 }; double Arg4 = 214.6509731258811; verify_case(4, Arg4, getMin(Arg0, Arg1, Arg2, Arg3)); }
      private void test_case_5() { int Arg0 = 1153; int Arg1 = 510796; int[] Arg2 = new int[] { 620098, 123973, 433301, 979878, 840041, 235274, 318632, 531855, 692279, 170205, 447593, 889343, 227891, 431794, 805857, 372924, 442311, 188735, 641145, 255310, 706431, 940991}; int[] Arg3 = new int[] { 448323, 600002, 124621, 744113, 77937, 518888, 550087, 484118, 313436, 602964, 272807, 505014, 571839, 575328, 372747, 57628, 934073, 150249, 344435, 85525, 921294, 172046 }; double Arg4 = 47.78070167229568; verify_case(5, Arg4, getMin(Arg0, Arg1, Arg2, Arg3)); }
      private void test_case_6() { int Arg0 = 100000; int Arg1 = 1000000; int[] Arg2 = new int[] { 100, 1100, 2100, 3100, 4100, 5100, 6100, 7100, 8100, 9100, 10100, 11100, 12100, 13100, 14100, 15100, 16100, 17100, 18100, 19100, 20100, 21100, 22100, 23100, 24100, 25100, 26100, 27100, 28100, 29100, 30100, 31100, 32100, 33100, 34100, 35100, 36100, 37100, 38100, 39100, 40100, 41100, 42100, 43100, 44100, 45100, 46100, 47100, 48100, 49100};
            int[] Arg3 = new int[] { 10, 110, 210, 310, 410, 510, 610, 710, 810, 910, 1010, 1110, 1210, 1310, 1410, 1510, 1610, 1710, 1810, 1910, 2010, 2110, 2210, 2310, 2410, 2510, 2610, 2710, 2810, 2910, 3010, 3110, 3210, 3310, 3410, 3510, 3610, 3710, 3810, 3910, 4010, 4110, 4210, 4310, 4410, 4510, 4610, 4710, 4810, 4910};
            double Arg4 = 500.09796687260206; verify_case(6, Arg4, getMin(Arg0, Arg1, Arg2, Arg3));
      }

      // END CUT HERE

      // BEGIN CUT HERE
      [STAThread]
      public static void Main(string[] args) {
            EllysRivers item = new EllysRivers();
            item.run_test(-1);
            Console.ReadLine();
      }
      // END CUT HERE
}
