using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0138 {
        public Node CopyRandomList(Node head) {
            if (head == null) {
                return null;
            }
            var map = new Dictionary<Node, Node>();
            for (var curr = head; curr != null; curr = curr.next) {
                map[curr] = new Node(curr.val);
            }
            for (var curr = head; curr != null; curr = curr.next) {
                if (curr.next != null) {
                    map[curr].next = map[curr.next];
                }
                if (curr.rand != null) {
                    map[curr].rand = map[curr.rand];
                }
            }
            return map[head];
        }

        public class Node {
            public int val;
            public Node next;
            public Node rand;
            
            public Node(int val) {
                this.val = val;
                next = null;
                rand = null;
            }
        }
    }
}
