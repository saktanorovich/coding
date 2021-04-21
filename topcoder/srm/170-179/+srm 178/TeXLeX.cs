using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
    public class TeXLeX {
        public int[] getTokens(string input) {
            var result = new List<int>();
            if (input.Length > 3) {
                if (input.Substring(0, 2) == "^^") {
                    if (Hex.IndexOf(input[2]) >= 0 && Hex.IndexOf(input[3]) >= 0) {
                        result.AddRange(getTokens(ToHex(input.Substring(2, 2)) + input.Substring(4)));
                        return result.ToArray();
                    }
                }
            }
            if (input.Length > 2) {
                if (input.Substring(0, 2) == "^^") {
                    if (input[2] > 63) {
                        result.AddRange(getTokens((char)(input[2] - 64) + input.Substring(3)));
                        return result.ToArray();
                    }
                    if (input[2] < 64) {
                        result.AddRange(getTokens((char)(input[2] + 64) + input.Substring(3)));
                        return result.ToArray();
                    }
                }
            }
            if (input.Length > 0) {
                result.Add(input[0]);
                result.AddRange(getTokens(input.Substring(1)));
            }
            return result.ToArray();
        }

        private static char ToHex(string s) {
            int result = 0, pow = 1;
            for (var i = s.Length - 1; i >= 0; --i) {
                result += Hex.IndexOf(s[i]) * pow;
                pow *= 16;
            }
            return (char)result;
        }

        private const string Hex = "0123456789abcdef";
    }
}