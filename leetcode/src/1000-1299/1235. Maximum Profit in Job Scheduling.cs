using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1235 {
        public int JobScheduling(int[] startTime, int[] endTime, int[] profit) {
            var jobs = new List<Job>();
            jobs.Add(new Job());
            for (var i = 0; i < profit.Length; ++i) {
                jobs.Add(new Job {
                    startTime = startTime[i],
                    endTime = endTime[i],
                    profit = profit[i]
                });
            }
            return JobScheduling(jobs.ToArray());
        }

        private int JobScheduling(Job[] jobs) {
            Array.Sort(jobs, (a, b) => a.endTime - b.endTime);
            var opt = new int[jobs.Length];
            for (var i = 1; i < jobs.Length; ++i) {
                int lo = 0, hi = i - 1;
                while (lo < hi) {
                    var x = (lo + hi + 1) / 2;
                    if (jobs[x].endTime > jobs[i].startTime) {
                        hi = x - 1;
                    } else {
                        lo = x;
                    }
                }
                opt[i] = Math.Max(opt[lo] + jobs[i].profit, opt[i - 1]);
            }
            return opt.Last();
        }

        private sealed class Job {
            public int startTime;
            public int endTime;
            public int profit;
        }
    }
}
