using System;

class IntegerPalindrome
{
    private int len;
    private long[,] dp;
    private long ans;
    private string digits;
    private long f(int l, int d)
    {
        if (dp[l, d] == -1)
        {
            dp[l, d] = 0;
            for (int d1 = 0; d1 < 10; ++d1)
            {
                dp[l, d] += f(l - 2, d1);
            }
        }
        return dp[l, d];
    }
    private void build(int llen, long count)
    {
        if (count == 0)
        {
            if (digits.Length != len / 2 + len % 2)
            {
                for (int i = digits.Length + 1; i <= len / 2 + len % 2; ++i)
                {
                    digits += "9";
                }
            }
            string reverseDigits = string.Empty;
            for (int i = (len % 2 == 0) ? digits.Length - 1 : digits.Length - 2; i >= 0; -- i)
            {
                reverseDigits += digits[i];
            }
            ans = long.Parse(digits + reverseDigits);
            return;
        }
        int d1 = (llen == len) ? 1 : 0;
        long temp = 0;
        for (int d = d1; d < 10; ++d)
        {
            temp += f(llen, d);
            if (temp >= count)
            {
                digits += d.ToString();
                build(llen - 2, (temp == count) ? 0 : count - (d - d1) * f(llen, d));
                return;
            }
        }
    }
    public long findByIndex(int k)
    {
        ++k;
        if (k < 10)
        {
            return k;
        }
        dp = new long[18, 10];
        for (int l = 0; l < 18; ++ l)
            for (int d = 0; d < 10; ++d)
            {
                dp[l, d] = -1;
            }
        for (int d = 0; d < 10; ++d)
        {
            dp[0, d] = 0;
            dp[1, d] = dp[2, d] = 1;
        }
        long count = 0, temp = 0;
        len = 0;
        while (count <= k)
        {
            len++;
            temp = count;
            for (int d = 1; d < 10; ++d)
            {
                count += f(len, d);
                if (count > k)
                {
                    break;
                }
            }
        }
        if (temp == k)
        {
            len--;
        }
        count = temp;
        digits = string.Empty;
        build(len, k - count);
        return ans;
    }
    static void Main(string[] args)
    {
        IntegerPalindrome ip = new IntegerPalindrome();
        Console.WriteLine(ip.findByIndex(199999997));
        Console.WriteLine(ip.findByIndex(999999997));
        Console.WriteLine(ip.findByIndex(1000000000));
        Console.WriteLine(ip.findByIndex(23746));
        Console.WriteLine(ip.findByIndex(235));
        Console.WriteLine(ip.findByIndex(19));
        Console.WriteLine(ip.findByIndex(9));
        Console.WriteLine(ip.findByIndex(8));

        Console.ReadLine();
    }
}
