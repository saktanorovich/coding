using System;
using System.Collections.Generic;
 
public class Shopping
{
    public int minNumber(int X, int[] coin)
    {
        int[] dp = new int[X + 1];
        dp[0] = 0;
        for (int i = 1; i <= X; ++i)
        {
            dp[i] = 2 * X;
            for (int j = 0; j < coin.Length; ++j)
            {
                if (coin[j] <= i)
                {
                    int up = i - coin[j];
                    if (coin[j] > up)
                    {
                        up = coin[j] - 1;
                    }
                    dp[i] = Math.Min(dp[i], dp[up] + 1);
                }
            }
            if (dp[X] > X)
            {
                return -1;
            }
        }
        return dp[X];
    }

    // BEGIN CUT HERE
    public void run_test(int Case) { if ((Case == -1) || (Case == 0)) test_case_0(); if ((Case == -1) || (Case == 1)) test_case_1(); if ((Case == -1) || (Case == 2)) test_case_2(); if ((Case == -1) || (Case == 3)) test_case_3(); }
	private void verify_case(int Case, int Expected, int Received) {
		Console.Write("Test Case #" + Case + "...");
		if (Expected == Received) 
			Console.WriteLine("PASSED"); 
		else { 
			Console.WriteLine("FAILED"); 
			Console.WriteLine("\tExpected: \"" + Expected + '\"');
			Console.WriteLine("\tReceived: \"" + Received + '\"'); } }
	private void test_case_0() { int Arg0 = 20; int[] Arg1 = new int[]{1, 2, 5, 10}; int Arg2 = 5; verify_case(0, Arg2, minNumber(Arg0, Arg1)); }
	private void test_case_1() { int Arg0 = 7; int[] Arg1 = new int[]{2, 4, 1, 7}; int Arg2 = 3; verify_case(1, Arg2, minNumber(Arg0, Arg1)); }
	private void test_case_2() { int Arg0 = 20; int[] Arg1 = new int[]{2,4,6,8}; int Arg2 = -1; verify_case(2, Arg2, minNumber(Arg0, Arg1)); }
	private void test_case_3() { int Arg0 = 600; int[] Arg1 = new int[]{1,2,3,10,11,30}; int Arg2 = 25; verify_case(3, Arg2, minNumber(Arg0, Arg1)); }

// END CUT HERE


    // BEGIN CUT HERE
    [STAThread]
    public static void Main(string[] args)
    {
        Shopping item = new Shopping();
        item.run_test(-1);
        Console.ReadLine();
    }
    // END CUT HERE
}
