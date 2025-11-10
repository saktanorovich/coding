#include <bits/stdc++.h>
using namespace std;

typedef double ull;

template<class K>
K read() {
	K value; cin >> value;
	return value;
}

int main() {
	ios::sync_with_stdio(false);
	auto n = read<int>();
	auto total = vector<vector<ull>>(n + 1, vector<ull>(3, 0));
	auto graph = vector<vector<int>>(n + 1, vector<int>());
	auto indeg = vector<int>(n + 1);
	for (int curr = 3; curr <= n; ++curr) {
		auto k = read<int>();
		while (k-- > 0) {
			int from = read<int>();
			if (from >= 3) {
				graph[from].push_back(curr);
				indeg[curr] ++;
			} else {
				total[curr][from] ++;
			}
		}
	}
	auto queue = std::queue<int>();
	for (int curr = 3; curr <= n; ++curr) {
		if (indeg[curr] == 0) {
			queue.push(curr);
		}
	}
	while (!queue.empty()) {
		auto curr = queue.front(); queue.pop();
		for (auto next : graph[curr]) {
			total[next][1] += total[curr][1];
			total[next][2] += total[curr][2];
			indeg[next] --;
			if (indeg[next] == 0) {
				queue.push(next);
			}
		}
	}
	auto q = read<int>();
	while (q-- > 0) {
		auto x = read<ull>();
		auto y = read<ull>();
		auto s = read<int>();
		cout << (indeg[s] == 0 &&
			x >= total[s][1] &&
			y >= total[s][2]);
	}
	return 0;
}