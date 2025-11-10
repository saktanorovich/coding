#include <algorithm>
#include <iostream>
#include <map>
#include <set>
#include <sstream>
#include <string>
#include <vector>
#include <unordered_set>
using namespace std;

const int MIN_TIME = 0;
const int MAX_TIME = (int)1e+9 + 1;

template<class K>
K read() {
	K value; cin >> value;
	return value;
}

struct event {
	int time;
	int cost;
	bool operator <(event const &other) const {
		if (this->time != other.time) {
			return this->time < other.time;
		} else {
			return this->cost < other.cost;
		}
	}
};

struct pot {
private:
	std::vector<event> events;
	std::vector<long long> prefix;
	std::size_t size;
private:
	std::size_t lower(int &time) {
		size_t lo = 0;
		size_t hi = size - 1;
		while (lo < hi) {
			auto x = (lo + hi) / 2;
			if (events[x].time < time) {
				lo = x + 1;
			} else {
				hi = x;
			}
		}
		return lo;
	}
	std::size_t upper(int &time) {
		size_t lo = 0;
		size_t hi = size - 1;
		while (lo < hi) {
			auto x = (lo + hi + 1) / 2;
			if (events[x].time > time) {
				hi = x - 1;
			} else {
				lo = x;
			}
		}
		return hi;
	}

public:
	pot() {
		events.push_back({ MIN_TIME, 0 });
		events.push_back({ MAX_TIME, 0 });
		size = 2;
	}
	void push(event const &e) {
		events.push_back(e);
		size ++;
	}
	void init() {
		sort(events.begin(), events.end());
		prefix = std::vector<long long>(size, 0);
		for (auto i = 1; i < size; ++i) {
			prefix[i] = prefix[i - 1]  + events[i].cost;
		}
	}
	auto find(int &st, int &en) {
		auto ind_st = lower(st);
		auto ind_en = upper(en);
		long long answer = 0L;
		answer += prefix[ind_en];
		answer -= prefix[ind_st - 1];
		return answer;
	}
};

int main() {
	auto n = read<int>();
	auto p1 = pot();
	auto p2 = pot();
	for (int i = 0; i < n; ++i) {
		auto st = read<int>();
		auto en = read<int>();
		auto ct = read<int>();
		p1.push({ st, ct });
		p2.push({ en, en - st });
	}
	p1.init();
	p2.init();
	auto q = read<int>();
	for (int i = 0; i < q; ++i) {
		auto st = read<int>();
		auto en = read<int>();
		auto ty = read<int>();
		if (ty == 1) {
			cout << p1.find(st, en);
		} else {
			cout << p2.find(st, en);
		}
		if (i + 1 < q) {
			cout << " ";
		}
	}
	cout << endl;
	return 0;
}