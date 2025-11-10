using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0850 {
        public int RectangleArea(int[][] rectangles) {
            var es = new List<(int xpos, int ymin, int ymax, int type)>();
            foreach (var r in rectangles) {
                var xmin = r[0];
                var ymin = r[1];
                var xmax = r[2];
                var ymax = r[3];
                es.Add((xmin, ymin, ymax, +1));
                es.Add((xmax, ymin, ymax, -1));
            }
            es.Sort(((int xpos, int ymin, int ymax, int type) e1, (int xpos, int ymin, int ymax, int type) e2) => {
                if (e1.xpos != e2.xpos) {
                    return e1.xpos - e2.xpos;
                } else {
                    return e1.type - e2.type;
                }
            });
            var tree = new Node(0, max);
            var area = 0L;
            var xpos = 0;
            foreach (var e in es) {
                if (e.xpos != xpos) {
                    area += 1L * (e.xpos - xpos) * tree.get();
                    area %= mod;
                }
                tree.set(e.ymin, e.ymax, e.type);
                xpos = e.xpos;
            }
            return (int)area;
        }

        private const int mod = (int)1e9 + 7;
        private const int max = (int)1e9;

        private class Node {
            private Node lChild;
            private Node rChild;
            private int l, r;
            private long val;

            public Node(int l, int r) {
                this.l = l;
                this.r = r;
            }

            public int get() {
                return get(this);
            }

            public void set(int l, int r, int add) {
                set(this, l, r, add);
            }
            
            private static int get(Node node) {
                if (node.val != 0) {
                    return node.r - node.l;
                } else {
                    var res = 0;
                    if (node.lChild != null) res += get(node.lChild);
                    if (node.rChild != null) res += get(node.rChild);
                    return res;
                }
            }

            private static void set(Node node, int l, int r, int add) {
                if (l >= r) {
                    return;
                } else if (node.l == l && node.r == r) {
                    node.val += add;
                } else if (node.l <= r && node.r >= l) {
                    var x = (node.l + node.r) / 2;
                    if (node.lChild == null) node.lChild = new Node(node.l, x);
                    if (node.rChild == null) node.rChild = new Node(x, node.r);
                    set(node.lChild, l, Math.Min(x, r), add);
                    set(node.rChild, Math.Max(x, l), r, add);
                }
            }
        }
    }
}
