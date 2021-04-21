class Solution {
  public double getHitProbability(int R, int C, int[,] G) {
    var have = 0;
    for (var i = 0; i < R; ++i) {
      for (var j = 0; j < C; ++j) {
        have += G[i, j];
      }
    }
    return 1.0 * have / (R * C);
  }
}
