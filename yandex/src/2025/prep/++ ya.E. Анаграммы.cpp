#include <iostream>
#include <vector>
using namespace std;

int same(std::string &a, std::string &b) {
	std::vector<int> freq(26, 0);
	int n = a.length();
	int m = b.length();
	if (n != m) {
		return 0;
	}
	for (int i = 0; i < n; ++i) {
		freq[a[i] - 'a'] ++;
		freq[b[i] - 'a'] --;
	}
	for (int c = 0; c < 26; ++c) {
		if (freq[c] != 0) {
			return 0;
		}
	}
	return 1;
}

template<class K>
K read() {
	K value; cin >> value;
	return value;
}

int main() {
	auto a = read<std::string>();
	auto b = read<std::string>();
	cout << same(a, b) << endl;
	return 0;
}