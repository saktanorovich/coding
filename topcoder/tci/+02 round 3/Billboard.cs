using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithms {
      public class Billboard {
            public string[] enlarge(string message, string[] letters) {
                  string[] result = new string[5];
                  for (int r = 0; r < 5; ++r) {
                        result[r] = string.Empty;
                        for (int i = 0; i < message.Length; ++i) {
                              for (int j = 0; j < letters.Length; ++j) {
                                    if (letters[j][0].Equals(message[i])) {
                                          result[r] += letters[j].Substring(2 + r * 5 + r, 5);
                                          break;
                                    }
                              }
                              if (i + 1 < message.Length) {
                                    result[r] += '.';
                              }
                        }
                  }
                  return result;
            }

            private static string ToString<T>(T[] a) {
                  string result = string.Empty;
                  for (int i = 0; i < a.Length; ++i) {
                        result += a[i].ToString();
                        if (i + 1 < a.Length) {
                              result += ' ';
                        }
                        result += Environment.NewLine;
                  }
                  return result + Environment.NewLine;
            }

            public static void Main(string[] args) {
                  Console.WriteLine(ToString(new Billboard().enlarge("TOPCODER", new string[] {
 "T:#####-..#..-..#..-..#..-..#.."
,"O:#####-#...#-#...#-#...#-#####"
,"P:####.-#...#-####.-#....-#...."
,"C:.####-#....-#....-#....-.####"
,"D:####.-#...#-#...#-#...#-####."
,"E:#####-#....-####.-#....-#####"
,"R:####.-#...#-####.-#.#..-#..##"})));

                  Console.WriteLine("Press any key to continue...");
                  Console.ReadLine();
            }
      }
}