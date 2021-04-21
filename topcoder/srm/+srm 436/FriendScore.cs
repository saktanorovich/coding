using System;

namespace TopCoder.Algorithm {
      public class FriendScore {
            public int highestScore(string[] friends) {
                  int result = 0;
                  for (int i = 0; i < friends.Length; ++i) {
                        int count = 0;
                        for (int j = 0; j < friends.Length; ++j) {
                              if (j != i) {
                                    if (friends[j][i] == 'Y') {
                                          ++count;
                                    }
                                    else {
                                          for (int k = 0; k < friends.Length; ++k) {
                                                if (k != j && k != i) {
                                                      if (friends[k][i] == 'Y' && friends[k][j] == 'Y') {
                                                            ++count;
                                                            break;
                                                      }
                                                }
                                          }
                                    }
                              }
                        }
                        result = Math.Max(result, count);
                  }
                  return result;
            }

            // BEGIN CUT HERE
            public void run_test(int Case) { if ((Case == -1) || (Case == 0)) test_case_0(); if ((Case == -1) || (Case == 1)) test_case_1(); if ((Case == -1) || (Case == 2)) test_case_2(); if ((Case == -1) || (Case == 3)) test_case_3(); if ((Case == -1) || (Case == 4)) test_case_4(); }
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
            private void test_case_0() {
                  string[] Arg0 = new string[]{"NNN",
 "NNN",
 "NNN"}; int Arg1 = 0; verify_case(0, Arg1, highestScore(Arg0));
            }
            private void test_case_1() {
                  string[] Arg0 = new string[]{"NYY",
 "YNY",
 "YYN"}; int Arg1 = 2; verify_case(1, Arg1, highestScore(Arg0));
            }
            private void test_case_2() {
                  string[] Arg0 = new string[]{"NYNNN",
 "YNYNN", 
 "NYNYN", 
 "NNYNY", 
 "NNNYN"}; int Arg1 = 4; verify_case(2, Arg1, highestScore(Arg0));
            }
            private void test_case_3() {
                  string[] Arg0 = new string[]{"NNNNYNNNNN",
 "NNNNYNYYNN",
 "NNNYYYNNNN",
 "NNYNNNNNNN",
 "YYYNNNNNNY",
 "NNYNNNNNYN",
 "NYNNNNNYNN",
 "NYNNNNYNNN",
 "NNNNNYNNNN",
 "NNNNYNNNNN"}; int Arg1 = 8; verify_case(3, Arg1, highestScore(Arg0));
            }
            private void test_case_4() {
                  string[] Arg0 = new string[]{"NNNNNNNNNNNNNNY",
 "NNNNNNNNNNNNNNN",
 "NNNNNNNYNNNNNNN",
 "NNNNNNNYNNNNNNY",
 "NNNNNNNNNNNNNNY",
 "NNNNNNNNYNNNNNN",
 "NNNNNNNNNNNNNNN",
 "NNYYNNNNNNNNNNN",
 "NNNNNYNNNNNYNNN",
 "NNNNNNNNNNNNNNY",
 "NNNNNNNNNNNNNNN",
 "NNNNNNNNYNNNNNN",
 "NNNNNNNNNNNNNNN",
 "NNNNNNNNNNNNNNN",
 "YNNYYNNNNYNNNNN"}; int Arg1 = 6; verify_case(4, Arg1, highestScore(Arg0));
            }

            // END CUT HERE

            // BEGIN CUT HERE
            [STAThread]
            public static void Main(string[] args) {
                  FriendScore item = new FriendScore();
                  item.run_test(-1);
                  Console.ReadLine();
            }
            // END CUT HERE
      }
}