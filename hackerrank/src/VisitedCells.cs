using System;

namespace interview.hackerrank {
    public class VisitedCells {
        private static int[] di = { 0, +1, 0, -1 };
        private static int[] dj = { +1, 0, -1, 0 };

        public int count(int n, int m) {
            var a = new int[n, m];
            for (int i = 0, j = 0, k = 0; true; k = (k + 1) % 4) {
                a[i, j] = 1;
                var makeMove = false;
                for (var r = 0; r < 4; ++r) {
                    if (inbound(i + di[k], j + dj[k], n, m)) {
                        if (a[i + di[k], j + dj[k]] == 0) {
                            makeMove = true;
                            break;
                        }
                    }
                    k = (k + 1) % 4;
                }
                if (!makeMove) {
                    break;
                }
                i += di[k];
                j += dj[k];
            }
            var result = 0;
            for (var i = 0; i < n; ++i) {
                for (var j = 0; j < m; ++j) {
                    result += a[i, j];
                }
            }
            return result;
        }

        private bool inbound(int i, int j, int n, int m) {
            return 0 <= i && i < n && 0 <= j && j < m;
        }
    }
}
