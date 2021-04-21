using System;
using System.Collections.Generic;

public class EllysXors {
      public long getXor(long l, long r) {
            return getXor(l - 1) ^ getXor(r);
      }

      private long getXor(long x) {
            if (x % 4 == 0) {
                  return x;
            }
            long y = 4 * (x / 4), res = 0;
            for (long i = y; i <= x; ++i) {
                  res = res ^ i;
            }
            return res;
      }

      // BEGIN CUT HERE
      public void run_test(int Case) { if ((Case == -1) || (Case == 0)) test_case_0(); if ((Case == -1) || (Case == 1)) test_case_1(); if ((Case == -1) || (Case == 2)) test_case_2(); if ((Case == -1) || (Case == 3)) test_case_3(); if ((Case == -1) || (Case == 4)) test_case_4(); }
      private void verify_case(int Case, long Expected, long Received) {
            Console.Write("Test Case #" + Case + "...");
            if (Expected == Received)
                  Console.WriteLine("PASSED");
            else {
                  Console.WriteLine("FAILED");
                  Console.WriteLine("\tExpected: \"" + Expected + '\"');
                  Console.WriteLine("\tReceived: \"" + Received + '\"');
            }
      }
      private void test_case_0() { long Arg0 = 3l; long Arg1 = 10l; long Arg2 = 8l; verify_case(0, Arg2, getXor(Arg0, Arg1)); }
      private void test_case_1() { long Arg0 = 5l; long Arg1 = 5l; long Arg2 = 5l; verify_case(1, Arg2, getXor(Arg0, Arg1)); }
      private void test_case_2() { long Arg0 = 13l; long Arg1 = 42l; long Arg2 = 39l; verify_case(2, Arg2, getXor(Arg0, Arg1)); }
      private void test_case_3() { long Arg0 = 666l; long Arg1 = 1337l; long Arg2 = 0l; verify_case(3, Arg2, getXor(Arg0, Arg1)); }
      private void test_case_4() { long Arg0 = 1234567l; long Arg1 = 89101112l; long Arg2 = 89998783l; verify_case(4, Arg2, getXor(Arg0, Arg1)); }

      // END CUT HERE

      // BEGIN CUT HERE
      [STAThread]
      public static void Main(string[] args) {
            EllysXors item = new EllysXors();
            item.run_test(-1);
            Console.ReadLine();
      }
      // END CUT HERE
}
