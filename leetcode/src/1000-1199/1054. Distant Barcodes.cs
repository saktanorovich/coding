using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1054 {
        public int[] RearrangeBarcodes(int[] barcodes) {
            var cnt = new int[10000 + 1];
            foreach (var c in barcodes) {
                ++cnt[c];
            }
            Array.Sort(barcodes, Comparer<int>.Create((a, b) => {
                if (cnt[a] != cnt[b]) {
                    return cnt[b] - cnt[a];
                }
                return a - b;
            }));
            var res = new int[barcodes.Length];
            var e = 0;
            var o = 1;
            foreach (var c in barcodes) {
                while (cnt[c] > 0 && e < barcodes.Length) {
                    res[e] = c;
                    cnt[c]--;
                    e += 2;
                }
                while (cnt[c] > 0 && o < barcodes.Length) {
                    res[o] = c;
                    cnt[c]--;
                    o += 2;
                }
            }
            return res;
        }
    }
}
