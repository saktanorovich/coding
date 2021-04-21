using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0430 {
        public Node Flatten(Node head) {
            var main = head;
            while (main != null) {
                if (main.child != null) {
                    var prev = main;
                    var temp = main.next;
                    var curr = main.child;
                    while (curr != null) {
                        curr.prev = prev;
                        prev.next = curr;
                        prev = curr;
                        curr = curr.next;
                    }
                    if (temp != null) {
                        temp.prev = prev;
                        prev.next = temp;
                    }
                    main.child = null;
                }
                main = main.next;
            }
            return head;
        }

        public class Node {
            public int val;
            public Node prev;
            public Node next;
            public Node child;
        }
    }
}
