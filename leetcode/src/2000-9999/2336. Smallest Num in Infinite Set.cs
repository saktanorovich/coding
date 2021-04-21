using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_2336 {
        public class SmallestInfiniteSet {
            private int threshold;
            private SortedSet<int> addedBack;

            public SmallestInfiniteSet() {
                threshold = 1;
                addedBack = new SortedSet<int>();
            }
            
            public int PopSmallest() {
                if (addedBack.Count > 0) {
                    var smallest = addedBack.Min;
                    addedBack.Remove(smallest);
                    return smallest;
                }
                return threshold++;
            }
            
            public void AddBack(int num) {
                if (num < threshold) {
                    addedBack.Add(num);
                }
            }
        }
    }
}
