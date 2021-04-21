using System;
using System.Collections.Generic;

class Solution {
  public int getMaximumEatenDishCount(int N, int[] D, int K) {
    var answ = 0;
    var have = new HashSet<int>();
    var list = new LinkedList<int>();
    for (var i = 0; i < N; ++i) {
      if (have.Add(D[i])) {
        answ ++;
        list.AddLast(D[i]);
        if (have.Count > K) {
          have.Remove(list.First.Value);
          list.RemoveFirst();
        }
      }
    }
    return answ;
  }
}
