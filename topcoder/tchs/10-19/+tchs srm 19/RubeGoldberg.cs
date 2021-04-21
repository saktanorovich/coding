using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class RubeGoldberg {
        public int howClose(string[] parts, string last, int target) {
            var transition = new List<int>[4, 4];
            for (var src = 0; src < 4; ++src) {
                for (var dst = 0; dst < 4; ++dst) {
                    transition[src, dst] = new List<int>();
                }
            }
            foreach (var part in parts) {
                var items = part.Split(' ');
                var src = energy[items[0]];
                var dst = energy[items[1]];
                var due = int.Parse(items[2]);
                transition[src, dst].Add(due);
            }
            reach = new bool[4][];
            queue = new Queue<Tuple<int, int>>();
            foreach (var en in energy.Values) {
                reach[en] = new bool[maxTime];
                addqu(en, 0);
            }
            for (; queue.Count > 0; queue.Dequeue()) {
                var en = queue.Peek().Item1;
                var at = queue.Peek().Item2;
                for (var nx = 0; nx < 4; ++nx) {
                    foreach (var due in transition[en, nx]) {
                        if (0 < due && due + at < maxTime) {
                            if (reach[nx][due + at] == false) {
                                addqu(nx, due + at);
                            }
                        }
                    }
                }
            }
            var res = int.MaxValue;
            for (var at = 0; at < maxTime; ++at) {
                if (reach[energy[last]][at]) {
                    res = Math.Min(res, Math.Abs(target - at));
                }
            }
            return res;
        }

        private void addqu(int en, int at) {
            reach[en][at] = true;
            var entry = new Tuple<int, int>(en, at);
            queue.Enqueue(entry);
        }

        private Queue<Tuple<int, int>> queue;
        private bool[][] reach;

        private static readonly Dictionary<string, int> energy = new Dictionary<string, int> {
            { "CHEMICAL", 0 },
            { "ELECTRIC", 1 },
            { "MECHANICAL", 2 },
            { "THERMAL", 3 },
        };
        private static readonly int maxTime = 250000 * 2 + 1;
    }
}
