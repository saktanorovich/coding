using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0012 {
        public string IntToRoman(int num) {
            var res = String.Empty;
            foreach (var entry in rules) {
                while (num >= entry.Key) {
                    num -= entry.Key;
                    res += entry.Value;
                }
            }
            return res;
        }

        private readonly KeyValueList<int, string> rules = new KeyValueList<int, string> {
            { 1000, "M"  },
            {  900, "CM" },
            {  500, "D"  },
            {  400, "CD" },
            {  100, "C"  },
            {   90, "XC" },
            {   50, "L"  },
            {   40, "XL" },
            {   10, "X"  },
            {    9, "IX" },
            {    5, "V"  },
            {    4, "IV" },
            {    1, "I"  }
        };

        private class KeyValueList<TKey, TValue> : List<KeyValuePair<TKey, TValue>> {
            public void Add(TKey key, TValue value) {
                Add(new KeyValuePair<TKey, TValue>(key, value));
            }
        }
    }
}
