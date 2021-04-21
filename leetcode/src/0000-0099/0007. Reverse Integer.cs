public class Solution {
    public int Reverse(int x) {
        var l = int.MinValue / 10;
        var r = int.MaxValue / 10;
        var z = 0;
        while (x != 0) {
            if (z < l || z > r) {
                return 0;
            }
            var m = x % 10;
            z *= 10;
            z += m;
            x /= 10;
        }
        return z;
    }
}