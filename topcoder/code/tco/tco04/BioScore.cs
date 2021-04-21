using System;
using System.Collections.Generic;
 
public class BioScore
{
    public double maxAvg(string[] knownSet)
    {
        Dictionary<char, int> code = new Dictionary<char, int>();
        code.Add('A', 0);
        code.Add('C', 1);
        code.Add('T', 2);
        code.Add('G', 3);

        int n = knownSet.Length;
        int sequence_length = knownSet[0].Length;
        int[,] frequency = new int[4, 4];
        for (int i = 0; i < n; ++i)
        {
            for (int j = i + 1; j < n; ++j)
            {
                for (int k = 0; k < sequence_length; ++k)
                {
                    int a = code[knownSet[i][k]];
                    int b = code[knownSet[j][k]];
                    ++frequency[Math.Max(a, b), Math.Min(a, b)];
                }
            }
        }
        int[,] score = new int[4, 4];
        int max_score = int.MinValue;

        /* brute force of diagonal elements */
        for (int i0 = 1; i0 <= 10; ++i0)
        {
            for (int i1 = 1; i1 <= 10; ++i1)
            {
                for (int i2 = 1; i2 <= 10; ++i2)
                {
                    for (int i3 = 1; i3 <= 10; ++i3)
                    {
                        int dsum = i0 + i1 + i2 + i3;
                        if ((dsum & 1) == 1)
                        {
                            continue;
                        }
                        score[0, 0] = i0;
                        score[1, 1] = i1;
                        score[2, 2] = i2;
                        score[3, 3] = i3;
                        
                        /* find any valid score matrix */
                        int hsum = -1 * dsum / 2;
                        for (int i = 1; i < 4; ++i)
                        {
                            for (int j = 0; j < i; ++j)
                            {
                                score[i, j] = (hsum >= 10 ? 10 : (hsum <= -10 ? -10 : hsum));
                                hsum -= score[i, j];
                            }
                        }

                        /* optimize current score matrix */
                        for (bool exit = false; !exit; )
                        {
                            exit = true;
                            for (int i = 1; i < 4; ++i)
                            {
                                for (int j = 0; j < i; ++j)
                                {
                                    for (int p = 1; p < 4; ++p)
                                    {
                                        for (int q = 0; q < p; ++q)
                                        {
                                            if (i == p && j == q)
                                            {
                                                continue;
                                            }
                                            if (frequency[i, j] > frequency[p, q])
                                            {
                                                int delta = Math.Min(10 - score[i, j], score[p, q] - (-10));
                                                if (delta != 0)
                                                {
                                                    score[i, j] += delta;
                                                    score[p, q] -= delta;
                                                    exit = false;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        /* calculate score */
                        int total_score = 0;
                        for (int i = 0; i < 4; ++i)
                        {
                            for (int j = 0; j <= i; ++j)
                            {
                                total_score += score[i, j] * frequency[i, j];
                            }
                        }
                        max_score = Math.Max(max_score, total_score);
                    }
                }
            }
        }
        return max_score / (n * (n - 1) * 0.5);
    }

    
// BEGIN CUT HERE
	public void run_test(int Case) { if ((Case == -1) || (Case == 0)) test_case_0(); if ((Case == -1) || (Case == 1)) test_case_1(); if ((Case == -1) || (Case == 2)) test_case_2(); }
	private void verify_case(int Case, double Expected, double Received) {
		Console.Write("Test Case #" + Case + "...");
		if (Expected == Received) 
			Console.WriteLine("PASSED"); 
		else { 
			Console.WriteLine("FAILED"); 
			Console.WriteLine("\tExpected: \"" + Expected + '\"');
			Console.WriteLine("\tReceived: \"" + Received + '\"'); } }
	private void test_case_0() { string[] Arg0 = new string[]{"AAA","AAA","AAC"}; double Arg1 = 30.0; verify_case(0, Arg1, maxAvg(Arg0)); }
	private void test_case_1() { string[] Arg0 = new string[]{"ACTGACTGACTG","GACTTGACCTGA"}; double Arg1 = -4.0; verify_case(1, Arg1, maxAvg(Arg0)); }
	private void test_case_2() { string[] Arg0 = new string[]{"ACTAGAGAC","AAAAAAAAA","TAGTCATAC","GCAGCATTC"}; double Arg1 = 50.5; verify_case(2, Arg1, maxAvg(Arg0)); }

// END CUT HERE


    // BEGIN CUT HERE
    [STAThread]
    public static void Main(string[] args)
    {
        BioScore item = new BioScore();
        item.run_test(-1);
        Console.ReadLine();
    }
    // END CUT HERE
}
