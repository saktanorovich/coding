using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0019 {
        public ListNode RemoveNthFromEnd(ListNode head, int n) {
            var fake = new ListNode(-1) {
                next = head
            };
            var fast = fake;
            var slow = fake;
            while (n-- > 0) {
                fast = fast.next;
            }
            while (fast.next != null) {
                fast = fast.next;
                slow = slow.next;
            }
            slow.next = slow.next.next;
            return fake.next;
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
