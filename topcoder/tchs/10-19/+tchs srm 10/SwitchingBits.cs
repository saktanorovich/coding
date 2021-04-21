using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class SwitchingBits {
        public int minSwitches(string[] s) {
            return minSwitches("#" + string.Join("", s));
        }

        private int minSwitches(string s) {
            var cnt = new int[2];
            for (int i = 1; i < s.Length; ++i) {
                if (s[i] != s[i - 1]) {
                    ++cnt[s[i] - '0'];
                }
            }
            return Math.Min(cnt[0], cnt[1]);
        }
    }
}
