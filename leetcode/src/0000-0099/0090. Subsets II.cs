public class Solution {
    public IList<IList<int>> SubsetsWithDup(int[] nums) {
        Array.Sort(nums);
        var res = new List<IList<int>>();
        for (var set = 0; set < 1 << nums.Length; ++set) {
            var cur = new List<int>();
            for (var i = 0; i < nums.Length; ++i) {
                if ((set & (1 << i)) != 0) {
                    cur.Add(nums[i]);
                }
            }
            if (isInSet(res, cur) == false) {
                res.Add(cur);
            }
        }
        return res;
    }

    private bool isInSet(List<IList<int>> source, List<int> target) {
        foreach (var current in source) {
            if (match(current, target)) {
                return true;
            }
        }
        return false;
    }

    private bool match(IList<int> a, IList<int> b) {
        if (a.Count == b.Count) {
            for (var i = 0; i < a.Count; ++i) {
                if (a[i] != b[i]) {
                    return false;
                }
            }
            return true;
        }
        return false;
    }
}
