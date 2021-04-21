using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class KleofasTail {
      public long countGoodSequences(long K, long A, long B) {
            if (K == 0) {
                  return (B - A + 1);
            }
            return countGoodSequences(B, K) - countGoodSequences(A - 1, K);
      }

      private long countGoodSequences(long a, long k) {
            if (k > a) {
                  return 0;
            }
            long result = count(a, k);
            if ((k & 1) == 0) {
                  result += count(a, k + 1);
            }
            return result;
      }

      private long count(long a, long k) {
            if (k <= a) {
                  long result = 1;
                  for (int shift = 1; true; ++shift) {
                        k = k << 1;
                        if (length(k) < length(a)) {
                              result += (1L << shift);
                        }
                        else {
                              if (k <= a) {
                                    long aprefix = a >> shift;
                                    long kprefix = k >> shift;
                                    if (aprefix > kprefix) {
                                          result += (1L << shift);
                                    }
                                    else {
                                          result += (a & total(shift)) + 1;
                                    }
                              }
                              break;
                        }
                  }
                  return result;
            }
            return 0;
      }

      private int length(long a) {
            int result = 0;
            for (; a > 0; a = a >> 1) {
                  ++result;
            }
            return result;
      }

      private long total(int x) {
            return (1L << x) - 1;
      }

      // BEGIN CUT HERE
      public void run_test(int Case) { if ((Case == -1) || (Case == 0)) test_case_0(); if ((Case == -1) || (Case == 1)) test_case_1(); if ((Case == -1) || (Case == 2)) test_case_2();
            if ((Case == -1) || (Case == 3)) test_case_3(); if ((Case == -1) || (Case == 4)) test_case_4();
            if ((Case == -1) || (Case == 5)) test_case_5();
            if ((Case == -1) || (Case == 6)) test_case_6();
      }
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
      private void test_case_0() { long Arg0 = 3l; long Arg1 = 4l; long Arg2 = 8l; long Arg3 = 2l; verify_case(0, Arg3, countGoodSequences(Arg0, Arg1, Arg2)); }
      private void test_case_1() { long Arg0 = 1l; long Arg1 = 23457l; long Arg2 = 123456l; long Arg3 = 100000l; verify_case(1, Arg3, countGoodSequences(Arg0, Arg1, Arg2)); }
      private void test_case_2() { long Arg0 = 1234567890123456l; long Arg1 = 10l; long Arg2 = 1000000l; long Arg3 = 0l; verify_case(2, Arg3, countGoodSequences(Arg0, Arg1, Arg2)); }
      private void test_case_3() { long Arg0 = 0l; long Arg1 = 0l; long Arg2 = 2l; long Arg3 = 3l; verify_case(3, Arg3, countGoodSequences(Arg0, Arg1, Arg2)); }
      private void test_case_4() { long Arg0 = 2l; long Arg1 = 3l; long Arg2 = 3l; long Arg3 = 1l; verify_case(4, Arg3, countGoodSequences(Arg0, Arg1, Arg2)); }
      private void test_case_5() { long Arg0 = 13l; long Arg1 = 12345l; long Arg2 = 67890123l; long Arg3 = 8387584l; verify_case(5, Arg3, countGoodSequences(Arg0, Arg1, Arg2)); }
      private void test_case_6() { long Arg0 = 1l; long Arg1 = 0l; long Arg2 = 1000000000000000000L; long Arg3 = 1000000000000000000L; verify_case(6, Arg3, countGoodSequences(Arg0, Arg1, Arg2)); }

      // END CUT HERE

      // BEGIN CUT HERE
      [STAThread]
      public static void Main(string[] args) {
            KleofasTail item = new KleofasTail();
            item.run_test(6);
            Console.ReadLine();
      }
      // END CUT HERE
}
