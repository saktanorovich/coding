using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0636 {
        public int[] ExclusiveTime(int n, IList<string> logs) {
            var stack = new Stack<int>();
            var units = new int[n + 1];
            var clock = 0;
            stack.Push (0);
            for (var i = 0; i < logs.Count; ++i) {
                var log = logs[i].Split(':');
                var func = int.Parse(log[0]) + 1;
                var time = int.Parse(log[2]);
                if (log[1] == "start") {
                    units[stack.Peek()] += time - clock;
                    stack.Push(func);
                    clock = time;
                } else {
                    units[stack.Peek()] += time - clock + 1;
                    stack.Pop();
                    clock = time + 1;
                }
            }
            return units.Skip(1).ToArray();
        }
    }
}
