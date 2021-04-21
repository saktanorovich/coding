using System;

namespace interview.hackerrank {
    public class IsItPossible {
        private readonly string Y = "Yes";
        private readonly string N = "No";

        private readonly string[,] cache = new string[1000 + 1, 1000 + 1];

        public string isitpossible(int a, int b, int c, int d) {
            if (cache[a, b] == null) {
                if (a == c && b == d) {
                    cache[a, b] = Y;
                }
                else {
                    cache[a, b] = N;
                    if (a + b <= 1000) {
                        var res1 = isitpossible(a + b, b, c, d);
                        var res2 = isitpossible(a, a + b, c, d);
                        if (res1 == Y || res2 == Y) {
                            cache[a, b] = Y;
                        }
                    }
                }
            }
            return cache[a, b];
        }
    }
}
