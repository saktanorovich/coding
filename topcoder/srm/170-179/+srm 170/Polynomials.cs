using System;

namespace TopCoder.Algorithm {
    public class Polynomials {
        public long integralPoints(int ymax, int xmax, string equation) {
            return integralPoints(ymax, xmax, equation.Split('='));
        }

        private static long integralPoints(int ymax, int xmax, string[] equation) {
            var fy = parse(equation[0], 'y');
            var fx = parse(equation[1], 'x');
            var result = 0L;
            for (int x = 0, y = 0; x <= xmax && y <= ymax;) {
                var ry = fy(y);
                var rx = fx(x);
                if (rx == ry) {
                    var xx = x;
                    var yy = y;
                    while (fy(yy) == ry && yy <= ymax) ++yy;
                    while (fx(xx) == rx && xx <= xmax) ++xx;
                    result += 1L * (xx - x) * (yy - y);
                    x = xx;
                    y = yy;
                    continue;
                }
                if (rx < ry)
                    ++x;
                else
                    ++y;
            }
            return result;
        }

        private static Func<long, long> parse(string polynom, char f) {
            var terms = polynom.Split('+');
            var a = new long[10];
            foreach (var term in terms) {
                var power = 0;
                var coefficient = term;
                if (term.IndexOf(f) >= 0) {
                    var items = term.Split(new[] { f, '^' }, StringSplitOptions.RemoveEmptyEntries);
                    power = int.Parse(items[1]);
                    coefficient = items[0];
                }
                a[power] += int.Parse(coefficient);
            }
            return x => {
                var result = 0L;
                for (var i = 9; i >= 0; --i) {
                    result = result * x + a[i];
                }
                return result;
            };
        }
    }
}