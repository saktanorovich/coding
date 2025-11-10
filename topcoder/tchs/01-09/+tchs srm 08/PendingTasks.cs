using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class PendingTasks {
        public int latestProcess(int[] supertasks) {
            var tree = supertasks.Select(i => new List<int>()).ToArray();
            for (var i = 0; i + 1 < supertasks.Length; ++i) {
                tree[supertasks[i]].Add(i);
            }
            return doit(tree, supertasks.Length - 1) - 1;
        }

        private static int doit(List<int>[] tree, int task) {
            if (tree[task].Count > 1) {
                var res = 0;
                foreach (var a in tree[task]) {
                    foreach (var b in tree[task]) {
                        if (a != b) {
                            var amt = time(tree, a) + doit(tree, b);
                            foreach (var c in tree[task]) {
                                if (c != a && c != b) {
                                    amt += wait(tree, c);
                                }
                            }
                            res = Math.Max(res, amt);
                        }
                    }
                }
                return res + 1;
            }
            return time(tree, task);
        }

        private static int wait(List<int>[] tree, int task) {
            if (tree[task].Count > 1) {
                var res = 0;
                foreach (var a in tree[task]) {
                    var amt = time(tree, a);
                    foreach (var b in tree[task]) {
                        if (a != b) {
                            amt += wait(tree, b);
                        }
                    }
                    res = Math.Max(res, amt);
                }
                return res;
            }
            return time(tree, task) - 1;
        }

        private static int time(List<int>[] tree, int task) {
            var res = 1;
            foreach (var next in tree[task]) {
                res += time(tree, next);
            }
            return res;
        }
    }
}
