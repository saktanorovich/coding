using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopCoder.Algorithm {
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

        private static T[] Concat<T>(T item, IEnumerable<T> list) {
            var result = new List<T>(new[] { item });
            result.AddRange(list);
            return result.ToArray();
        }

        private static T[] Remove<T>(IEnumerable<T> list, int index) {
            var result = new List<T>(list);
            result.RemoveAt(index);
            return result.ToArray();
        }
    }
}