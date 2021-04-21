using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0815 {
        public int NumBusesToDestination(int[][] routes, int S, int T) {
            var buses = new Dictionary<int, List<int>>();
            var btake = new HashSet<int>();
            var stake = new HashSet<int>();
            for (var bus = 0; bus < routes.Length; ++bus) {
                foreach (var stop in routes[bus]) {
                    if (buses.ContainsKey(stop) == false) {
                        buses.Add(stop, new List<int>());
                    }
                    buses[stop].Add(bus);
                }
            }
            var q = new Queue<int>();
            q.Enqueue(S);
            stake.Add(S);
            for (var changes = 0; q.Count > 0; ++changes) {
                var size = q.Count;
                while (size-- > 0) {
                    var stop = q.Dequeue();
                    if (stop == T) {
                        return changes;
                    }
                    foreach (var bus in buses[stop]) {
                        if (btake.Add(bus)) {
                            foreach (var next in routes[bus]) {
                                if (stake.Add(next)) {
                                    q.Enqueue(next);
                                }
                            }
                        }
                    }
                }
            }
            return -1;
        }
    }
}
