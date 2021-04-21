using System;

namespace TopCoder.Algorithm {
    public class Partial {
        public string derivative(string expr, string vars) {
            return stringify(derivative(parse(expr), vars));
        }

        private static int[,,] derivative(int[,,] expr, string vars) {
            foreach (var d in vars) {
                expr = derivative(expr, d);
            }
            return expr;
        }

        private static int[,,] derivative(int[,,] expr, char d) {
            var res = new int[10, 10, 10];
            for (var x = 0; x < 10; ++x)
                for (var y = 0; y < 10; ++y)
                    for (var z = 0; z < 10; ++z) {
                        if (d == 'x' && x < 9) res[x, y, z] = (x + 1) * expr[x + 1, y, z];
                        if (d == 'y' && y < 9) res[x, y, z] = (y + 1) * expr[x, y + 1, z];
                        if (d == 'z' && z < 9) res[x, y, z] = (z + 1) * expr[x, y, z + 1];
                    }
            return res;
        }

        private static string stringify(int[,,] expr) {
            var result = string.Empty;
            for (var sum = 81; sum >= 0; --sum)
                for (var x = 9; x >= 0; --x)
                    for (var y = 9; y >= 0; --y)
                        for (var z = 9; z >= 0; --z) {
                            if (x + y + z == sum) {
                                result += stringify(x, y, z, expr[x, y, z]);
                            }
                        }
            result = result
                .Replace("^1]", "]")
                .Replace("1[*", "[")
                    .Replace("[", string.Empty)
                    .Replace("]", string.Empty)
            .Trim('+', ' ');
            if (string.IsNullOrEmpty(result)) {
                result = "0";
            }
            return result;
        }

        private static string stringify(int x, int y, int z, int m) {
            var result = string.Empty;
            if (m > 0) {
                result += m;
                if (x > 0) result += string.Format("[*x^{0}]", x);
                if (y > 0) result += string.Format("[*y^{0}]", y);
                if (z > 0) result += string.Format("[*z^{0}]", z);
                return string.Format(" + [{0}]", result);
            }
            return result;
        }

        private static int[,,] parse(string expr) {
            var result = new int[10, 10, 10];
            foreach (var term in expr.Split(new[] { '+', ' ' }, StringSplitOptions.RemoveEmptyEntries)) {
                var x = 0;
                var y = 0;
                var z = 0;
                var m = 1;
                foreach (var item in term.Split('*')) {
                    if (parse(item, "x", ref x) ||
                        parse(item, "y", ref y) ||
                        parse(item, "z", ref z)) {
                        continue;
                    }
                    m *= int.Parse(item);
                }
                result[x, y, z] += m;
            }
            return result;
        }

        private static bool parse(string term, string variable, ref int result) {
            if (term.StartsWith(variable)) {
                term = term.Replace(variable, "").Replace("^", "");
                var power = 1;
                if (term.Length > 0) {
                    power = int.Parse(term);
                }
                result += power;
                return true;
            }
            return false;
        }
    }
}