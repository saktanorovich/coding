using System;
using System.Collections.Generic;

class SolvePolynomial
{
    private int n;
    private int[] a;
    private List<int> list;
    private void check(int x)
    {
        long result = a[0];
        for (int i = 1; i <= n; ++i)
        {
        	if (result % x != 0)
        	{
        		return;
        	}
        	result = result / x + a[i];
        }
        if (result == 0)
        {
            if (!list.Contains(x))
            {
                list.Add(x);
            }
        }
    }
    public int[] integerRoots(int[] x, int[] y, int n)
    {
        this.n = n;
        int lx = x.Length;
        int ly = y.Length;
        a = new int[n + 1];
        for (int i = 0; i <= n; ++i)
        {
            int p = i % lx;
            int q = (i + y[i % ly]) % lx;
            a[i] = x[p];
            x[p] = x[q];
            x[q] = a[i];
        }
        list = new List<int>();
        if (a[0] == 0)
        {
            list.Add(0);
        }
        for (int i = 0; i <= n; ++i)
        {
            if (a[i] != 0)
            {
                for (int d = 1; d * d <= Math.Abs(a[i]); ++d)
                {
                    if (a[i] % d == 0)
                    {
                        check(d);
                        check(a[i] / d);
                        check(-d);
                        check(-a[i] / d);
                    }
                }
                break;
            }
        }
        list.Sort();
        return list.ToArray();
    }
}
