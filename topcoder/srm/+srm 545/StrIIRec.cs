using System;
using System.Collections.Generic;

public class StrIIRec {
      public string recovstr(int n, int minInv, string minStr) {
            bool[] used = new bool[n];
            foreach (char c in minStr) {
                  used[c - 'a'] = true;
            }
            for (int i = 0; i < n; ++i) {
                  if (!used[i]) {
                        minStr += (char)('a' + i);
                  }
            }
            return new string(build(minStr.ToCharArray(), n, minInv));
      }

      private char[] build(char[] current, int n, int required) {
            if (getInversionsCount(current) < required) {
                  for (int i = n - 1; i >= 0; --i) {
                        char[] prefix = new char[i];
                        char[] suffix = new char[n - i];
                        Array.Copy(current, 0, prefix, 0, i);
                        Array.Copy(current, i, suffix, 0, n - i);
                        int prefixCount = getInversionsCount(prefix);
                        for (int p = 0; p < prefix.Length; ++p) {
                              for (int s = 0; s < suffix.Length; ++s) {
                                    if (prefix[p] > suffix[s]) {
                                          ++prefixCount;
                                    }
                              }
                        }
                        int suffixCount = (n - i) * (n - i - 1) / 2;
                        if (prefixCount + suffixCount >= required) {
                              if (i > 0) {
                                    return (new string(prefix) +
                                                new string(build(suffix, n - i, required - prefixCount))).ToCharArray();
                              }
                              else {
                                    Array.Sort(current, 1, n - 1);
                                    for (int j = 1; j < n; ++j) {
                                          if (current[j] > current[0]) {
                                                char x = current[0];
                                                current[0] = current[j];
                                                current[j] = x;
                                                return build(current, n, required);
                                          }
                                    }
                              }
                        }
                  }
            }
            return current;
      }

      private int getInversionsCount(char[] s) {
            int result = 0;
            for (int i = 0; i < s.Length; ++i) {
                  for (int j = i + 1; j < s.Length; ++j) {
                        if (s[i] > s[j]) {
                              ++result;
                        }
                  }
            }
            return result;
      }

      // BEGIN CUT HERE
      public void run_test(int Case) { if ((Case == -1) || (Case == 0)) test_case_0(); if ((Case == -1) || (Case == 1)) test_case_1(); if ((Case == -1) || (Case == 2)) test_case_2(); if ((Case == -1) || (Case == 3)) test_case_3(); if ((Case == -1) || (Case == 4)) test_case_4(); }
      private void verify_case(int Case, string Expected, string Received) {
            Console.Write("Test Case #" + Case + "...");
            if (Expected == Received)
                  Console.WriteLine("PASSED");
            else {
                  Console.WriteLine("FAILED");
                  Console.WriteLine("\tExpected: \"" + Expected + '\"');
                  Console.WriteLine("\tReceived: \"" + Received + '\"');
            }
      }
      private void test_case_0() { int Arg0 = 2; int Arg1 = 1; string Arg2 = "ab"; string Arg3 = "ba"; verify_case(0, Arg3, recovstr(Arg0, Arg1, Arg2)); }
      private void test_case_1() { int Arg0 = 9; int Arg1 = 1; string Arg2 = "efcdgab"; string Arg3 = "efcdgabhi"; verify_case(1, Arg3, recovstr(Arg0, Arg1, Arg2)); }
      private void test_case_2() { int Arg0 = 11; int Arg1 = 55; string Arg2 = "debgikjfc"; string Arg3 = "kjihgfedcba"; verify_case(2, Arg3, recovstr(Arg0, Arg1, Arg2)); }
      private void test_case_3() { int Arg0 = 15; int Arg1 = 0; string Arg2 = "e"; string Arg3 = "eabcdfghijklmno"; verify_case(3, Arg3, recovstr(Arg0, Arg1, Arg2)); }
      private void test_case_4() { int Arg0 = 9; int Arg1 = 20; string Arg2 = "fcdebiha"; string Arg3 = "fcdehigba"; verify_case(4, Arg3, recovstr(Arg0, Arg1, Arg2)); }

      // END CUT HERE

      // BEGIN CUT HERE
      [STAThread]
      public static void Main(string[] args) {
            StrIIRec item = new StrIIRec();
            item.run_test(-1);
            Console.ReadLine();
      }
      // END CUT HERE
}
