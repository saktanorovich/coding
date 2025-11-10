using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
    public class ProbabilityTree {
        public int[] getOdds(string[] tree, int lo, int up) {
            var p = new int[tree.Length];
            var a = new double[tree.Length];
            var b = new double[tree.Length];
            var e = new double[tree.Length];
            for (var n = 0; n < tree.Length; ++n) {
                var data = tree[n].Split(' ');
                if (data.Length > 1) {
                    p[n] = int.Parse(data[0]);
                    a[n] = int.Parse(data[1]) / 100.0;
                    b[n] = int.Parse(data[2]) / 100.0;
                }
                else {
                    p[n] = -1;
                    e[n] = int.Parse(data[0]) / 100.0;
                }
            }
            var result = new List<int>();
            for (var n = 0; n < tree.Length; ++n) {
                var path = new Stack<int>();
                for (var x = n; x >= 0; x = p[x]) {
                    path.Push(x);
                }
                while (path.Count > 0) {
                    var x = path.Pop();
                    if (e[x] < 1e-9) {
                        e[x] = e[p[x]] * a[x] + (1 - e[p[x]]) * b[x];
                    }
                }
                if (lo <= 100 * e[n] && 100 * e[n] <= up) {
                    result.Add(n);
                }
            }
            return result.ToArray();
        }
    }
}