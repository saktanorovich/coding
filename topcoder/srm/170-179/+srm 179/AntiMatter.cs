using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
    public class AntiMatter {
        public string unstable(int[] xform) {
            var steps = new List<int>();
            for (var i = 0; i < xform.Length; ++i) {
                for (var j = 0; j < xform.Length; ++j) {
                    if (!steps.Contains(xform[i] - xform[j])) {
                        steps.Add(xform[i] - xform[j]);
                    }
                }
            }
            var possible = new int[2 * Max];
            dfs(possible, steps, 0);
            var result = 0;
            for (var a = 0; a < Max; ++a) {
                for (var b = 0; b < Max; ++b) {
                    result += possible[Math.Abs(a - b)];
                }
            }
            if (result == Max * Max) {
                return "1.00000000";
            }
            return string.Format(".{0}", result.ToString().PadLeft(8, '0'));
        }

        private void dfs(int[] possible, IList<int> steps, int diff) {
            var stack = new Stack<int>();
            for (stack.Push(diff); stack.Count > 0;) {
                diff = stack.Pop();
                possible[diff] = 1;
                foreach (var step in steps) {
                    if (0 <= diff + step && diff + step < 2 * Max) {
                        if (possible[diff + step] == 0) {
                            stack.Push(diff + step);
                        }
                    }
                }
            }
        }

        private const int Max = 10000;
    }
}