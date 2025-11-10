using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class DotNotation {
            public int countAmbiguity(string dotForm) {
                  return count(dotForm).Count;
            }

            private Dictionary<long, bool> count(string dotForm) {
                  Dictionary<long, bool> result;
                  if (!memo.TryGetValue(dotForm, out result)) {
                        result = new Dictionary<long, bool>();
                        if ("0123456789".IndexOf(dotForm) >= 0) {
                              result.Add(long.Parse(dotForm), true);
                        }
                        else {
                              for (int i = 0; i < dotForm.Length; ++i) {
                                    if (isdominant(dotForm, dotForm.Length, i)) {
                                          Dictionary<long, bool> le = count(dotForm.Substring(0, i).Trim('.'));
                                          Dictionary<long, bool> ri = count(dotForm.Substring(i + 1).Trim('.'));
                                          foreach (long a in le.Keys) {
                                                foreach (long b in ri.Keys) {
                                                      try {
                                                            long c = 0;
                                                            switch (dotForm[i]) {
                                                                  case '+': c = a + b; break;
                                                                  case '-': c = a - b; break;
                                                                  case '*': c = a * b; break;
                                                                  case '/': c = a / b; break;
                                                            }
                                                            if (-oo <= c && c <= +oo) {
                                                                  result[c] = true;
                                                            }
                                                      }
                                                      catch (Exception) {
                                                      }
                                                }
                                          }
                                    }
                              }
                        }
                        memo.Add(dotForm, result);
                  }
                  return result;
            }

            private bool isdominant(string dotForm, int n, int ix) {
                  if ("+-*/".IndexOf(dotForm[ix]) >= 0) {
                        int[] ledots = new int[n];
                        int[] ridots = new int[n];
                        for (int i = 0; i < n; ++i) {
                              if ("+-*/".IndexOf(dotForm[i]) >= 0) {
                                    for (int k = i - 1; k > 0 && dotForm[k].Equals('.'); ) {
                                          ledots[i] = ledots[i] + 1;
                                          k = k - 1;
                                    }
                                    for (int k = i + 1; k < n && dotForm[k].Equals('.'); ) {
                                          ridots[i] = ridots[i] + 1;
                                          k = k + 1;
                                    }
                              }
                        }
                        for (int i = 0; i <= ix - 1; ++i) {
                              if (ridots[i] > ledots[ix]) {
                                    return false;
                              }
                        }
                        for (int i = ix + 1; i < n; ++i) {
                              if (ledots[i] > ridots[ix]) {
                                    return false;
                              }
                        }
                        return true;
                  }
                  return false;
            }

            private readonly SortedDictionary<string, Dictionary<long, bool>> memo = new SortedDictionary<string, Dictionary<long, bool>>();
            private readonly long oo = +2000000000;
      }
}