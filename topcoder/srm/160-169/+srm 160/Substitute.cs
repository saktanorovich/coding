using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TopCoder.Algorithm {
      public class Substitute {
            public int getValue(string key, string code) {
                  string decoded = string.Empty;
                  for (int i = 0; i < code.Length; ++i) {
                        int index = key.IndexOf(code[i]);
                        if (index >= 0) {
                              decoded += string.Format("{0}", (index + 1) % 10);
                        }
                  }
                  int result = 0, ten = 1;
                  for (int i = decoded.Length - 1; i >= 0; --i) {
                        result += (decoded[i] - '0') * ten;
                        ten *= 10;
                  }
                  return result;
            }
      }
}