#include <iostream>
#include <memory>
#include <vector>
#include <algorithm>
#include <string>

using namespace std;

typedef unsigned int uint;
typedef vector<int> tdecimal;
typedef vector<int> tbinary;

tdecimal s, d;
string c;
tbinary bin1, bin2;
int case_id = 0;

void load(tdecimal& dec) 
{
	char c;
	dec.clear();
	do 
	{
		cin.get(c);
		if (c > ' ')
			dec.push_back((int) (c) - 48);
		else
			break;
	} while (1);
}

bool load_nums(tdecimal& dec1, tdecimal& dec2) 
{
	load(dec1);
	load(dec2);
	if (dec1.size() == 1 && dec2.size() == 1 && (dec1[0] + dec2[0] == 0))
	{
		return false;
	}
	return true;
}

void save(int id, string& dec, bool flag = true) {
	cout << "Case Number " << id << ": ";
	if (flag)
		cout << dec;
	else
		cout << "impossible";
	cout << endl;
}

int div2(tdecimal& dec, tdecimal& x) 
{
	uint n = (uint)dec.size(), i = 0;
	int rem = dec[n - 1] % 2, what = dec[0];

	x.clear();
	if (what < 2)
	{
		if (n > 1)
			what = 10 * what + dec[++ i];
	}
	i ++;
	for ( ; i < n; ++ i)
	{
		x.push_back(what / 2);
		what = (what % 2) * 10 + dec[i];
	}
	x.push_back(what / 2);
	return rem;
}

void dec2bin(tdecimal& dec, tbinary& bin) 
{
	tdecimal x;

	bin.clear();
	while (1)
	{
		bin.push_back(div2(dec, x));
		if (x.size() == 1 && x[0] < 2)
		{
			break;
		}
		dec.assign(x.begin(), x.end());
	}
	if (x[0] == 1)
	{
		bin.push_back(1);
	}
	reverse(bin.begin(), bin.end());
}

void mul2(tdecimal& dec) {
	tdecimal x;
	int s = 0;
	for (int i = (int)dec.size() - 1; i >= 0; -- i)
	{
		s += 2 * dec[i];
		x.push_back(s % 10);
		s /= 10;
	}
	if (s != 0)
	{
		x.push_back(s);
	}
	dec.assign(x.begin(), x.end());
	reverse(dec.begin(), dec.end());
}

void add(tdecimal& dec, int value) {
	tdecimal x;
	int s = 0, n = (int)dec.size();
	for (int i = n - 1; i >= 0; -- i)
	{
		s += dec[i];
		if (i == n - 1)
		{
            		s += value;
		}
		x.push_back(s % 10);
		s /= 10;
	}
	if (s != 0)
	{
		x.push_back(s);
	}
	dec.assign(x.begin(), x.end());
	reverse(dec.begin(), dec.end());
}

string bin2decstring(tbinary& bin) 
{
	tdecimal dec;
	dec.push_back(bin[0]);
	for (uint i = 1; i < bin.size(); ++ i)
	{
		mul2(dec);
		add(dec, bin[i]);
	}
	string result(dec.begin(), dec.end());
	for (uint i = 0; i < result.length(); ++ i)
	{
		result[i] += '0';
	}
	return result;
}

bool analyze(int cone, tbinary& c)
{
	uint size = c.size();
	c[1] = cone;
	for (uint i = 2; i < size; ++ i)
	{
		c[i] = (bin1[i - 2] + bin2[i - 2] + c[i - 2] + c[i - 1]) % 2;
	}
	return (bin1[size - 2] + c[size - 2] + c[size - 1]) % 2 == bin2[size - 2];
}

bool process(vector<int>& dec1, vector<int>& dec2, string& result) {
	result = "";
	dec2bin(dec1, bin1);
	dec2bin(dec2, bin2);
	uint size = max(bin1.size(), bin2.size());
	for (uint i = bin1.size(); i < size; ++ i)
	{
		bin1.insert(bin1.begin(), 0);
	}
	for (uint i = bin2.size(); i < size; ++ i)
	{
		bin2.insert(bin2.begin(), 0);
	}
	tbinary c;
	tdecimal d;
	string temp;
	bool res = false;
	
	c.assign(size + 1, 0);
	if (analyze(0, c))
	{
		res = true;
		result = bin2decstring(c);
	}

	c.assign(size + 1, 0);
	if (analyze(1, c))
	{
		temp = bin2decstring(c);
		if (result.length() == 0 || temp.length() < result.length())
			result = temp;
		else if (temp.length() == result.length() && temp < result)
			result = temp;
		res = true;
	}

	return res;
}

int main() {
	bool first = true;
	while (load_nums(s, d)) {
		if (!first)
		{
			cout << endl;
		}
		first = false;
		case_id ++;
		if (process(s, d, c))
			save(case_id, c);
		else
			save(case_id, c, false);
	}
	return 0;
}
