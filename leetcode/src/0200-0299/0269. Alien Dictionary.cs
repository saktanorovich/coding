using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0269 {
        private static readonly string ABC = "abcdefghijklmnopqrstuvwxyz";

        public string FindOrder(IList<string> words) {
            if (words == null || words.Count < 2) {
                return ABC;
            } else {
                return FindOrder(words, words.Count);
            }
        }

        private string FindOrder(IList<string> words, int n) {
            var dependOn = new bool[ABC.Length, ABC.Length];
            var inDegree = new int [ABC.Length];
            for (var i = 0; i + 1 < n; ++i) {
                var curr = words[i];
                var next = words[i + 1];
                var length = Math.Min(curr.Length, next.Length);
                for (var k = 0; k < length; ++k) {
                    if (curr[k] != next[k]) {
                        var a = curr[k] - 'a';
                        var b = next[k] - 'a';
                        if (dependOn[b, a] == false) {
                            dependOn[a, b] = true;
                            inDegree[b] ++;
                        } else {
                            return string.Empty;
                        }
                        break;
                    }
                }
            }
            return Build(dependOn, inDegree);
        }

        private string Build(bool[,] dependOn, int[] inDegree) {
            var queue = new Queue<int>();
            for (var i = 0; i < ABC.Length; ++i) {
                if (inDegree[i] == 0) {
                    queue.Enqueue(i);
                }
            }
            var order = new StringBuilder();
            while (queue.Count > 0) {
                var curr = queue.Dequeue();
                order.Append((char)(curr + 'a'));
                for (var next = 0; next < ABC.Length; ++next) {
                    if (dependOn[curr, next]) {
                        inDegree[next] --;
                        if (inDegree[next] == 0) {
                            queue.Enqueue(next);
                        }
                    }
                }
            }
            if (order.Length != inDegree.Length) {
                return string.Empty;
            }
            return order.ToString();
        }
    }
}
