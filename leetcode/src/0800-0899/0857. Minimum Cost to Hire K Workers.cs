using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0857 {
        public double MincostToHireWorkers(int[] quality, int[] wage, int K) {
            var order = new int[quality.Length];
            for (var i = 0; i < quality.Length; ++i) {
                order[i] = i;
            }
            Array.Sort(order, (a, b) => {
                return wage[a] * quality[b] - wage[b] * quality[a];
            });
            var heap = new SortedSet<int>(
                Comparer<int>.Create((a, b) => {
                    if (quality[a] != quality[b]) {
                        return quality[a] - quality[b];
                    }
                    return a - b;
            }));
            var suma = 0.0;
            var answ = double.MaxValue;
            for (var i = 0; i < quality.Length; ++i) {
                if (heap.Count == K) {
                    suma -= quality[heap.Max];
                    heap.Remove(heap.Max);
                }
                heap.Add(order[i]);
                suma += quality[order[i]];
                if (heap.Count == K) {
                    answ = Math.Min(answ, suma * wage[order[i]] / quality[order[i]]);
                }
            }
            return answ;
        }
    }
}
