public class Solution {
    public string LongestCommonPrefix(string[] strs) {
        var res = new StringBuilder("");
        for (var i = 0; i < strs[0].Length; ++i) {
            var c = strs[0][i];
            if (match(strs, i, c)) {
                res.Append(c);
            } else break;
        }
        return res.ToString();
    }

    private bool match(string[] strs, int p, char c) {
        foreach (var s in strs) {
            if (s.Length == p || s[p] != c) {
                return false;
            }
        }
        return true;
    }
}
