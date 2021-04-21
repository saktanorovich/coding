using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0739 {
        public int[] DailyTemperatures(int[] temperatures) {
            var res = new int[temperatures.Length];
            var sta = new Stack<int>();
            sta.Push(temperatures.Length - 1);
            for (var i = temperatures.Length - 2; i >= 0; --i) {
                while (sta.Count > 0) {
                    if (temperatures[i] >= temperatures[sta.Peek()]) {
                        sta.Pop();
                    } else break;
                }
                if (sta.Count > 0) {
                    res[i] = sta.Peek() - i;
                }
                sta.Push(i);
            }
            return res;
        }
    }
}
