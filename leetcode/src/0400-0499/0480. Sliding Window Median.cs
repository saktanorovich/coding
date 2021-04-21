using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0480 {
        public double[] MedianSlidingWindow(int[] nums, int k) {
            return MedianSlidingWindow(nums, nums.Length, k);
        }

        private double[] MedianSlidingWindow(int[] nums, int n, int k) {
            var window = new SlidingWindow(nums, k);
            var result = new List<double>();
            result.Add(window.Median());
            for (var i = k; i < n; ++i) {
                window.Remove(i - k);
                window.Insert(i);
                result.Add(window.Median());
            }
            return result.ToArray();
        }

        private sealed class SlidingWindow {
            private readonly SortedSet<(int, int)> ls;
            private readonly SortedSet<(int, int)> rs;
            private readonly int[] nums;

            public SlidingWindow(int[] nums, int k) {
                var cmp = Comparer<(int idx, int val)>.Create((x, y) => {
                    if (x.val != y.val) {
                        return x.val < y.val ? -1 : x.val > y.val ? +1 : 0;
                    } else {
                        return x.idx < y.idx ? -1 : x.idx > y.idx ? +1 : 0;
                    }
                });
                this.ls = new SortedSet<(int, int)>(cmp);
                this.rs = new SortedSet<(int, int)>(cmp);
                this.nums = nums;
                for (var i = 0; i < k; ++i) {
                    ls.Add((i, nums[i]));
                }
                Balance();
            }

            public void Insert(int indx) {
                ls.Add((indx, nums[indx]));
                Balance();
            }

            public void Remove(int indx) {
                ls.Remove((indx, nums[indx]));
                rs.Remove((indx, nums[indx]));
                Balance();
            }

            public double Median() {
                var count = ls.Count + rs.Count;
                if (count % 2 == 0) {
                    return (1.0 * ls.Max.Item2 + rs.Min.Item2) / 2.0;
                }
                return ls.Max.Item2;
            }

            private void Balance() {
                while (ls.Count > rs.Count) {
                    var max = ls.Max;
                    rs.Add(max);
                    ls.Remove(max);
                }
                while (rs.Count > ls.Count) {
                    var min = rs.Min;
                    ls.Add(min);
                    rs.Remove(min);
                }
            }
        }
    }
}
