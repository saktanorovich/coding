using System;
using System.Collections.Generic;
using System.Text;

namespace coding.leetcode {
    public class Solution_0085 {
        public int MaximalRectangle(char[][] matrix) {
            if (matrix == null || matrix.Length == 0) {
                return 0;
            }
            return MaximalRectangle(matrix, matrix.Length, matrix[0].Length);
        }

        private int MaximalRectangle(char[][] matrix, int n, int m) {
            var result = 0;
            var height = new int[m + 2];
            for (var i = 0; i < n; ++i) {
                for (var j = 1; j <= m; ++j) {
                    if (matrix[i][j - 1] == '0') {
                        height[j] = 0;
                    } else {
                        height[j]++;
                    }
                }
                result = Math.Max(result, MaximalRectangle(height));
            }
            return result;
        }

        private int MaximalRectangle(int[] height) {
            var h = new List<int>();
            h.Add(0);
            h.AddRange(height);
            h.Add(0);
            var res = 0;
            var sta = new Stack<int>();
            for (var ri = 0; ri < h.Count; ++ri) {
                while (sta.Count > 0) {
                    var le = sta.Pop();
                    if (h[le] > h[ri]) {
                        res = Math.Max(res, h[le] * (ri - (sta.Peek() + 1)));
                    } else {
                        sta.Push(le);
                        break;
                    }
                }
                sta.Push(ri);
            }
            return res;
        }
    }
}
