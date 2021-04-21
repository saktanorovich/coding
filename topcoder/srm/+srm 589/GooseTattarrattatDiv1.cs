using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TopCoder.Algorithm {
      public class GooseTattarrattatDiv1 {
            public int getmin(string s) {
                  return getmin(Array.ConvertAll(s.ToCharArray(), delegate(char value) {
                        return value - 'a';
                  }), s.Length);
            }

            /* We can note that palindrome can be described by a fixed number of classes where
             * each class is represented by a character. In this case we can merge classes with less
             * cardinality to the class with greater cardinality. */
            private int getmin(int[] text, int n) {
                  int result = 0;
                  while (!isPalindrome(text, n)) {
                        text = process(text, n, ref result);
                  }
                  return result;
            }

            private int[] process(int[] text, int n, ref int result) {
                  int[] cardinality = new int[26];
                  for (int i = 0; i < n; ++i) {
                        ++cardinality[text[i]];
                  }
                  List<int>[] graph = Array.ConvertAll(new List<int>[26], delegate(List<int> value) {
                        return new List<int>();
                  });
                  for (int i = 0; i < n; ++i) {
                        if (text[i] != text[n - 1 - i]) {
                              if (!graph[text[i]].Contains(text[n - 1 - i])) {
                                    graph[text[i]].Add(text[n - 1 - i]);
                              }
                        }
                  }
                  int maximumElement = -1;
                  for (int i = 0; i < 26; ++i) {
                        if (graph[i].Count > 0) {
                              if (maximumElement == -1 || cardinality[i] > cardinality[maximumElement]) {
                                    maximumElement = i;
                              }
                        }
                  }
                  for (int i = 0; i < graph[maximumElement].Count; ++i) {
                        result += cardinality[graph[maximumElement][i]];
                  }
                  return Array.ConvertAll(text, delegate(int value) {
                        if (graph[maximumElement].Contains(value)) {
                              return maximumElement;
                        }
                        return value;
                  });
            }

            private bool isPalindrome(int[] text, int n) {
                  for (int i = 0, j = n - 1; i < j; ++i, --j) {
                        if (text[i] != text[j]) {
                              return false;
                        }
                  }
                  return true;
            }

            internal static void Main(string[] args) {
                  Console.WriteLine(new GooseTattarrattatDiv1().getmin("geese")); // Returns: 2
                  Console.WriteLine(new GooseTattarrattatDiv1().getmin("tattarrattat")); // Returns: 0
                  Console.WriteLine(new GooseTattarrattatDiv1().getmin("xyyzzzxxx")); // Returns: 2
                  Console.WriteLine(new GooseTattarrattatDiv1().getmin("abaabb")); // Returns: 3
                  Console.WriteLine(new GooseTattarrattatDiv1().getmin("xrepayuyubctwtykrauccnquqfuqvccuaakylwlcjuyhyammag")); // Returns: 11
                  Console.WriteLine(new GooseTattarrattatDiv1().getmin("abcdefghijklmnopqrstuvwxyzyxwvutsrqponmlkjihgfedcb")); // Returns: 48

                  Console.WriteLine("\nPress any key to continue...");
                  Console.ReadKey();
            }
      }
}
