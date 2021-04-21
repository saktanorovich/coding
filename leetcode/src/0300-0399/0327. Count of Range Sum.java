public class Solution {
    public int countRangeSum(int[] nums, int lower, int upper) {
        if (nums.length < 0) {
            return new Small(nums).solve(lower, upper);
        } else {
            return new Large(nums).solve(lower, upper);
        }
    }

    private class Small {
        private long[] sums;

        public Small(int[] nums) {
            sums = new long[nums.length + 1];
            for (var i = 1; i <= nums.length; ++i) {
                sums[i] = sums[i - 1] + nums[i - 1];
            }
        }

        public int solve(int lower, int upper) {
            var answ = 0;
            for (var i = 1; i < sums.length; ++i) {
                for (var j = i; j < sums.length; ++j) {
                    var curr = sums[j] - sums[i - 1];
                    if (lower <= curr && curr <= upper) {
                        answ = answ + 1;
                    }
                }
            }
            return answ;
        }
    }

    private class Large {
        private long[] sums;

        public Large(int[] nums) {
            sums = new long[nums.length];
            sums[0] = nums[0];
            for (var i = 1; i < nums.length; ++i) {
                sums[i] = sums[i - 1] + nums[i];
            }
        }

        public int solve(int lower, int upper) {
            return count(0, sums.length, lower, upper);
        }

        private int count(int l, int r, int lower, int upper) {
            if (l + 1 == r) {
                if (lower <= sums[l] && sums[l] <= upper) {
                    return 1;
                } else {
                    return 0;
                }
            }
            var x = (l + r) / 2;
            var res = 0;
            res += count(l, x, lower, upper);
            res += count(x, r, lower, upper);
            var a = x;
            var b = x;
            for (var k = l; k < x; ++k) {
                while (a < r && sums[a] - sums[k] <  lower) ++a;
                while (b < r && sums[b] - sums[k] <= upper) ++b;
                res += b - a;
            }
            merge(l, r, x);
            return res;
        }

        private void merge(int l, int r, int x) {
            var temp = new long[r - l];
            var a = l;
            var b = x;
            var i = 0;
            while (a < x && b < r) {
                if (sums[a] < sums[b]) {
                    temp[i] = sums[a]; ++i; ++a;
                } else {
                    temp[i] = sums[b]; ++i; ++b;
                }
            }
            while (a < x) { temp[i] = sums[a]; ++i; ++a; }
            while (b < r) { temp[i] = sums[b]; ++i; ++b; }
            for (var k = l; k < r; ++k) {
                sums[k] = temp[k - l];
            }
        }
    }
}