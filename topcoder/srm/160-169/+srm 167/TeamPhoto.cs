using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class TeamPhoto {
        public int minDiff(int[] height) {
            return minDiff(height, height.Length);
        }

        private static int minDiff(int[] height, int n) {
            var h0 = height[0];
            var h1 = height[1];
            var h2 = height[2];
            var left = (n - 3) / 2;
            if (height.Length % 2 == 1) {
                return minDiff(h0, h1, h2, height.Skip(3), left);
            }
            var res1 = minDiff(h0, h1, h2, height.Skip(3), left);
            var res2 = minDiff(h0, h1, h2, height.Skip(3), left + 1);
            return Math.Min(res1, res2);
        }

        private static int minDiff(int h0, int h1, int h2, IEnumerable<int> members, int left) {
            members = members.OrderBy(x => x);
            var result = int.MaxValue;
            foreach (var shifted in shift(members)) {
                var lres = minDiff(h1, h0, shifted.Take(left));
                var rres = minDiff(h0, h2, shifted.Skip(left));
                result = Math.Min(result, lres + rres);
            }
            return result;
        }

        private static int minDiff(int h0, int h1, IEnumerable<int> members) {
            var result = int.MaxValue;
            for (var left = 0; left < members.Count(); ++left) {
                var lpart = members.Take(left);
                var rpart = members.Skip(left);
                result = Math.Min(result, evaluate(h0, h1, lpart, rpart));
                result = Math.Min(result, evaluate(h0, h1, lpart, rpart.Reverse()));
                result = Math.Min(result, evaluate(h0, h1, lpart.Reverse(), rpart));
                result = Math.Min(result, evaluate(h0, h1, lpart.Reverse(), rpart.Reverse()));
            }
            return result;
        }

        private static IEnumerable<T[]> shift<T>(IEnumerable<T> a) {
            var items = new List<T>();
            items.AddRange(a);
            items.AddRange(a);
            var result = new List<T[]>();
            for (var i = 0; i < a.Count(); ++i) {
                result.Add(items.Skip(i).Take(a.Count()).ToArray());
            }
            return result;
        }

        private static int evaluate(int h0, int h1, IEnumerable<int> lpart, IEnumerable<int> rpart) {
            var heights = new List<int>();
            heights.Add(h0);
            heights.AddRange(lpart);
            heights.AddRange(rpart);
            heights.Add(h1);
            return evaluate(heights);
        }

        private static int evaluate(IList<int> height) {
            var result = 0;
            for (var i = 1; i < height.Count; ++i) {
                result += Math.Abs(height[i] - height[i - 1]);
            }
            return result;
        }
    }
}