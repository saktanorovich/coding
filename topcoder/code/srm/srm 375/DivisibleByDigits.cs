using System;
using System.Collections.Generic;

    class DivisibleByDigits
    {
        public long getContinuation(int n)
        {
            List<int> digits = new List<int>();
            string ns = n.ToString();
            for (int x = n; x > 0; x /= 10)
            {
                if (x % 10 != 0 && x % 10 != 1)
                {
                    digits.Add(x % 10);
                }
            }
            long result = n;
            long ten = 1;
            while (true)
            {
                for (int i = 0; i < ten; ++i)
                {
                    long x = result * ten + i;
                    bool ok = true;
                    foreach (int d in digits)
                    {
                        if (x % d != 0)
                        {
                            ok = false;
                            break;
                        }
                    }
                    if (ok)
                    {
                        return x;
                    }
                }
                ten *= 10;
            }
        }
        static void Main(string[] args)
        {
            DivisibleByDigits dbd = new DivisibleByDigits();
            Console.WriteLine(dbd.getContinuation(13));
            Console.WriteLine(dbd.getContinuation(648));
            Console.WriteLine(dbd.getContinuation(566));
            Console.WriteLine(dbd.getContinuation(1000000000));
            Console.WriteLine(dbd.getContinuation(83));
            Console.WriteLine(dbd.getContinuation(987654321));

            Console.ReadLine();
        }
    }
