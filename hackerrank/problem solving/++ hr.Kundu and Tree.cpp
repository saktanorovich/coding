#include <cmath>
#include <cstdio>
#include <iostream>
#include <vector>
using namespace std;

const long mod = (long)1e9 + 7;

struct utils {
private:
    static long pow(long x, long k) {
        if (k == 0) {
            return 1;
        } else if (k % 2 == 0) {
            return pow(x * x % mod, k / 2);
        } else {
            return x * pow(x, k - 1) % mod;
        }
    }
    static long inv(long x) {
        return pow(x, mod - 2);
    }
public:
    static long div(long x, long d) {
        return x * inv(d) % mod;
    }
    static long mul(long x, long y) {
        return (x * y) % mod;
    }
    static long add(long x, long d) {
        return (x + d) % mod;
    }
    static long sub(long x, long d) {
        return (x - d + mod) % mod;
    }
};

struct dsu {
private:
    vector<int> cnt;
    vector<int> par;
public:
    dsu(int n) {
        this->cnt = vector<int>(n, 1);
        this->par = vector<int>(n);
        for (int x = 0; x < n; ++x) {
            par[x] = x;
        }
    }
    int find(int const &x) {
        if (par[x] != x) {
            par[x] = find(par[x]);
        }
        return par[x];
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

class tree {
public:
    virtual void add(int const &u, int const &v, int const &c) = 0;
    virtual int get() = 0;
    virtual ~tree() {}
};

class tree_small : public tree {
private:
    vector<vector<pair<int, int>>> edg;
    vector<vector<int>> cnt;
    int n;
private:
    void dfs(int const &r, int const &a, int const &p, int const &v) {
        cnt[r][a] = v;
        for (auto e : edg[a]) {
            int b = e.first;
            int c = e.second;
            if (b != p) {
                dfs(r, b, a, v + c);
            }
        }
    }
public:
    tree_small(int const &n) {
        this->n = n;
        this->edg = vector<vector<pair<int, int>>>(n);
        this->cnt = vector<vector<int>>(n, vector<int>(n, 0));
    }
    void add(int const &u, int const &v, int const &c) override {
        edg[u].push_back({ v, c });
        edg[v].push_back({ u, c });
    }
    int get() override {
        for (int a = 0; a < n; ++a) {
            dfs(a, a, a, 0);
        }
        long res = 0;
        for (int a = 0; a < n; ++a) {
            for (int b = a + 1; b < n; ++b) {
                for (int c = b + 1; c < n; ++c) {
                    if (cnt[a][b] && cnt[a][c] && cnt[b][c]) {
                        res = utils::add(res, 1);
                    }
                }
            }
        }
        return (int)res;
    }
};

class tree_large : public tree {
private:
    dsu *d;
    int n;
private:
    long n2(long x) {
        long res = x;
        res = utils::mul(res, x - 1);
        res = utils::div(res, 2);
        return res;
    }
    long n3(long x) {
        long res = x;
        res = utils::mul(res, x - 1);
        res = utils::mul(res, x - 2);
        res = utils::div(res, 6);
        return res;
    }
public:
    tree_large(int const &n) {
        this->n = n;
        this->d = new dsu(n);
    }
    void add(int const &u, int const &v, int const &c) override {
        if (c == 0) {
            // connect: b edges
            d->join(u, v);
        } else {
            // nothing: r edges
        }
    }
    int get() override {
        long res = n3(n);
        for (int node = node; node < n; ++node) {
            int root = d->find(node);
            if (root != node) {
                // we'd like to check only roots (nice trick :))
                continue;
            }
            int have = d->size(root);
            if (have >= 3) {
                auto tmp = n3(have);
                res = utils::sub(res, tmp);
            }
            if (have >= 2) {
                auto tmp = n2(have);
                tmp = utils::mul(tmp, n - have);
                res = utils::sub(res, tmp);
            }
        }
        return (int)res;
    }
    ~tree_large() override {
        delete d;
    }
};

template<class K>
K read() {
    K value; cin >> value;
    return value;
}

tree* get_tree(int const &n) {
    if (n < 1) {
        return new tree_small(n);
    } else {
        return new tree_large(n);
    }
}

int main() {
    auto n = read<int>();
    auto t = get_tree(n);
    for (int i = 1; i < n; ++i) {
        auto u = read<int>() - 1;
        auto v = read<int>() - 1;
        auto c = read<char>();
        if (c == 'r') {
            t->add(u, v, 1);
        } else if (c == 'b') {
            t->add(u, v, 0);
        } else {
            throw runtime_error("?");
        }
    }
    cout << t->get() << endl;
    delete t;
    return 0;
}
