using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class CheatCode {
            public int[] matches(string keyPresses, string[] codes) {
                  List<int> result = new List<int>();
                  for (int i = 0; i < codes.Length; ++i) {
                        if (matches(keyPresses, codes[i])) {
                              result.Add(i);
                        }
                  }
                  return result.ToArray();
            }

            private bool matches(string keyPresses, string code) {
                  if (!string.IsNullOrEmpty(code)) {
                        long[,] automaton = buildNFA(code, code.Length);
                        long state = 1;
                        foreach (char key in keyPresses) {
                              long next = 0;
                              for (int sub = 0; sub <= code.Length; ++sub) {
                                    if (contains(state, sub)) {
                                          next |= automaton[sub, signal(key)];
                                    }
                              }
                              state = next;
                        }
                        return contains(state, code.Length);
                  }
                  return true;
            }

            private long[,] buildNFA(string code, int n) {
                  long[,] automaton = new long[n + 1, alphabet.Length];
                  for (int input = 0; input < alphabet.Length; ++input) {
                        automaton[0, input] |= makeSet(0);
                        automaton[n, input] |= makeSet(n);
                  }
                  for (int sub = 0; sub + 1 < code.Length; ++sub) {
                        automaton[sub + 1, signal(code[sub + 0])] |= makeSet(sub + 1);
                        automaton[sub + 1, signal(code[sub + 1])] |= makeSet(sub + 2);
                  }
                  for (int sub = 0; sub <= n; ++sub) {
                        automaton[sub, signal(code[0])] |= makeSet(1);
                  }
                  return automaton;
            }

            private bool contains(long set, int ix) {
                  return (set & makeSet(ix)) == makeSet(ix);
            }

            private long makeSet(int ix) {
                  return 1L << ix;
            }

            private int signal(char key) {
                  return alphabet.IndexOf(key);
            }

            private static readonly string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
      }
}