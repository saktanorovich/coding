using System;

namespace TopCoder.Algorithm {
    public class BenfordsLaw {
        public int questionableDigit(int[] transactions, int threshold) {
            var actual = new int[10];
            foreach (var data in transactions) {
                ++actual[int.Parse("" + data.ToString()[0])];
            }
            var expected = new double[10];
            for (var d = 1; d < 10; ++d) {
                expected[d] = Math.Log10(1.0 + 1.0 / d) * transactions.Length;
            }
            for (var d = 1; d < 10; ++d) {
                if (actual[d] * threshold <= expected[d] || expected[d] * threshold <= actual[d]) {
                    return d;
                }
            }
            return -1;
        }
    }
}