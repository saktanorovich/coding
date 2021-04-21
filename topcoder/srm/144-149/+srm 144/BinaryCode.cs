using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class BinaryCode {
            public string[] decode(string message) {
                  return new string[] {
                        decode(0, parse(message), message.Length),
                        decode(1, parse(message), message.Length)
                  };
            }

            private string decode(int first, int[] encrypted, int n) {
                  int[] decrypted = new int[n + 2];
                  decrypted[1] = first;
                  for (int i = 1; i <= n; ++i) {
                        decrypted[i + 1] = encrypted[i - 1] - decrypted[i] - decrypted[i - 1];
                  }
                  if (decrypted[0] == 0 && decrypted[n + 1] == 0) {
                        string result = string.Empty;
                        for (int i = 1; i <= n; ++i) {
                              if (0 <= decrypted[i] && decrypted[i] <= 1) {
                                    result += decrypted[i].ToString();
                              }
                              else return "NONE";
                        }
                        return result;
                  }
                  return "NONE";
            }

            private int[] parse(string message) {
                  return Array.ConvertAll(message.ToCharArray(), delegate(char c) {
                        return int.Parse(c.ToString());
                  });
            }
      }
}