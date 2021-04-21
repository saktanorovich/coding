using System;
 
public class UnfoldingTriangles
{
	public int solve(string[] grid, int unfoldLimit)
	{
        int n = grid.Length;
        int m = grid[0].Length;
        int result = -1;
        for (int triangleSideLength = 1; triangleSideLength <= Math.Min(n, m); ++triangleSideLength)
        {
            for (int i = triangleSideLength - 1; i < n; ++i)
            {
                for (int j = triangleSideLength - 1; j < m; ++j)
                {
                    bool is_diagonal_ok = true;
                    for (int k = 0; k < triangleSideLength; ++k)
                    {
                        if (grid[i - k][j - triangleSideLength + k + 1] != '/')
                        {
                            is_diagonal_ok = false;
                            break;
                        }
                    }
                    if (is_diagonal_ok)
                    {
                        bool is_sides_ok = true;
                        for (int k = 0; k < triangleSideLength; ++k)
                        {
                            if (i + 1 < n)
                            {
                                is_sides_ok = is_sides_ok && (grid[i + 1][j - k] != '#');
                            }
                            if (j + 1 < m)
                            {
                                is_sides_ok = is_sides_ok && (grid[i - k][j + 1] != '#');
                            }
                            if (!is_sides_ok)
                            {
                                break;
                            }
                        }
                        if (is_sides_ok)
                        {
                            int count = 0;
                            bool is_contain_empty_cell = false;
                            for (int r = i, k = triangleSideLength - 2; r > i - triangleSideLength; --r, --k)
                            {
                                for (int c = j - k; c <= j; ++c)
                                {
                                    if (grid[r][c] == '.')
                                    {
                                        is_contain_empty_cell = true;
                                        break;
                                    }
                                    else if (grid[r][c] == '/')
                                    {
                                        ++count;
                                    }
                                }
                                if (is_contain_empty_cell)
                                {
                                    break;
                                }
                            }
                            if (count <= unfoldLimit && !is_contain_empty_cell)
                            {
                                result = Math.Max(result, getSquaresCount(triangleSideLength));
                            }
                        }
                    }
                }
            }
        }
        return result;
	}

    private int getSquaresCount(int triangleSideLength)
    {
        return triangleSideLength * (triangleSideLength + 1) / 2;
    }

    // BEGIN CUT HERE
    public void run_test(int Case) { if ((Case == -1) || (Case == 0)) test_case_0(); if ((Case == -1) || (Case == 1)) test_case_1(); if ((Case == -1) || (Case == 2)) test_case_2(); if ((Case == -1) || (Case == 3)) test_case_3(); if ((Case == -1) || (Case == 4)) test_case_4(); if ((Case == -1) || (Case == 5)) test_case_5(); }
	private void verify_case(int Case, int Expected, int Received) {
		Console.Write("Test Case #" + Case + "...");
		if (Expected == Received) 
			Console.WriteLine("PASSED"); 
		else { 
			Console.WriteLine("FAILED"); 
			Console.WriteLine("\tExpected: \"" + Expected + '\"');
			Console.WriteLine("\tReceived: \"" + Received + '\"'); } }
	private void test_case_0() { string[] Arg0 = new string[]{".../",
 "../#",
 "./#/",
 "/#//"}; int Arg1 = 4; int Arg2 = 10; verify_case(0, Arg2, solve(Arg0, Arg1)); }
	private void test_case_1() { string[] Arg0 = new string[]{".../",
 "../#",
 "./#/",
 "/#//"}; int Arg1 = 2; int Arg2 = 3; verify_case(1, Arg2, solve(Arg0, Arg1)); }
	private void test_case_2() { string[] Arg0 = new string[]{"////",
 "////",
 "////",
 "////"}; int Arg1 = 5; int Arg2 = 6; verify_case(2, Arg2, solve(Arg0, Arg1)); }
	private void test_case_3() { string[] Arg0 = new string[]{".....#...",
 "....###.."}; int Arg1 = 10; int Arg2 = -1; verify_case(3, Arg2, solve(Arg0, Arg1)); }
	private void test_case_4() { string[] Arg0 = new string[]{"#//#",
 "#//#",
 "####",
 "///#"}; int Arg1 = 4; int Arg2 = 1; verify_case(4, Arg2, solve(Arg0, Arg1)); }
	private void test_case_5() { string[] Arg0 = new string[]{".../.../",
 "../#..//",
 "./.#.///",
 "/###...."}; int Arg1 = 3; int Arg2 = 6; verify_case(5, Arg2, solve(Arg0, Arg1)); }

// END CUT HERE

	// BEGIN CUT HERE
	[STAThread]
	public static void Main(string[] args)
	{
		UnfoldingTriangles item = new UnfoldingTriangles();
		item.run_test(-1);
		Console.ReadLine();
	}
	// END CUT HERE
}
