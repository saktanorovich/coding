using System;

    class RugSizes
    {
        public int rugCount(int area)
        {
            int result = 0;
            for (int a = 1; a * a <= area; ++a)
            {
                int b = area / a;
                if (area % a == 0)
                {
                    if (a == b)
                    {
                        result++;
                    }
                    else if (a % 2 != 0 || b % 2 != 0)
                    {
                        result++;
                    }
                }
            }
            return result;
        }
        static void Main(string[] args)
        {
        }
    }
