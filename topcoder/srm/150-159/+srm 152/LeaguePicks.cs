using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class LeaguePicks {
            public int[] returnPicks(int position, int friends, int picks) {
                  List<int> list = new List<int>();
                  for (int times = 0; times < picks; ++times) {
                        list.AddRange(getList(1, friends, +1));
                        list.AddRange(getList(friends, 1, -1));
                  }
                  List<int> result = new List<int>();
                  for (int i = 0; i < picks; ++i) {
                        if (list[i] == position) {
                              result.Add(i + 1);
                        }
                  }
                  return result.ToArray();
            }

            private List<int> getList(int lo, int hi, int inc) {
                  List<int> result = new List<int>();
                  for (int i = lo; i != hi + inc; i += inc) {
                        result.Add(Math.Abs(i));
                  }
                  return result;
            }
      }
}