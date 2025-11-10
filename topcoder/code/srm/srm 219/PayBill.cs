using System;
using System.Collections.Generic;

    class PayBill
    {
        public int[] whoPaid(int[] meals, int totalMoney)
        {
            int n = meals.Length;
            List<int> result = new List<int>();
            int[] can = new int[totalMoney + 1];
            can[0] = -1;
            for (int i = 0; i < n; ++i)
            {
                if (meals[i] <= totalMoney)
                {
                    for (int j = totalMoney - meals[i]; j >= 0; --j)
                    {
                        if (can[j] != 0 && can[j + meals[i]] == 0)
                        {
                            can[j + meals[i]] = i + 1;
                        }
                    }
                }
            }
            while (totalMoney != 0)
            {
                result.Add(can[totalMoney] - 1);
                totalMoney -= meals[can[totalMoney] - 1];
            }
            result.Sort();
            return result.ToArray();
        }
    }
