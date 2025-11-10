#include <iostream>
#include <queue>
#include <vector>
using namespace std;

template<class K>
K read() {
	K value; cin >> value;
	return value;
}

int main() {
	auto n = read<int>();
	std::vector<long> x(n);
	std::vector<long> y(n);
	for (int i = 0; i < n; ++i) {
		x[i] = read<long>();
		y[i] = read<long>();
	}
	auto k = read<int>();
	auto s = read<int>() - 1;
	auto f = read<int>() - 1;
	auto b = std::vector<int>(n, n * n + 1);
	b[s] = 0;
	auto q = std::queue<int>();
	q.push(s);
	while (!q.empty()) {
		int u = q.front(); q.pop();
		for (int v = 0; v < n; ++v) {
			auto d = std::abs(x[u] - x[v]) + std::abs(y[u] - y[v]);
			if (d <= k) {
				if (b[v] > b[u] + 1) {
					b[v] = b[u] + 1;
					q.push(v);
				}
			}
		}
	}
	cout << (b[f] < n * n + 1 ? b[f] : -1) << endl;
	return 0;
}