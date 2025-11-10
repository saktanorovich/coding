using System;
using System.Collections.Generic;

public class ElectionFraudDiv1 {
      public int MinimumVoters(int[] percentages) {
            int n = percentages.Length;
            double min = 0, max = 0;
            for (int i = 0; i < n; ++i) {
                  min += Math.Max(0, percentages[i] - 0.5);
                  max += percentages[i] + 0.5;
            }
            if (min <= 100 && max > 100) {
                  Array.Sort(percentages);
                  for (int voters = 1; ; ++voters) {
                        cache.Clear();
                        if (possible(percentages, 0, voters, voters)) {
                              return voters;
                        }
                  }
            }
            return -1;
      }

      private SortedDictionary<long, bool> cache = new SortedDictionary<long, bool>();

      private bool possible(int[] percentages, int ix, int voters, int remaining) {
            long key = ix * 1000000000L + remaining;
            if (cache.ContainsKey(key)) {
                  return cache[key];
            }
            if (remaining == 0) {
                  return (ix == percentages.Length);
            }
            if (ix < percentages.Length) {
                  int x = (int)(1.0 * percentages[ix] * voters / 100);
                  for (int y = x; y >= 0; --y) {
                        int exm = percent(1.0 * y / voters);
                        if (exm == percentages[ix]) {
                              bool res = possible(percentages, ix + 1, voters, remaining - y);
                              if (res) {
                                    return true;
                              }
                        }
                        else if (exm < percentages[ix]) {
                              break;
                        }
                  }
                  for (int y = x + 1; y <= remaining; ++y) {
                        int exm = percent(1.0 * y / voters);
                        if (exm == percentages[ix]) {
                              bool res = possible(percentages, ix + 1, voters, remaining - y);
                              if (res) {
                                    return true;
                              }
                        }
                        else if (exm > percentages[ix]) {
                              break;
                        }
                  }
            }
            cache.Add(key, false);
            return false;
      }

      private int percent(double x) {
            return (int)Math.Floor(x * 100 + 0.5);
      }

      // BEGIN CUT HERE
      public void run_test(int Case) { if ((Case == -1) || (Case == 0)) test_case_0(); if ((Case == -1) || (Case == 1)) test_case_1(); if ((Case == -1) || (Case == 2)) test_case_2();
            if ((Case == -1) || (Case == 3)) test_case_3(); if ((Case == -1) || (Case == 4)) test_case_4();
            if ((Case == -1) || (Case == 5)) test_case_5();
            if ((Case == -1) || (Case == 6)) test_case_6();
            if ((Case == -1) || (Case == 7)) test_case_7();
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
      private void test_case_0() { int[] Arg0 = new int[] { 33, 33, 33 }; int Arg1 = 3; verify_case(0, Arg1, MinimumVoters(Arg0)); }
      private void test_case_1() { int[] Arg0 = new int[] { 29, 29, 43 }; int Arg1 = 7; verify_case(1, Arg1, MinimumVoters(Arg0)); }
      private void test_case_2() { int[] Arg0 = new int[] { 12, 12, 12, 12, 12, 12, 12, 12 }; int Arg1 = -1; verify_case(2, Arg1, MinimumVoters(Arg0)); }
      private void test_case_3() { int[] Arg0 = new int[] { 13, 13, 13, 13, 13, 13, 13, 13 }; int Arg1 = 8; verify_case(3, Arg1, MinimumVoters(Arg0)); }
      private void test_case_4() { int[] Arg0 = new int[] { 0, 1, 100 }; int Arg1 = 200; verify_case(4, Arg1, MinimumVoters(Arg0)); }
      private void test_case_5() { int[] Arg0 = new int[] { 3, 1, 4, 1, 5, 9, 2, 6, 5, 3, 5, 8, 9, 7, 9, 3, 2, 3, 8, 4 }; int Arg1 = 97; verify_case(5, Arg1, MinimumVoters(Arg0)); }
      private void test_case_6() { int[] Arg0 = new int[] { 0, 0, 0, 0, 0, 0, 0, 34, 34, 34 }; int Arg1 = -1; verify_case(6, Arg1, MinimumVoters(Arg0)); }
      private void test_case_7() {
            int[] Arg0 = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 17, 0, 0, 17, 17, 0, 0, 0, 0, 0, 17, 0, 0, 0, 0, 17, 0, 0, 17, 0, 0, 0, 0, 0, 0, 0, 0 };
            int Arg1 = 6; verify_case(7, Arg1, MinimumVoters(Arg0));
      }

      // END CUT HERE

      // BEGIN CUT HERE
      [STAThread]
      public static void Main(string[] args) {
            ElectionFraudDiv1 item = new ElectionFraudDiv1();
            item.run_test(-1);
            Console.ReadLine();
      }
      // END CUT HERE
}
