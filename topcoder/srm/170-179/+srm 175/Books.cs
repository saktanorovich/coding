using System;

namespace TopCoder.Algorithm {
    public class Books {
        public int sortMoves(string[] titles) {
            var inc = new int[titles.Length];
            for (var i = 0; i < titles.Length; ++i) {
                inc[i] = 1;
                for (var j = 0; j < i; ++j) {
                    if (string.CompareOrdinal(titles[j], titles[i]) <= 0) {
                        inc[i] = Math.Max(inc[i], inc[j] + 1);
                    }
                }
            }
            var max = 1;
            for (var i = 0; i < titles.Length; ++i) {
                max = Math.Max(max, inc[i]);
            }
            return titles.Length - max;
        }
    }
}