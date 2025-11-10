#include <bits/stdc++.h>
using namespace std;

string ltrim(const string &);
string rtrim(const string &);
vector<string> split(const string &);

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

vector<int> componentsInGraph(vector<vector<int>> gb) {
    auto n = gb.size();
    auto d = dsu(2 * n);
    for (auto i = 0; i < n; ++i) {
        auto a = gb[i][0] - 1;
        auto b = gb[i][1] - 1;
        d.join(a, b);
    }
    int small = 2 * n;
    int large = 0;
    for (auto i = 0; i < n; ++i) {
        int size = d.size(i);
        if (size > 1) {
            small = std::min(small, size);
            large = std::max(large, size);
        }
    }
    auto res = vector<int>();
    res.push_back(small);
    res.push_back(large);
    return res;
}

int main() {
    ofstream fout(getenv("OUTPUT_PATH"));
    string n_temp;
    getline(cin, n_temp);
    int n = stoi(ltrim(rtrim(n_temp)));
    vector<vector<int>> gb(n);
    for (int i = 0; i < n; i++) {
        gb[i].resize(2);
        string gb_row_temp_temp;
        getline(cin, gb_row_temp_temp);
        vector<string> gb_row_temp = split(rtrim(gb_row_temp_temp));
        for (int j = 0; j < 2; j++) {
            int gb_row_item = stoi(gb_row_temp[j]);
            gb[i][j] = gb_row_item;
        }
    }
    vector<int> result = componentsInGraph(gb);
    for (size_t i = 0; i < result.size(); i++) {
        fout << result[i];
        if (i != result.size() - 1) {
            fout << " ";
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

