using System;
using System.Collections.Generic;
 
public class DigitalCounter
{
    private int[] nsegments = new int[] { 6, 2, 5, 5, 4, 5, 6, 3, 7, 5 };

    public long nextEvent(string current_number)
    {
        long current = long.Parse(current_number);
        int n = current_number.Length;
        long loop_length = 1;
        for (int i = 1; i <= n; ++i)
        {
            loop_length *= 10;
        }
        int current_nsegments = get_nsegments(current_number);
        bool[,] dp = new bool[n + 1, 7 * n + 1];
        string[,] lex_smallest_suffix = new string[n + 1, 7 * n + 1];
        dp[0, 0] = true;
        for (int i = 1; i <= n; ++i)
        {
            for (int k = 0; k < nsegments.Length; ++k)
            {
                for (int j = 0; j <= 7 * n; ++j)
                {
                    if (dp[i - 1, j])
                    {
                        if (!dp[i, j + nsegments[k]])
                        {
                            dp[i, j + nsegments[k]] = true;
                            lex_smallest_suffix[i, j + nsegments[k]] = k + lex_smallest_suffix[i - 1, j];
                        }
                    }
                }
            }
        }
        long result = loop_length;
        for (int i = 0; i < n; ++i)
        {
            for (int j = 0; j < nsegments.Length; ++j)
            {
                string prefix = current_number.Substring(0, i) + j;
                int required = current_nsegments - get_nsegments(prefix);
                if (required >= 0 && dp[n - i - 1, required])
                {
                    long next = long.Parse(prefix + lex_smallest_suffix[n - i - 1, required]);
                    if (next < current)
                    {
                        result = Math.Min(result, loop_length - current + next);
                    }
                    else if (next > current)
                    {
                        result = Math.Min(result, next - current);
                    }
                }
            }
        }
        return result;
    }

    private int get_nsegments(string number)
    {
        int result = 0;
        foreach (char c in number)
        {
            result += nsegments[int.Parse(c.ToString())];
        }
        return result;
    }

    // BEGIN CUT HERE
    public void run_test(int Case)
    { 
        if ((Case == -1) || (Case == 0)) test_case_0(); if ((Case == -1) || (Case == 1)) test_case_1(); 
        if ((Case == -1) || (Case == 2)) test_case_2(); if ((Case == -1) || (Case == 3)) test_case_3(); if ((Case == -1) || (Case == 4)) test_case_4(); 
        if ((Case == -1) || (Case == 5)) test_case_5();
        if ((Case == -1) || (Case == 6)) test_case_6();
        if ((Case == -1) || (Case == 7)) test_case_7();
        if ((Case == -1) || (Case == 8)) test_case_8();
    }
	private void verify_case(int Case, long Expected, long Received) {
		Console.Write("Test Case #" + Case + "...");
		if (Expected == Received) 
			Console.WriteLine("PASSED"); 
		else { 
			Console.WriteLine("FAILED"); 
			Console.WriteLine("\tExpected: \"" + Expected + '\"');
			Console.WriteLine("\tReceived: \"" + Received + '\"'); } }
	private void test_case_0() { string Arg0 = "1"; long Arg1 = 10l; verify_case(0, Arg1, nextEvent(Arg0)); }
	private void test_case_1() { string Arg0 = "3"; long Arg1 = 2l; verify_case(1, Arg1, nextEvent(Arg0)); }
	private void test_case_2() { string Arg0 = "9"; long Arg1 = 3l; verify_case(2, Arg1, nextEvent(Arg0)); }
	private void test_case_3() { string Arg0 = "99"; long Arg1 = 5l; verify_case(3, Arg1, nextEvent(Arg0)); }
	private void test_case_4() { string Arg0 = "654371"; long Arg1 = 43l; verify_case(4, Arg1, nextEvent(Arg0)); }
	private void test_case_5() { string Arg0 = "007"; long Arg1 = 11l; verify_case(5, Arg1, nextEvent(Arg0)); }
    private void test_case_6() { string Arg0 = "065927413888888"; long Arg1 = 2000000l; verify_case(6, Arg1, nextEvent(Arg0)); }
    private void test_case_7() { string Arg0 = "622383635971111"; long Arg1 = 140001l; verify_case(7, Arg1, nextEvent(Arg0)); }
    private void test_case_8() { string Arg0 = "788888888888888"; long Arg1 = 11120000000000l; verify_case(8, Arg1, nextEvent(Arg0)); }

// END CUT HERE

	// BEGIN CUT HERE
	[STAThread]
	public static void Main(string[] args)
	{
		DigitalCounter item = new DigitalCounter();
		item.run_test(-1);
		Console.ReadLine();
	}
	// END CUT HERE
}
