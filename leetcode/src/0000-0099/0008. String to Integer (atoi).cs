public class Solution {
    public int MyAtoi(string s) {
        for (var i = 0; i < s. Length; ++i) {
            if (s[i] != ' ') {
                return atoi(s, i);
            }
        }
        return 0;
    }

    private static int atoi(string s, int p) {
        var sign = 1;
        switch (s[p]) {
            case '-': sign = -1; p = p + 1; break;
            case '+': sign = +1; p = p + 1; break;
        }
        var answ = 0;
        for (; p < s.Length; ++p) {
            if ('0' <= s[p] && s[p] <= '9') {
                var d = s[p] - '0';
                if (overflow(answ, d)) {
                    return sign > 0 ? int.MaxValue : int.MinValue;
                }
                answ *= 10;
                answ += d;
            } else break;
        }
        return sign * answ;
    }

    private static bool overflow(int value, int d) {
        if (value > int.MaxValue / 10) {
            return true;
        }
        if (value < int.MaxValue / 10) {
            return false;
        }
        return d > 7;
    }
}