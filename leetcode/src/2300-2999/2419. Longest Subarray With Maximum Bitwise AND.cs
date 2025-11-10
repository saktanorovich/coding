public class Solution {
    public int LongestSubarray(int[] nums) {
        var res = 0;
        var max = 0;
        for (var i = 0; i < nums.Length;) {
            var j = i + 1;
            while (j <= nums.Length) {
                if (j < nums.Length && nums[i] == nums[j]) {
                    j = j + 1;
                }
                else break;
            }
            if (max < nums[i]) {
                max = nums[i];
                res = j - i;
            } else if (max == nums[i]) {
                if (res < j - i) {
                    res = j - i;
                }
            }
            i = j;
        }
        return res;
    }
}