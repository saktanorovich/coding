using System;

namespace TopCoder.Algorithm {
    public class OddsAndEvens {
        public string find(string[] sums, string[] muls) {
            for (var n = 1; ; ++n)
                if (n * (n - 1) / 2 == sums.Length) {
                    return find(compress(sums), compress(muls), n);
                }
        }

        private static string find(int[] sums, int[] muls, int n) {
            for (var even = 0; even <= n; ++even) {
                if (equals(sums, get(even, n - even, (x, y) => (x + y) % 2)) &&
                    equals(muls, get(even, n - even, (x, y) => (x * y) % 2))) {
                        return string.Format("EVEN {0} ODD {1}", even, n - even);
                }
            }
            return "IMPOSSIBLE";
        }

        private static bool equals(int[] a, int[] b) {
            if (a.Length == b.Length) {
                for (var i = 0; i < a.Length; ++i)
                    if (a[i] != b[i])
                        return false;
                return true;
            }
            return false;
        }

        private static int[] get(int even, int odd, Func<int, int, int> f) {
            var elements = new int[even + odd];
            for (var i = 0; i < odd; ++i) {
                elements[i] = 1;
            }
            var result = new int[2];
            for (var i = 0; i < elements.Length; ++i)
                for (var j = i + 1; j < elements.Length; ++j)
                    ++result[f(elements[i], elements[j])];
            return result;
        }

        private static int[] compress(string[] results) {
            var result = new int[2];
            foreach (var res in results)
                if (res == "EVEN")
                    ++result[0];
                else
                    ++result[1];
            return result;
        }
    }
}