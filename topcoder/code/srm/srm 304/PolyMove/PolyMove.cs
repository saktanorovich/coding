using System;

    class PolyMove
    {
        private int n;
        private int next(int i)
        {
            return (i + 1 + n) % n;
        }
        private int prev(int i)
        {
            return (i - 1 + n) % n;
        }
        double dist(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
        }
        public double addedArea(int[] x, int[] y)
        {
            n = x.Length;
            double result = 0;
            for (int i = 0; i < n; ++i)
            {
                double[] dp = new double[n];
                dp[i] = dist(x[next(i)], y[next(i)], x[prev(i)], y[prev(i)]) / 2;
                dp[next(i)] = dp[i];
                for (int j = next(next(i)); j != prev(i); j = next(j))
                {
                    dp[j] = Math.Max(dp[prev(j)], dp[prev(prev(j))] + dist(x[next(j)], y[next(j)], x[prev(j)], y[prev(j)]) / 2);
                }
                result = Math.Max(result, dp[prev(prev(i))]);
            }
            return result;
        }
        static void Main(string[] args)
        {
            PolyMove pm = new PolyMove();
            Console.WriteLine(pm.addedArea(new int[] { 0, 1, 2 }, new int[] { 0, 1, 0 }));
            Console.WriteLine(pm.addedArea(new int[] { 0, 1, 1, 0 }, new int[] { 1, 1, 0, 0 }));
            Console.WriteLine(pm.addedArea(new int[] { 25, 50, 50, 0, 0 }, new int[] { 1000, 50, 0, 0, 50 }));
            Console.WriteLine(pm.addedArea(new int[] { 0, 10, 0 }, new int[] { 10, 0, 0 }));

            Console.ReadLine();
        }
    }
