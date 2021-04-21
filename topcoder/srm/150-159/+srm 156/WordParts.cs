using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class WordParts {
            public int partCount(string original, string compound) {
                  Dictionary<string, bool> map = new Dictionary<string, bool>();
                  for (int length = 1; length <= original.Length; ++length) {
                        map[prefix(original, length)] = true;
                        map[suffix(original, length)] = true;
                  }
                  int[] dp = new int[compound.Length + 1];
                  for (int length = 1; length <= compound.Length; ++length) {
                        dp[length] = int.MaxValue / 2;
                        if (map.ContainsKey(prefix(compound, length))) {
                              dp[length] = 1;
                        }
                        else {
                              string current = prefix(compound, length);
                              for (int sub = 1; sub < length; ++sub) {
                                    if (map.ContainsKey(suffix(current, length - sub))) {
                                          dp[length] = Math.Min(dp[length], dp[sub] + 1);
                                    }
                              }
                        }
                  }
                  if (dp[compound.Length] < int.MaxValue / 2) {
                        return dp[compound.Length];
                  }
                  return -1;
            }

            private string prefix(string s, int length) {
                  return s.Substring(0, length);
            }

            private string suffix(string s, int length) {
                  return s.Substring(s.Length - length, length);
            }
      }
}