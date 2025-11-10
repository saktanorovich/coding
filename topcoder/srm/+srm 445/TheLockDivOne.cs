using System;

public class TheLockDivOne
{
    public string password(int n, long k)
    {
        if (n == 0)
            return string.Empty;
        long total = (1L << n);
        if (k <= (total >> 1))
            return "0" + password(n - 1, k);
        else
        {
            string candidate1 = "1" + get_password(n - 1, total >> 1);
            long x = k - (total >> 1);
            if (x > 1)
            {
                string candidate2 = "1" + password(n - 1, x - 1);
                return candidate1.CompareTo(candidate2) > 0 ? candidate1 : candidate2;
            }
            return candidate1;
        }
    }

    private string get_password(int n, long k)
    {
        if (n == 0)
            return string.Empty;
        long total = (1L << n);
        if (k <= (total >> 1))
            return "0" + get_password(n - 1, k);
        else
        {
            long x = k - (total >> 1);
            if (x == 1)
                return "1" + get_password(n - 1, total >> 1);
            else
                return "1" + get_password(n - 1, x - 1);
        }
    }
}
