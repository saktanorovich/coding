using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class MNS {
            public int combos(int[] numbers) {
                  SortedDictionary<long, bool> store = new SortedDictionary<long, bool>();
                  for (int[] permutation = new int[9] { 0, 1, 2, 3, 4, 5, 6, 7, 8 }; true; ) {
                        if (isMagicSquare(numbers, permutation)) {
                              store[hash(numbers, permutation)] = true;
                        }
                        if ((permutation = PermuteUtils.next(permutation)) == null) {
                              break;
                        }
                  }
                  return store.Count;
            }

            private bool isMagicSquare(int[] numbers, int[] p) {
                  int[] row = new int[3] {
                        numbers[p[0]] + numbers[p[1]] + numbers[p[2]],
                        numbers[p[3]] + numbers[p[4]] + numbers[p[5]],
                        numbers[p[6]] + numbers[p[7]] + numbers[p[8]]
                  };
                  int[] col = new int[3] {
                        numbers[p[0]] + numbers[p[3]] + numbers[p[6]],
                        numbers[p[1]] + numbers[p[4]] + numbers[p[7]],
                        numbers[p[2]] + numbers[p[5]] + numbers[p[8]]
                  };
                  for (int i = 1; i < 3; ++i) {
                        if (row[i] != row[i - 1] ||
                              col[i] != col[i - 1] ||
                                    row[i] != col[i]) {
                                          return false;
                        }
                  }
                  return true;
            }

            private long hash(int[] numbers, int[] p) {
                  long result = 0;
                  for (int i = 0; i < p.Length; ++i) {
                        result = result * 10L + numbers[p[i]];
                  }
                  return result;
            }

            private static class PermuteUtils {
                  public static void swap<T>(T[] list, int i, int j) {
                        T temp = list[i];
                        list[i] = list[j];
                        list[j] = temp;
                  }

                  public static T[] next<T>(T[] p) where T : IComparable<T> {
                        for (int i = p.Length - 1; i >= 0; --i) {
                              if (i + 1 < p.Length) {
                                    if (p[i].CompareTo(p[i + 1]) < 0) {
                                          for (int j = p.Length - 1; j > i; --j) {
                                                if (p[j].CompareTo(p[i]) > 0) {
                                                      swap(p, i, j);
                                                      for (int x = i + 1, y = p.Length - 1; x < y; ++x, --y) {
                                                            swap(p, x, y);
                                                      }
                                                      return p;
                                                }
                                          }
                                    }
                              }
                        }
                        return null;
                  }
            }
      }
}