public class Solution {
    public int NumberOfGoodPartitions(int[] nums) {
        var last = new Dictionary<int, int>();
        for (var i = nums.Length - 1; i >= 0; --i) {
            if (last.ContainsKey(nums[i]) == false) {
                last.Add(nums[i], i);
            }
        }
        var next = 0;
        var answ = 1;
        for (var i = 0; i <= nums.Length - 1; ++i) {
            if (i > next) {
                answ *= 2;
                if (answ >= mod) {
                    answ %= mod;
                }
            }
            if (next < last[nums[i]]) {
                next = last[nums[i]];
            }
        }
        return answ;
    }

    private const int mod = (int)1e9 + 7;
}
