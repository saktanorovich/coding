public class NumArray {
    private readonly int[] tree;
    private readonly int n;

    public NumArray(int[] nums) {
        this.n = nums.Length;
        this.tree = new int[2 * n];
        for (var i = 0; i < n; ++i) {
            tree[i + n] = nums[i];
        }
        for (var i = n - 1; i > 0; --i) {
            tree[i] = tree[i << 1] + tree[i << 1 | 1];
        }
    }
    
    public void Update(int at, int val) {
        tree[at += n] = val;
        for (; at > 1; at >>= 1) {
            tree[at >> 1] = tree[at] + tree[at ^ 1];
        }
    }
    
    public int SumRange(int le, int ri) {
        var sum = 0;
        for (le += n, ri += n + 1; le < ri; le >>= 1, ri >>= 1) {
            if ((le & 1) == 1) {
                sum += tree[le ++];
            }
            if ((ri & 1) == 1) {
                sum += tree[-- ri];
            }
        }
        return sum;
    }
}
