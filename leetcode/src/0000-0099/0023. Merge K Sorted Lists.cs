using System;
using System.Collections.Generic;
using System.Linq;

namespace coding.leetcode {
    public class Solution_0023 {
        public ListNode MergeKLists(ListNode[] lists) {
            if (lists.Length > 0) {
                return Merge(lists);
            } else {
                return null;
            }
        }

        private ListNode Merge(ListNode[] lists) {
            if (lists.Length > 1) {
                var mid = lists.Length / 2;
                var L = Merge(lists.Take(mid).ToArray());
                var R = Merge(lists.Skip(mid).ToArray());
                if (L == null) {
                    return R;
                }
                if (R == null) {
                    return L;
                }
                var H = (ListNode)null;
                var T = (ListNode)null;
                while (L != null && R != null) {
                    if (L.val < R.val) {
                        L = Next(ref H, ref T, L);
                    } else {
                        R = Next(ref H, ref T, R);
                    }
                }
                if (L != null) {
                    T.next = L;
                }
                if (R != null) {
                    T.next = R;
                }
                return H;
            }
            return lists[0];
        }

        private ListNode Next(ref ListNode H, ref ListNode T, ListNode X) {
            if (H == null) {
                H = X;
                T = X;
            } else {
                T.next = X;
                T = X;
            }
            return X.next;
        }

        public class ListNode {
            public int val;
            public ListNode next;

            public ListNode(int x) {
                val = x;
            }
        }
    }
}