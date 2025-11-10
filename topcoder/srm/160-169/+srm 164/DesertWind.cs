using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
    public class DesertWind {
        public int daysNeeded(string[] theMap) {
            return daysNeeded(theMap, theMap.Length, theMap[0].Length);
        }

        private static int daysNeeded(string[] theMap, int n, int m) {
            var best = new int[n, m];
            var targeti = 0;
            var targetj = 0;
            for (var posi = 0; posi < n; ++posi) {
                for (var posj = 0; posj < m; ++posj) {
                    best[posi, posj] = int.MaxValue;
                    if (theMap[posi][posj] == '*') {
                        best[posi, posj] = 0;
                    }
                    if (theMap[posi][posj] == '@') {
                        targeti = posi;
                        targetj = posj;
                    }
                }
            }
            for (var process = true; process;) {
                process = false;
                for (var nexti = 0; nexti < n; ++nexti)
                    for (var nextj = 0; nextj < m; ++nextj) {
                        if (theMap[nexti][nextj] == 'X') {
                            continue;
                        }
                        var reach = new List<int>();
                        for (var k = 0; k < 8; ++k) {
                            var curri = nexti + di[k];
                            var currj = nextj + dj[k];
                            if (0 <= curri && curri < n && 0 <= currj && currj < m) {
                                if (best[curri, currj] < int.MaxValue) {
                                    reach.Add(best[curri, currj]);
                                }
                            }
                        }
                        reach.Sort();
                        for (var i = 0; i < reach.Count; ++i) {
                            var time = reach[i] + 3;
                            for (var j = i + 1; j < reach.Count; ++j) {
                                if (reach[i] <= reach[j] && reach[j] < time) {
                                    time = Math.Min(time, reach[j] + 1);
                                }
                            }
                            if (best[nexti, nextj] > time) {
                                best[nexti, nextj] = time;
                                process = true;
                            }
                        }
                    }
            }
            if (best[targeti, targetj] < int.MaxValue) {
                return best[targeti, targetj];
            }
            return -1;
        }

        private static readonly int[] di = {  0, -1,  0, +1, -1, +1, -1, +1 };
        private static readonly int[] dj = { -1,  0, +1,  0, -1, +1, +1, -1 };
    }
}