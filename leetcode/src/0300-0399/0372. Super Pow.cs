public class Solution {
    public int SuperPow(int a, int[] b) {
        var res = 1;
        for (var i = b.Length - 1; i >= 0; --i) {
            res *= pow(a, b[i]);
            res %= 1337;
            a = pow(a, 10);
        }
        return res;
    }

    private static int pow(int x, int k) {
        var p = 1;
        var a = x % 1337;
        for (var i = 1; i <= k; ++i) {
            p *= a;
            p %= 1337;
        }
        return p;
    }
}
