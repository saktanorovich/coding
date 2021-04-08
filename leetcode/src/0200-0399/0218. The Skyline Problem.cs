using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0218 {
        public IList<IList<int>> GetSkyline(int[][] buildings) {
            var e = new List<Event>();
            for (var i = 0; i < buildings.Length; ++i) {
                e.Add(new Event(buildings[i][0], buildings[i][2], +1, i));
                e.Add(new Event(buildings[i][1], buildings[i][2], -1, i));
            }
            e.Sort((a, b) => {
                if (a.xpos != b.xpos) {
                    return a.xpos - b.xpos;
                }
                if (a.type != b.type) {
                    return b.type - a.type;
                }
                if (a.type > 0) {
                    return b.ypos - a.ypos;
                } else {
                    return a.ypos - b.ypos;
                }
            });
            var h = new SortedSet<Event>(
                Comparer<Event>.Create((a, b) => {
                    if (a.ypos != b.ypos) {
                        return a.ypos - b.ypos;
                    } else {
                        return a.mark - b.mark;
                    }
            }));
            h.Add(new Event(0, 0, 0, -1));
            var line = new List<IList<int>>();
            var last = 0;
            for (var i = 0; i < e.Count; ++i) {
                if (e[i].type > 0) {
                    h.Add(e[i]);
                } else {
                    h.Remove(e[i]);
                }
                var ypos = h.Max.ypos;
                if (ypos != last) {
                    line.Add(new[] { e[i].xpos, ypos });
                    last = ypos;
                }
            }
            return line;
        }

        private class Event {
            public readonly int xpos;
            public readonly int ypos;
            public readonly int type;
            public readonly int mark;

            public Event(int xpos, int ypos, int type, int mark) {
                this.xpos = xpos;
                this.ypos = ypos;
                this.type = type;
                this.mark = mark;
            }
        }
    }
}
