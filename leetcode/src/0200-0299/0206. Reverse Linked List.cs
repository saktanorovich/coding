using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0206 {
        public ListNode ReverseList(ListNode head) {
            ListNode prev = null;
            ListNode next = null;
            while (head != null) {
                next = head.next;
                head.next = prev;
                prev = head;
                head = next;
            }
            return prev;
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
