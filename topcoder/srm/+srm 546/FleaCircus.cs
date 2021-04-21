using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TopCoder.Algorithm {
      public class FleaCircus {
            private const long modulo = (long)(1e9 + 9);

            public int countArrangements(string[] afterFourClicks) {
                  string state = string.Empty;
                  for (int i = 0; i < afterFourClicks.Length; ++i) {
                        state = state + afterFourClicks[i];
                  }
                  string[] splitted = state.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                  return (int)countArrangements(Array.ConvertAll(splitted, delegate(string s) {
                        return int.Parse(s);
                  }), splitted.Length);
            }

            /* First of all split permutation into disjoint cycles. In order to solve the problem
             * it is enough to know information about lengthes of the cycles. Because after each
             * permutation each cycle splits into the cycles of the same length we can process
             * the cycles by their length independently. Applying cycle of the length L e.g. S times resulting
             * to gcd(L, S) cycles of the length L / gcd(L, S) */
            private long countArrangements(int[] state, int n) {
                  List<List<int>> cycles = new List<List<int>>();
                  bool[] visited = new bool[n];
                  for (int i = 0; i < n; ++i) {
                        if (!visited[i]) {
                              List<int> cycle = new List<int>();
                              int current = i;
                              do {
                                    visited[current] = true;
                                    cycle.Add(current);
                                    current = state[current];
                              } while (current != i);
                              cycles.Add(cycle);
                        }
                  }
                  int[] count = new int[n + 1];
                  foreach (List<int> cycle in cycles) {
                        ++count[cycle.Count];
                  }
                  List<int>[] groups = Array.ConvertAll(new List<int>[n + 1], delegate(List<int> value) {
                        return new List<int>();
                  });
                  for (int len = 1; len <= n; ++len) {
                        int numOfCycles = gcd(len, 4);
                        groups[len / numOfCycles].Add(numOfCycles);
                  }
                  long result = 1;
                  long[] dp = new long[n + 1];
                  for (int len = 1; len <= n; ++len) {
                        if (count[len] > 0) {
                              dp[0] = 1;
                              for (int cnt = 1; cnt <= count[len]; ++cnt) {
                                    dp[cnt] = 0;
                                    foreach (int g in groups[len]) {
                                          if (g <= cnt) {
                                                /* First of all calculate the number of ways to select g cycles one after another
                                                 * e.g. ABCD, ABDC, ACBD, ACDB, ADBC, ADCB (we should ignore cyclic shifts). Then
                                                 * calculate the number of ways to join the cycles (the positions of the elements of
                                                 * the first cycle should not be changed). */
                                                dp[cnt] = (dp[cnt] + (a(cnt - 1, g - 1) * pow(len, g - 1) % modulo) * dp[cnt - g]) % modulo;
                                          }
                                    }
                              }
                              result = (result * dp[count[len]]) % modulo;
                        }
                  }
                  return result;
            }

            private long pow(int a, int k) {
                  if (k == 0) {
                        return 1;
                  }
                  else if (k % 2 == 0) {
                        return pow(a * a, k >> 1);
                  }
                  else {
                        return a * pow(a, k - 1);
                  }
            }

            private long a(long n, int k) {
                  long result = 1;
                  for (int i = 0; i < k; ++i) {
                        result = (result * (n - i)) % modulo;
                  }
                  return result;
            }

            private int gcd(int a, int b) {
                  while (a != 0 && b != 0) {
                        if (a > b) {
                              a %= b;
                        }
                        else {
                              b %= a;
                        }
                  }
                  return (a + b);
            }

            // BEGIN CUT HERE
            public void run_test(int Case) { if ((Case == -1) || (Case == 0)) test_case_0(); if ((Case == -1) || (Case == 1)) test_case_1(); if ((Case == -1) || (Case == 2)) test_case_2(); if ((Case == -1) || (Case == 3)) test_case_3(); if ((Case == -1) || (Case == 4)) test_case_4(); if ((Case == -1) || (Case == 5)) test_case_5(); }
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
            private void test_case_0() { string[] Arg0 = new string[] { "1 2 0 3" }; int Arg1 = 1; verify_case(0, Arg1, countArrangements(Arg0)); }
            private void test_case_1() { string[] Arg0 = new string[] { "1 2 ", "0 3" }; int Arg1 = 1; verify_case(1, Arg1, countArrangements(Arg0)); }
            private void test_case_2() { string[] Arg0 = new string[] { "0 1 2" }; int Arg1 = 4; verify_case(2, Arg1, countArrangements(Arg0)); }
            private void test_case_3() { string[] Arg0 = new string[] { "0 1 2 3 5 4" }; int Arg1 = 0; verify_case(3, Arg1, countArrangements(Arg0)); }
            private void test_case_4() { string[] Arg0 = new string[] { "3 6 7 9 8 2 1", "0 5 1 0 4" }; int Arg1 = 4; verify_case(4, Arg1, countArrangements(Arg0)); }
            private void test_case_5() { string[] Arg0 = new string[] { "1 0 7 5 6 3 4 2" }; int Arg1 = 48; verify_case(5, Arg1, countArrangements(Arg0)); }

            // END CUT HERE

            // BEGIN CUT HERE
            [STAThread]
            public static void Main(string[] args) {
                  FleaCircus item = new FleaCircus();
                  item.run_test(-1);
                  Console.ReadLine();
            }
            // END CUT HERE
      }
}