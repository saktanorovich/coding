using System;

namespace coding.nielsen {
    public static class Program {
        public static void Main_Another() {
            for (var n = 1; n <= 20; ++n) {
                for (var k = 0; k <= n; ++k) {
                    var small = new Small().Solve(n, k);
                    var large = new Large().Solve(n, k);
                    if (large != small) {
                        Console.WriteLine($"FAILED: n={n}, k={k}: actual = {large}, expected={small}");
                        return;
                    }
                }
            }
            Console.WriteLine("PASSED");
        }

        private class Small {
            public int Solve(int n, int k) {
                var ones = (1 << k) - 1;
                var answ = 0;
                for (var mask = 0; mask < 1 << n; ++mask) {
                    for (var i = 0; i <= n; ++i) {
                        if (((mask >> i) & ones) == ones) {
                            answ ++;
                            break;
                        }
                    }
                }
                return answ;
            }
        }

        private class Large {
            public int Solve(int n, int k) {
                var answ = new int[n + 1];
                for (var i = 0; i <= k; ++i) {
                    answ[i] = 1 << i;
                }
                answ[k] = answ[k] - 1;
                for (var i = k; i < n; ++i) {
                    answ[i + 1] = 2 * answ[i] - answ[i - k];
                }
                return (1 << n) - answ[n];
            }
        }
    }
}
