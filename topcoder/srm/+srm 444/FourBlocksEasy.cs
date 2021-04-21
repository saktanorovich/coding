using System;
 
public class FourBlocksEasy
{
    public int maxScore(string[] g)
    {
        int[,] f = new int[2, g[0].Length];
        int n = g[0].Length;
        for (int i = 0; i < n - 1; )
        {
            if (g[0][i] == '.' && g[1][i] == '.' && g[0][i + 1] == '.' && g[1][i + 1] == '.')
            {
                f[0, i] = 4;
                f[1, i] = 4;
                f[0, i + 1] = 4;
                f[1, i + 1] = 4;
                i = i + 2;
            }
            else
            {
                ++i;
            }
        }
        int result = 0;
        for (int i = 0; i < n; ++i)
        {
            for (int j = 0; j < 2; ++j)
            {
                if (f[j, i] == 4)
                {
                    result += 4;
                }
                else
                {
                    ++result;
                }
            }
        }
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
	private void test_case_0() { string[] Arg0 = new string[]{".....1..1..",
 "..1.....1.."}; int Arg1 = 70; verify_case(0, Arg1, maxScore(Arg0)); }
	private void test_case_1() { string[] Arg0 = new string[]{"....................",
 "...................."}
; int Arg1 = 160; verify_case(1, Arg1, maxScore(Arg0)); }
	private void test_case_2() { string[] Arg0 = new string[]{".1.........11.........",
 "..1.1......11........."}
; int Arg1 = 128; verify_case(2, Arg1, maxScore(Arg0)); }
	private void test_case_3() { string[] Arg0 = new string[]{"......1.....1...1.",
 ".................."}; int Arg1 = 108; verify_case(3, Arg1, maxScore(Arg0)); }

// END CUT HERE

	// BEGIN CUT HERE
	[STAThread]
	public static void Main(string[] args)
	{
		FourBlocksEasy item = new FourBlocksEasy();
		item.run_test(-1);
		Console.ReadLine();
	}
	// END CUT HERE
}
