using System;

class Solution {
  public int getArtisticPhotographCount(int N, string C, int X, int Y) {
    var res = 0;
    res += calc(N, C, X, Y, 'P', 'B');
    res += calc(N, C, X, Y, 'B', 'P');
    return res;
  }

  private int calc(int N, string C, int X, int Y, char P, char B) {
    var res = 0;
    for (var i = 0; i + 2 < N; ++i) {
      if (C[i] == P) {
        for (var j = i + 2; j < N; ++j) {
          if (C[j] == B) {
            for (var k = i + 1; k < j; ++k) {
              if (C[k] == 'A') {
                if (X <= k - i && k - i <= Y && X <= j - k && j - k <= Y) {
                  res += 1;
                }
              }
            }
          }
        }
      }
    }
    return res;
  }
}
