using System;
using System.Collections.Generic;

namespace interview.hackerrank {
    public class ArrayFilling {
        public int[] fill(string[] a, int n) {
            return fill(Array.ConvertAll(a, s => {
                var list = Array.ConvertAll(s.Split(' '), num => int.Parse(num) - 1);
                var result = new List<int>();
                for (var i = 1; i < list.Length; ++i) {
                    result.Add(list[i]);
                }
                return result;
            }), n);
        }

        private int[] fill(List<int>[] a, int n) {
            var indegree = new int[n];
            for (var i = 0; i < n; ++i) {
                foreach (int j in a[i]) {
                    ++indegree[j];
                }
            }
            var bucket = Array.ConvertAll(new int[n], x => new List<int>());
            for (var i = 0; i < n; ++i) {
                if (indegree[i] == 0) {
                    bucket[0].Add(i);
                }
            }
            for (var level = 0; level < n; ++level) {
                foreach (var i in bucket[level]) {
                    foreach (var j in a[i]) {
                        --indegree[j];
                        if (indegree[j] == 0) {
                            bucket[level + 1].Add(j);
                        }
                    }
                }
            }
            var result = new int[n];
            for (var i = 0; i < bucket.Length; ++i) {
                foreach (var j in bucket[i]) {
                    result[j] = i + 1;
                }
            }
            return verify(a, result, n);
        }

        private int[] verify(List<int>[] a, int[] b, int n) {
            for (var i = 0; i < n; ++i) {
                foreach (var j in a[i]) {
                    if (b[i] >= b[j]) {
                        return Array.ConvertAll(new int[n], x => -1);
                    }
                }
            }
            return b;
        }
    }
}
