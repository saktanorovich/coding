using System;

namespace TopCoder.Algorithm {
    public class Soccer {
        public int maxPoints(int[] wins, int[] ties) {
            var result = 0;
            for (var i = 0; i < wins.Length; ++i) {
                result = Math.Max(result, 3 * wins[i] + ties[i]);
            }
            return result;
        }
    }
}