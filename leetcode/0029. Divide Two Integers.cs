public class Solution {
    public int Divide(int dividend, int divisor) {
        return divide(dividend, divisor);
    }

    private static int divide(int a, int b) {
        if (b == +1) {
            return a;
        }
        if (b == -1 && a == int.MinValue) {
            return int.MaxValue;
        }
        return 0;
    }
}
