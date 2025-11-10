using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_2542 {
        public long MaxScore(int[] nums1, int[] nums2, int k) {
            var a = new (int, int)[nums1.Length];
            for (var i = 0; i < nums1.Length; ++i) {
                a[i] = new (nums1[i], nums2[i]);
            }
            return MaxScore(a, a.Length, k);
        }

        private static long MaxScore((int num1, int num2)[] a, int n, int k) {
            Array.Sort(a, (x, y) => {
                return y.num2 - x.num2;
            });
            var summ = 0L;
            var heap = new PriorityQueue<int, int>();
            for (var i = 0; i < k; ++i) {
                summ += a[i].num1;
                heap.Enqueue(a[i].num1, a[i].num1);
            }
            var best = summ * a[k - 1].num2;
            for (var i = k; i < n; ++i) {
                summ += a[i].num1;
                heap.Enqueue(a[i].num1, a[i].num1);
                summ -= heap.Dequeue();
                var curr = summ * a[i].num2;
                if (best < curr) {
                    best = curr;
                }
            }
            return best;
        }
    }
}
