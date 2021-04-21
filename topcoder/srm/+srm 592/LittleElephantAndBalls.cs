using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class LittleElephantAndBalls {
            private readonly string alphabet = "RGB";

            public int getNumber(string s) {
                  int result = 0;
                  IDictionary<char, int>[] total = new IDictionary<char, int>[2];
                  for (int i = 0; i < 2; ++i) {
                        total[i] = new Dictionary<char, int>();
                        for (int k = 0; k < alphabet.Length; ++k) {
                              total[i][alphabet[k]] = 0;
                        }
                  }
                  for (int p = 0; p < s.Length; ++p) {
                        char c = s[p];
                        for (int i = 0; i < 2; ++i) {
                              for (int k = 0; k < alphabet.Length; ++k) {
                                    result += total[i][alphabet[k]];
                              }
                        }
                        for (int i = 0; i < 2; ++i) {
                              if (total[i][c] == 0) {
                                    total[i][c] = 1;
                                    break;
                              }
                        }
                  }
                  return result;
            }

            internal static void Main(string[] args) {
                  Console.WriteLine(new LittleElephantAndBalls().getNumber("RGB")); // Returns: 3
                  Console.WriteLine(new LittleElephantAndBalls().getNumber("RGGRBBB")); // Returns: 21
                  Console.WriteLine(new LittleElephantAndBalls().getNumber("RRRGBRR")); // Returns: 16
                  Console.WriteLine(new LittleElephantAndBalls().getNumber("RRRR")); // Returns: 5
                  Console.WriteLine(new LittleElephantAndBalls().getNumber("GGRRRGR")); // Returns: 18
                  Console.WriteLine(new LittleElephantAndBalls().getNumber("G")); //Returns: 0

                  Console.WriteLine("\nPress any key to continue...");
                  Console.ReadKey();
            }
      }
}
