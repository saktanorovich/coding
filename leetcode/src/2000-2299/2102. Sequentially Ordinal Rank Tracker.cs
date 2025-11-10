using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_2102 {
        public class SORTracker {
            private readonly SortedSet<(int, string)> ls;
            private readonly SortedSet<(int, string)> rs;

            public SORTracker() {
                ls = new SortedSet<(int, string)>(Comparer<(int, string)>.Create(Cmp));
                rs = new SortedSet<(int, string)>(Comparer<(int, string)>.Create(Cmp));
            }
            
            public void Add(string name, int score) {
                ls.Add((score, name));
                var max = ls.Max;
                ls.Remove(max);
                rs.Add(max);
            }
            
            public string Get() {
                var min = rs.Min;
                rs.Remove(min);
                ls.Add(min);
                return ls.Max.Item2;
            }

            private int Cmp((int, string) x, (int, string) y) {
                if (x.Item1 != y.Item1)
                    return y.Item1.CompareTo(x.Item1);
                else
                    return x.Item2.CompareTo(y.Item2);
            }
        }
    }
}