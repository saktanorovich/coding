#include <iostream>
using namespace std;

void make(int n, string s, int o, int c) {
	if (o + c == 2 * n) {
		cout << s << endl;
		return;
	}
	if (o < n) make(n, s + "(", o + 1, c);
	if (c < o) make(n, s + ")", o, c + 1);
}

template<class K>
K read() {
	K value; cin >> value;
	return value;
}

int main() {
	auto n = read<int>();
	make(n, "", 0, 0);
	return 0;
}