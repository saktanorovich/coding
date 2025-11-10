using System;
using System.Collections.Generic;

    class SpiralConstruction
    {
        class Point
        {
            public int x, y;

            public Point(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            public static Point operator -(Point p1, Point p2)
            {
                return new Point(p1.x - p2.x, p1.y - p2.y);
            }
        }

        private int n;
        private int result;
        private List<Point> p;

        private int sp(Point p1, Point p2)
        {
            return p1.x * p2.x + p1.y * p2.y;
        }

        private int vp(Point p1, Point p2)
        {
            return p1.x * p2.y - p1.y * p2.x;
        }

        private void run(int set, int last, int prev, int count)
        {
            result = Math.Max(result, count);
            int who = prev;
            for (int i = 0; i <= n; ++i)
            {
                if ((set & (1 << i)) != 0 && i != last && i != prev)
                {
                    int rotateToWho = vp(p[who] - p[last], p[i] - p[last]);
                    if (rotateToWho < 0)
                    {
                        who = i;
                    }
                }
            }
            for (int i = 0; i <= n; ++i)
            {
                if ((set & (1 << i)) == 0)
                {
                    int rotateToWho = vp(p[who] - p[last], p[i] - p[last]);
                    int rotateToLast = vp(p[last] - p[prev], p[i] - p[last]);
                    int scalar = sp(p[last] - p[prev], p[i] - p[last]);
                    int rotateWhoToLast = vp(p[last] - p[prev], p[who] - p[last]);
                    if (rotateToWho < 0)
                    {
                        if (rotateToLast > 0 || (rotateToLast == 0 && scalar > 0))
                        {
                            run(set ^ (1 << i), i, last, count + 1);
                        }
                    }
                    else if (rotateToWho == 0 && rotateWhoToLast == 0 && scalar > 0)
                    {
                        run(set ^ (1 << i), i, last, count + 1);
                    }
                }
            }
        }

        public int longestSpiral(string[] points)
        {
            n = points.Length;
            p = new List<Point>(Array.ConvertAll<string, Point>(points, delegate(string s)
            {
                int[] xy = Array.ConvertAll<string, int>(s.Split(new string[] { " " },
                    StringSplitOptions.RemoveEmptyEntries), delegate(string num)
                    {
                        return int.Parse(num);
                    });
                return new Point(xy[0], xy[1]);
            }));
            p.Add(new Point(0, 0));
            result = 0;
            for (int i = 0; i < n; ++i)
            {
                run((1 << n) ^ (1 << i), i, n, 1);
            }
            return result;
        }
    }
