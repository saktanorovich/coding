using System;
using System.Collections;
using System.Collections.Generic;

public class SetAndSet {
      private const int maxBitsCount = 20;

      public long countandset(int[] a) {
            int mask = (1 << maxBitsCount) - 1;
            for (int i = 0; i < a.Length; ++i) {
                  mask &= a[i];
            }
            return countandset(Array.ConvertAll<int, int>(a, delegate(int x) {
                  return x - mask;
            }), a.Length);
      }

      private long countandset(int[] a, int n) {
            long[] interestingSets = new long[maxBitsCount];
            for (int bit = 0; bit < maxBitsCount; ++bit) {
                  for (int i = 0; i < n; ++i) {
                        if ((a[i] & (1 << bit)) == 0) {
                              interestingSets[bit] |= (1L << i);
                        }
                  }
            }
            interestingSets = unique(interestingSets);
            bool[,] graph = new bool[interestingSets.Length, interestingSets.Length];
            for (int i = 0; i < interestingSets.Length; ++i) {
                  for (int j = i + 1; j < interestingSets.Length; ++j) {
                        if ((interestingSets[i] & interestingSets[j]) != 0) {
                              graph[i, j] = true;
                              graph[j, i] = true;
                        }
                  }
            }
            long invalidColoringsCount = 0;
            byte[][] dst = new byte[1 << interestingSets.Length][];
            dst[0] = new byte[interestingSets.Length];
            for (int subSet = 1; subSet < (1 << interestingSets.Length); ++subSet) {
                  for (byte i = 0; i < interestingSets.Length; ++i) {
                        if ((subSet & (1 << i)) != 0) {
                              dst[subSet] = (byte[])dst[subSet ^ (1 << i)].Clone();
                              dst[subSet][i] = i;
                              for (byte j = i; j < interestingSets.Length; ++j) {
                                    if ((subSet & (1 << j)) != 0) {
                                          if (graph[i, j]) {
                                                union(dst[subSet], i, j);
                                          }
                                    }
                              }
                              break;
                        }
                  }
                  long elements = 0, sign = -1;
                  int componentsCount = 0;
                  for (int i = 0; i < interestingSets.Length; ++i) {
                        if ((subSet & (1 << i)) != 0) {
                              if (dst[subSet][i] == i) {
                                    ++componentsCount;
                              }
                              elements |= interestingSets[i];
                              sign = -sign;
                        }
                  }
                  invalidColoringsCount += sign * (1L << componentsCount) * (1L << (n - cardinality(elements)));;
            }
            return (1L << n) - invalidColoringsCount;
      }

      private void union(byte[] dst, byte x, byte y) {
            byte dx = find(dst, x);
            byte dy = find(dst, y);
            if (dx != dy) {
                  dst[dx] = dy;
            }
      }

      private byte find(byte[] dst, byte x) {
            if (dst[x] != x) {
                  dst[x] = find(dst, dst[x]);
            }
            return dst[x];
      }

      private int cardinality(long set) {
            int result = 0;
            for (; set > 0; set -= (set & (-set))) {
                  ++result;
            }
            return result;
      }

      private long[] unique(long[] a) {
            List<long> result = new List<long>();
            for (int i = 0; i < a.Length; ++i) {
                  bool found = false;
                  foreach (long x in result) {
                        if (x == a[i]) {
                              found = true;
                              break;
                        }
                  }
                  if (!found) {
                        result.Add(a[i]);
                  }
            }
            return result.ToArray();
      }

