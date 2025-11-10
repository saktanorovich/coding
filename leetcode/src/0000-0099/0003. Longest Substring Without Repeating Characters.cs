public class Solution {
    public int LengthOfLongestSubstring(string s) {
        var map = new int[256];
        var res = 0;
        for (int l = 0, r = 0; r < s.Length;) {
            if (map[s[r]] == 0) {
                map[s[r]] = 1;
                r++;
                if (res < r - l) {
                    res = r - l;
                }
            } else {
                map[s[l]]--;
                l++;
            }
        }
        return res;
    }
}