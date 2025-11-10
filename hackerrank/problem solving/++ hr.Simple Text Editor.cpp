#include <iostream>
#include <stack>
using namespace std;

template<class V>
V read() {
    V value; cin >> value;
    return value;
}

class text_editor {
private:
    std::string text;
    std::stack<std::string> undo;
public:
    text_editor() {
        text = "";
        undo.push(text);
    }
public:
    void append(string const &w) {
        text.append(w);
        undo.push(text);
    }
    void remove(int const &k) {
        text = text.substr(0, text.size() - k);
        undo.push(text);
    }
    char output(int const &k) {
        return text[k - 1];
    }
    void cancell() {
        undo.pop();
        text = undo.top();
    }
};

int main() {
    text_editor teditor;
    int q = read<int>();
    for (int i = 0; i < q; ++i) {
        int t = read<int>();
        if (t == 1) {
            teditor.append(read<std::string>());
        } else if (t == 2) {
            teditor.remove(read<int>());
        } else if (t == 3) {
            auto c = teditor.output(read<int>());
            std::cout << c << std::endl;
        } else if (t == 4) {
            teditor.cancell();
        }
    }
    return 0;
}

