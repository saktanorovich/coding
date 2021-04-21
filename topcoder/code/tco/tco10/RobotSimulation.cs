using System;
using System.Collections.Generic;

public class RobotSimulation
{
      public class Pair : IComparable<Pair>
      {
            public int First, Second;

            public Pair(int First, int Second)
            {
                  this.First = First;
                  this.Second = Second;
            }

            #region IComparable<Pair> Members

            public int CompareTo(Pair other)
            {
                  if (First != other.First)
                  {
                        return Math.Sign(First - other.First);
                  }
                  if (Second != other.Second)
                  {
                        return Math.Sign(Second - other.Second);
                  }
                  return 0;
            }

            #endregion
      }

      private void run(string program, SortedDictionary<Pair, bool> map, ref int x, ref int y)
      {
            foreach (char c in program)
            {
                  switch (c)
                  {
                        case 'U': y = y + 1; break;
                        case 'D': y = y - 1; break;
                        case 'R': x = x + 1; break;
                        case 'L': x = x - 1; break;
                  }
                  Pair pair = new Pair(x, y);
                  if (!map.ContainsKey(pair))
                  {
                        map.Add(pair, true);
                  }
            }
      }

      private const int ntests = 4;

      public int cellsVisited(string program, int times)
      {
            SortedDictionary<Pair, bool> map = new SortedDictionary<Pair, bool>();
            int[] temp = new int[ntests];
            int x = 0, y = 0;
            map.Add(new Pair(x, y), true);
            for (int i = 0; i < ntests; ++i)
            {
                  run(program, map, ref x, ref y);
                  temp[i] = map.Count;
            }
            if (times < ntests)
            {
                  return temp[times - 1];
            }
            int difference = temp[ntests - 1] - temp[ntests - 2];
            return temp[ntests - 1] + (times - ntests) * difference;
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
      private void test_case_0() { string Arg0 = "RRR"; int Arg1 = 100; int Arg2 = 301; verify_case(0, Arg2, cellsVisited(Arg0, Arg1)); }
      private void test_case_1() { string Arg0 = "DDU"; int Arg1 = 100; int Arg2 = 102; verify_case(1, Arg2, cellsVisited(Arg0, Arg1)); }
      private void test_case_2() { string Arg0 = "URLD"; int Arg1 = 100; int Arg2 = 3; verify_case(2, Arg2, cellsVisited(Arg0, Arg1)); }
      private void test_case_3() { string Arg0 = "UUDUDDLLDR"; int Arg1 = 1; int Arg2 = 7; verify_case(3, Arg2, cellsVisited(Arg0, Arg1)); }
      private void test_case_4() { string Arg0 = "UUDUDDLLDR"; int Arg1 = 12345678; int Arg2 = 37037039; verify_case(4, Arg2, cellsVisited(Arg0, Arg1)); }
      private void test_case_5() { string Arg0 = "RRUUULLDD"; int Arg1 = 3603602; int Arg2 = 10810815; verify_case(5, Arg2, cellsVisited(Arg0, Arg1)); }

      // END CUT HERE


      // BEGIN CUT HERE
      [STAThread]
      public static void Main(string[] args)
      {
            RobotSimulation item = new RobotSimulation();
            item.run_test(-1);
            Console.ReadLine();
      }
      // END CUT HERE
}
