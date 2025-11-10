#include <vector>
#include <cmath>
#include <iostream>

using namespace std;

struct Point
{
	int x, y;
	Point(int xx, int yy)
	{
		x = xx;
		y = yy;
	}
};

class PolygonCover
{
	int n;
	vector<Point> p;
	vector<double> dp;
private:
	double area(Point p1, Point p2, Point p3)
	{
		double x2 = p2.x - p1.x, y2 = p2.y - p1.y;
		double x3 = p3.x - p1.x, y3 = p3.y - p1.y;
		return abs(x2 * y3 - x3 * y2) / 2;
	}
public:
	double getArea(vector<int> x, vector<int> y)
	{
		n = x.size();
		for (int i = 0; i < n; ++i)
		{
			p.push_back(Point(x[i], y[i]));
		}
		dp.assign(1 << n, 1e100);
		dp[0] = 0;
		for (int set = 0; set < 1 << n; ++set)
		{
			for (int i = 0; i < n; ++i)
				for (int j = 0; j < n; ++j)
					for (int k = 0; k < n; ++k)
					{
						if (i != j && i != k && j != k)
						{
							int sset = set | (1 << i) | (1 << j) | (1 << k);
							dp[sset] = min(dp[sset], dp[set] + area(p[i], p[j], p[k]));
						}
					}
		}
		return dp[(1 << n) - 1];
	}
};

int main()
{
	{
		PolygonCover pc;
		int xx[] = { 0, 10, 0, -10 };
		int yy[] = { -10, 0, 10, 0 };
		vector<int> x(xx, xx + 4);
		vector<int> y(yy, yy + 4);
		cout << pc.getArea(x, y) << endl; // 200.0
	}

	{
		PolygonCover pc;
		int xx[] = { -1, 2, -2, 0 };
		int yy[] = { -1, 0, 2, -1 };
		vector<int> x(xx, xx + 4);
		vector<int> y(yy, yy + 4);
		cout << pc.getArea(x, y) << endl; // 2.0
	}

	{
		PolygonCover pc;
		int xx[] = { 2, 0, -2, -1, 1, 0 };
		int yy[] = { 0, 2, 0, -2, -2, 1 };
		vector<int> x(xx, xx + 6);
		vector<int> y(yy, yy + 6);
		cout << pc.getArea(x, y) << endl; // 3.0
	}

	{
		PolygonCover pc;
		int xx[] = { 1, 0, -4, 0, 1, 4 };
		int yy[] = { 1, 4, 0, -4, -1, 0 };
		vector<int> x(xx, xx + 6);
		vector<int> y(yy, yy + 6);
		cout << pc.getArea(x, y) << endl; // 6.0
	}

	return 0;
}
