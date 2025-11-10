#include <iostream>
#include <vector>
using namespace std;

int count(std::string &J, std::string &S) {
	std::vector<bool> jew(26, false);
	for (int i = 0; i < J.length(); ++i) {
		jew[J[i] - 'a'] = true;
	}
	int res = 0;
	for (int i = 0; i < S.length(); ++i) {
		if (jew[S[i] - 'a']) {
			res ++;
		}
	}
	return res;
}

template<class K>
K read() {
	K value; cin >> value;
	return value;
}

int main() {
	auto J = read<std::string>();
	auto S = read<std::string>();
	cout << count(J, S) << endl;
	return 0;
}
