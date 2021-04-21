using System;

    class BusinessPlan
    {
        public int howLong(int[] expense, int[] revenue, int[] ptime, int c, int d)
        {
            if (c >= d)
            {
                return 0;
            }
            int n = expense.Length;
            int[] best = new int[d + 1];
            for (int i = 0; i <= d; ++i)
            {
                best[i] = 1000000000;
            }
            best[c] = 0;
            for (int i = c; i < d; ++i)
                for (int j = 0; j < n; ++j)
                    if (expense[j] <= i)
                    {
                        int money = Math.Min(d, i + revenue[j] - expense[j]);
                        best[money] = Math.Min(best[money], best[i] + ptime[j]);
                    }
            return (best[d] == 1000000000 ? -1 : best[d]);
        }
        static void Main(string[] args)
        {
            BusinessPlan bp = new BusinessPlan();
            Console.WriteLine(bp.howLong(new int[] {1,4}, new int[] {2,10}, new int[] {1,2}, 1, 10));
            Console.WriteLine(bp.howLong(new int[] {11}, new int[] {20}, new int[] {10}, 10, 10));
            Console.WriteLine(bp.howLong(new int[] {11}, new int[] {20}, new int[] {10}, 10, 11));
            Console.WriteLine(bp.howLong(new int[] {1,1,1}, new int[] {3,4,8}, new int[] {1,2,3}, 1, 11));
            Console.WriteLine(bp.howLong(new int[] {99999,1,99998,2,99997,3,99996,4,99995,5}, 
                                            new int[] {100000,100000,100000,100,100000,100000,100000,100000,100000,100000}, 
                                            new int[] {1,9,1,10,1,9,1,9,1,9}, 2, 100));
        
            Console.ReadLine();
        }
    }
