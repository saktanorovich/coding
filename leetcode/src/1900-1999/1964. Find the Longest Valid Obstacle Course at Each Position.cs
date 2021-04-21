using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1964 {
        public int[] LongestObstacleCourseAtEachPosition(int[] obstacles) {
            return doit(obstacles, obstacles.Length);
        }

        private static int[] doit(int[] a, int n) {
            var ans = new int[n];
            var lis = new int[n];
            var len = 0;
            for (var idx = 0; idx < n; ++idx) {
                var pos = find(lis, len, a[idx]);
                if (pos == len) {
                    len++;
                }
                lis[pos] = a[idx];
                ans[idx] = pos +1;
            }
            return ans;
        }

        /** O(log(n)) **/
        private static int find(int[] lis, int len, int val) {
            if (len == 0) {
                return 0;
            }
            int lo = 0, hi = len - 1;
            while (lo < hi) {
                var pos = (lo + hi + 1) / 2;
                if (lis[pos] > val) {
                    hi = pos - 1;
                } else {
                    lo = pos;
                }
            }
            return val >= lis[lo] ? lo + 1 : lo;
        }
        /** O(n) **
        private static int find(int[] lis, int len, int val) {
            for (var i = len - 1; i >= 0; --i) {
                if (val >= lis[i]) {
                    return i + 1;
                }
            }
            return 0;
        }
        /**/
    }
}
