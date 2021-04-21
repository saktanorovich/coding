using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TopCoder.Algorithm {
      public class ReversalChain {
            private static readonly int oo = +1000000;
            private SortedDictionary<string, int> memo;

            public int minReversal(string init, string goal) {
                  memo = new SortedDictionary<string, int>();
                  int result = run(init, goal);
                  if (result < +oo) {
                        return result;
                  }
                  return -1;
            }

            private int run(string init, string goal) {
                  int result;
                  if (!memo.TryGetValue(init + '#' + goal, out result)) {
                        result = +oo;
                        memo[init + '#' + goal] = result;
                        if (init == goal) {
                              result = 0;
                        }
                        else if (init.Length > 1) {
                              for (int i = 0; i < init.Length; ++i) {
                                    for (int j = init.Length - 1; j > i; --j) {
                                          result = Math.Min(result, run(reverse(init, i, j), goal.Substring(i, j - i + 1)) + 1);
                                          if (init[j] != goal[j]) {
                                                break;
                                          }
                                    }
                                    if (init[i] != goal[i]) {
                                          break;
                                    }
                              }
                        }
                        memo[init + '#' + goal] = result;
                  }
                  return result;
            }

            private string reverse(string s, int from, int to) {
                  string result = string.Empty;
                  for (int i = to; i >= from; --i) {
                        result += s[i];
                  }
                  return result;
            }

            public static void Main() {
                  Console.WriteLine(new ReversalChain().minReversal("1100", "0110"));
                  Console.WriteLine(new ReversalChain().minReversal("111000", "101010"));
                  Console.WriteLine(new ReversalChain().minReversal("0", "1"));
                  Console.WriteLine(new ReversalChain().minReversal("10101", "10101"));
                  Console.WriteLine(new ReversalChain().minReversal("111000111000", "001100110011"));
                  Console.WriteLine(new ReversalChain().minReversal(
                        "11111111111111111100000000000000001111111111111111",
                        "11111111110000000011111111111111110000000011111111"));

                  Console.WriteLine("\nPress any key to continue...");
                  Console.ReadKey();
            }
      }
}