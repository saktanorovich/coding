#include <bits/stdc++.h>
using namespace std;

string ltrim(const string &);
string rtrim(const string &);
vector<string> split(const string &);

int andXorOr(vector<int> a) {
    // the problem is the same as
    //   for each a[i] find the nearest
    //   smallest element in a[1:i - 1]
    auto res = 0;
    auto sta = std::stack<int>();
    for (auto x : a) {
        while (!sta.empty()) {
            res = std::max(res, x ^ sta.top());
            if (sta.top() > x) {
                sta.pop();
            } else break;
        }
        sta.push(x);
    }
    return res;
}

int main() {
    ofstream fout(getenv("OUTPUT_PATH"));
    string a_count_temp;
    getline(cin, a_count_temp);
    int a_count = stoi(ltrim(rtrim(a_count_temp)));
    string a_temp_temp;
    getline(cin, a_temp_temp);
    vector<string> a_temp = split(rtrim(a_temp_temp));
    vector<int> a(a_count);
    for (int i = 0; i < a_count; i++) {
        int a_item = stoi(a_temp[i]);
        a[i] = a_item;
    }
    int result = andXorOr(a);
    fout << result << "\n";
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

