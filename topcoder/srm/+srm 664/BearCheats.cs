using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class BearCheats {
        public string eyesight(int a, int b) {
            return eyesight(digitize(a), digitize(b));
        }

        private string eyesight(IList<int> a, IList<int> b) {
            for (int i = 0, cnt = 0; i < a.Count; ++i) {
                if (a[i] != b[i]) {
                    if (++cnt > 1)
                        return "glasses";
                }
            }
            return "happy";
        }

        private IList<int> digitize(int x) {
            var result = new List<int>();
            for (; x > 0; x /= 10) {
                result.Add(x % 10);
            }
            return result;
        }
    }
}
