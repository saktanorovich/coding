using System;
using System.Collections.Generic;

    class CropCircles
    {
        private long vectorProduct(int x1, int y1, int x2, int y2)
        {
            return x1 * y2 - x2 * y1;
        }
        private long dist(int x1, int y1, int x2, int y2)
        {
            return (x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2);
        }
        public int countCircles(int[] x, int[] y)
        {
            int n = x.Length;
            bool[,,] used = new bool[n, n, n];
            int result = 0;
            for (int i = 0; i < n; ++i)
                for (int j = i + 1; j < n; ++j)
                    for (int k = j + 1; k < n; ++k)
                        if (!used[i, j, k])
                        {
                            if (vectorProduct(x[j] - x[i], y[j] - y[i], x[k] - x[i], y[k] - y[i]) == 0)
                            {
                                used[i, j, k] = true;
                                continue;
                            }
                            result = result + 1;
                            List<int> list = new List<int>();
                            list.Add(i);
                            list.Add(j);
                            list.Add(k);
                            for (int l = k + 1; l < n; ++l)
                            {
                                List<long> d = new List<long>();
                                d.Add(dist(x[i], y[i], x[j], y[j]) * dist(x[k], y[k], x[l], y[l]));
                                d.Add(dist(x[j], y[j], x[k], y[k]) * dist(x[l], y[l], x[i], y[i]));
                                d.Add(dist(x[i], y[i], x[k], y[k]) * dist(x[j], y[j], x[l], y[l]));
                                d.Sort();
                                if ((d[0] + d[1] - d[2]) * (d[0] + d[1] - d[2]) == 4 * d[0] * d[1])
                                {
                                    list.Add(l);
                                }
                            }
                            for (int i1 = 0; i1 < list.Count; ++i1)
                                for (int j1 = 0; j1 < list.Count; ++j1)
                                    for (int k1 = 0; k1 < list.Count; ++k1)
                                    {
                                        used[list[i1], list[j1], list[k1]] = true;
                                    }
                        }
            return result;
        }
        static void Main(string[] args)
        {
            CropCircles cc = new CropCircles();
            Console.WriteLine(cc.countCircles(new int[] { 1, 2, 1, 2, 8 }, new int[] { 2, 1, 8, 9, 9 }));
            Console.WriteLine(cc.countCircles(new int[] { 0, 4, 7 }, new int[] { 3, 3, 3 }));
            Console.WriteLine(cc.countCircles(new int[] { 0, 10, 10, 10, 20 }, new int[] { 10, 0, 10, 20, 10 }));
            Console.WriteLine(cc.countCircles(new int[] { 0, 10, 11, 10, 21 }, new int[] { 10, 0, 11, 20, 10 }));

            Console.ReadLine();
        }
    }

