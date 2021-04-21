using System;
using System.Collections.Generic;

namespace interview.hackerrank {
    public class Subpalindrome {
        public int palindrome(string str) {
            var hash = new Dictionary<string, int>();
            for (var center = 0; center < str.Length; ++center) {
                for (var offset = 0; ; ++offset) {
                    if (0 <= center - offset && center + offset < str.Length) {
                        if (str[center - offset] == str[center + offset]) {
                            var palin = str.Substring(center - offset, 2 * offset + 1);
                            if (!hash.ContainsKey(palin)) {
                                hash[palin] = 1;
                            }
                        }
                        else break;
                    }
                    else break;
                }
                for (var d = -1; d <= +1; d += 2) {
                    var le = Math.Min(center + d, center);
                    var ri = Math.Max(center + d, center);
                    if (0 <= le && ri < str.Length) {
                        if (str[le] == str[ri]) {
                            for (var offset = 0; true; ++offset) {
                                if (0 <= le - offset && ri + offset < str.Length) {
                                    if (str[le - offset] == str[ri + offset]) {
                                        var palin = str.Substring(le - offset, 2 * offset + 2);
                                        if (!hash.ContainsKey(palin)) {
                                            hash[palin] = 1;
                                        }
                                    }
                                    else break;
                                }
                                else break;
                            }
                        }
                    }
                }
            }
            return hash.Keys.Count;
        }
    }
}
