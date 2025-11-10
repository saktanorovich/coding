class Solution {
    public boolean isNumber(String s) {
        var state = 0;
        for (var c : s.toCharArray()) {
            var signal = getSignal(c);
            state = next[state][signal];
        }
        final var target = state;
        return IntStream.of(
            new int[] { 1, 4, 6 }
        ).anyMatch(x -> x == target);
    }

    private int getSignal(char c) {
        if ('0' <= c && c <= '9')
            return SygDigit;
        if (c == '+' || c == '-')
            return SygSign;
        if (c == 'e' || c == 'E')
            return SygExp;
        if (c == '.')
            return SygDot;
        return SygNone;
    }

    private final int SygNone  = 0;
    private final int SygDigit = 1;
    private final int SygSign  = 2;
    private final int SygDot   = 3;
    private final int SygExp   = 4;

    private final int[][] next = {
        new int[] { 8, 1, 2, 3, 8 },
        new int[] { 8, 1, 8, 4, 5 },
        new int[] { 8, 1, 8, 3, 8 },
        new int[] { 8, 4, 8, 8, 8 },
        new int[] { 8, 4, 8, 8, 5 },
        new int[] { 8, 6, 7, 8, 8 },
        new int[] { 8, 6, 8, 8, 8 },
        new int[] { 8, 6, 8, 8, 8 },
        new int[] { 8, 8, 8, 8, 8 }
    };
}