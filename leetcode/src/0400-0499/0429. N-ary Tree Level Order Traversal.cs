/**
public class Node {
    public int val;
    public IList<Node> children;

    public Node() {}

    public Node(int val) {
        this.val = val;
    }

    public Node(int val, IList<Node> children) {
        this.val      = val;
        this.children = children;
    }
}
*/
public class Solution {
    public IList<IList<int>> LevelOrder(Node root) {
        return LevelOrder(root, new List<IList<int>>(), 0);
    }

    private IList<IList<int>> LevelOrder(Node root, List<IList<int>> order, int level) {
        if (root == null) {
            return order;
        }
        if (order.Count <= level) {
            order.Add(new List<int>());
        }
        order[level].Add(root.val);
        if (root.children != null) {
            foreach (var node in root.children) {
                LevelOrder(node, order, level + 1);
            }
        }
        return order;
    }
}