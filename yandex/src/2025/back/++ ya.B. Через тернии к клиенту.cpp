#include <iostream>
#include <map>
#include <set>
#include <string>
#include <vector>
using namespace std;

struct log_entry {
	size_t time;
	size_t type;
	bool operator<(const log_entry& other) const {
		if (this->time != other.time) {
			return this->time < other.time;
		} else {
			return this->type < other.type;
		}
    }
};

size_t calc(std::set<log_entry> const &log) {
	size_t now = 0;
	size_t res = 0;
	for (auto it : log) {
		if (it.type == 0) {
			now = it.time;
		} else if (it.type > 1) {
			res += it.time - now;
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
	auto n = read<int>();
	auto log = std::map<size_t, std::set<log_entry>>();
	auto abc = std::string("ABSC");
	for (int i = 0; i < n; ++i) {
		auto d = read<size_t>();
		auto h = read<size_t>();
		auto m = read<size_t>();
		auto r = read<size_t>();
		auto s = read<std::string>();
		log[r].insert(log_entry {
			time: (d - 1) * 24 * 60 + h * 60 + m,
			type: abc.find(s)
		});
	}
	auto cnt = log.size();
	for (auto it = log.begin(); cnt > 0; ++it) {
		cout << calc(it->second);
		if (cnt > 1) {
			cout << " ";
		}
		cnt --;
	}
	cout << endl;
	/**
	for (auto it : log) {
		cout << it.first << ":";
		for (auto jt : it.second) {
			cout << " (" << jt.time << ", " << jt.type << ")";
		}
		cout << endl;
	}
	/**/
	return 0;
}
