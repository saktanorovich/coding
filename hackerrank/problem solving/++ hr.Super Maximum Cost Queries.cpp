#include <bits/stdc++.h>
using namespace std;

string ltrim(const string &);
string rtrim(const string &);
vector<string> split(const string &);

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
    tree() { }
    virtual ~tree() {
    }
public:
    virtual void init() = 0;
    virtual long long calc(int const &) = 0;
};

class tree_small : public tree {
private:
    vector<vector<pair<int, int>>> tree;
    vector<vector<int>> cost;
    size_t size;
private:
    void walk(int const & r, int const & a, int const &p, int const &v) {
        cost[r][a] = v;
        for (auto e : tree[a]) {
            auto b = e.first;
            auto w = e.second;
            if (b != p) {
                walk(r, b, a, std::max(v, w));
            }
        }
    }
public:
    tree_small(vector<vector<int>> &edges) {
        this->size = edges.size() + 1;
        this->tree = vector<vector<pair<int, int>>>(size);
        this->cost = vector<vector<int>>(size, vector<int>(size, 0));
        for (auto e : edges) {
            auto u = e[0] - 1;
            auto v = e[1] - 1;
            auto w = e[2];
            this->tree[u].push_back({ v, w });
            this->tree[v].push_back({ u, w });
        }
    }
public:
    void init() override {
        for (size_t i = 0; i < size; ++i) {
            walk(i, i, i, 0);
        }
    }
    long long calc(int const &C) override {
        long long answ = 0;
        for (size_t i = 0; i < size; ++i) {
            for (size_t j = i + 1; j < size; ++j) {
                if (cost[i][j] <= C) {
                    answ ++;
                }
            }
        }
        return answ;
    }
};

class tree_large : public tree {
private:
    vector<vector<int>> tree;
    vector<long long> summa;
    vector<int> costs;
    size_t size;
private:
public:
    tree_large(vector<vector<int>> &edges) {
        this->size = edges.size() + 1;
        for (auto e : edges) {
            auto u = e[0] - 1;
            auto v = e[1] - 1;
            auto w = e[2];
            this->tree.push_back(vector<int> { u, v, w });
        }
    }
public:
    void init() override {
        std::sort(tree.begin(), tree.end(),
            [&](auto &a, auto &b) {
                return a[2] < b[2];
        });
        dsu d(size);
        costs.push_back(0);
        summa.push_back(0);
        for (auto e : tree) {
            auto uc = d.find(e[0]);
            auto vc = d.find(e[1]);
            auto uz = d.size(uc);
            auto vz = d.size(vc);
            auto sz = 1LL * uz * vz;
            if (costs.back() != e[2]) {
                costs.push_back(e[2]);
                summa.push_back(summa.back());
                summa.back() += sz;
            } else {
                summa.back() += sz;
            }
            d.join(uc, vc);
        }
    }
    long long calc(int const &C) override {
        size_t lo = 0;
        size_t hi = costs.size() - 1;
        while (lo < hi) {
            size_t x = (lo + hi + 1) >> 1;
            if (costs[x] > C) {
                hi = x - 1;
            } else {
                lo = x;
            }
        }
        return summa[hi];
    }
};

vector<long long> solve(vector<vector<int>> &edges, vector<vector<int>> &queries) {
    tree *t;
    int n = edges.size() + 1;
    if (n < 1) {
        t = new tree_small(edges);
    } else {
        t = new tree_large(edges);
    }
    t->init();
    vector<long long> res;
    for (size_t i = 0; i < queries.size(); ++i) {
        auto L = queries[i][0];
        auto R = queries[i][1];
        long H = 0;
        H += t->calc(R);
        H -= t->calc(L - 1);
        res.push_back(H);
    }
    delete t;
    return res;
}

int main() {
    ofstream fout(getenv("OUTPUT_PATH"));
    string first_multiple_input_temp;
    getline(cin, first_multiple_input_temp);
    vector<string> first_multiple_input = split(rtrim(first_multiple_input_temp));
    int n = stoi(first_multiple_input[0]);
    int q = stoi(first_multiple_input[1]);
    vector<vector<int>> tree(n - 1);
    for (int i = 0; i < n - 1; i++) {
        tree[i].resize(3);
        string tree_row_temp_temp;
        getline(cin, tree_row_temp_temp);
        vector<string> tree_row_temp = split(rtrim(tree_row_temp_temp));
        for (int j = 0; j < 3; j++) {
            int tree_row_item = stoi(tree_row_temp[j]);
            tree[i][j] = tree_row_item;
        }
    }
    vector<vector<int>> queries(q);
    for (int i = 0; i < q; i++) {
        queries[i].resize(2);
        string queries_row_temp_temp;
        getline(cin, queries_row_temp_temp);
        vector<string> queries_row_temp = split(rtrim(queries_row_temp_temp));
        for (int j = 0; j < 2; j++) {
            int queries_row_item = stoi(queries_row_temp[j]);
            queries[i][j] = queries_row_item;
        }
    }
    auto result = solve(tree, queries);
    for (int i = 0; i < q; i++) {
        fout << result[i];
        if (i + 1 < q) {
            fout << "\n";
        }
    }
    fout << "\n";
    fout.close();
    return 0;
}

string ltrim(const string &str) {
    string s(str);
    s.erase(
        s.begin(),
        find_if(s.begin(), s.end(), not1(ptr_fun<int, int>(isspace)))
    );
    return s;
}

string rtrim(const string &str) {
    string s(str);
    s.erase(
        find_if(s.rbegin(), s.rend(), not1(ptr_fun<int, int>(isspace))).base(),
        s.end()
    );
    return s;
}

vector<string> split(const string &str) {
    vector<string> tokens;
    string::size_type start = 0;
    string::size_type end = 0;
    while ((end = str.find(" ", start)) != string::npos) {
        tokens.push_back(str.substr(start, end - start));
        start = end + 1;
    }
    tokens.push_back(str.substr(start));
    return tokens;
}
