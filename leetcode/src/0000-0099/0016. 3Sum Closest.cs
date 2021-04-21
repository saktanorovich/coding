public class Solution {
    public int ThreeSumClosest(int[] nums, int target) {
        Array.Sort(nums);
        return ThreeSumClosest(nums, nums.Length, target);
    }

    private int ThreeSumClosest(int[] nums, int n, int target) {
        var closest = int.MaxValue / 2;
        for (var i = 0; i + 2 < n; ++i) {
            var l = i + 1;
            var r = n - 1;
            while (l < r) {
                var current = nums[i] + nums[l] + nums[r];
                if (Math.Abs(closest - target) > Math.Abs(current - target)) {
                    closest = current;
                }
                if (current == target) {
                    return current;
                } else if (current < target) {
                    ++l;
                } else if (current > target) {
                    --r;
                }
            }
        }
        return closest;
    }
}