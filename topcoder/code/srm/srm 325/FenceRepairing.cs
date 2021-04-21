using System;

    class FenceRepairing
    {
        public double calculateCost(string[] boards)
        {
            string fence = string.Empty;
            for (int i = 0; i < boards.Length; ++i)
            {
                fence += boards[i];
            }
            double[] cost = new double[fence.Length];
            int[] a = new int[fence.Length];
            if (fence[0].Equals('X'))
            {
                cost[0] = 1.0;
                a[0] = 1;
            }
            for (int i = 1; i < fence.Length; ++i)
            {
                a[i] = a[i - 1] + (fence[i].Equals('X') ? 1 : 0);
                cost[i] = 1000000000000.0;
                for (int j = 0; j < i; ++j)
                {
                    cost[i] = Math.Min(cost[i], cost[j] + (a[i] - a[j] == 0 ? 0 : Math.Sqrt(i - j)));
                }
                cost[i] = Math.Min(cost[i], Math.Sqrt(i + 1));
            }
            return cost[fence.Length - 1];
        }
        static void Main(string[] args)
        {
            FenceRepairing fr = new FenceRepairing();
            Console.WriteLine(fr.calculateCost(new string[] { "X.X...X.X" }));
            Console.WriteLine(fr.calculateCost(new string[] { "X.X.....X" }));
            Console.WriteLine(fr.calculateCost(new string[] { "X.......", "......XX", ".X......", ".X...X.." }));
            Console.WriteLine(fr.calculateCost(new string[] {".X.......X", "..........", "...X......", "...X..X...", "..........",
 "..........", "..X....XX.", ".........X", "XXX", ".XXX.....X"}));

            Console.ReadLine();
        }
    }
