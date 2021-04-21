class Solution {
    public int minSubArrayLen(int target, int[] nums) {
        var sum = 0L;
        var res = Integer.MAX_VALUE;
        var i = 0;
        var j = 0;
        while (j < nums.length) {
            sum += nums[j];
            j = j + 1;
            while (sum >= target) {
                res = Math.min(res, j - i);
                sum -= nums[i];
                i = i + 1;
            }
        }
        return res < Integer.MAX_VALUE ? res : 0;
    }
}