using System;
using System.Collections.Generic;

public class FourBlocks {
    private int[] f;
    private int[,] dp;
    private int n, m;

    public int maxScore(string[] grid) {
        n = grid[0].Length;
        m = grid.Length;
        f = new int[n];
        for (int r = 0; r < n; ++r) {
            for (int c = 0; c < m; ++c) {
                if (grid[c][r] == '1') {
                    f[r] += (1 << c);
                }
            }
        }
        dp = new int[n, 1 << m];
        for (int i = 0; i < (1 << m); ++i) {
            dp[0, i] = 0;
            for (int j = 1; j < n; ++j) {
                dp[j, i] = -1;
            }
        }
        int fourBlocks = run(n - 1);
        return n * m - 4 * fourBlocks + 16 * fourBlocks;
    }

    private void generate(int row, int column, int fourBlocks) {
        if (column == m) {
            dp[row, f[row]] = Math.Max(dp[row, f[row]], run(row - 1) + fourBlocks);
            return;
        }
        if (column < m - 1) {
            if (!contains(f[row], column) && !contains(f[row], column + 1) && 
                !contains(f[row - 1], column) && !contains(f[row - 1], column + 1))
            {
                f[row - 1] += 1 << column;
                f[row - 1] += 1 << (column + 1);
                generate(row, column + 2, fourBlocks + 1);
                f[row - 1] -= 1 << column;
                f[row - 1] -= 1 << (column + 1);
            }
        }
        generate(row, column + 1, fourBlocks);
    }

    private bool contains(int x, int bit) {
        return ((x >> bit) & 1) == 1;
    }

    private int run(int row) {
        if (dp[row, f[row]] == -1) {
            generate(row, 0, 0);
        }
        return dp[row, f[row]];
    }

    // BEGIN CUT HERE
    public void run_test(int Case) { if ((Case == -1) || (Case == 0)) test_case_0(); 
        if ((Case == -1) || (Case == 1)) test_case_1(); if ((Case == -1) || (Case == 2)) test_case_2(); 
        if ((Case == -1) || (Case == 3)) test_case_3();
        if ((Case == -1) || (Case == 4)) test_case_4(); 
    }
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
    private void test_case_1() { string[] Arg0 = new string[]{"...1.",
 ".....",
 ".1..1",
 ".....",
 "1...."}; int Arg1 = 73; verify_case(1, Arg1, maxScore(Arg0)); }
    private void test_case_2() { string[] Arg0 = new string[]{"...1.",
 ".1...",
 "..1.1",
 "1...."}; int Arg1 = 20; verify_case(2, Arg1, maxScore(Arg0)); }
    private void test_case_3() { string[] Arg0 = new string[]{".....1...",
 ".....1...",
 "111111111",
 ".....1...",
 ".....1..."}; int Arg1 = 117; verify_case(3, Arg1, maxScore(Arg0)); }

        private void test_case_4() { string[] Arg0 = new string[]{
            "............1............", 
            ".........................", 
            "............1............", 
            ".........................", 
            ".........................", 
            "............1............", 
            ".........................", 
            ".........................", 
            ".........................", 
            "............1............"
        }; int Arg1 = 970; verify_case(4, Arg1, maxScore(Arg0)); }

    // END CUT HERE

    // BEGIN CUT HERE
    [STAThread]
    public static void Main(string[] args)
    {
        FourBlocks item = new FourBlocks();
        item.run_test(-1);
        Console.ReadLine();
    }
    // END CUT HERE
}
