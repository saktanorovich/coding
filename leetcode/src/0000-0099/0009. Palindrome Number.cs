public class Solution {
    public bool IsPalindrome(int x) {
        if (x < 0) {
            return false;
        }
        return x == Rev(x);
    }

    private long Rev(long x) {
        var y = 0L;
        while (x > 0) {
            y = y * 10 + x % 10;
            x = x / 10;
        }
        return y;
    }
}
