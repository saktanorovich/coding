#include <vector>
#include <string>
#include <sstream>
#include <cmath>
#include <iostream>
using namespace std;

	class TreasuresPacking
    {
	private:
		vector<double> dp;
		vector<bool> used;
		vector<int> wd, wnd, cd, cnd;
	private:
		double getcost(int w)
		{
			double curw = 0, curc = 0;
			for (int i = 0; i < (int)wd.size(); ++i)
			{
				if (w - curw >= wd[i])
				{
					curw += wd[i];
					curc += cd[i];
				}
				else
				{
					curc += (double)cd[i] * (w - curw) / wd[i];
					curw += w - curw;
				}
			}
			return (w == curw ? curc : 0);
		}
	public:
		double maximizeCost(vector<string> treasures, int w)
        {
			int sum = 0;
			for (int i = 0; i < (int)treasures.size(); ++i)
			{
				istringstream iss(treasures[i]);
				int c, w;
				char d;
				iss >> w >> c >> d;
				if (d == 'Y')
				{
					wd.push_back(w);
					cd.push_back(c);
				}
				else
				{
					wnd.push_back(w);
					cnd.push_back(c);
				}
				sum += w;
			}
			w = min(w, sum);
			for (int i = 0; i < (int)wd.size(); ++i)
				for (int j = i + 1; j < (int)wd.size(); ++j)
				{
					if (cd[j] * wd[i] > cd[i] * wd[j])
					{
						int temp = cd[i]; cd[i] = cd[j]; cd[j] = temp;
						temp = wd[i]; wd[i] = wd[j]; wd[j] = temp;
					}
				}
			dp.assign(w + 1, 0);
			used.assign(w + 1, false);
			used[0] = true;
			for (int i = 0; i < (int)wnd.size(); ++i)
			{
				if (wnd[i] <= w)
				{
					for (int j = w - wnd[i]; j >= 0; --j)
					{
						if (used[j])
						{
							dp[j + wnd[i]] = max(dp[j + wnd[i]], dp[j] + cnd[i]);
							used[j + wnd[i]] = true;
						}
					}
				}
			}
			double result = 0;
			for (int i = 0; i <= w; ++ i)
			{
				result = max(result, dp[i] + getcost(w - i));
			}
            return result;
        }
    };

int main()
{	
	return 0;
}


