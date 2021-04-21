public class Solution {
    public int LongestValidParentheses(string s) {
        var sta = new Stack<int>();
        var tik = new byte[s.Length];
        for (var i = 0; i < s.Length; ++i) {
            if (s[i] == '(') {
                sta.Push(i);
            } else {
                if (sta.Count > 0) {
                    var j = sta.Pop();
                    tik[j] = 1;
                    tik[i] = 1;
                }
            }
        }
        var res = 0;
        for (var i = 0; i < s.Length; ++i) {
            for (var j = i; j < s.Length; ++j) {
                if (tik[j] == 0) {
                    break;
                }
                res = Math.Max(res, j - i + 1);
            }
        }
        return res;
    }
}
