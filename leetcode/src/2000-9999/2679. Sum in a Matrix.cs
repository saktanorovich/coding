public class Solution {
    public int MatrixSum(int[][] nums) {
        return MatrixSum(nums, nums.Length, nums[0].Length);
    }

    private int MatrixSum(int[][] nums, int n, int m) {
        if (n < 1) {
            return new Small().MatrixSum(nums, n, m);
        } else {
            return new Large().MatrixSum(nums, n, m);
        }
    }

    // O(m) memory -- out-of-place
    private sealed class Small {
        public int MatrixSum(int[][] nums, int n, int m) {
            var max = new int[m];
            var que = new PriorityQueue<int, int>();
            for (var i = 0; i < n; ++i) {
                for (var j = 0; j < m; ++j) {
                    que.Enqueue(j, nums[i][j]);
                }
                for (var j = 0; j < m; ++j) {
                    var k = que.Dequeue();
                    if (max[j] < nums[i][k]) {
                        max[j] = nums[i][k];
                    }
                }
            }
            return max.Sum();
        }
    }

    // O(1) memory -- in-place
    private sealed class Large {
        public int MatrixSum(int[][] nums, int n, int m) {
            for (var i = 0; i < n; ++i) {
                Array.Sort(nums[i]);
            }
            var res = 0;
            for (var j = 0; j < m; ++j) {
                var max = 0;
                for (var i = 0; i < n; ++i) {
                    if (max < nums[i][j]) {
                        max = nums[i][j];
                    }
                }
                res += max;
            }
            return res;
        }
    }
}
