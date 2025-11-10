#include <iostream>
#include <map>
#include <set>
#include <sstream>
#include <string>
#include <vector>
#include <unordered_set>
using namespace std;

template<class K>
K read() {
	K value; cin >> value;
	return value;
}

std::vector<std::string> split(std::string const &s) {
	std::stringstream sstream(s);
	std::vector<std::string> tokens;
	std::string token;
	while (std::getline(sstream, token, ',')) {
		tokens.push_back(token);
	}
	return tokens;
}

struct person {
	std::string f;
	std::string i;
	std::string o;
	int d;
	int m;
	int y;
private:
	size_t summa(int x) const {
		size_t sum = 0;
		while (x > 0) {
			sum += x % 10;
			x /= 10;
		}
		return sum;
	}
	size_t step1() const {
		std::unordered_set<char> sym;
		for (char c : f) sym.insert(c);
		for (char c : i) sym.insert(c);
		for (char c : o) sym.insert(c);
		return sym.size();
	}
	size_t step2() const {
		size_t res = 0;
		res += summa(d);
		res += summa(m);
		res *= 64;
		return res;
	}
	size_t step3() const {
		auto c = f[0];
		auto w = 256;
		if ('A' <= c && c <= 'Z') {
			w *= c - 'A' + 1;
		} else {
			w *= c - 'a' + 1;
		}
		return w;
	}
	std::string step4(auto const &x) const {
		stringstream ss;
		ss << std::hex << std::uppercase << x;
		return ss.str();
	}
	std::string step5(auto const &s) const {
		int n = s.length();
		if (n < 3) {
			std::string r = s;
			while (r.length() < 3) {
				r = "0" + r;
			}
			return r;
		} else {
			return s.substr(n - 3);
		}
	}
public:
	std::string hash() const {
		/**
		cerr
			<< f << " "
			<< i << " "
			<< o << " "
			<< d << " "
			<< m << " "
			<< y << endl;
		/**/
		size_t sum = 0;
		sum += step1();
		sum += step2();
		sum += step3();
		auto h = step4(sum);
		h = step5(h);
		return h;
	}
public:
	static person make(std::vector<std::string> const &s) {
		return person {
			f: s[0],
			i: s[1],
			o: s[2],
			d: stoi(s[3]),
			m: stoi(s[4]),
			y: stoi(s[5])
		};
	}
};

int main() {
	auto n = read<int>();
	cin.ignore();
	std::string line;
	for (int i = 0; i < n; ++i) {
		std::getline(cin, line);
		auto p = person::make(split(line));
		cout << p.hash();
		if (i + 1 < n) {
			cout << " ";
		}
	}
	cout << endl;
	return 0;
}