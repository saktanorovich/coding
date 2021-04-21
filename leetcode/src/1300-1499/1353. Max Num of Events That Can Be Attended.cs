using System;
using System.Collections.Generic;

namespace coding.leetcode {
    public class Solution_1353 {
        public int MaxEvents(int[][] events) {
            var pull = new List<Event>();
            foreach (var ev in events) {
                pull.Add(new Event(ev[0], ev[1]));
            }
            pull.Sort((x, y) => x.begAt - y.begAt);
            return MaxEvents(pull);
        }

        private static int MaxEvents(List<Event> pull) {
            var answ = 0;
            var time = 0;
            var open = new PriorityQueue<int, int>();
            for (var indx = 0; indx < pull.Count || open.Count > 0;) {
                if (open.Count == 0) {
                    time = pull[indx].begAt;
                }
                while (indx < pull.Count) {
                    if (pull[indx].begAt <= time) {
                        open.Enqueue(pull[indx].endAt, pull[indx].endAt);
                        indx = indx + 1;
                    } else break;
                }
                open.Dequeue();
                answ = answ + 1;
                time = time + 1;
                while (open.Count > 0) {
                    var endAt = open.Peek();
                    if (endAt < time) {
                        open.Dequeue();
                    } else break;
                }
            }
            return answ;
        }

        private struct Event {
            public readonly int begAt;
            public readonly int endAt;

            public Event(int begAt, int endAt) {
                this.begAt = begAt;
                this.endAt = endAt;
            }
        }
    }
}
/*
 * [[1,2],[2,3],[3,4]]             : 3
 * [[1,2],[2,3],[3,4],[1,2]]       : 4
 * [[1,2],[1,2],[3,3],[1,5],[1,5]] : 5
 * [[1,4],[4,4],[2,2],[3,4],[1,1]] : 4
 * [[1,5],[1,5],[1,5],[2,3],[2,3]] : 5
 */
 