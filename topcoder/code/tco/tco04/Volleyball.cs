using System;
using System.Collections.Generic;

public class Volleyball
{
    private bool[,] cached = new bool[1000, 1000];
    private double[,] dp = new double[1000, 1000];

    private bool is_completed(int sScore, int rScore)
    {
        return (sScore >= 15 && sScore - rScore >= 2);
    }

    private double __win(int sScore, int rScore, double probWinServe)
    {
        if (sScore >= 1000 || rScore >= 1000) return 0;

        if (cached[sScore, rScore])
            return dp[sScore, rScore];

        if (is_completed(sScore, rScore)) return 1.0;
        if (is_completed(rScore, sScore)) return 0.0;

        cached[sScore, rScore] = true;
        dp[sScore, rScore] = probWinServe * __win(sScore + 1, rScore, probWinServe) + (1 - probWinServe) * (1 - __win(rScore + 1, sScore, probWinServe));

        return dp[sScore, rScore];
    }

    public double win(int sScore, int rScore, double probWinServe)
    {
        for (int i = 0; i < 1000; ++i)
            for (int j = 0; j < 1000; ++j)
                cached[i, j] = false;
        return __win(sScore, rScore, probWinServe);
    }


    // BEGIN CUT HERE
    public void run_test(int Case)
    {
        if ((Case == -1) || (Case == 0)) test_case_0();
        if ((Case == -1) || (Case == 1)) test_case_1();
        if ((Case == -1) || (Case == 2)) test_case_2();
        if ((Case == -1) || (Case == 3)) test_case_3();
    }
    private void verify_case(int Case, double Expected, double Received)
    {
        Console.Write("Test Case #" + Case + "...");
        if (Math.Abs(Expected - Received) <= 1e-9)
            Console.WriteLine("PASSED");
        else
        {
            Console.WriteLine("FAILED");
            Console.WriteLine("\tExpected: \"" + Expected + '\"');
            Console.WriteLine("\tReceived: \"" + Received + '\"');
        }
    }
    private void test_case_0() { int Arg0 = 13; int Arg1 = 13; double Arg2 = .5; double Arg3 = 0.5; verify_case(0, Arg3, win(Arg0, Arg1, Arg2)); }
    private void test_case_1() { int Arg0 = 1; int Arg1 = 14; double Arg2 = 0.01; double Arg3 = 3.355704697986578E-27; verify_case(1, Arg3, win(Arg0, Arg1, Arg2)); }
    private void test_case_2() { int Arg0 = 8; int Arg1 = 12; double Arg2 = 0.4; double Arg3 = 0.046377890909090946; verify_case(2, Arg3, win(Arg0, Arg1, Arg2)); }
    private void test_case_3() { int Arg0 = 4; int Arg1 = 3; double Arg2 = 0.01; double Arg3 = 0.6662085066547871; verify_case(3, Arg3, win(Arg0, Arg1, Arg2)); }

    // END CUT HERE


    // BEGIN CUT HERE
    [STAThread]
    public static void Main(string[] args)
    {
        Volleyball item = new Volleyball();
        item.run_test(-1);
        Console.ReadLine();
    }
    // END CUT HERE
}
