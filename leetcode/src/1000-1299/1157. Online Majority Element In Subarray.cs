using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1157 {
        // Inspired by Boyer-Moore majority vote algorithm
        public class MajorityChecker {
            private readonly Dictionary<int, List<int>> indx;
            private readonly (int elem, int freq)[] tree;
            private readonly int size;

            public MajorityChecker(int[] arr) {
                size = arr.Length;
                tree = new (int elem, int freq)[4 * arr.Length];
                indx = new Dictionary<int, List<int>>();
                for (var i = 0; i < arr.Length; ++i) {
                    indx.TryAdd(arr[i], new List<int>());
                    indx[arr[i]].Add(i);
                }
                Build(arr, 1, 0, arr.Length - 1);
            }

            public int Query(int left, int right, int threshold) {
                var pair = Query(1, 0, size - 1, left, right);
                if (pair.freq > 0) {
                    var a = Count(indx[pair.elem], left - 1);
                    var b = Count(indx[pair.elem], right);
                    if (b - a >= threshold) {
                        return pair.elem;
                    }
                }
                return -1;
            }

            private (int elem, int freq) Query(int node, int lo, int hi, int le, int ri) {
                if (lo == le && hi == ri) {
                    return tree[node];
                }
                if (lo <= ri && le <= hi) {
                    var x = (lo + hi) / 2;
                    var a = Query(2 * node, lo, x, le, Math.Min(x, ri));
                    var b = Query(2 * node + 1, x + 1, hi, Math.Max(x + 1, le), ri);
                    return Merge(a, b);
                } else {
                    return (0, 0);
                }
            }

            private int Count(List<int> a, int p) {
                if (p < 0) {
                    return 0;
                }
                int lo = 0, hi = a.Count - 1;
                while (lo < hi) {
                    var x = (lo + hi + 1) / 2;
                    if (a[x] > p) {
                        hi = x - 1;
                    } else {
                        lo = x;
                    }
                }
                return a[lo] <= p ? lo + 1 : lo;
            }

            private void Build(int[] arr, int node, int lo, int hi) {
                if (lo == hi) {
                    tree[node] = (arr[lo], 1);
                } else {
                    var x = (lo + hi) / 2;
                    Build(arr, 2 * node, lo, x);
                    Build(arr, 2 * node + 1, x + 1, hi);
                    tree[node] = Merge(tree[2 * node], tree[2 * node + 1]);
                }
            }

            private (int, int) Merge((int elem, int freq) a, (int elem, int freq) b) {
                if (a.elem == b.elem) {
                    return (a.elem, a.freq + b.freq);
                }
                if (a.freq > b.freq) {
                    return (a.elem, a.freq - b.freq);
                } else {
                    return (b.elem, b.freq - a.freq);
                }
            }
        }
    }
}
