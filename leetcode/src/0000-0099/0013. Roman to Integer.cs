public class Solution {
    public int RomanToInt(string s) {
        var res = 0;
        for (var i = 0; i < s.Length; ++i) {
            if (i + 1 < s.Length) {
                if (Int(s[i]) < Int(s[i +1]))
                    res -= Int(s[i]);
                else
                    res += Int(s[i]);
            } else {
                res += Int(s[i]);
            }
        }
        return res;
    }

    private static int Int(char c) {
        switch (c) {
            case 'I': return 1;
            case 'V': return 5;
            case 'X': return 10;
            case 'L': return 50;
            case 'C': return 100;
            case 'D': return 500;
            case 'M': return 1000;
        }
        return 0;
    }
}