using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TopCoder.Algorithm {
      public class ShortPalindromes {
            public string shortest(string word) {
                  return shortest(word, word.Length);
            }

            private string shortest(string word, int n) {
                  int[,] minAddings = new int[n, n];
                  string[,] best = new string[n, n];
                  for (int i = 0; i < n; ++i) {
                        for (int j = 0; j < n; ++j) {
                              minAddings[i, j] = int.MaxValue / 2;
                              if (j < i) {
                                    minAddings[i, j] = 0;
                              }
                              best[i, j] = string.Empty;
                        }
                        minAddings[i, i] = 0;
                        best[i, i] = word[i].ToString();
                  }
                  for (int len = 2; len <= n; ++len) {
                        for (int i = 0; i <= n - len; ++i) {
                              int j = i + len - 1;
                              if (word[i] == word[j]) {
                                    minAddings[i, j] = minAddings[i + 1, j - 1];
                                    best[i, j] = word[i] + best[i + 1, j - 1] + word[j];
                              }
                              else {
                                    string iappended = word[i] + best[i + 1, j] + word[i];
                                    string jappended = word[j] + best[i, j - 1] + word[j];
                                    if (minAddings[i + 1, j] < minAddings[i, j - 1]) {
                                          minAddings[i, j] = minAddings[i + 1, j] + 1;
                                          best[i, j] = iappended;
                                    }
                                    else if (minAddings[i + 1, j] > minAddings[i, j - 1]) {
                                          minAddings[i, j] = minAddings[i, j - 1] + 1;
                                          best[i, j] = jappended;
                                    }
                                    else {
                                          minAddings[i, j] = minAddings[i + 1, j] + 1;
                                          best[i, j] = iappended;
                                          if (jappended.CompareTo(iappended) < 0) {
                                                best[i, j] = jappended;
                                          }
                                    }
                              }
                        }
                  }
                  return best[0, n - 1];
            }
      }
}