      // BEGIN CUT HERE
      public void run_test(int Case) {
            if ((Case == -1) || (Case == 0)) test_case_0();
            if ((Case == -1) || (Case == 1)) test_case_1();
            if ((Case == -1) || (Case == 2)) test_case_2();
            if ((Case == -1) || (Case == 3)) test_case_3();
            if ((Case == -1) || (Case == 4)) test_case_4();
            if ((Case == -1) || (Case == 5)) test_case_5();
            if ((Case == -1) || (Case == 6)) test_case_6();
            if ((Case == -1) || (Case == 7)) test_case_7();
            if ((Case == -1) || (Case == 8)) test_case_8();
            if ((Case == -1) || (Case == 9)) test_case_9();
            if ((Case == -1) || (Case == 10)) test_case_10();
      }
      private void verify_case(int Case, long Expected, long Received) {
            Console.Write("Test Case #" + Case + "...");
            if (Expected == Received)
                  Console.WriteLine("PASSED");
            else {
                  Console.WriteLine("FAILED");
                  Console.WriteLine("\tExpected: \"" + Expected + '\"');
                  Console.WriteLine("\tReceived: \"" + Received + '\"');
            }
      }
      private void test_case_0() { int[] Arg0 = new int[] { 1, 2 }; long Arg1 = 0l; verify_case(0, Arg1, countandset(Arg0)); }
      private void test_case_1() { int[] Arg0 = new int[] { 1, 2, 3, 4 }; long Arg1 = 2l; verify_case(1, Arg1, countandset(Arg0)); }
      private void test_case_2() { int[] Arg0 = new int[] { 1, 2, 3, 4, 5 }; long Arg1 = 8l; verify_case(2, Arg1, countandset(Arg0)); }
      private void test_case_3() { int[] Arg0 = new int[] { 6, 6, 6 }; long Arg1 = 6l; verify_case(3, Arg1, countandset(Arg0)); }
      private void test_case_4() { int[] Arg0 = new int[] { 13, 10, 4, 15, 4, 8, 4, 2, 4, 14, 0 }; long Arg1 = 1728l; verify_case(4, Arg1, countandset(Arg0)); }
      private void test_case_5() { int[] Arg0 = new int[] { 990, 1022, 986, 191, 767, 215, 767, 1023, 892, 859, 1018, 427, 1015, 507, 503, 957, 1021, 1023, 559, 990, 1007, 851, 975, 1007, 767, 991, 478, 999, 1023, 891, 732, 669, 862, 1015 }; long Arg1 = 13100809120l; verify_case(5, Arg1, countandset(Arg0)); }
      private void test_case_6() { int[] Arg0 = new int[] { 1032060, 1048047, 1048555, 1046399, 950139, 1038315, 978937, 753471, 779775, 475007, 1038323, 1038317, 1039983, 524283, 785909, 949243, 1048250, 1048207, 1047039, 1040061, 1031655, 1048510, 521967, 655287, 785913, 221183, 916399, 1040285, 1042927, 1036287, 981949, 1032063, 1042431, 1047469, 915455, 909175, 786399, 1048249, 1039039, 1013754, 392175, 1015541, 979963, 966652, 524271, 499709, 1048318, 1040127, 1048181, 891903 }; long Arg1 = 651703714456398l; verify_case(6, Arg1, countandset(Arg0)); }
      private void test_case_7() { int[] Arg0 = new int[] { 870666, 357325, 884713, 432879, 970069, 537563, 280787, 1005935, 863965, 831597 }; long Arg1 = 0l; verify_case(7, Arg1, countandset(Arg0)); }
      private void test_case_8() { int[] Arg0 = new int[] { 1, 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024, 2048, 4096, 8192, 16384, 32768, 65536, 131072, 262144, 524288 }; long Arg1 = 1048534l; verify_case(8, Arg1, countandset(Arg0)); }
      private void test_case_9() { int[] Arg0 = new int[] { 1043196, 914906, 589286, 319355 }; long Arg1 = 0l; verify_case(9, Arg1, countandset(Arg0)); }
      private void test_case_10() { int[] Arg0 = new int[] { 946158, 1044479, 1044350, 982895, 1048327, 1047527, 966589, 980988, 1015806, 978943, 1048031, 1015603, 966303, 786431, 1048511, 1043435, 1026879, 516046, 1044467, 491507 }; long Arg1 = 0l; verify_case(10, Arg1, countandset(Arg0)); }
      // END CUT HERE

      // BEGIN CUT HERE
      [STAThread]
      public static void Main(string[] args) {
            SetAndSet item = new SetAndSet();
            item.run_test(-1);
            Console.ReadLine();
      }
      // END CUT HERE
}
