using System;
using System.Collections.Generic;

public class Flush
{
        public double size(int[] suits, int number)
        {
                long[,] c = new long[53, 53];
                c[0, 0] = 1;
                for (int i = 1; i < 53; ++i)
                {
                        c[i, 0] = 1;
                        for (int j = 1; j < 53; ++j)
                        {
                                c[i, j] = c[i - 1, j - 1] + c[i - 1, j];
                        }
                }
                long count = 0;
                for (int i0 = 0; i0 <= suits[0]; ++i0)
                        for (int i1 = 0; i1 <= suits[1]; ++i1)
                                for (int i2 = 0; i2 <= suits[2]; ++i2)
                                        for (int i3 = 0; i3 <= suits[3]; ++i3)
                                        {
                                                if (i0 + i1 + i2 + i3 == number)
                                                {
                                                        long flush = Math.Max(Math.Max(i0, i1), Math.Max(i2, i3));
                                                        long n = c[suits[0], i0] * c[suits[1], i1] * c[suits[2], i2] * c[suits[3], i3];
                                                        count = count + flush * n;
                                                }
                                        }
                double expected = (double)count / c[suits[0] + suits[1] + suits[2] + suits[3], number];
                return expected;
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
        private void test_case_0() { int[] Arg0 = new int[] { 2, 2, 2, 2 }; int Arg1 = 2; double Arg2 = 1.1428571428571428; verify_case(0, Arg2, size(Arg0, Arg1)); }
        private void test_case_1() { int[] Arg0 = new int[] { 1, 4, 7, 10 }; int Arg1 = 22; double Arg2 = 10.0; verify_case(1, Arg2, size(Arg0, Arg1)); }
        private void test_case_2() { int[] Arg0 = new int[] { 13, 13, 13, 13 }; int Arg1 = 49; double Arg2 = 13.0; verify_case(2, Arg2, size(Arg0, Arg1)); }
        private void test_case_3() { int[] Arg0 = new int[] { 13, 13, 13, 13 }; int Arg1 = 26; double Arg2 = 8.351195960938014; verify_case(3, Arg2, size(Arg0, Arg1)); }
        private void test_case_4() { int[] Arg0 = new int[] { 13, 13, 13, 13 }; int Arg1 = 0; double Arg2 = 0.0; verify_case(4, Arg2, size(Arg0, Arg1)); }

        // END CUT HERE
        
        // BEGIN CUT HERE
        [STAThread]
        public static void Main(string[] args)
        {
                Flush item = new Flush();
                item.run_test(-1);
                Console.ReadLine();
        }
        // END CUT HERE
}
