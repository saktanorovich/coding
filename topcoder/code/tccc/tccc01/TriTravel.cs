using System;

class TriTravel
{
    public int bestWayDown(int[] triValues)
    {
        int n = triValues.Length;
        int[,] best = new int[n + 1, n + 1];
        for (int i = 0; i <= n; ++i)
            for (int j = 0; j <= n; ++j)
            {
                best[i, j] = -1000000000;
            }
        int pos = 0, level = 0;
        while (pos < n)
        {
            for (int i = pos, j = 0; i <= pos + level; ++i, ++j)
            {
                best[level, j] = triValues[i];
            }
            pos = pos + level + 1;
            ++level;
        }
        for (int i = 1; i < level; ++i)
            for (int j = 0; j < i + 1; ++j)
            {
                if (j == 0)
                    best[i, j] += best[i - 1, j];
                else if (j == i)
                    best[i, j] += best[i - 1, i - 1];
                else
                    best[i, j] += Math.Max(best[i - 1, j - 1], best[i - 1, j]);
            }
        int result = -1000000000;
        for (int j = 0; j < n + 1; ++j)
        {
            result = Math.Max(result, best[level - 1, j]);
        }
        return result;
    }
    static void Main(string[] args)
    {
        TriTravel tt = new TriTravel();
        Console.WriteLine(tt.bestWayDown(new int[] { 1, 6, 7, 4, -1, 6, 5, 8, 9, 0 }));
        Console.WriteLine(tt.bestWayDown(new int[] { -10 }));
        Console.WriteLine(tt.bestWayDown(new int[] { 3, 9, 0, 9, 0, 0, 9, 0, 0, 0, 9, 0, 0, 0, 100 }));
        Console.WriteLine(tt.bestWayDown(new int[] { 5, 9, 2, 15, 12, 0, 13, 16, 0, 0, 21, 17, 0, 0, 0, -99, -99, -99, -99, -99, 0 }));
        Console.WriteLine(tt.bestWayDown(new int[] { 6, 1, 1, 1, 1, 1, 1, 2, 1, 1 }));
        Console.WriteLine(tt.bestWayDown(new int[] { 7, 8, 5, 4, 3, 2, 7, 6, 7, 8, 1, 3, 9, 6, 2 }));

        Console.ReadLine();
    }
}
