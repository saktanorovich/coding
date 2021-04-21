class Solution {
    public String longestPalindrome(String s) {
        return longestPalindrome(s, s.length());
    }

    private String longestPalindrome(String s, int n) {
        var f = new boolean[n][n];
        for (var i = 0; i < n; ++i) {
            f[i][i] = true;
            for (var j = 0; j < i; ++j) {
                f[i][j] = true;
            }
        }
        for (var k = 1; k < n; ++k) {
            for (var i = 0; i + k < n; ++i) {
                var j = i + k;
                if (s.charAt(i) == s.charAt(j)) {
                    f[i][j] = f[i + 1][j - 1];
                }
            }
        }
        var max = 0;
        var res = "";
        for (var i = 0; i < n; ++i) {
            for (var j = i; j < n; ++j) {
                if (f[i][j]) {
                    if (max < j - i + 1) {
                        max = j - i + 1;
                        res = s.substring(i, j + 1);
                    }
                }
            }
        }
        return res;
    }
}