using System;

class Solution {
  public int getMinimumDeflatedDiscCount(int N, int[] R) {
    var res = 0;
    var top = R[N - 1];
    for (var i = N - 2; i >= 0; --i) {
      if (R[i] >= top) {
        res ++;
        top --;
      } else {
        top = R[i];
      }
    }
    return top > 0 ? res : -1;
  }
}
