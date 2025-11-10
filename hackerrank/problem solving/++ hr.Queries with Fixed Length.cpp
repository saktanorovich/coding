#include <bits/stdc++.h>
using namespace std;

string ltrim(const string &);
string rtrim(const string &);
vector<string> split(const string &);

struct window {
private:
    std::vector<int> deque;
public:
    void add(int const &value) {
        while (!deque.empty()) {
            if (deque.back() < value) {
                deque.pop_back();
            } else break;
        }
        deque.push_back(value);
    }
    void pop(int const &value) {
        auto it = deque.begin();
        if (*it == value) {
            deque.erase(it);
        }
    }
    int max() {
        return deque.front();
    }
};

vector<int> small(vector<int> const &arr, vector<int> const &queries, int const &n, int const &q) {
    auto maxj = [&](vector<int> const &a, int const &i, int const &d) {
        auto res = 0;
        for (auto j = i; j < i + d; ++j) {
            res = std::max(res, a[j]);
        }
        return res;
    };
    auto res = vector<int>();
    for (auto d : queries) {
        auto mini = (int)1e+6;
        for (auto i = 0; i <= n - d; ++i) {
            mini = std::min(mini, maxj(arr, i, d));
        }
        res.push_back(mini);
    }
    return res;
}

vector<int> large(vector<int> const &arr, vector<int> const &queries, int const &n, int const &q) {
    vector<int> res;
    for (auto d : queries) {
        window win;
        for (int j = 0; j < d; ++j) {
            win.add(arr[j]);
        }
        auto mini = win.max();
        for (int j = d; j < n; ++j) {
            win.pop(arr[j - d]);
            win.add(arr[j]);
            int maxj = win.max();
            if (mini > maxj) {
                mini = maxj;
            }
        }
        res.push_back(mini);
    }
    return res;
}

vector<int> solve(vector<int> arr, vector<int> queries) {
    auto n = arr.size();
    if (n < 1) {
        return small(arr, queries, n, queries.size());
    } else {
        return large(arr, queries, n, queries.size());
    }
}

int main() {
    ofstream fout(getenv("OUTPUT_PATH"));
    string first_multiple_input_temp;
    getline(cin, first_multiple_input_temp);
    vector<string> first_multiple_input = split(rtrim(first_multiple_input_temp));
    int n = stoi(first_multiple_input[0]);
    int q = stoi(first_multiple_input[1]);
    string arr_temp_temp;
    getline(cin, arr_temp_temp);
    vector<string> arr_temp = split(rtrim(arr_temp_temp));
    vector<int> arr(n);
    for (int i = 0; i < n; i++) {
        int arr_item = stoi(arr_temp[i]);
        arr[i] = arr_item;
    }
    vector<int> queries(q);
    for (int i = 0; i < q; i++) {
        string queries_item_temp;
        getline(cin, queries_item_temp);
        int queries_item = stoi(ltrim(rtrim(queries_item_temp)));
        queries[i] = queries_item;
    }
    vector<int> result = solve(arr, queries);
    for (size_t i = 0; i < result.size(); i++) {
        fout << result[i];
        if (i != result.size() - 1) {
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
