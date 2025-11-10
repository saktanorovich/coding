using System;

public class PageNumbers
{
    public int[] getCounts(int n)
    {
        int[] digits = new int[n.ToString().Length];
        for (int i = 0; i < digits.Length; ++i)
        {
            digits[i] = int.Parse(n.ToString()[i].ToString());
        }
        int[] result = new int[10];
        for (int len = 1; len < digits.Length; ++len)
        {
            for (int d = 1; d < 10; ++d)
            {
                result[d] += getPow10(len - 1);
            }
            for (int p = 2; p <= len; ++p)
            {
                for (int d = 0; d < 10; ++d)
                {
                    int left = 9 * getPow10(p - 2);
                    int right = getPow10(len - p);
                    result[d] += left * right;
                }
            }
        }
        for (int d = 1; d < digits[0]; ++d)
        {
            result[d] += getPow10(digits.Length - 1);
        }
        result[digits[0]] += getNumber(digits, 1, digits.Length - 1) + 1;
        for (int p = 1; p < digits.Length; ++p)
        {
            for (int d = 0; d < 10; ++d)
            {
                if (d < digits[p])
                {
                    int left = getNumber(digits, 0, p - 1) - getPow10(p - 1) + 1;
                    int right = getPow10(digits.Length - p - 1);
                    result[d] += left * right;
                }
                else if (d == digits[p])
                {
                    {
                        int left = getNumber(digits, 0, p - 1) - getPow10(p - 1);
                        int right = getPow10(digits.Length - p - 1);
                        result[d] += left * right;
                    }
                    {
                        int left = 1;
                        int right = getNumber(digits, p + 1, digits.Length - 1) + 1;
                        result[d] += left * right;
                    }
                }
                else if (d > digits[p])
                {
                    int left = getNumber(digits, 0, p - 1) - getPow10(p - 1);
                    int right = getPow10(digits.Length - p - 1);
                    result[d] += left * right;
                }
            }
        }
        return result;
    }

    private int getNumber(int[] digits, int lo, int hi)
    {
        int result = 0;
        for (int i = lo; i <= hi; ++i)
        {
            result = (result * 10) + digits[i];
        }
        return result;
    }

    private int getPow10(int pow)
    {
        int result = 1;
        for (int i = 1; i <= pow; ++i)
        {
            result *= 10;
        }
        return result;
    }

    // BEGIN CUT HERE
    public void run_test(int Case) { if ((Case == -1) || (Case == 0)) test_case_0(); if ((Case == -1) || (Case == 1)) test_case_1(); if ((Case == -1) || (Case == 2)) test_case_2(); if ((Case == -1) || (Case == 3)) test_case_3(); if ((Case == -1) || (Case == 4)) test_case_4(); }
	private void verify_case(int Case, int[] Expected, int[] Received) {
		Console.Write("Test Case #" + Case + "...");
		if (equal_arrays(Expected, Received)) 
			Console.WriteLine("PASSED"); 
		else { 
			Console.WriteLine("FAILED"); 
			Console.WriteLine("\tExpected: " + print_array(Expected)); 
			Console.WriteLine("\tReceived: " + print_array(Received)); } }
	string print_array(int[] V) {
		System.Text.StringBuilder builder = new System.Text.StringBuilder();
		builder.Append("{ ");
		foreach (int o in V) {
			builder.Append('\"');
			builder.Append(o.ToString());
			builder.Append("\",");
		}
		builder.Append(" }");
		return builder.ToString();
	}
	bool equal_arrays(int[] a, int[]b) {
		if (a.Length != b.Length) return false;
		for (int i = 0; i < a.Length; ++i) if (a[i] != b[i]) return false;
		return true;
	}
	private void test_case_0() { int Arg0 = 7; int[] Arg1 = new int[]{0, 1, 1, 1, 1, 1, 1, 1, 0, 0 }; verify_case(0, Arg1, getCounts(Arg0)); }
	private void test_case_1() { int Arg0 = 11; int[] Arg1 = new int[]{1, 4, 1, 1, 1, 1, 1, 1, 1, 1 }; verify_case(1, Arg1, getCounts(Arg0)); }
	private void test_case_2() { int Arg0 = 19; int[] Arg1 = new int[]{1, 12, 2, 2, 2, 2, 2, 2, 2, 2 }; verify_case(2, Arg1, getCounts(Arg0)); }
	private void test_case_3() { int Arg0 = 999; int[] Arg1 = new int[]{189, 300, 300, 300, 300, 300, 300, 300, 300, 300 }; verify_case(3, Arg1, getCounts(Arg0)); }
	private void test_case_4() { int Arg0 = 543212345; int[] Arg1 = new int[]{429904664, 541008121, 540917467, 540117067, 533117017, 473117011, 429904664, 429904664, 429904664, 429904664 }; verify_case(4, Arg1, getCounts(Arg0)); }

// END CUT HERE

	// BEGIN CUT HERE
	[STAThread]
	public static void Main(string[] args)
	{
        PageNumbers item = new PageNumbers();
		item.run_test(-1);
		Console.ReadLine();
	}
    // END CUT HERE
}
