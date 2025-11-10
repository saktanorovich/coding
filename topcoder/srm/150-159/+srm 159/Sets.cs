using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TopCoder.Algorithm {
      public class Sets {
            public int[] operate(int[] a, int[] b, string operation) {
                  return operations[operation](new List<int>(a), new List<int>(b));
            }

            private delegate int[] Func(List<int> a, List<int> b);

            private static readonly IDictionary<string, Func> operations;

            static Sets() {
                  operations = new Dictionary<string, Func>();
                  operations["UNION"] = union;
                  operations["INTERSECTION"] = intersection;
                  operations["SYMMETRIC DIFFERENCE"] = symmetricDifference;
            }

            private static int[] union(List<int> a, List<int> b) {
                  List<int> result = new List<int>();
                  foreach (int element in a) {
                        if (!result.Contains(element)) {
                              result.Add(element);
                        }
                  }
                  foreach (int element in b) {
                        if (!result.Contains(element)) {
                              result.Add(element);
                        }
                  }
                  result.Sort();
                  return result.ToArray();
            }

            private static int[] intersection(List<int> a, List<int> b) {
                  List<int> result = new List<int>();
                  foreach (int element in a) {
                        if (b.Contains(element)) {
                              result.Add(element);
                        }
                  }
                  result.Sort();
                  return result.ToArray();

            }

            private static int[] symmetricDifference(List<int> a, List<int> b) {
                  List<int> result = new List<int>();
                  foreach (int element in a) {
                        if (!result.Contains(element)) {
                              if (!b.Contains(element)) {
                                    result.Add(element);
                              }
                        }
                  }
                  foreach (int element in b) {
                        if (!result.Contains(element)) {
                              if (!a.Contains(element)) {
                                    result.Add(element);
                              }
                        }
                  }
                  result.Sort();
                  return result.ToArray();
            }
      }
}