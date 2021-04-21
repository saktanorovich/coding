using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TopCoder.Algorithms {
      public class Bullets {
            public int match(string[] guns, string bullet) {
                  for (int k = 0; k < bullet.Length; ++k) {
                        for (int i = 0; i < guns.Length; ++i) {
                              if (guns[i].Length == bullet.Length) {
                                    bool match = true;
                                    for (int j = 0; j < bullet.Length; ++j) {
                                          if (guns[i][j] != bullet[(j + k) % bullet.Length]) {
                                                match = false;
                                                break;
                                          }
                                    }
                                    if (match) return i;
                              }
                        }
                  }
                  return -1;
            }

            private static string ToString<T>(T[] a) {
                  string result = string.Empty;
                  for (int i = 0; i < a.Length; ++i) {
                        result += a[i].ToString();
                        if (i + 1 < a.Length) {
                              result += ' ';
                        }
                  }
                  return result + Environment.NewLine;
            }

            public static void Main(string[] args) {
                  Console.WriteLine("Press any key to continue...");
                  Console.ReadLine();
            }
      }
}