using System;
using System.Collections.Generic;

public class MarblesInABag
{
        public double getProbability(int nred, int nblue)
        {
                double[,] dp = new double[2, nblue + 1];
                int level = 0;
                for (int b = 1; b <= nblue; ++b)
                {
                        dp[level, b] = b % 2;
                }
                for (int r = 1; r <= nred; ++r)
                {
                        level = 1 - level;
                        for (int b = 0; b <= nblue; ++b)
                        {
                                dp[level, b] = 0.0;
                                if ((r + b) % 2 == 1)
                                {
                                        if (b > 1)
                                        {
                                                dp[level, b] += get_prob(r + b, b) * dp[level, b - 2];
                                        }
                                        if (b > 0)
                                        {
                                                dp[level, b] += get_prob(r + b, r) * dp[1 - level, b - 1];
                                        }
                                }
                        }
                }
                return dp[level, nblue];
        }

        private double get_prob(int n, int k)
        {
                return 1.0 * k / n;
        }

        // BEGIN CUT HERE
        public void run_test(int Case) { if ((Case == -1) || (Case == 0)) test_case_0(); if ((Case == -1) || (Case == 1)) test_case_1(); if ((Case == -1) || (Case == 2)) test_case_2(); if ((Case == -1) || (Case == 3)) test_case_3(); if ((Case == -1) || (Case == 4)) test_case_4(); }
        private void verify_case(int Case, double Expected, double Received)
        {
                Console.Write("Test Case #" + Case + "...");
                if (Expected == Received)
                        Console.WriteLine("PASSED");
                else
                {
                        Console.WriteLine("FAILED");
                        Console.WriteLine("\tExpected: \"" + Expected + '\"');
                        Console.WriteLine("\tReceived: \"" + Received + '\"');
                }
        }
        private void test_case_0() { int Arg0 = 1; int Arg1 = 2; double Arg2 = 0.3333333333333333; verify_case(0, Arg2, getProbability(Arg0, Arg1)); }
        private void test_case_1() { int Arg0 = 2; int Arg1 = 3; double Arg2 = 0.13333333333333333; verify_case(1, Arg2, getProbability(Arg0, Arg1)); }
        private void test_case_2() { int Arg0 = 2; int Arg1 = 5; double Arg2 = 0.22857142857142856; verify_case(2, Arg2, getProbability(Arg0, Arg1)); }
        private void test_case_3() { int Arg0 = 11; int Arg1 = 6; double Arg2 = 0.0; verify_case(3, Arg2, getProbability(Arg0, Arg1)); }
        private void test_case_4() { int Arg0 = 4; int Arg1 = 11; double Arg2 = 0.12183372183372182; verify_case(4, Arg2, getProbability(Arg0, Arg1)); }

        // END CUT HERE


        // BEGIN CUT HERE
        [STAThread]
        public static void Main(string[] args)
        {
                MarblesInABag item = new MarblesInABag();
                item.run_test(-1);
                Console.ReadLine();
        }
        // END CUT HERE
}
