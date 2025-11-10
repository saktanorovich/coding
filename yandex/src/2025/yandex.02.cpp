/**
Для двух массивов целых чисел длины N, для всех K от 1 до N, найдите количество общих чисел
на префиксах длины K. Числа в пределах массива могут повторяться, пересечение считается без
учета кратности.

Пример:
Массив 1: 1 1 5 7
Массив 2: 5 1 7 1
   Ответ: 0 1 2 3
*/
#include <iostream>
#include <set>
#include <vector>
using namespace std;

vector<int> find(int n, vector<int> const &a, vector<int> const &b) {
    vector<int> res;
    set<int> seta;
    set<int> setb;
    int count = 0;
    for (int k = 0; k < n; ++k) {
        int ak = a[k];
        int bk = b[k];
        if (!seta.count(ak)) {
            count += setb.count(ak);
            seta.insert(ak);
        }
        if (!setb.count(bk)) {
            count += seta.count(bk);
            setb.insert(bk);
        }
        res.push_back(count);
    }
    return res;
}

template<class K>
K read() {
    K value; cin >> value;
    return value;
}

int main() {
    int n = read<int>();
    vector<int> a;
    vector<int> b;
    for (int i = 0; i < n; ++i) {
        a.push_back(read<int>());
    }
    for (int i = 0; i < n; ++i) {
        b.push_back(read<int>());
    }
    vector<int> p = find(n, a, b);
    for (int i = 0; i < n; ++i) {
        cout << p[i];
        if (i + 1 < n) {
            cout << " ";
        }
    }
    cout << endl;
    return 0;
}