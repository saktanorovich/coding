public class Solution {
    public int MinimumSubarrayLength(int[] nums, int k) {
        var answ = -1;
        var bits = new int[32];
        for (int l = 0, r = 0; r < nums.Length;) {
            if (r < nums.Length) {
                set(bits, nums[r], +1);
                r++;
            }
            for (; l < r; ++l) {
                if (val(bits) >= k) {
                    if (answ == -1 || answ > r - l) {
                        answ = r - l;
                    }
                    set(bits, nums[l], -1);
                } else {
                    break;
                }
            }
        }
        return answ;
    }

    private static void set(int[] bits, int val, int bit) {
        for (var i = 0; i < 32; ++i) {
            if ((val & (1 << i)) != 0) {
                bits[i] += bit;
            }
        }
    }

    private static int val(int[] bits) {
        var answ = 0;
        for (var i = 0; i < 32; ++i) {
            if (bits[i] > 0) {
                answ |= 1 << i;
            }
        }
        return answ;
    }
}