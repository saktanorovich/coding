using System;
using System.Collections.Generic;

public class CarrotJumping
{
    public int theJump(int init)
    {
        int modulo = 1000000007;

        Queue<long> positions = new Queue<long>();
        Dictionary<long, bool> registered_positions = new Dictionary<long, bool>();
        registered_positions.Add(init, true);
        positions.Enqueue(init);
        for (int t = 0; t <= 100000; ++t)
        {
            int count = positions.Count;
            for (int i = 0; i < count; ++i)
            {
                long position = positions.Dequeue();
                if (position % modulo == 0)
                    return t;
                long pos1 = (4 * position + 3) % modulo, pos2 = (8 * position + 7) % modulo;
                if (!registered_positions.ContainsKey(pos1))
                {
                    registered_positions.Add(pos1, true);
                    positions.Enqueue(pos1);
                }
                if (!registered_positions.ContainsKey(pos2))
                {
                    registered_positions.Add(pos2, true);
                    positions.Enqueue(pos2);
                }
            }
        }
        return -1;
    }


    // BEGIN CUT HERE
    public void run_test(int Case) { if ((Case == -1) || (Case == 0)) test_case_0(); if ((Case == -1) || (Case == 1)) test_case_1(); if ((Case == -1) || (Case == 2)) test_case_2(); if ((Case == -1) || (Case == 3)) test_case_3(); if ((Case == -1) || (Case == 4)) test_case_4(); if ((Case == -1) || (Case == 5)) test_case_5(); }
    private void verify_case(int Case, int Expected, int Received)
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
    private void test_case_0() { int Arg0 = 125000000; int Arg1 = 1; verify_case(0, Arg1, theJump(Arg0)); }
    private void test_case_1() { int Arg0 = 281250001; int Arg1 = 2; verify_case(1, Arg1, theJump(Arg0)); }
    private void test_case_2() { int Arg0 = 18426114; int Arg1 = 58; verify_case(2, Arg1, theJump(Arg0)); }
    private void test_case_3() { int Arg0 = 4530664; int Arg1 = 478; verify_case(3, Arg1, theJump(Arg0)); }
    private void test_case_4() { int Arg0 = 705616876; int Arg1 = 100000; verify_case(4, Arg1, theJump(Arg0)); }
    private void test_case_5() { int Arg0 = 852808441; int Arg1 = -1; verify_case(5, Arg1, theJump(Arg0)); }

    // END CUT HERE


    // BEGIN CUT HERE
    [STAThread]
    public static void Main(string[] args)
    {
        CarrotJumping item = new CarrotJumping();
        item.run_test(-1);
        Console.ReadLine();
    }
    // END CUT HERE
}
