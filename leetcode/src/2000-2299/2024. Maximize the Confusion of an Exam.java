class Solution {
    public int maxConsecutiveAnswers(String answer, int k) {
        return Math.max(solve(answer, 'F', k), solve(answer, 'T', k));
    }

    private int solve(String s, char c, int k) {
        var answ = 0;
        var lptr = 0;
        var rptr = 0;
        var have = 0;
        for (; rptr < s.length(); ++rptr) {
            if (s.charAt(rptr) == c) {
                have ++;
            }
            while (have > k) {
                if (s.charAt(lptr) == c) {
                    have --;
                }
                lptr ++;
            }
            answ = Math.max(answ, rptr - lptr + 1);
        }
        return answ;
    }
}
