using System;
using System.Collections.Generic;

public class FlipGame {
      public int minOperations(string[] board) {
            int n = board.Length;
            int m = board[0].Length;
            int[,] _board = new int[n, m];
            for (int i = 0; i < n; ++i) {
                  for (int j = 0; j < m; ++j) {
                        if (board[i][j] == '1') {
                              _board[i, j] = 1;
                        }
                  }
            }
            return minOperations(_board, n, m);
      }

      private int minOperations(int[,] board, int n, int m) {
            int total = 0;
            for (int i = 0; i < n; ++i) {
                  for (int j = 0; j < m; ++j) {
                        total += board[i, j];
                  }
            }
            int result = 0;
            while (total != 0) {
                  ++result;
                  int[] state = new int[n];
                  for (int i = 0; i < n; ++i) {
                        state[i] = -1;
                        for (int j = m - 1; j >= 0; --j) {
                              if (board[i, j] == 1) {
                                    state[i] = j;
                                    break;
                              }
                        }
                  }
                  for (int i = 1; i < n; ++i) {
                        if (state[i] < state[i - 1]) {
                              state[i] = state[i - 1];
                        }
                  }
                  for (int i = 0; i < n; ++i) {
                        for (int j = 0; j <= state[i]; ++j) {
                              total += - board[i, j] + (1 - board[i, j]);
                              board[i, j] = 1 - board[i, j];
                        }
                  }
            }
            return result;
      }

      // BEGIN CUT HERE
      public void run_test(int Case) { if ((Case == -1) || (Case == 0)) test_case_0(); if ((Case == -1) || (Case == 1)) test_case_1(); if ((Case == -1) || (Case == 2)) test_case_2(); if ((Case == -1) || (Case == 3)) test_case_3(); if ((Case == -1) || (Case == 4)) test_case_4(); }
      private void verify_case(int Case, int Expected, int Received) {
            Console.Write("Test Case #" + Case + "...");
            if (Expected == Received)
                  Console.WriteLine("PASSED");
            else {
                  Console.WriteLine("FAILED");
                  Console.WriteLine("\tExpected: \"" + Expected + '\"');
                  Console.WriteLine("\tReceived: \"" + Received + '\"');
            }
      }
      private void test_case_0() {
            string[] Arg0 = new string[]{
 "1000",
 "1110",
 "1111"}; int Arg1 = 1; verify_case(0, Arg1, minOperations(Arg0));
      }
      private void test_case_1() {
            string[] Arg0 = new string[]{
 "1111",
 "1111",
 "1111"}; int Arg1 = 1; verify_case(1, Arg1, minOperations(Arg0));
      }
      private void test_case_2() {
            string[] Arg0 = new string[]{
 "00",
 "00",
 "00"}; int Arg1 = 0; verify_case(2, Arg1, minOperations(Arg0));
      }
      private void test_case_3() {
            string[] Arg0 = new string[]{
 "00000000",
 "00100000",
 "01000000",
 "00001000",
 "00000000"}; int Arg1 = 4; verify_case(3, Arg1, minOperations(Arg0));
      }
      private void test_case_4() {
            string[] Arg0 = new string[]{
 "000000000000001100000000000000",
 "000000000000011110000000000000",
 "000000000000111111000000000000",
 "000000000001111111100000000000",
 "000000000011111111110000000000",
 "000000000111111111111000000000",
 "000000001100111111001100000000",
 "000000011000011110000110000000",
 "000000111100111111001111000000",
 "000001111111111111111111100000",
 "000011111111111111111111110000",
 "000111111111000000111111111000",
 "001111111111100001111111111100",
 "011111111111110011111111111110",
 "111111111111111111111111111111"}; int Arg1 = 29; verify_case(4, Arg1, minOperations(Arg0));
      }

      // END CUT HERE

      // BEGIN CUT HERE
      [STAThread]
      public static void Main(string[] args) {
            FlipGame item = new FlipGame();
            item.run_test(-1);
            Console.ReadLine();
      }
      // END CUT HERE
}
