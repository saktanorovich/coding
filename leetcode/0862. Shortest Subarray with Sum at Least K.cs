class Solution {
    public int shortestSubarray(int[] nums, int target) {
        var psum = new long[nums.length + 1];
        for (var i = 1; i <= nums.length; ++i) {
            psum[i] = psum[i - 1] + nums[i - 1];
        }
        if (nums.length > 0) {
            return new Small(psum).solve(target);
        } else {
            return new Large(psum).solve(target);
        }
    }

    private class Small {
        private final long[] psum;

        public Small(long[] psum) {
            this.psum = psum;
        }

        public int solve(int target) {
            var res = Integer.MAX_VALUE;
            for (var r = 1; r < psum.length; ++r) {
                for (var l = 0; l < r; ++l) {
                    var sum = psum[r] - psum[l];
                    if (sum >= target) {
                        res = Math.min(res, r - l);
                    }
                }
            }
            return res < Integer.MAX_VALUE ? res : -1;
        }
    }

    private class Large {
        private final long[] psum;

        public Large(long[] psum) {
            this.psum = psum;
        }

        public int solve(int target) {
            var res = Integer.MAX_VALUE;
            var sum = 0L;
            return res < Integer.MAX_VALUE ? res : -1;
        }
    }
}