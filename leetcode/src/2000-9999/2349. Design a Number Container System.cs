using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_2349 {
        public class NumberContainers {
            private Dictionary<int, int> array;
            private Dictionary<int, SortedSet<int>> index;

            public NumberContainers() {
                array = new Dictionary<int, int>();
                index = new Dictionary<int, SortedSet<int>>();
            }

            public void Change(int idx, int num) {
                if (array.TryGetValue(idx, out var old)) {
                    index[old].Remove(idx);
                }
                array[idx] = num;
                index.TryAdd(num, new SortedSet<int>());
                index[num].Add(idx);
            }

            public int Find(int num) {
                if (index.TryGetValue(num, out var set)) {
                    return set.Any() ? set.Min : -1;
                }
                return -1;
            }
        }
    }
}