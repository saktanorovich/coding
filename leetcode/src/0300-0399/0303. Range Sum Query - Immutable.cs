public class NumArray {
    private readonly int[] sum;

    public NumArray(int[] nums) {
        sum = new int[nums.Length];
        sum[0] = nums[0];
        for (var i = 1; i < nums.Length; ++i) {
            sum[i] = sum[i - 1] + nums[i];
        }
    }

    public int SumRange(int left, int right) {
        var res = sum[right];
        if (left > 0) {
            res -= sum[left - 1];
        }
        return res;
    }
}
