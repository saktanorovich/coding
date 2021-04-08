using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0148 {
        public ListNode SortList(ListNode head) {
            if (head == null || head.next == null) {
                return head;
            }
            var half = Half(head);
            var left = SortList(head);
            var righ = SortList(half);
            head = new ListNode(-1);
            var last = head;
            while (left != null && righ != null) {
                if (left.val < righ.val) {
                    last.next = left;
                    left = left.next;
                } else {
                    last.next = righ;
                    righ = righ.next;
                }
                last = last.next;
            }
            if (left != null) last.next = left;
            if (righ != null) last.next = righ;
            return head.next;
        }

        private ListNode Half(ListNode head) {
            var slow = head;
            var fast = head;
            var last = head;
            while (fast != null && fast.next != null) {
                last = slow;
                slow = slow.next;
                fast = fast.next.next;
            }
            last.next = null;
            return slow;
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
