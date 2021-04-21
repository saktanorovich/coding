using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1396 {
        public class UndergroundSystem {
            private Dictionary<int, (string stat, int time)> checkInAt;
            private Dictionary<string, Dictionary<string, int>> count;
            private Dictionary<string, Dictionary<string, int>> summa;

            public UndergroundSystem() {
                checkInAt = new Dictionary<int, (string stat, int time)>();
                count = new Dictionary<string, Dictionary<string, int>>();
                summa = new Dictionary<string, Dictionary<string, int>>();
            }

            public void CheckIn(int id, string stationName, int t) {
                if (checkInAt.ContainsKey(id) == false) {
                    checkInAt.Add(id, (stationName, t));
                } else {
                    checkInAt[id] = (stationName, t);
                }
            }

            public void CheckOut(int id, string stationName, int t) {
                var last = checkInAt[id];
                count.TryAdd(last.stat, new Dictionary<string, int>());
                summa.TryAdd(last.stat, new Dictionary<string, int>());
                count[last.stat].TryAdd(stationName, 0);
                summa[last.stat].TryAdd(stationName, 0);
                count[last.stat][stationName] += 1;
                summa[last.stat][stationName] += t - last.time;
            }

            public double GetAverageTime(string startStation, string endStation) {
                return 1.0 * summa[startStation][endStation] / count[startStation][endStation];
            }
        }
    }
}
