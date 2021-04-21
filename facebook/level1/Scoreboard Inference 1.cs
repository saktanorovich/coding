class Solution {
  public int getMinProblemCount(int N, int[] S) {
    var max = S[0];
    foreach (var s in S) {
      max = Math.Max(max, s);
    }
    var res = max / 2;
    foreach (var s in S) {
      if (s % 2 == 1) {
        res += 1;
        break;
      }
    }
    return res;
  }
}
