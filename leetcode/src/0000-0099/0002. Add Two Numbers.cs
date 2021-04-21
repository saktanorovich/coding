/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int val = 0, ListNode next = null) {
 *         this.val = val;
 *         this.next = next;
 *     }
 * }
 */
public class Solution {
    public ListNode AddTwoNumbers(ListNode l1, ListNode l2) {
        var head = new ListNode();
        var curr = head;
        var flag = 0;
        while (l1 != null || l2 != null || flag > 0) {
            var val = flag;
            if (l1 != null) { val += l1.val; l1 = l1.next; }
            if (l2 != null) { val += l2.val; l2 = l2.next; }
            flag = val / 10;
            curr.next = new ListNode(val % 10);
            curr = curr.next;
        }
        return head.next;
    }
}
