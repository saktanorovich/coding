using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0399 {
        public double[] CalcEquation(IList<IList<string>> equations, double[] values, IList<IList<string>> queries) {
            var vars = new Dictionary<string, int>();
            foreach (var e in equations) {
                foreach (var v in e) {
                    if (vars.ContainsKey(v) == false) {
                        vars.Add(v, vars.Count);
                    }
                }
            }
            var eval = new double[vars.Count, vars.Count];
            for (var i = 0; i < equations.Count; ++i) {
                var e = equations[i];
                eval[vars[e[0]], vars[e[1]]] = 1.0 * values[i];
                eval[vars[e[1]], vars[e[0]]] = 1.0 / values[i];
            }
            for (var k = 0; k < vars.Count; ++k) {
                for (var i = 0; i < vars.Count; ++i) {
                    for (var j = 0; j < vars.Count; ++j) {
                        if (eval[i, k] > 0 && eval[k, j] > 0) {
                            eval[i, j] = eval[i, k] * eval[k, j];
                        }
                    }
                }
            }
            var res = new List<double>();
            foreach (var q in queries) {
                if (vars.TryGetValue(q[0], out var x) && vars.TryGetValue(q[1], out var y)) {
                    if (eval[x, y] > 0) {
                        res.Add(eval[x, y]);
                        continue;
                    }
                }
                res.Add(-1);
            }
            return res.ToArray();
        }
    }
}
