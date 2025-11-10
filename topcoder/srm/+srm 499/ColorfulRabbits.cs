using System;
using System.Collections.Generic;

public class ColorfulRabbits {
      public int getMinimum(int[] replies) {
            SortedDictionary<int, int> total = new SortedDictionary<int, int>();
            for (int i = 0; i < replies.Length; ++i) {
                  if (!total.ContainsKey(replies[i])) {
                        total.Add(replies[i], 0);
                  }
                  ++total[replies[i]];
            }
            int result = 0;
            foreach (KeyValuePair<int, int> pair in total) {
                  int groupSize = pair.Key + 1;
                  if (pair.Value <= groupSize) {
                        result += groupSize;
                  }
                  else {
                        result += (pair.Value / groupSize) * groupSize;
                        if (pair.Value % groupSize != 0) {
                              result += groupSize;
                        }
                  }
            }
            return result;
      }

      // BEGIN CUT HERE
      public void run_test(int Case) { if ((Case == -1) || (Case == 0)) test_case_0(); if ((Case == -1) || (Case == 1)) test_case_1(); if ((Case == -1) || (Case == 2)) test_case_2(); }
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
            int[] Arg0 = new int[] { 1, 1, 2, 2 }
                  ; int Arg1 = 5; verify_case(0, Arg1, getMinimum(Arg0));
      }
      private void test_case_1() {
            int[] Arg0 = new int[] { 0 }
                  ; int Arg1 = 1; verify_case(1, Arg1, getMinimum(Arg0));
      }
      private void test_case_2() {
            int[] Arg0 = new int[] { 2, 2, 44, 2, 2, 2, 444, 2, 2 }
                  ; int Arg1 = 499; verify_case(2, Arg1, getMinimum(Arg0));
      }

      // END CUT HERE

      // BEGIN CUT HERE
      [STAThread]
      public static void Main(string[] args) {
            ColorfulRabbits item = new ColorfulRabbits();
            item.run_test(-1);
            Console.ReadLine();
      }
      // END CUT HERE
}
