using System;
using System.Collections.Generic;
 
public class RectangleError
{
    public bool between(double x, double lo, double hi)
    {
        return (lo <= x && x <= hi);
    }

	public double bottomRange(double topMin, double topMax, double leftMin, double leftMax, double rightMin, double rightMax)
	{
        double bottomMin, x;
        if (between(leftMin, rightMin, rightMax) || between(leftMax, rightMin, rightMax) ||
            between(rightMin, leftMin, leftMax) || between(rightMax, leftMin, leftMax))
        {
            bottomMin = topMin;
        }
        else
        {
            x = Math.Min(Math.Abs(leftMax - rightMin), Math.Abs(rightMax - leftMin));
            bottomMin = Math.Sqrt(x * x + topMin * topMin);
        }
        x = Math.Max(Math.Abs(leftMax - rightMin), Math.Abs(rightMax - leftMin));
        double bottomMax = Math.Sqrt(x * x + topMax * topMax);
        return bottomMax - bottomMin;
	}

	
// BEGIN CUT HERE
	public void run_test(int Case) { if ((Case == -1) || (Case == 0)) test_case_0(); if ((Case == -1) || (Case == 1)) test_case_1(); if ((Case == -1) || (Case == 2)) test_case_2(); if ((Case == -1) || (Case == 3)) test_case_3(); if ((Case == -1) || (Case == 4)) test_case_4(); }
	private void verify_case(int Case, double Expected, double Received) {
		Console.Write("Test Case #" + Case + "...");
		if (Expected == Received) 
			Console.WriteLine("PASSED"); 
		else { 
			Console.WriteLine("FAILED"); 
			Console.WriteLine("\tExpected: \"" + Expected + '\"');
			Console.WriteLine("\tReceived: \"" + Received + '\"'); } }
	private void test_case_0() { double Arg0 = 50; double Arg1 = 50; double Arg2 = 50; double Arg3 = 50; double Arg4 = 50; double Arg5 = 50; double Arg6 = 0.0; verify_case(0, Arg6, bottomRange(Arg0, Arg1, Arg2, Arg3, Arg4, Arg5)); }
	private void test_case_1() { double Arg0 = 5; double Arg1 = 5; double Arg2 = 50; double Arg3 = 50; double Arg4 = 5; double Arg5 = 5; double Arg6 = 0.0; verify_case(1, Arg6, bottomRange(Arg0, Arg1, Arg2, Arg3, Arg4, Arg5)); }
	private void test_case_2() { double Arg0 = 5; double Arg1 = 100; double Arg2 = 5; double Arg3 = 100; double Arg4 = 5; double Arg5 = 100; double Arg6 = 132.93114224133723; verify_case(2, Arg6, bottomRange(Arg0, Arg1, Arg2, Arg3, Arg4, Arg5)); }
	private void test_case_3() { double Arg0 = 5; double Arg1 = 10; double Arg2 = 15; double Arg3 = 20; double Arg4 = 25; double Arg5 = 30; double Arg6 = 10.95668856545447; verify_case(3, Arg6, bottomRange(Arg0, Arg1, Arg2, Arg3, Arg4, Arg5)); }
	private void test_case_4() { double Arg0 = 10; double Arg1 = 20; double Arg2 = 30; double Arg3 = 40; double Arg4 = 35; double Arg5 = 45; double Arg6 = 15.0; verify_case(4, Arg6, bottomRange(Arg0, Arg1, Arg2, Arg3, Arg4, Arg5)); }

// END CUT HERE

	
	// BEGIN CUT HERE
	[STAThread]
	public static void Main(string[] args)
	{
		RectangleError item = new RectangleError();
		item.run_test(-1);
		Console.ReadLine();
	}
	// END CUT HERE
}

