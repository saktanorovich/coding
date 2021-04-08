using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0092 {
        public ListNode ReverseBetween(ListNode head, int m, int n) {
            ListNode lptr = null;
            ListNode rptr = head;
            while (m > 1) {
                lptr = rptr;
                rptr = rptr.next;
                m--;
                n--;
            }
            ListNode prev = null;
            ListNode next = rptr;
            ListNode from = rptr;
            while (n > 0) {
                rptr = next.next;
                next.next = prev;
                prev = next;
                next = rptr;
                n--;
            }
            from.next = rptr;
            if (lptr != null) {
                lptr.next = prev;
            } else {
                head = prev;
            }
            return head;
        }

        public class ListNode {
            public int val;
            public ListNode next;

            public ListNode(int val = 0, ListNode next = null) {
                this.val = val;
                this.next = next;
            }
        }
    }
}
