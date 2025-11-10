using System;
using System.Collections.Generic;

public class TheEncryptionDivOne
{
      private class State
      {
            private int[,] _state = new int[3, 3];

            public State(int[,] s)
            {
                  for (int i = 0; i < 3; ++i)
                        for (int j = 0; j < 3; ++j)
                              _state[i, j] = s[i, j];
            }

            public State(State s)
                  : this(s._state)
            {
            }

            public int this[int i, int j]
            {
                  get
                  {
                        return _state[i, j];
                  }
                  set
                  {
                        _state[i, j] = value;
                  }
            }

            public override bool Equals(object obj)
            {
                  if (obj is State)
                  {
                        State s = (State)obj;
                        for (int i = 0; i < 3; ++i)
                              for (int j = 0; j < 3; ++j)
                                    if (i > 0 || j > 0)
                                          if (_state[i, j] != s[i, j])
                                                return false;
                        return true;
                  }
                  return false;
            }

            public override string ToString()
            {
                  return base.ToString();
            }

            public override int GetHashCode()
            {
                  string result = string.Empty;
                  for (int i = 0; i < 3; ++i)
                        for (int j = 0; j < 3; ++j)
                              if (i > 0 || j > 0)
                                    result += _state[i, j];
                  return result.GetHashCode();
            }
      }

      private const long modulo = 1234567891;
      private const int undefined = -1;

      public int count(string message, string encoded_message)
      {
            int[] permutation = new int[52];
            int[] back_permutation = new int[52];
            for (int i = 0; i < 52; ++i)
                  permutation[i] = back_permutation[i] = undefined;
            for (int i = 0; i < message.Length; ++i)
            {
                  int code = get_code(message[i]);
                  int back_code = get_code(encoded_message[i]);
                  if (code % 26 == back_code % 26)
                        return 0;
                  if (permutation[code] == undefined)
                        permutation[code] = back_code;
                  else if (permutation[code] != back_code)
                        return 0;
                  if (back_permutation[back_code] == undefined)
                        back_permutation[back_code] = code;
                  else if (back_permutation[back_code] != code)
                        return 0;
            }
            List<int> set1 = new List<int>();
            List<int> set2 = new List<int>();
            for (int i = 0; i < 52; ++i)
            {
                  if (permutation[i] == undefined)
                        set1.Add(i % 26);
                  if (back_permutation[i] == undefined)
                        set2.Add(i % 26);
            }
            if (set1.Count == set2.Count)
            {
                  int[] cnt1 = new int[26];
                  int[] cnt2 = new int[26];
                  for (int i = 0; i < set1.Count; ++i)
                  {
                        ++cnt1[set1[i]];
                        ++cnt2[set2[i]];
                  }
                  int[,] state = new int[3, 3];
                  for (int i = 0; i < 26; ++i)
                        if (cnt1[i] + cnt2[i] > 0)
                        {
                              ++state[cnt1[i], cnt2[i]];
                        }
                  return (int)run(new State(state));
            }
            return 0;
      }

      private Dictionary<State, long> _cache = new Dictionary<State, long>();

      private long run(State state)
      {
            if (!_cache.ContainsKey(state))
            {
                  int candidate = undefined;
                  for (int i = 1; i < 3; ++i) /* iterate starting from 1 because a char should exist in the 1st row */
                        for (int j = 0; j < 3; ++j)
                              if (state[i, j] > 0)
                              {
                                    candidate = i * 3 + j;
                                    break;
                              }
                  long result = 0;
                  if (candidate == undefined)
                        result = 1;
                  else
                  {
                        int ix = candidate / 3, jx = candidate % 3;
                        for (int ic = 0; ic < 3; ++ic)
                              for (int jc = 1; jc < 3; ++jc) /* iterate starting from 1 because a char should exist in the 2nd row */
                                    if (state[ic, jc] > 0)
                                    {
                                          long total = state[ic, jc];
                                          if (ic == ix && jc == jx)
                                                total = total - 1;
                                          total *= jc; /* note that 'a' and 'A' denote different letters, so we should multiply total by 2 in the case when jc equals to 2 */
                                          if (total > 0)
                                          {
                                                State next = new State(state);
                                                --next[ix, jx];
                                                --next[ic, jc];
                                                ++next[ix - 1, jx];
                                                ++next[ic, jc - 1];
                                                result = (result + (total * run(next)) % modulo) % modulo;
                                          }
                                    }
                  }
                  _cache.Add(state, result);
            }
            return _cache[state];
      }

      private int get_code(char c)
      {
            if ('a' <= c && c <= 'z')
                  return c - 'a';
            return 26 + c - 'A';
      }

      // BEGIN CUT HERE
      public void run_test(int Case) { if ((Case == -1) || (Case == 0)) test_case_0(); if ((Case == -1) || (Case == 1)) test_case_1(); if ((Case == -1) || (Case == 2)) test_case_2(); if ((Case == -1) || (Case == 3)) test_case_3(); }
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
      private void test_case_0() { string Arg0 = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWX"; string Arg1 = "cdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ"; int Arg2 = 2; verify_case(0, Arg2, count(Arg0, Arg1)); }
      private void test_case_1() { string Arg0 = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWX"; string Arg1 = "bcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXY"; int Arg2 = 1; verify_case(1, Arg2, count(Arg0, Arg1)); }
      private void test_case_2() { string Arg0 = "topcoder"; string Arg1 = "TOPCODER"; int Arg2 = 0; verify_case(2, Arg2, count(Arg0, Arg1)); }
      private void test_case_3() { string Arg0 = "thisisaveryhardproblem"; string Arg1 = "nobodywillsolveittoday"; int Arg2 = 0; verify_case(3, Arg2, count(Arg0, Arg1)); }

      // END CUT HERE


      // BEGIN CUT HERE
      [STAThread]
      public static void Main(string[] args)
      {
            TheEncryptionDivOne item = new TheEncryptionDivOne();
            item.run_test(-1);
            Console.ReadLine();
      }
      // END CUT HERE
}
