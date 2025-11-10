public class Solution {
    public long FindKthSmallest(int[] coins, int k) {
        long lo = 1, hi = (long)1e+12;
        while (lo < hi) {
            var x = (lo + hi) / 2;
            if (count(coins, x) < k) {
                lo = x + 1;
            } else {
                hi = x;
            }
        }
        return lo;
    }

    private long count(int[] coins, long x) {
        var res = 0L;
        for (var set = 1; set < 1 << coins.Length; ++set) {
            var have = new List<int>();
            for (var i = 0; i < coins.Length; ++i) {
                if ((set & (1 << i)) != 0) {
                    have.Add(coins[i]);
                }
            }
            var take = x / lcm(have);
            if (have.Count % 2 == 1) {
                res += take;
            } else {
                res -= take;
            }
        }
        return res;
    }

    private long lcm(IList<int> a) {
        var res = 1L;
        for (var i = 0; i < a.Count; ++i) {
            res = res * a[i] / gcd(res, a[i]);
        }
        return res;
    }

    private long gcd(long a, long b) {
        while (a != 0 && b != 0) {
            if (a > b) {
                a %= b;
            } else {
                b %= a;
            }
        }
        return a + b;
    }
}