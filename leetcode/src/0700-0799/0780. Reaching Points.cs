using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0780 {
        public bool ReachingPoints(int sx, int sy, int tx, int ty) {
            while (sx <= tx && sy <= ty) {
                if (sx == tx && sy == ty) {
                    return true;
                }
                if (sx == tx) return (ty - sy) % sx == 0;
                if (sy == ty) return (tx - sx) % sy == 0;

                if (tx > ty)
                    tx %= ty;
                else
                    ty %= tx;
            }
            return false;
        }
    }
}
