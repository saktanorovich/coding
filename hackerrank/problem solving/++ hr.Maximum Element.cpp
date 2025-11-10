#include <bits/stdc++.h>
using namespace std;

string ltrim(const string &);
string rtrim(const string &);

vector<int> getMax(vector<string> operations) {
    auto res = std::vector<int>();
    auto sta = std::stack<int>();
    sta.push(0);
    for (auto& op_s : operations) {
        istringstream ss(op_s);
        int op; ss >> op;
        if (op == 1) {
            int val; ss >> val;
            sta.push(std::max(sta.top(), val));
        } else if (op == 2) {
            sta.pop();
        } else {
            res.push_back(sta.top());
        }
    }
    return res;
}

int main() {
    ofstream fout(getenv("OUTPUT_PATH"));
    string n_temp;
    getline(cin, n_temp);
    int n = stoi(ltrim(rtrim(n_temp)));
    vector<string> ops(n);
    for (int i = 0; i < n; i++) {
        string ops_item;
        getline(cin, ops_item);
        ops[i] = ops_item;
    }
    vector<int> res = getMax(ops);
    for (size_t i = 0; i < res.size(); i++) {
        fout << res[i];
        if (i != res.size() - 1) {
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

