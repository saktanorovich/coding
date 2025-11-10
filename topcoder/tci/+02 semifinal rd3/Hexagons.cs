using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class Hexagons {
            public int[] centers(string[] pieces) {
                  List<int> result = new List<int>();
                  foreach (List<int> configuration in PermuteUtils.permute(new List<int>(new int[7] { 0, 1, 2, 3, 4, 5, 6 }))) {
                        string[] order = new string[6];
                        for (int i = 0; i < 6; ++i) {
                              order[i] = pieces[configuration[i + 1]];
                        }
                        if (possible(pieces[configuration[0]], order)) {
                              if (!result.Contains(configuration[0] + 1)) {
                                    result.Add(configuration[0] + 1);
                              }
                        }
                  }
                  result.Sort();
                  return result.ToArray();
            }

            private bool possible(string center, string[] pieces) {
                  for (int i = 0; i < 6; ++i) {
                        while (pieces[i][0] != center[i]) {
                              pieces[i] = rotate(pieces[i]);
                        }
                  }
                  for (int i = 0; i < 6; ++i) {
                        if (pieces[i][5] != pieces[(i + 1) % 6][1]) {
                              return false;
                        }
                  }
                  return true;
            }

            private string rotate(string piece) {
                  return piece.Substring(1, piece.Length - 1) + piece[0];
            }

            private static class PermuteUtils {
                  public static List<List<T>> permute<T>(List<T> list) {
                        List<List<T>> result = new List<List<T>>();
                        if (list.Count == 0) {
                              result.Add(new List<T>());
                        }
                        else {
                              for (int i = 0; i < list.Count; ++i) {
                                    foreach (List<T> res in permute(remove(list, i))) {
                                          result.Add(concat(new List<T>(new T[] { list[i] }), res));
                                    }
                              }
                        }
                        return result;
                  }

                  public static List<T> concat<T>(List<T> list1, List<T> list2) {
                        List<T> result = new List<T>(list1);
                        foreach (T item in list2) {
                              result.Add(item);
                        }
                        return result;
                  }

                  private static List<T> remove<T>(List<T> list, int index) {
                        List<T> result = new List<T>(list);
                        result.RemoveAt(index);
                        return result;
                  }
            }
      }
}