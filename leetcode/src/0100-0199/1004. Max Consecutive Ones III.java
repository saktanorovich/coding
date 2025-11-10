class Solution {
    public int longestOnes(int[] nums, int k) {
        var answ = 0;
        var lptr = 0;
        var rptr = 0;
        var have = 0;
        for (; rptr < nums.length; ++rptr) {
            if (nums[rptr] == 0) {
                have ++;
            }
            while (have > k) {
                if (nums[lptr] == 0) {
                    have --;
                }
                lptr ++;
            }
            answ = Math.max(answ, rptr - lptr + 1);
        }
        return answ;
    }
}
