#include <cmath>
#include <cstdio>
#include <vector>
#include <iostream>
using namespace std;

struct dsu {
private:
    vector<int> cnt;
    vector<int> par;
private:
    int find(int const &x) {
        if (par[x] != x) {
            par[x] = find(par[x]);
        }
        return par[x];
    }
public:
    dsu(int n) {
        this->cnt = vector<int>(n, 1);
        this->par = vector<int>(n);
        for (int x = 0; x < n; ++x) {
            par[x] = x;
        }
    }
    void join(int const &a, int const &b) {
        auto x = find(a);
        auto y = find(b);
        if (x != y) {
            if (cnt[x] < cnt[y]) {
                par[x] = y;
                cnt[y] += cnt[x];
            } else {
                par[y] = x;
                cnt[x] += cnt[y];
            }
        }
    }
    int size(int const &x) {
        auto p = find(x);
        return cnt[p];
    }
};

template<class K>
K read() {
    K value; cin >> value;
    return value;
}

int main() {
    auto n = read<int>();
    auto q = read<int>();
    auto d = dsu(n);
    while (q--) {
        auto t = read<char>();
        if (t == 'M') {
            auto a = read<int>() - 1;
            auto b = read<int>() - 1;
            d.join(a, b);
        } else if (t == 'Q') {
            auto x = read<int>() - 1;
            cout << d.size(x) << endl;
        } else {
            throw runtime_error("?");
        }
    }
    return 0;
}
