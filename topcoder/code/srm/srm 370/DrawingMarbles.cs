using System;

class DrawingMarbles
{
    public double sameColor(int[] colors, int n)
    {
        double result = 0;
        double all = 0;
        for (int i = 0; i < colors.Length; ++i)
        {
            all += colors[i];
        }
        for (int i = 0; i < colors.Length; ++i)
        {
            if (colors[i] >= n)
            {
                double p = 1;
                for (int j = 1; j <= n; ++j)
                {
                    p *= (colors[i] - j + 1) / (all - j + 1);
                }
                result += p;
            }
        }
        return result;
    }
}
