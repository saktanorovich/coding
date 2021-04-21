using System;

public class TourCounting
{
	int n;
	int module;
	long[,] a;
	long[,] ones(int n)
	{
		long[,] c = new long[n, n];
		for (int i = 0; i < n; ++ i)
			c[i, i] = 1;
		return c;
	}
	long[,] zero(int n)
	{
		return new long[n, n];
	}
	long[,] mul(long[,] a, long[,] b, int n)
	{
		long[,] c = new long[n, n];
		for (int i = 0; i < n; ++ i)
			for (int j = 0; j < n; ++ j)
				for (int k = 0; k < n; ++ k)
					c[i, j] = (c[i, j] + a[i, k] * b[k, j]) % module;
		return c;
	}
	long[,] sum(long[,] a, long[,] b, int n)
	{
		long[,] c = new long[n, n];
		for (int i = 0; i < n; ++ i)
			for (int j = 0; j < n; ++ j)
				c[i, j] = (a[i, j] + b[i, j]) % module;
		return c;
	}
	long[,] pow(long[,] a, int n, int p)
	{
		if (p == 0)
			return ones(n);
		else if (p % 2 != 0)
			return mul(pow(a, n, p - 1), a, n);
		else
			return pow(mul(a, a, n), n, p / 2);
	}
	long[,] gssum(long[,] a, int n, int h)
	{
		if (h == 0)
			return zero(n);
		else if (h % 2 != 0)
			return sum(a, mul(a, gssum(a, n, h - 1), n), n);
		else
		{
			long[,] b = gssum(a, n, h / 2);
			return sum(b, mul(pow(a, n, h / 2), b, n), n);
		}
	}
	public int countTours(string[] g, int k, int m)
	{
		n = g.Length;
		module = m;
		a = new long[n, n];
		for (int i = 0; i < n; ++ i)
			for (int j = 0; j < n; ++ j)
				if (g[i][j] == 'Y')
					a[i, j] = 1;
		long[,] d = gssum(a, n, k - 1);
		long res = 0;
		for (int i = 0; i < n; ++ i)
			res = (res + d[i, i]) % module;
		return (int)res;
	}
}
 
 
 
