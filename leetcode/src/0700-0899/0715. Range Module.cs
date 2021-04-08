using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class RangeModule {
        private readonly IList<int> ls;
        private readonly IList<int> rs;
        private int sz;

        public RangeModule() {
            ls = new int[1000];
            rs = new int[1000];
            sz = 0;
        }

        public void AddRange(int L, int R) {
            var l = index(L);
            if (l < 0) {
                l = ~l;
            } else {
                L = ls[l];
            }
            var r = index(R);
            if (r < 0) {
                r = ~r - 1;
            } else {
                R = rs[r];
            }
            remove(l, r);
            insert(L, R);
        }

        public void RemoveRange(int L, int R) {
            var r = index(R);
            if (r < 0) {
                r = ~r - 1;
            } else {
                insert(R, rs[r]);
            }
            var l = index(L);
            if (l < 0) {
                l = ~l;
            } else {
                rs[l] = L;
                l++;
            }
            remove(l, r);
        }

        public bool QueryRange(int L, int R) {
            var l = index(L);
            var r = index(R);
            if (l == r) {
                return l >= 0;
            }
            return false;
        }

        private int index(int x) {
            for (var i = 0; i < sz; ++i) {
                var l = ls[i];
                var r = rs[i];
                if (l <= x && x <= r) {
                    return i;
                }
                if (x < l) {
                    return ~i;
                }
            }
            return ~sz;
        }

        private void insert(int l, int r) {
            var p = sz;
            for (var i = 0; i < sz; ++i) {
                if (ls[i] == l) {
                    ls[i] = l;
                    rs[i] = r;
                    return;
                }
                if (ls[i] > l) {
                    p = i;
                    break;
                }
            }
            for (var i = ++sz; i > p; --i) {
                ls[i] = ls[i - 1];
                rs[i] = rs[i - 1];
            }
            ls[p] = l;
            rs[p] = r;
        }

        private void remove(int l, int r) {
            var d = r - l + 1;
            for (++r; r < sz; ++l, ++r) {
                ls[l] = ls[r];
                rs[l] = rs[r];
            }
            sz -= d;
        }
    }
}
