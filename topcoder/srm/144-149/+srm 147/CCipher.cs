using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class CCipher {
            public string decode(string cipherText, int shift) {
                  string result = string.Empty;
                  for (int i = 0; i < cipherText.Length; ++i) {
                        result += alphabet[((alphabet.IndexOf(cipherText[i]) - shift + 26) % 26)];
                  }
                  return result;
            }

            private static readonly string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
      }
}