using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0826 {
        public int MaxProfitAssignment(int[] difficulty, int[] profit, int[] worker) {
            var order = Enumerable.Range(0, difficulty.Length).ToArray();
            Array.Sort(order, (a, b) => {
                return difficulty[a] - difficulty[b];
            });
            Array.Sort(worker);
            var max = 0;
            var res = 0;
            for (int i = 0, j = 0; i < worker.Length; ++i) {
                while (j < difficulty.Length && difficulty[order[j]] <= worker[i]) {
                    if (max < profit[order[j]]) {
                        max = profit[order[j]];
                    }
                    j = j + 1;
                }
                res += max;
            }
            return res;
        }
    }
}
