using System;
 
public class TheNewHouseDivOne
{
    public double distance(int[] x, int[] y, int k)
    {
        double result = double.MaxValue / 2;
        for (int i = -50; i <= +50; ++i)
        {
            for (int j = -50; j <= +50; ++j)
            {
                for (double dx = -0.5; dx <= 0.5; dx += 0.5)
                {
                    for (double dy = -0.5; dy <= 0.5; dy += 0.5)
                    {
                        result = Math.Min(result, get_kth_distance(x, y, i + dx, j + dy, k));
                    }
                }
            }
        }
        return result;
    }

    private double get_kth_distance(int[] x, int[] y, double nx, double ny, int k)
    {
        double[] d = new double[x.Length];
        for (int i = 0; i < x.Length; ++i)
        {
            d[i] = Math.Abs(x[i] - nx) + Math.Abs(y[i] - ny);
        }
        Array.Sort(d);
        return d[k - 1];
    }


}


// Powered by FileEdit
// Powered by TZTester 1.01 [25-Feb-2003] [modified for C# by Petr]
// Powered by CodeProcessor
