using System;

public class SquareFreeNumbers
{
    public int getCount(long min, long max)
    {
        int n = (int)(max - min + 1);
        bool[] sieve = new bool[n];
        for (long i = 2; i * i <= max; ++i)
        {
            long square = i * i;
            long times = min / square;
            for (; times * square < min; ++times) ;
            long first = times * square;
            for (long j = first; j <= max; j += square)
            {
                sieve[(int)(j - min)] = true;
            }
        }
        int result = 0;
        for (int i = 0; i < n; ++i)
        {
            if (!sieve[i])
            {
                ++result;
            }
        }
        return result;
    }

    // BEGIN CUT HERE
    public void run_test(int Case) { if ((Case == -1) || (Case == 0)) test_case_0(); if ((Case == -1) || (Case == 1)) test_case_1();
        if ((Case == -1) || (Case == 2)) test_case_2();
        if ((Case == -1) || (Case == 3)) test_case_3();
    }
	private void verify_case(int Case, int Expected, int Received) {
		Console.Write("Test Case #" + Case + "...");
		if (Expected == Received) 
			Console.WriteLine("PASSED"); 
		else { 
			Console.WriteLine("FAILED"); 
			Console.WriteLine("\tExpected: \"" + Expected + '\"');
			Console.WriteLine("\tReceived: \"" + Received + '\"'); } }
	private void test_case_0() { long Arg0 = 1l; long Arg1 = 10l; int Arg2 = 7; verify_case(0, Arg2, getCount(Arg0, Arg1)); }
	private void test_case_1() { long Arg0 = 15l; long Arg1 = 15l; int Arg2 = 1; verify_case(1, Arg2, getCount(Arg0, Arg1)); }
	private void test_case_2() { long Arg0 = 1l; long Arg1 = 1000l; int Arg2 = 608; verify_case(2, Arg2, getCount(Arg0, Arg1)); }
    private void test_case_3() { long Arg0 = 2178l; long Arg1 = 2190l; int Arg2 = 8; verify_case(3, Arg2, getCount(Arg0, Arg1)); }

// END CUT HERE

	// BEGIN CUT HERE
	[STAThread]
	public static void Main(string[] args)
	{
		SquareFreeNumbers item = new SquareFreeNumbers();
		item.run_test(3);
		Console.ReadLine();
	}
	// END CUT HERE
}
