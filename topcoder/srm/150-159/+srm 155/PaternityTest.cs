using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class PaternityTest {
            public int[] possibleFathers(string child, string mother, string[] men) {
                  List<int> result = new List<int>();
                  for (int m = 0; m < men.Length; ++m) {
                        for (int set = 0; set < (1 << child.Length); ++set) {
                              if (2 * cardinality(set) == child.Length) {
                                    if (testdna(new string[] { mother, men[m] }, child, set)) {
                                          result.Add(m);
                                          break;
                                    }
                              }
                        }
                  }
                  return result.ToArray();
            }

            private bool testdna(string[] parents, string child, int set) {
                  int[] match = new int[] { 0, 0 };
                  for (int i = 0; i < child.Length; ++i) {
                        match[(set >> i) & 1] += Convert.ToInt32(parents[(set >> i) & 1][i].Equals(child[i]));
                  }
                  return 2 * match[0] == child.Length && 2 * match[1] == child.Length;
            }

            private int cardinality(int set) {
                  if (set > 0) {
                        return 1 + cardinality(set & (set - 1));
                  }
                  return 0;
            }
      }
}