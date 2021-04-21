#include <map>
#include <string>
#include <sstream>
#include <cstdio>
using namespace std;

class NewMoneySystem
{
private:
	map<pair<long long, int>, long long> cache;
	long long run(long long n, int k)
	{
		if (n == 0)
			return 0;
		if (k == 1)
			return n;
		long long &result = cache[make_pair(n, k)];
		if (result == 0)
		{
			result = n;
			for (int i = 2; i <= 5; ++ i)
			{
				result = min(result, n % i + run(n / i, k - 1));
			}
		}
		return result;
	}
public:
	long long chooseBanknotes(string num, int k)
	{
		istringstream is(num);
		long long n;
		is >> n;
		return run(n, k);
	}
};

int main()
{
	NewMoneySystem nms;
	printf("%I64u\n", nms.chooseBanknotes(string("924323565426323626"), 50));
	return 0;
}