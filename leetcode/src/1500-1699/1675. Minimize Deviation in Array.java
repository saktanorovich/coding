class Solution {
    public int minimumDeviation(int[] nums) {
        var set = new TreeSet<Integer>();
        for (var x : nums) {
            if (x % 2 == 1) {
                set.add(x * 2);
            } else {
                set.add(x);
            }
        }
        var res = Integer.MAX_VALUE;
        while (true) {
            var min = set.first();
            var max = set.last();
            var has = max - min;
            if (res > has) {
                res = has;
            }
            if (max % 2 == 0) {
                set.remove(max);
                set.add(max / 2);
            } else break;
        }
        return res;
    }
}