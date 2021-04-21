using System;

namespace TopCoder.Algorithm {
    public class PolynomialMultiplier {
        public string product(string a, string b) {
            return stringify(product(parse(a), parse(b)));
        }

        private static int[] product(int[] a, int[] b) {
            var result = new int[19];
            for (var i = 0; i < 10; ++i)
                for (var j = 0; j < 10; ++j)
                    if (i + j < 20) {
                        result[i + j] += a[i] * b[j];
                    }
            return result;
        }

        private static string stringify(int[] p) {
            var result = string.Empty;
            for (var i = 18; i >= 0; --i) {
                if (p[i] > 0) {
                    result += stringify(i, p[i]);
                }
            }
            return result
                .Replace("^1]", "]")
                .Replace("[1*", "[")
                    .Replace("[", string.Empty)
                    .Replace("]", string.Empty)
            .Trim('+', ' ');
        }

        private static string stringify(int power, int multiplier) {
            if (power == 0) {
                return string.Format(" + [{0}]", multiplier);
            }
            return string.Format(" + [{0}*x^{1}]", multiplier, power);
        }

        private static int[] parse(string s) {
            var result = new int[10];
            foreach (var term in s.Split(new[] { ' ', '+' }, StringSplitOptions.RemoveEmptyEntries)) {
                if (term.IndexOf('x') >= 0) {
                    result[parse(term, '^', 1)] += parse(term, '*', 0);
                    continue;
                }
                result[0] += int.Parse(term);
            }
            return result;
        }

        private static int parse(string term, char c, int last) {
            var pos = term.IndexOf(c);
            if (pos >= 0) {
                if (last > 0) {
                    return int.Parse(term.Substring(pos + 1));
                }
                return int.Parse(term.Remove(pos));
            }
            return 1;
        }
    }
}