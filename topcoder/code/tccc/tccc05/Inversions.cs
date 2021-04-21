using System;
using System.Collections.Generic;

class Inversions
{
    public int[] createExample(int n, int inv)
    {
        if (inv > n * (n - 1) / 2)
        {
            return new int[0];
        }
        List<int> p = new List<int>();
        for (int i = 1; i <= n; ++i)
        {
            p.Add(i);
        }
        List<int> result = new List<int>();
        int left = 0;
        for (int i = 0; i < n; ++i)
        {
            int inva = (n - i) * (n - i - 1) / 2;
            if (inva > inv)
            {
                result.Add(p[i]);
            }
            else if (inva == inv)
            {
                left = i;
                break;
            }
            else if (inva < inv)
            {
                left = i - 1;
                result.RemoveAt(result.Count - 1);
                break;
            }
        }
        if ((n - left) * (n - left - 1) / 2 == inv)
        {
            for (int i = n - 1; i >= left; --i)
            {
                result.Add(p[i]);
            }
        }
        else
        {
            for (int i = 0; i < left; ++i)
            {
                p.RemoveAt(0);
            }
            n = p.Count;
            for (int i = 0; i < n - 1; ++i)
            {
                int inva = (n - i - 1) * (n - i - 2) / 2;
                for (int j = 0; j < n; ++j)
                {
                    if (inva + j == inv)
                    {
                        inv -= j;
                        result.Add(p[j]);
                        p.RemoveAt(j);
                        break;
                    }
                }
            }
            result.Add(p[0]);
        }
        return result.ToArray();
    }
}
