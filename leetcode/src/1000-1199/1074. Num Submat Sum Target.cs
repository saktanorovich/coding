using System;
using System.Collections.Generic;
using System.Text;

namespace coding.leetcode {
    public class Solution_1074 {
        public int NumSubmatrixSumTarget(int[][] matrix, int target) {
            return NumSubmatrixSumTarget(matrix, matrix.Length, matrix[0].Length, target);
        }

        private int NumSubmatrixSumTarget(int[][] matrix, int n, int m, int target) {
            var ans = 0;
            for (var j = 0; j < m; ++j) {
                var nums = new int[n];
                for (var k = j; k < m; ++k) {
                    for (var i = 0; i < n; ++i) {
                        nums[i] += matrix[i][k];
                    }
                    ans += SubarraySum(nums, target);
                }
            }
            return ans;
        }

        private int SubarraySum(int[] nums, int target) {
            var cumul = new Dictionary<int, int>();
            var summa = 0;
            var total = 0;
            cumul.Add(0, 1);
            foreach (var x in nums) {
                summa += x;
                if (cumul.TryGetValue(summa - target, out var count)) {
                    total += count;
                }
                if (cumul.ContainsKey(summa) == false) {
                    cumul[summa] = 0;
                }
                cumul[summa]++;
            }
            return total;
        }
    }
}
