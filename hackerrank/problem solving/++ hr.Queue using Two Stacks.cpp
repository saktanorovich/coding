#include <cmath>
#include <cstdio>
#include <iostream>
#include <stack>
using namespace std;

template<class K>
class Queue {
private:
    std::stack<K> inp;
    std::stack<K> out;
private:
    void balance() {
        if (out.empty()) {
            while (!inp.empty()) {
                out.push(inp.top());
                inp.pop();
            }
        }
    }
public:
    void enqueu(const K& value) {
        inp.push(value);
    }
    void dequeu() {
        balance();
        out.pop();
    }
    K front() {
        balance();
        return out.top();
    }
};

template<class V>
V read() {
    V value; cin >> value;
    return value;
}

int main() {
    auto queries = read<int>();
    auto queue = Queue<int>();
    for (int i = 0; i < queries; ++i) {
        int q = read<int>();
        if (q == 1) {
            queue.enqueu(read<int>());
        } else if (q == 2) {
            queue.dequeu();
        } else if (q == 3) {
            cout << queue.front() << endl;
        }
    }
    return 0;
}

