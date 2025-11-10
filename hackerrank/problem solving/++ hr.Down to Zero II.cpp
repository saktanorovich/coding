#include <bits/stdc++.h>
using namespace std;

const int max_n = (int)1e6;
int memo[max_n + 1];

int downToZero(int n) {
    if (memo[n] != -1) {
        return memo[n];
    }
    int res = downToZero(n - 1) + 1;
    for (int a = 2; a * a <= n; ++a) {
        int b = n / a;
        if (b * a == n) {
            res = std::min(res, downToZero(b) + 1);
        }
    }
    memo[n] = res;
    return res;
}

template<class K>
K read() {
    K value; cin >> value;
    return value;
}

int main() {
    memo[0] = 0; memo[1] = 1;
    for (int i = 2; i <= max_n; ++i) {
        memo[i] = -1;
    }
    int queries = read<int>();
    for (int q = 0; q < queries; ++q) {
        int n = read<int>();
        int r = downToZero(n);
        cout << r << "\n";
    }
    return 0;
}

