class Solution {
    public String reorganizeString(String s) {
        return new Solver().solve(s);
    }

    private class Solver {
        private final int[] map;

        public Solver() {
            this.map = new int[26];
        }

        public String solve(String s) {
            for (var c : s.toCharArray()) {
                this.map[c - 'a'] ++;
            }
            var max = 0;
            for (var ptr = 0; ptr < 26; ++ptr) {
                if (map[max] < map[ptr]) {
                    max = ptr;
                }
            }
            if (2 * map[max] > s.length() + 1) {
                return "";
            }
            return build(s, max);
        }

        private String build(String s, int ptr) {
            var ind = 0;
            var res = new char[s.length()];
            while (map[ptr] > 0) {
                res[ind] = (char)(ptr + 'a');
                ind += 2;
                map[ptr] --;
            }
            for (ptr = 0; ptr < 26; ++ptr) {
                while (map[ptr] > 0) {
                    if (ind >= s.length()) {
                        ind = 1;
                    }
                    res[ind] = (char)(ptr + 'a');
                    ind += 2;
                    map[ptr] --;
                }
            }
            return String.valueOf(res);
        }
    }
}