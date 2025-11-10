#include <bits/stdc++.h>
using namespace std;

string ltrim(const string &);
string rtrim(const string &);

struct trie {
private:
    vector<trie*> next;
    bool word;
public:
    trie() {
        next = vector<trie*>(10, nullptr);
        word = false;
    }
public:
    bool has(string const &s) {
        trie* curr = this;
        for (char c : s) {
            auto p = c - 'a';
            if (curr->next[p] == nullptr) {
                curr->next[p] = new trie();
            }
            curr = curr->next[p];
            if (curr->word) {
                return true;
            }
        }
        curr->word = true;
        return std::any_of(curr->next.begin(), curr->next.end(),
            [](auto& x) {
                return x != nullptr;
            });
    }
};

void noPrefix(vector<string> words) {
    trie t;
    for (auto word : words) {
        if (t.has(word)) {
            cout << "BAD SET" << endl;
            cout << word;
            return;
        }
    }
    cout << "GOOD SET" << endl;
}

int main() {
    string n_temp;
    getline(cin, n_temp);
    int n = stoi(ltrim(rtrim(n_temp)));
    vector<string> words(n);
    for (int i = 0; i < n; i++) {
        string words_item;
        getline(cin, words_item);
        words[i] = words_item;
    }
    noPrefix(words);
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
