using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1383 {
        public int MaxPerformance(int n, int[] speed, int[] efficiency, int k) {
            var heap = new SortedSet<int>(
                Comparer<int>.Create((a, b) => {
                    if (speed[a] != speed[b]) {
                        return speed[a] - speed[b];
                    }
                    return a - b;
            }));
            var order = Enumerable.Range(0, n).ToArray();
            Array.Sort(order, (a, b) => {
                return efficiency[b] - efficiency[a];
            });
            var best = 0L;
            var summ = 0L;
            for (var i = 0; i < n; ++i) {
                heap.Add(order[i]);
                summ += speed[order[i]];
                if (heap.Count > k) {
                    var slowest = heap.First();
                    summ -= speed[slowest];
                    heap.Remove(slowest);
                }
                if (best < efficiency[order[i]] * summ) {
                    best = efficiency[order[i]] * summ;
                }
            }
            return (int)(best % 1000000007);
        }
    }
}
