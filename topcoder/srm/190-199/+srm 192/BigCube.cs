using System;

namespace TopCoder.Algorithm {
    public class BigCube {
        public string largestCube(string[] range) {
            var lo = new long[range.Length];
            var hi = new long[range.Length];
            for (var i = 0; i < range.Length; ++i) {
                var data = range[i].Split('-');
                lo[i] = long.Parse(data[0]);
                hi[i] = long.Parse(data[1]);
            }
            return largestCube(lo, hi);
        }

        private static string largestCube(long[] lo, long[] hi) {
            for (var res = 100000L; res >= 0; --res) {
                var cube = res * res * res;
                for (var i = 0; i < lo.Length; ++i)
                    if (lo[i] <= cube && cube <= hi[i]) {
                        return cube.ToString();
                    }
            }
            return string.Empty;
        }
    }
}