using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithms {
      public class Lexer {
            public string[] tokenize(string[] tokens, string input) {
                  List<string> result = new List<string>();
                  while (!string.IsNullOrEmpty(input)) {
                        int ix = -1;
                        for (int i = 0; i < tokens.Length; ++i) {
                              if (input.IndexOf(tokens[i]) == 0) {
                                    if (ix == -1 || tokens[i].Length > tokens[ix].Length) {
                                          ix = i;
                                    }
                              }
                        }
                        if (ix != -1) {
                              result.Add(tokens[ix]);
                              input = input.Substring(tokens[ix].Length, input.Length - tokens[ix].Length);
                        }
                        else {
                              input = input.Substring(1, input.Length - 1);
                        }
                  }
                  return result.ToArray();
            }

            private static string ToString(string[] a) {
                  string result = string.Empty;
                  for (int i = 0; i < a.Length; ++i) {
                        result += a[i];
                        if (i + 1 < a.Length) {
                              result += ' ';
                        }
                  }
                  return result + Environment.NewLine;
            }

            public static void Main(string[] args) {
                  Console.WriteLine(ToString(new Lexer().tokenize(new string[] { "ab", "aba", "A" }, "ababbbaAab")));
                  Console.WriteLine(ToString(new Lexer().tokenize(new string[] { "AbCd", "dEfG", "GhIj" }, "abCdEfGhIjAbCdEfGhIj")));

                  Console.WriteLine("Press any key to continue...");
                  Console.ReadLine();
            }
      }
}