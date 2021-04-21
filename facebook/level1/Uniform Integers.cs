using System;

class Solution {
  public int getUniformIntegerCountInInterval(long A, long B) {
    var res = 0;
    for (var d = 1; d <= 9; ++d) {
      var x = 0L;
      for (var l = 1; l <= 12; ++l) {
        x = x * 10 + d;
        if (A <= x && x <= B) {
          res ++;
        }
      }
    }
    return res;
  }
}
