using System;
using System.Collections.Generic;
 
public class GirlsAndBoys
{
    private const int oo = +1000000000;

    public int sortThem(string row)
    {
        int result = +oo, current;

        current = 0;
        for (int i = 0, k = 0; i < row.Length; ++i)
        {
            if (row[i] == 'B')
            {
                current += i - k;
                ++k;
            }
        }
        result = Math.Min(result, current);

        current = 0;
        for (int i = 0, k = 0; i < row.Length; ++i)
        {
            if (row[i] == 'G')
            {
                current += i - k;
                ++k;
            }
        }
        result = Math.Min(result, current);

        current = 0;
        for (int i = row.Length - 1, k = row.Length - 1; i >= 0; --i)
        {
            if (row[i] == 'B')
            {
                current += k - i;
                --k;
            }
        }
        result = Math.Min(result, current);

        current = 0;
        for (int i = row.Length - 1, k = row.Length - 1; i >= 0; --i)
        {
            if (row[i] == 'G')
            {
                current += k - i;
                --k;
            }
        }
        result = Math.Min(result, current);

        return result;
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
	private void test_case_0() { string Arg0 = "GGBBG"; int Arg1 = 2; verify_case(0, Arg1, sortThem(Arg0)); }
	private void test_case_1() { string Arg0 = "BBBBGGGG"; int Arg1 = 0; verify_case(1, Arg1, sortThem(Arg0)); }
	private void test_case_2() { string Arg0 = "BGBGBGBGGGBBGBGBGG"; int Arg1 = 33; verify_case(2, Arg1, sortThem(Arg0)); }
	private void test_case_3() { string Arg0 = "B"; int Arg1 = 0; verify_case(3, Arg1, sortThem(Arg0)); }

// END CUT HERE


    // BEGIN CUT HERE
    [STAThread]
    public static void Main(string[] args)
    {
        GirlsAndBoys item = new GirlsAndBoys();
        item.run_test(-1);
        Console.ReadLine();
    }
    // END CUT HERE
}
