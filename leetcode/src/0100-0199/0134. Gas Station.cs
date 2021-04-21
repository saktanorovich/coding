using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0134 {
        public int CanCompleteCircuit(int[] gas, int[] cost) {
            var tank = 0;
            var have = 0;
            var from = 0;
            for (var i = 0; i < gas.Length; ++i) {
                tank += gas[i] - cost[i];
                have += gas[i] - cost[i];
                if (have < 0) {
                    have = 0;
                    from =-1;
                } else if (from < 0) {
                    from = i;
                }
            }
            return tank >= 0 ? from : -1;
        }
    }
}
