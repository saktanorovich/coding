class Solution {
    public int minDays(int[] bloomDay, int m, int k) {
        var lo = 0;
        var hi = (int)1e+9 + 7;
        while (lo < hi) {
            var day = (lo + hi) / 2;
            var cnt = bouquets(bloomDay, day, k);
            if (cnt < m) {
                lo = day + 1;
            } else {
                hi = day;
            }
        }
        return lo < (int)1e+9 + 7 ? lo : -1;
    }

    private int bouquets(int[] bloomDay, int day, int k) {
        var res = 0;
        var cnt = 0;
        for (var i = 0; i < bloomDay.length; ++i) {
            if (bloomDay[i] <= day) {
                cnt ++;
            } else {
                cnt = 0;
            }
            if (cnt == k) {
                res ++;
                cnt = 0;
            }
        }
        return res;
    }
}
