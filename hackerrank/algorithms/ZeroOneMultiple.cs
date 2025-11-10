using System;
using System.Text;

namespace interview.hackerrank {
    public class ZeroOneMultiple {
        /* It can be shown that such number always exists considering the following: 1 mod n = m1, 11 mod n = m2, .., 11..11 mod n = k > 0. By Dirichlet
         * principle the two numbers have the same remainder (assume numbers i and j) so the number j - i is multiple of n. But the original problem
         * is equivalent to find such ai in {0,1} that sum{ai} -> min, sum{(ai * 10^i} mod n) = 0. */
        public string smallest(int n) {
            if (n > 1) {
                var mod = new long[n + 1];
                var rem = new long[n];
                for (var i = 0; i < n; ++i) {
                    rem[i] = -1;
                }
                mod[0] = 1;
                rem[1] = 0;
                for (var pow = 1; pow <= n && rem[0] < 1; ++pow) {
                    mod[pow] = (10 * mod[pow - 1]) % n;
                    for (var curr = 0; curr < n; ++curr) {
                        if (0 <= rem[curr] && rem[curr] < pow) {
                            var next = (curr + mod[pow]) % n;
                            if (rem[next] == -1) {
                                rem[next] = pow;
                            }
                        }
                    }
                    if (rem[mod[pow]] == -1) {
                        rem[mod[pow]] = pow;
                    }
                }
                var digits = new char[n + 1];
                for (var i = 0; i <= n; ++i) {
                    digits[i] = '0';
                }
                for (long curr = 0; ;) {
                    digits[rem[curr]] = '1';
                    curr = (curr - mod[rem[curr]] + n) % n;
                    if (curr == 0) {
                        break;
                    }
                }
                for (var i = n; i >= 0; --i) {
                    if (digits[i] != '0') {
                        var result = new StringBuilder();
                        for (; i >= 0; --i) {
                            result.Append(digits[i]);
                        }
                        return result.ToString();
                    }
                }
                return string.Empty;
            }
            return n.ToString();
        }
    }
}
