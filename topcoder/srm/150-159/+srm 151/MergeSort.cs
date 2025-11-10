using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class MergeSort {
            public int howManyComparisons(int[] numbers) {
                  return howManyComparisons(new List<int>(numbers));
            }

            private int howManyComparisons(List<int> list) {
                  int result = 0;
                  mergeSort(list, ref result);
                  return result;
            }

            private List<int> mergeSort(List<int> list, ref int result) {
                  if (list.Count > 1) {
                        int middle = list.Count >> 1;
                        List<int> list1 = mergeSort(new List<int>(subList(list, 0, middle)), ref result);
                        List<int> list2 = mergeSort(new List<int>(subList(list, middle, list.Count - middle)), ref result);
                        return merge(list1, list2, ref result);
                  }
                  return list;
            }

            private List<int> merge(List<int> list1, List<int> list2, ref int result) {
                  List<int> list = new List<int>();
                  while (list1.Count > 0 && list2.Count > 0) {
                        result = result + 1;
                        if (list1[0] < list2[0]) {
                              list.Add(list1[0]);
                              list1.RemoveAt(0);
                        }
                        else if (list1[0] > list2[0]) {
                              list.Add(list2[0]);
                              list2.RemoveAt(0);
                        }
                        else {
                              list.Add(list1[0]); list1.RemoveAt(0);
                              list.Add(list2[0]); list2.RemoveAt(0);
                        }
                  }
                  list.AddRange(list1);
                  list.AddRange(list2);
                  return list;
            }

            private List<int> subList(List<int> list, int from, int count) {
                  List<int> result = new List<int>();
                  for (int i = from; i < from + count; ++i) {
                        result.Add(list[i]);
                  }
                  return result;
            }
      }
}