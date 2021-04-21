using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopCoder.Algorithm {
    public class AlphaDice {
        public int badData(string[] rolls) {
            for (var pos = 0; pos < rolls.Length; ++pos) {
                if (inconsistent(rolls.Take(pos + 1).ToList())) {
                    return pos;
                }
            }
            return -1;
        }

        private static bool inconsistent(IList<string> rolls) {
            if (rolls.Last().Distinct().Count() == 3) {
                var labels = new List<char>();
                foreach (var roll in rolls) {
                    labels.AddRange(roll);
                }
                return inconsistent(rolls, labels.Distinct().ToList());
            }
            return true;
        }

        private static bool inconsistent(IList<string> rolls, IList<char> labels) {
            if (labels.Count <= 6) {
                while (labels.Count < 6) {
                    labels.Add('?');
                }
                foreach (var cubic in PermuteUtils.Permute(labels.ToArray())) {
                    if (consistent(rolls, cubic)) {
                        return false;
                    }
                }
            }
            return true;
        }

        private static bool consistent(IList<string> rolls, char[] cubic) {
            var near = new[] {
                new[] { 1, 2, 3, 4, 1 },
                new[] { 0, 4, 5, 2, 0 },
                new[] { 0, 1, 5, 3, 0 },
                new[] { 0, 2, 5, 4, 0 },
                new[] { 0, 3, 5, 1, 0 },
                new[] { 1, 4, 3, 2, 1 },
            };
            var corners = new List<string>();
            for (var i = 0; i < 6; ++i) {
                for (var j = 0; j < 4; ++j) {
                    corners.Add(cubic[i].ToString() + cubic[near[i][j]] + cubic[near[i][j + 1]]);
                }
            }
            foreach (var roll in rolls) {
                var match = false;
                foreach (var corner in corners) {
                    if (equals(roll, corner)) {
                        match = true;
                        break;
                    }
                }
                if (!match) return false;
            }
            return true;
        }

        private static bool equals(string a, string b) {
            for (var i = 0; i < a.Length; ++i) {
                if (a[i] != b[i]) {
                    return false;
                }
            }
            return true;
        }

        public static class PermuteUtils {
            public static IList<T[]> Permute<T>(T[] list) {
                var result = new List<T[]>();
                if (list.Length == 0) {
                    result.Add(new T[0]);
                }
                else {
                    for (var i = 0; i < list.Length; ++i) {
                        foreach (var res in Permute(Remove(list, i))) {
                            result.Add(Concat(list[i], res));
                        }
                    }
                }
                return result.ToArray();
            }

            private static T[] Concat<T>(T item, T[] list) {
                var result = new List<T>(new[] { item });
                result.AddRange(list);
                return result.ToArray();
            }

            private static T[] Remove<T>(T[] list, int index) {
                var result = new List<T>(list);
                result.RemoveAt(index);
                return result.ToArray();
            }
        }
    }
}