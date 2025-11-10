using System;

public class LightSwitches
{
    public long countPossibleConfigurations(string[] a)
    {
        int n = a[0].Length;
        int m = a.Length;
        bool[,] d = new bool[m, n];
        for (int s = 0; s < m; ++s)
            for (int b = 0; b < n; ++b)
            {
                d[s, b] = (a[s][b] == 'Y');
            }
        long res = 1;
        bool[] used = new bool[m];
        for (int b = 0; b < n; ++b)
            for (int s = 0; s < m; ++s)
            {
                if (!used[s] && d[s, b])
                {
                    used[s] = true;
                    for (int ss = 0; ss < m; ++ss)
                    {
                        if (d[ss, b] && ss != s)
                        {
                            for (int bb = b + 1; bb < n; ++bb)
                            {
                                d[ss, bb] ^= d[s, bb];
                            }
                        }
                    }
                    res = res * 2;
                    break;
                }
            }
        return res;
    }
}