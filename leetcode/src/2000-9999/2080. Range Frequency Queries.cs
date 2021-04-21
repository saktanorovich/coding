using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_2080 {
        public class RangeFreqQuery {
            private readonly Dictionary<int, List<int>> indexes;

            public RangeFreqQuery(int[] arr) {
                this.indexes = new Dictionary<int, List<int>>();
                for (var i = 0; i < arr.Length; ++i) {
                    if (indexes.TryGetValue(arr[i], out var index) == false) {
                        index = new List<int>();
                        indexes.Add(arr[i], index);
                    }
                    index.Add(i);
                }
            }
    
            public int Query(int le, int ri, int value) {
                if (indexes.TryGetValue(value, out var index) == false) {
                    return 0;
                }
                var l = index.BinarySearch(le);
                var r = index.BinarySearch(ri);
                if (l < 0) l = (l + 1) * -1;
                if (r < 0) r = (r + 2) * -1;
                return r - l + 1;
            }
        }
    }
}
