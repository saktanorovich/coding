public class Solution {
    public IList<int> DistanceK(TreeNode root, TreeNode target, int k) {
        var graph = make(root, new List<int>[500 + 1]);
        var answer = new List<int>();
        var length = new int[500 + 1];
        for (var i = 0; i <= 500; ++i) {
            length[i] = 1000_000;
        }
        length[target.val] = 0;
        var queue = new Queue<int>();
        for (queue.Enqueue(target.val); queue.Count > 0;) {
            var curr = queue.Dequeue();
            if (length[curr] == k) {
                answer.Add(curr);
                continue;
            }
            foreach (var next in graph[curr]) {
                if (length[next] > length[curr] + 1) {
                    length[next] = length[curr] + 1;
                    queue.Enqueue(next);
                }
            }
        }
        return answer;
    }

    private static List<int>[] make(TreeNode root, List<int>[] graph) {
        if (root == null) {
            return graph;
        }
        if (graph[root.val] == null) {
            graph[root.val] = new List<int>();
        }
        join(root, root.left,  graph);
        join(root, root.right, graph);
        make(root.left,  graph);
        make(root.right, graph);
        return graph;
    }

    private static void join(TreeNode root, TreeNode leaf, List<int>[] graph) {
        if (leaf == null) {
            return;
        }
        if (graph[leaf.val] == null) {
            graph[leaf.val] = new List<int>();
        }
        graph[root.val].Add(leaf.val);
        graph[leaf.val].Add(root.val);
    }

    /**
    * Definition for a binary tree node.
    * public class TreeNode {
    *     public int val;
    *     public TreeNode left;
    *     public TreeNode right;
    *     public TreeNode(int x) { val = x; }
    * }
    */
}
