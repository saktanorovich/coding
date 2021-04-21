using System;

public class NumericalPerfectionLevel {
      public int getLevel(long n) {
            int factors = 0;
            for (long i = 2; i * i <= n; ++i) {
                  while (n % i == 0) {
                        n /= i;
                        ++factors;
                  }
            }
            if (n != 1) {
                  ++factors;
            }
            int result = 0;
            while (factors >= 4) {
                  factors /= 4;
                  ++result;
            }
            return result;
      }

      // BEGIN CUT HERE
      public void run_test(int Case) { if ((Case == -1) || (Case == 0)) test_case_0(); if ((Case == -1) || (Case == 1)) test_case_1(); if ((Case == -1) || (Case == 2)) test_case_2(); if ((Case == -1) || (Case == 3)) test_case_3(); }
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
      private void test_case_0() { long Arg0 = 4l; int Arg1 = 0; verify_case(0, Arg1, getLevel(Arg0)); }
      private void test_case_1() { long Arg0 = 144l; int Arg1 = 1; verify_case(1, Arg1, getLevel(Arg0)); }
      private void test_case_2() { long Arg0 = 1152l; int Arg1 = 1; verify_case(2, Arg1, getLevel(Arg0)); }
      private void test_case_3() { long Arg0 = 1679616l; int Arg1 = 2; verify_case(3, Arg1, getLevel(Arg0)); }

      // END CUT HERE

      // BEGIN CUT HERE
      [STAThread]
      public static void Main(string[] args) {
            NumericalPerfectionLevel item = new NumericalPerfectionLevel();
            item.run_test(-1);
            Console.ReadLine();
      }
      // END CUT HERE
}
