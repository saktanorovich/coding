using System;
using System.Linq;

namespace TopCoder.Algorithm {
    public class StringBeads {
        public long numWays(int[] beads) {
            return numWaysImpl(beads.Concat(new int[5 - beads.Length]).ToArray());
        }

        private static long numWaysImpl(int[] beads) {
            var f = new long[beads[0] + 1, beads[1] + 1, beads[2] + 1, beads[3] + 1, beads[4] + 1, 5, 5];
            for (var x = 0; x < 5; ++x)
                for (var y = 0; y < 5; ++y)
                    if (beads[x] > 0 && beads[y] > 0 && x != y) {
                        int a = 0, b = 0, c = 0, d = 0, e = 0;
                        if (x == 0 || y == 0) a = 1;
                        if (x == 1 || y == 1) b = 1;
                        if (x == 2 || y == 2) c = 1;
                        if (x == 3 || y == 3) d = 1;
                        if (x == 4 || y == 4) e = 1;
                        f[a, b, c, d, e, x, y] = 1;
                    }
            for (var a = 0; a <= beads[0]; ++a)
            for (var b = 0; b <= beads[1]; ++b)
            for (var c = 0; c <= beads[2]; ++c)
            for (var d = 0; d <= beads[3]; ++d)
            for (var e = 0; e <= beads[4]; ++e)
                for (var x = 0; x < 5; ++x)
                for (var y = 0; y < 5; ++y) {
                    if (f[a, b, c, d, e, x, y] > 0 && x != y) {
                        if (a + 1 <= beads[0] && x != 0 && y != 0) f[a + 1, b, c, d, e, y, 0] += f[a, b, c, d, e, x, y];
                        if (b + 1 <= beads[1] && x != 1 && y != 1) f[a, b + 1, c, d, e, y, 1] += f[a, b, c, d, e, x, y];
                        if (c + 1 <= beads[2] && x != 2 && y != 2) f[a, b, c + 1, d, e, y, 2] += f[a, b, c, d, e, x, y];
                        if (d + 1 <= beads[3] && x != 3 && y != 3) f[a, b, c, d + 1, e, y, 3] += f[a, b, c, d, e, x, y];
                        if (e + 1 <= beads[4] && x != 4 && y != 4) f[a, b, c, d, e + 1, y, 4] += f[a, b, c, d, e, x, y];
                    }
                }
            var result = 0L;
            for (var x = 0; x < 5; ++x)
                for (var y = 0; y < 5; ++y) {
                    result += f[beads[0], beads[1], beads[2], beads[3], beads[4], x, y];
                }
            return result;
        }
    }
}