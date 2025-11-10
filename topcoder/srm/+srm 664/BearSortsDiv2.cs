using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class BearSortsDiv2 {
        public double getProbability(int[] seq) {
            return getProbability(seq, seq.Length);
        }

        private double getProbability(int[] seq, int n) {
            var array = new int[n];
            var where = new int[n];
            for (var i = 0; i < n; ++i) {
                array[i] = i;
                where[seq[i] - 1] = i;
            }
            var times = 0;
            mergeSort(array, 0, n,
                (x, y) => {
                    ++times;
                    return where[x] < where[y];
                });
            return Math.Log(0.5) * times;
        }

        private static void mergeSort(int[] a, int lo, int hi, Func<int, int, bool> less) {
            if (lo + 1 >= hi) return;

            var xx = (lo + hi) / 2;
            mergeSort(a, lo, xx, less);
            mergeSort(a, xx, hi, less);

            var merged = new List<int>();
            for (int p1 = lo, p2 = xx; p1 < xx || p2 < hi;) {
                if (p1 == xx) {
                    merged.Add(a[p2++]);
                    continue;
                }
                if (p2 == hi) {
                    merged.Add(a[p1++]);
                    continue;
                }
                merged.Add(less(a[p1], a[p2])
                    ? a[p1++]
                    : a[p2++]);
            }
            for (var i = lo; i < hi; ++i) {
                a[i] = merged[i - lo];
            }
        }
    }
}
