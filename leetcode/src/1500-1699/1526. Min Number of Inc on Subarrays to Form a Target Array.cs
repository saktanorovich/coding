using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1526 {
        public int MinNumberOperations(int[] target) {
            return MinNumberOperations(new Tree(target), target, 0, 0, target.Length - 1);
        }

        private int MinNumberOperations(Tree tree, int[] target, int value, int le, int ri) {
            if (le <= ri) {
                var min = tree.get(le, ri);
                var res = target[min] - value;
                res += MinNumberOperations(tree, target, target[min], le, min - 1);
                res += MinNumberOperations(tree, target, target[min], min + 1, ri);
                return res;
            }
            return 0;
        }

        public class Tree {
            private readonly int[] target;
            private readonly int[] min;
            private readonly int size;

            public Tree(int[] target) {
                this.target = target;
                size = target.Length;
                min = new int[4 * size];
                build(target, 1, 0, size - 1);
            }

            public int get(int le, int ri) {
                return get(1, 0, size - 1, le, ri);
            }

            private int get(int node, int lo, int hi, int le, int ri) {
                if (lo == le && hi == ri) {
                    return min[node];
                }
                if (lo <= ri && le <= hi) {
                    var xx = (lo + hi) / 2;
                    var l = get(2 * node, lo, xx, le, Math.Min(xx, ri));
                    var r = get(2 * node + 1, xx + 1, hi, Math.Max(xx + 1, le), ri);
                    if (l == -1) return r;
                    if (r == -1) return l;
                    return target[l] < target[r] ? l : r;
                } else {
                    return -1;
                }
            }

            private int build(int[] target, int node, int lo, int hi) {
                if (lo == hi) {
                    return min[node] = lo;
                } else {
                    var x = (lo + hi) / 2;
                    var l = build(target, 2 * node, lo, x);
                    var r = build(target, 2 * node + 1, x + 1, hi);
                    return min[node] = target[l] < target[r] ? l : r;
                }
            }
        }
    }
}
