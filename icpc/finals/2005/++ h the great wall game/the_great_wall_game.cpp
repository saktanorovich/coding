#include <algorithm>
#include <cmath>
#include <cstdio>
#include <iostream>
using namespace std;

const int max_n = 16;
const int oo = +1000000000;

int n;
pair<int, int> stones[max_n];
pair<int, int> wall[max_n];
int dp[1 << max_n];

int dist(pair<int, int>& p1, pair<int, int>& p2) {
      return abs(p1.first - p2.first) + abs(p1.second - p2.second);
}

inline bool contains(int set, int subset) {
      return (set & subset) == subset;
}

int run(int k, int set) {
      if (k < 0) {
            return 0;
      }
      if (dp[set] == +oo) {
            for (int i = 0; i < n; ++i) {
                  if (contains(set, 1 << i)) {
                        dp[set] = min(dp[set], run(k - 1, set ^ (1 << i)) + dist(stones[k], wall[i]));
                  }
            }
      }
      return dp[set];
}

int run() {
      for (int i = 0; i < (1 << n); ++i) {
            dp[i] = +oo;
      }
      return run(n - 1, (1 << n) - 1);
}

int solve() {
      int result = +oo;
      for (int i = 1; i <= n; ++i) {
            for (int j = 1; j <= n; ++j) {
                  wall[j - 1] = make_pair(i, j);
            }
            result = min(result, run());
            for (int j = 1; j <= n; ++j) {
                  wall[j - 1] = make_pair(j, i);
            }
            result = min(result, run());
      }
      for (int i = 1; i <= n; ++i) {
            wall[i - 1] = make_pair(i, i);
      }
      result = min(result, run());
      for (int i = 1; i <= n; ++i) {
            wall[i - 1] = make_pair(i, n + 1 - i);
      }
      result = min(result, run());
      return result;
}

int main() {
      for (int i = 1; ; ++i) {
            cin >> n;
            if (n == 0) {
                  return 0;
            }
            for (int j = 0; j < n; ++j) {
                  cin >> stones[j].first >> stones[j].second;
            }
            int nmoves = solve();
            cout << "Board " << i << ": " << nmoves << " moves required." << endl << endl;
      }
      return 0;
}
