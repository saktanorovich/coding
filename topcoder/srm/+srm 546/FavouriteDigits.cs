using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class FavouriteDigits {
      private long[] p10 = new long[20];
      private long[, , ,] cache = new long[20, 20, 20, 2];

      public long findNext(long n, int digit1, int count1, int digit2, int count2) {
            p10[0] = 1;
            for (int i = 1; i < 20; ++i) {
                  p10[i] = p10[i - 1] * 10;
            }
            for (int len = 0; len < 20; ++len) {
                  for (int c1 = 0; c1 < 20; ++c1) {
                        for (int c2 = 0; c2 < 20; ++c2) {
                              for (int greater = 0; greater < 2; ++greater) {
                                    cache[len, c1, c2, greater] = -1;
                              }
                        }
                  }
            }
            long result = long.MaxValue;
            for (int len = n.ToString().Length; len <= 19; ++len) {
                  result = Math.Min(result, calc(0, toDigitArray(n, len), digit1, count1, digit2, count2, 0));
                  if (result < long.MaxValue) {
                        return result;
                  }
            }
            return result;
      }

      private long calc(int position, int[] n, int digit1, int count1, int digit2, int count2, int greater) {
            if (position < n.Length) {
                  if (cache[n.Length - position, count1, count2, greater] == -1) {
                        long res = long.MaxValue;
                        if (count1 + count2 <= n.Length - position) {
                              int start = 0;
                              if (greater == 0) {
                                    start = n[position];
                                    if (position == 0 && start == 0) {
                                          start = 1;
                                    }
                              }
                              for (int d = start; d <= 9; ++d) {
                                    int cnt1 = count1;
                                    int cnt2 = count2;
                                    if (d == digit1) cnt1 = Math.Max(0, cnt1 - 1);
                                    if (d == digit2) cnt2 = Math.Max(0, cnt2 - 1);
                                    long suffix = calc(position + 1, n, digit1, cnt1, digit2, cnt2, greater > 0 ? greater : (d > n[position] ? 1 : 0));
                                    if (suffix < long.MaxValue) {
                                          res = Math.Min(res, d * p10[n.Length - position - 1] + suffix);
                                    }
                              }
                        }
                        cache[n.Length - position, count1, count2, greater] = res;
                  }
                  return cache[n.Length - position, count1, count2, greater];
            }
            if (count1 + count2 == 0) {
                  return 0;
            }
            return long.MaxValue;
      }

      private int[] toDigitArray(long x, int length) {
            int[] result = new int[length];
            for (int i = 0; i < length; ++i) {
                  result[length - i - 1] = (int)(x % 10);
                  x /= 10;
            }
            return result;
      }
      // BEGIN CUT HERE
      public void run_test(int Case) { if ((Case == -1) || (Case == 0)) test_case_0(); if ((Case == -1) || (Case == 1)) test_case_1();
            if ((Case == -1) || (Case == 2)) test_case_2(); if ((Case == -1) || (Case == 3)) test_case_3(); if ((Case == -1) || (Case == 4)) test_case_4();
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
      private void test_case_0() { long Arg0 = 47l; int Arg1 = 1; int Arg2 = 0; int Arg3 = 2; int Arg4 = 0; long Arg5 = 47l; verify_case(0, Arg5, findNext(Arg0, Arg1, Arg2, Arg3, Arg4)); }
      private void test_case_1() { long Arg0 = 47l; int Arg1 = 5; int Arg2 = 0; int Arg3 = 9; int Arg4 = 1; long Arg5 = 49l; verify_case(1, Arg5, findNext(Arg0, Arg1, Arg2, Arg3, Arg4)); }
      private void test_case_2() { long Arg0 = 47l; int Arg1 = 5; int Arg2 = 0; int Arg3 = 3; int Arg4 = 1; long Arg5 = 53l; verify_case(2, Arg5, findNext(Arg0, Arg1, Arg2, Arg3, Arg4)); }
      private void test_case_3() { long Arg0 = 47l; int Arg1 = 2; int Arg2 = 1; int Arg3 = 0; int Arg4 = 2; long Arg5 = 200l; verify_case(3, Arg5, findNext(Arg0, Arg1, Arg2, Arg3, Arg4)); }
      private void test_case_4() { long Arg0 = 123456789012345l; int Arg1 = 1; int Arg2 = 2; int Arg3 = 2; int Arg4 = 4; long Arg5 = 123456789012422l; verify_case(4, Arg5, findNext(Arg0, Arg1, Arg2, Arg3, Arg4)); }
      private void test_case_5() { long Arg0 = 92l; int Arg1 = 1; int Arg2 = 1; int Arg3 = 0; int Arg4 = 0; long Arg5 = 100l; verify_case(5, Arg5, findNext(Arg0, Arg1, Arg2, Arg3, Arg4)); }
      private void test_case_6() { long Arg0 = 1l; int Arg1 = 0; int Arg2 = 1; int Arg3 = 1; int Arg4 = 0; long Arg5 = 10l; verify_case(6, Arg5, findNext(Arg0, Arg1, Arg2, Arg3, Arg4)); }

      // END CUT HERE

      // BEGIN CUT HERE
      [STAThread]
      public static void Main(string[] args) {
            FavouriteDigits item = new FavouriteDigits();
            item.run_test(-1);
            Console.ReadLine();
      }
      // END CUT HERE
}
