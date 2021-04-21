using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
    public class TreasureHunt {
        public int[] findTreasure(string[] island, string[] instructions) {
            var map = new List<string>();
            map.Add("".PadLeft(island[0].Length + 2, '.'));
            foreach (var row in island) {
                map.Add(string.Format(".{0}.", row));
            }
            map.Add("".PadLeft(island[0].Length + 2, '.'));
            return findTreasure(map.ToArray(), island.Length, island[0].Length, instructions);
        }

        private static int[] findTreasure(string[] island, int n, int m, string[] instructions) {
            var estimatey = 0;
            var estimatex = 0;
            var ypos = new List<int>();
            var xpos = new List<int>();
            for (var y = 1; y <= n; ++y)
                for (var x = 1; x <= m; ++x) {
                    if (island[y][x] == 'X') {
                        estimatey = y;
                        estimatex = x;
                    }
                    if (island[y][x] != '.') {
                        for (var k = 0; k < 4; ++k)
                            if (island[y + dy[k]][x + dx[k]] == '.') {
                                var yy = y;
                                var xx = x;
                                foreach (var instr in instructions) {
                                    for (var p = 1; p <= int.Parse(instr.Substring(2)); ++p) {
                                        xx += dx["NSEW".IndexOf(instr[0])];
                                        yy += dy["NSEW".IndexOf(instr[0])];
                                        if (island[yy][xx] == '.') {
                                            goto end;
                                        }
                                    }
                                }
                                ypos.Add(yy);
                                xpos.Add(xx);
                                end: break;
                            }
                    }
                }
            if (xpos.Count > 0) {
                var indx = 0;
                for (var next = 1; next < xpos.Count; ++next) {
                    var indxDist = dist(xpos[indx], ypos[indx], estimatex, estimatey);
                    var nextDist = dist(xpos[next], ypos[next], estimatex, estimatey);
                    if (nextDist < indxDist) {
                        indx = next;
                    }
                }
                return new[] { xpos[indx] - 1, ypos[indx] - 1 };
            }
            return new int[0];
        }

        private static int dist(int x1, int y1, int x2, int y2) {
            return (x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1);
        }

        private static readonly int[] dy = { -1, +1, 0, 0 };
        private static readonly int[] dx = { 0, 0, +1, -1 };
    }
}