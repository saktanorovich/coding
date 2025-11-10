using System;
using System.Collections.Generic;

    class KthElement
    {
        private int ones(int x)
        {
            int result = 0;
            while (x > 0)
            {
                result++;
                x -= x & (-x);
            }
            return result;
        }
        public int find(int a, int b, int k)
        {
            if (k == 0)
            {
                return 0;
            }
            Dictionary<int, int> map = new Dictionary<int, int>();
            List<int> seq = new List<int>();
            seq.Add(0);
            map[0] = 0;
            int x0 = 0;
            for (int i = 1; ; ++i)
            {
                int x = a * ones(x0) + b;
                if (!map.ContainsKey(x))
                {
                    map[x] = i;
                    seq.Add(x);
                    if (i == k)
                    {
                        return x;
                    }
                }
                else
                {
                    return seq[(k - map[x]) % (i - map[x]) + map[x]];
                }
                x0 = x;
            }
        }
        static void Main(string[] args)
        {
            KthElement ke = new KthElement();
            Console.WriteLine(ke.find(0, 12, 5));
            Console.WriteLine(ke.find(1, 7, 15));
            Console.WriteLine(ke.find(15, 21, 500000001));
            Console.WriteLine(ke.find(79, 4, 700000000));
            Console.WriteLine(ke.find(293451, 765339, 900000000));
            Console.WriteLine(ke.find(60, 66, 1));
            Console.ReadLine();
        }
    }
