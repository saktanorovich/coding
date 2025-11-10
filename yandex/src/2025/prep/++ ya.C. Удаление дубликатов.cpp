#include <iostream>
#include <vector>
using namespace std;

template<class K>
K read() {
	K value; cin >> value;
	return value;
}

int main() {
	int n = read<int>();
	if (n < 1) {
		return 0;
	}
	long x = read<long>();
	for (int i = 1; i < n; ++i) {
		int y = read<long>();
		if (y != x) {
			cout << x << endl;
			x = y;
		}
	}
	cout << x << endl;
	return 0;
}