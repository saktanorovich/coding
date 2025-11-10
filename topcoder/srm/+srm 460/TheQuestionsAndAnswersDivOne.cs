using System;
using System.Collections.Generic;

public class TheQuestionsAndAnswersDivOne {
      public int find(int questions, string[] answers) {
            int nyes = 0, nno = 0;
            for (int i = 0; i < answers.Length; ++i) {
                  if (answers[i].Equals("Yes")) {
                        ++nyes;
                  }
                  else {
                        ++nno;
                  }
            }
            return run(questions, nyes, nno);
      }

      private int run(int questions, int nyes, int nno) {
            if (questions == 0) {
                  if (nyes == 0 && nno == 0) {
                        return 1;
                  }
                  return 0;
            }
            int result = 0;
            for (int i = 1; i <= nyes; ++i) {
                  result += c(nyes, i) * run(questions - 1, nyes - i, nno);
            }
            for (int i = 1; i <= nno; ++i) {
                  result += c(nno, i)  * run(questions - 1, nyes, nno - i);
            }
            return result;
      }

      private int c(int n, int k) {
            int numerator = 1;
            for (int i = 2; i <= n; ++i) {
                  numerator *= i;
            }
            int denominator = 1;
            for (int i = 2; i <= n - k; ++i) {
                  denominator *= i;
            }
            for (int i = 2; i <= k; ++i) {
                  denominator *= i;
            }
            return numerator / denominator;
      }

      // BEGIN CUT HERE
      public void run_test(int Case) { if ((Case == -1) || (Case == 0)) test_case_0(); if ((Case == -1) || (Case == 1)) test_case_1(); if ((Case == -1) || (Case == 2)) test_case_2(); if ((Case == -1) || (Case == 3)) test_case_3(); }
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
      private void test_case_0() { int Arg0 = 2; string[] Arg1 = new string[] { "No", "Yes" }; int Arg2 = 2; verify_case(0, Arg2, find(Arg0, Arg1)); }
      private void test_case_1() { int Arg0 = 2; string[] Arg1 = new string[] { "No", "No", "No" }; int Arg2 = 6; verify_case(1, Arg2, find(Arg0, Arg1)); }
      private void test_case_2() { int Arg0 = 3; string[] Arg1 = new string[] { "Yes", "No", "No", "Yes" }; int Arg2 = 12; verify_case(2, Arg2, find(Arg0, Arg1)); }
      private void test_case_3() { int Arg0 = 3; string[] Arg1 = new string[] { "Yes", "Yes", "Yes", "No" }; int Arg2 = 18; verify_case(3, Arg2, find(Arg0, Arg1)); }

      // END CUT HERE


      // BEGIN CUT HERE
      [STAThread]
      public static void Main(string[] args) {
            TheQuestionsAndAnswersDivOne item = new TheQuestionsAndAnswersDivOne();
            item.run_test(-1);
            Console.ReadLine();
      }
      // END CUT HERE
}
