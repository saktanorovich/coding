public class Solution {
    public bool EqualFrequency(string word) {
        var freq = new int[26];
        foreach (var c in word) {
            freq[c - 'a'] ++;
        }
        for (var i = 0; i < word.Length; ++i) {
            var c = word[i] - 'a';
            freq[c] --;
            if (equals(freq)) {
                return true;
            }
            freq[c] ++;
        }
        return false;
    }

    private static bool equals(int[] a) {
        var t = a.Max();
        for (var i = 0; i < a.Length; ++i) {
            if (a[i] > 0) {
                if (a[i] != t) {
                    return false;
                }
            }
        }
        return true;
    }
}