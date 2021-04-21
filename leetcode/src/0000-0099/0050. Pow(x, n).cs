public class Solution {
    public double MyPow(double x, int n) {
        if (n == 0) {
            return 1.0;
        }
        return doit(x, (long)n);
    }

    private static double doit(double x, long n) {
        var p = pow(x, n > 0 ? +n : -n);
        return n > 0
            ? 1 * p
            : 1 / p;
    }

    private static double pow(double x, long n) {
        if (n == 0) {
            return 1;
        } else if ((n & 1) == 0) {
            return pow(x * x, n / 2);
        } else {
            return x * pow(x, n - 1);
        }
    }
}