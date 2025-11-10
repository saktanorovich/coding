using System;

namespace TopCoder.Algorithm {
    public class SimpleIO {
        public int worstCase(string pattern, int target) {
            var result = -1;
            for (var start = 0; start < pattern.Length; ++start) {
                result = Math.Max(result, worstCase(pattern, start, target, pattern.Length));
            }
            return result;
        }

        private static int worstCase(string pattern, int start, int target, int n) {
            var presses = 0;
            for (var it = 0; it < n; ++it) {
                ++presses;
                if (unique(pattern + pattern, start, presses, n)) {
                    start = (start + presses) % n;
                    while (start != target) {
                        start = (start + 1) % n;
                        presses = presses + 1;
                    }
                    return presses;
                }
            }
            return -1;
        }

        private static bool unique(string pattern, int start, int presses, int n) {
            var last = start + presses;
            for (var len = 1; len <= presses; ++len) {
                /* assume unique sequence has length len and ends at the last.. */
                var target = pattern.Substring(last - len + 1, len);
                if (match(pattern, target, n) == 1) {
                    return true;
                }
            }
            return false;
        }

        private static int match(string pattern, string target, int n) {
            var result = 0;
            for (var i = 0; i < n; ++i) {
                if (pattern.Substring(i, target.Length) == target) {
                    result = result + 1;
                }
            }
            return result;
        }
    }
}