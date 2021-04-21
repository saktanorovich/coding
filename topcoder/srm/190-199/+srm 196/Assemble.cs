using System;

namespace TopCoder.Algorithm {
    public class Assemble {
        public int minCost(int[] connect) {
            var best = new int[connect.Length, connect.Length];
            for (var size = 2; size < connect.Length; ++size) {
                for (var beg = 0; beg + size < connect.Length; ++beg) {
                    var end = beg + size - 1;
                    best[beg, end] = int.MaxValue;
                    for (var mid = beg; mid < end; ++mid) {
                        best[beg, end] = Math.Min(best[beg, end], best[beg, mid] + best[mid + 1, end] + (connect[beg] + mid - beg + 1) * connect[mid + 1] * (connect[end + 1] + end - mid));
                    }
                }
            }
            return best[0, connect.Length - 2];
        }
    }
}