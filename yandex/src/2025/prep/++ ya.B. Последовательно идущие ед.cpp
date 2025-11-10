#include <iostream>
#include <vector>
using namespace std;

template<class K>
K read() {
	K value; cin >> value;
	return value;
}

int main() {
	auto n = read<int>();
	auto res = 0;
	auto cur = 0;
	for (int i = 0; i < n; ++i) {
		int x = read<int>();
		if (x == 1) {
			cur ++;
			if (res < cur) {
				res = cur;
			}
		} else {
			cur = 0;
		}
	}
	cout << res << endl;
	return 0;
}
