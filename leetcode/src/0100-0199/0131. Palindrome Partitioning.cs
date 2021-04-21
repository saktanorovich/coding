public class Solution {
    public IList<IList<string>> Partition(string s) {
        var res = new List<IList<string>>();
        for (var set = 0; set < 1 << s.Length - 1; ++set) {
            var part = make(s, set);
            if (okay(part)) {
                res.Add(part);
            }
        }
        return res;
    }

    private IList<string> make(string s, int set) {
        var res = new StringBuilder();
        for (var i = 0; i < s.Length; ++i) {
            res.Append(s[i]);
            if ((set & (1 << i)) != 0) {
                res.Append('|');
            }
        }
        return res.ToString().Split('|').ToArray();
    }

    private bool okay(IList<string> part) {
        foreach (var s in part) {
            for (int i = 0, j = s.Length - 1; i < j; ++i, --j) {
                if (s[i] != s[j]) {
                    return false;
                }
            }
        }
        return true;
    }
}
