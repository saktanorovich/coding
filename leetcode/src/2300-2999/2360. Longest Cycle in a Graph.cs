using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_2360 {
        public int LongestCycle(int[] g) {
            var s = new Stack<int>();
            var u = new bool[g.Length];
            var c = -1;
            for (var v = 0; v < g.Length; ++v) {
                if (u[v]) {
                    continue;
                }
                s.Push(v);
                for (var x = g[v]; x != -1; x = g[x]) {
                    if (u[x]) {
                        for (var t = 1; s.Count > 0; ++t) {
                            var y = s.Pop();
                            if (y == x) {
                                c = Math.Max(c, t);
                                break;
                            }
                        }
                        break;
                    }
                    u[x] = true;
                    s.Push(x);
                }
                s.Clear();
            }
            return c;
        }
    }
}