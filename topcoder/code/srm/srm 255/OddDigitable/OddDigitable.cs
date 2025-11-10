using System;
using System.Collections.Generic;

    class OddDigitable
    {
        public string findMultiple(int n, int m)
        {
            Queue<string> odd = new Queue<string>();
            Queue<int> rem = new Queue<int>();
            bool[] used = new bool[n];
            odd.Enqueue("");
            rem.Enqueue(0);
            bool first = true;
            while (odd.Count != 0)
            {
                int r = rem.Dequeue();
                string num = odd.Dequeue();
                if (r == m && !num.Equals("0") && !first)
                {
                    return num;
                }
                for (int i = 1; i <= 9; i += 2)
                {
                    int k = ((r * (10 % n)) % n + i % n) % n;
                    if (!used[k])
                    {
                        odd.Enqueue(num + i.ToString());
                        rem.Enqueue(k);
                        used[k] = true;
                    }
                }
                first = false;
            }
            return "-1";
        }
        static void Main(string[] args)
        {
            OddDigitable od = new OddDigitable();
            Console.WriteLine(od.findMultiple(10, 7));
            Console.WriteLine(od.findMultiple(22, 12));
            Console.WriteLine(od.findMultiple(29, 0));
            Console.WriteLine(od.findMultiple(5934, 2735));
            Console.ReadLine();
        }
    }
