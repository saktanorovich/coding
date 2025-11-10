using System;
 
public class RepaintTheChessboard
{
    private string[] chess_board1 = new string[]
        {
            "BWBWBWBW",
            "WBWBWBWB",
            "BWBWBWBW",
            "WBWBWBWB",
            "BWBWBWBW",
            "WBWBWBWB",
            "BWBWBWBW",
            "WBWBWBWB",
        };

    private string[] chess_board2 = new string[]
        {
            "WBWBWBWB",
            "BWBWBWBW",
            "WBWBWBWB",
            "BWBWBWBW",
            "WBWBWBWB",
            "BWBWBWBW",
            "WBWBWBWB",
            "BWBWBWBW",
        };

	public int minimumChanges(string[] board)
	{
        int result = int.MaxValue / 2;
        for (int i = 0; i <= board.Length - 8; ++i)
        {
            for (int j = 0; j <= board[0].Length - 8; ++j)
            {
                int current1 = 0, current2 = 0;
                for (int p = 0; p < 8; ++p)
                {
                    for (int q = 0; q < 8; ++q)
                    {
                        if (board[i + p][j + q] != chess_board1[p][q])
                        {
                            ++current1;
                        }
                        if (board[i + p][j + q] != chess_board2[p][q])
                        {
                            ++current2;
                        }
                    }
                }
                result = Math.Min(result, current1);
                result = Math.Min(result, current2);
            }
        }
        return result;
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
	private void test_case_0() { string[] Arg0 = new string[]{"BWBWBWBW",
 "WBWBWBWB",
 "BWBWBWBW",
 "WBWBWBWB",
 "BWBWBWBW",
 "WBWBWBWB",
 "BWBWBWBW",
 "WBWBWBWB"}
; int Arg1 = 0; verify_case(0, Arg1, minimumChanges(Arg0)); }
	private void test_case_1() { string[] Arg0 = new string[]{"WBWBWBWB",
 "BWBWBWBW",
 "WBWBWBWB",
 "BWBBBWBW",
 "WBWBWBWB",
 "BWBWBWBW",
 "WBWBWBWB",
 "BWBWBWBW"}; int Arg1 = 1; verify_case(1, Arg1, minimumChanges(Arg0)); }
	private void test_case_2() { string[] Arg0 = new string[]{"BBBBBBBBBBBBBBBBBBBBBBB",
 "BBBBBBBBBBBBBBBBBBBBBBB",
 "BBBBBBBBBBBBBBBBBBBBBBB",
 "BBBBBBBBBBBBBBBBBBBBBBB",
 "BBBBBBBBBBBBBBBBBBBBBBB",
 "BBBBBBBBBBBBBBBBBBBBBBB",
 "BBBBBBBBBBBBBBBBBBBBBBB",
 "BBBBBBBBBBBBBBBBBBBBBBB",
 "BBBBBBBBBBBBBBBBBBBBBBW"}
; int Arg1 = 31; verify_case(2, Arg1, minimumChanges(Arg0)); }
	private void test_case_3() { string[] Arg0 = new string[]{"BBBBBBBBBB",
 "BBWBWBWBWB",
 "BWBWBWBWBB",
 "BBWBWBWBWB",
 "BWBWBWBWBB",
 "BBWBWBWBWB",
 "BWBWBWBWBB",
 "BBWBWBWBWB",
 "BWBWBWBWBB",
 "BBBBBBBBBB"}; int Arg1 = 0; verify_case(3, Arg1, minimumChanges(Arg0)); }
	private void test_case_4() { string[] Arg0 = new string[]{"WBWBWBWB",
 "BWBWBWBW",
 "WBWBWBWB",
 "BWBBBWBW",
 "WBWBWBWB",
 "BWBWBWBW",
 "WBWBWWWB",
 "BWBWBWBW"}; int Arg1 = 2; verify_case(4, Arg1, minimumChanges(Arg0)); }
	private void test_case_5() { string[] Arg0 = new string[]{"BWWBWWBWWBWW",
 "BWWBWBBWWBWW",
 "WBWWBWBBWWBW",
 "BWWBWBBWWBWW",
 "WBWWBWBBWWBW",
 "BWWBWBBWWBWW",
 "WBWWBWBBWWBW",
 "BWWBWBWWWBWW",
 "WBWWBWBBWWBW",
 "BWWBWBBWWBWW",
 "WBWWBWBBWWBW"}
; int Arg1 = 15; verify_case(5, Arg1, minimumChanges(Arg0)); }

// END CUT HERE

	// BEGIN CUT HERE
	[STAThread]
	public static void Main(string[] args)
	{
		RepaintTheChessboard item = new RepaintTheChessboard();
		item.run_test(-1);
		Console.ReadLine();
	}
	// END CUT HERE
}
