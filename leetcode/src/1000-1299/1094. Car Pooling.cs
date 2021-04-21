using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1094 {
        public bool CarPooling(int[][] trips, int capacity) {
            var events = new List<(int type, int time, int capa)>();
            foreach (var trip in trips) {
                events.Add((0, trip[1], trip[0]));
                events.Add((1, trip[2], trip[0]));
            }
            events.Sort((
                (int type, int time, int capa) a,
                (int type, int time, int capa) b) => {
                    if (a.time != b.time) {
                        return a.time - b.time;
                    } else {
                        return b.type - a.type;
                    }
            });
            foreach (var ev in events) {
                if (ev.type == 0) {
                    capacity -= ev.capa;
                } else {
                    capacity += ev.capa;
                }
                if (capacity < 0) {
                    return false;
                }
            }
            return true;
        }
    }
}
