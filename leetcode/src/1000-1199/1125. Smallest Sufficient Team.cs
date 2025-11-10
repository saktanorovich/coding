using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1125 {
        public int[] SmallestSufficientTeam(string[] req_skills, IList<IList<string>> people) {
            var skills = new Dictionary<string, int>();
            foreach (var skill in req_skills) {
                skills[skill] = skills.Count;
            }
            return SmallestSufficientTeam(people.Select(a => {
                var set = 0;
                foreach (var skill in a) {
                    if (skills.TryGetValue(skill, out var x)) {
                        set |= 1 << x;
                    }
                }
                return set;
            }).ToArray(), skills.Count);
        }

        private int[] SmallestSufficientTeam(int[] people, int skills) {
            var best = new int[1 << skills];
            var from = new int[1 << skills];
            var prev = new int[1 << skills];
            for (var set = 1; set < 1 << skills; ++set) {
                best[set] = int.MaxValue;
            }
            for (var i = 0; i < people.Length; ++i) {
                for (var set = (1 << skills) - 1; set >= 0; --set) {
                    if (best[set] < int.MaxValue) {
                        if (best[set | people[i]] > best[set] + 1) {
                            best[set | people[i]] = best[set] + 1;
                            from[set | people[i]] = set;
                            prev[set | people[i]] = i;
                        }
                    }
                }
            }
            var res = new List<int>();
            for (var set = (1 << skills) - 1; set > 0;) {
                res.Add(prev[set]);
                set = from[set];
            }
            return res.ToArray();
        }
    }
}
