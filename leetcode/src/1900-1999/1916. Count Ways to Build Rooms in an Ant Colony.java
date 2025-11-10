class Solution {
    public int waysToBuildRooms(int[] prev) {
        List<Integer>[] tree = new ArrayList[prev.length];
        for (var i = 0; i < prev.length; ++i) {
            tree[i] = new ArrayList<>();
        }
        for (var i = 1; i < prev.length; ++i) {
            tree[prev[i]].add(i);
        }
        if (prev.length < 1) {
            return new Small(tree).solve();
        } else {
            return new Large(tree).solve();
        }
    }

    private class Small {
        private final List<Integer>[] tree;
        private final int comb[][];
        private final int size[];

        public Small(List<Integer>[] tree) {
            var n = tree.length;
            this.tree = tree;
            this.size = new int[n + 1];
            this.comb = new int[n + 1][n + 1];
            for (var i = 0; i <= n; ++i) {
                comb[i][0] = 1;
                for (var j = 1; j <= i; ++j) {
                    comb[i][j] += comb[i - 1][j] + comb[i - 1][j - 1];
                    if (comb[i][j] >= MOD) {
                        comb[i][j] -= MOD;
                    }
                }
            }
        }

        public int solve() {
            var res = dfs(0);
            return (int)res;
        }

        private long dfs(int curr) {
            size[curr] = 1;
            var answ = 1L;
            var suma = 0;
            for (var next : tree[curr]) {
                var have = dfs(next);
                size[curr] += size[next];
                suma += size[next];
                answ *= have;
                answ %= MOD;
                answ *= comb[suma][size[next]];
                answ %= MOD;
            }
            return answ;
        }
    }

    private class Large {
        private final List<Integer>[] tree;
        private final long fac[];
        private final long inv[];
        private final int size[];

        public Large(List<Integer>[] tree) {
            var n = tree.length;
            this.tree = tree;
            this.size = new int[n + 1];
            this.fac = new long[n + 1];
            this.inv = new long[n + 1];
            this.fac[0] = 1;
            for (var i = 1; i <= n; ++i) {
                fac[i] = fac[i - 1] * i % MOD;
            }
            for (var i = 0; i <= n; ++i) {
                inv[i] = pow(fac[i], MOD - 2);
            }
        }

        public int solve() {
            var res = dfs(0);
            return (int)res;
        }

        private long dfs(int curr) {
            size[curr] = 1;
            var answ = 1L;
            var suma = 0;
            for (var next : tree[curr]) {
                var have = dfs(next);
                size[curr] += size[next];
                suma += size[next];
                answ *= have;
                answ %= MOD;
                answ *= comb(suma, size[next]);
                answ %= MOD;
            }
            return answ;
        }

        private long comb(int n, int k) {
            var res = 1L;
            res *= fac[n];
            res *= inv[k];
            res %= MOD;
            res *= inv[n - k];
            res %= MOD;
            return res;
        }

        private long pow(long x, int k) {
            if (k == 0) {
                return 1;
            } else if (k % 2 == 0) {
                return pow(x * x % MOD, k / 2);
            } else {
                return x * pow(x, k - 1) % MOD;
            }
        }
    }

    private final int MOD = (int)1e9 + 7;
}