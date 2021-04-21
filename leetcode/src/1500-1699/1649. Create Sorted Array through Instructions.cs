using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1649 {
        public int CreateSortedArray(int[] instructions) {
            var tree = new AVLTree();
            var answ = 0;
            foreach (var item in instructions) {
                answ += tree.add(item);
                if (answ >= mod)
                    answ -= mod;
            }
            return answ;
        }
    
        private const int mod = (int)1e9 + 7;

        private sealed class AVLTree {
            private AVLTreeNode root;

            public int add(int item) {
                root = add(root, item);
                var ls = less(root, item);
                var gt = more(root, item);
                return Math.Min(ls, gt);
            }

            private AVLTreeNode add(AVLTreeNode node, int item) {
                if (ReferenceEquals(node, null)) {
                    return new AVLTreeNode(item);
                } else {
                    if (node.item == item) {
                        node.count++;
                        node.total++;
                        return node;
                    }
                    else if (item < node.item) node.leChild = add(node.leChild, item);
                    else if (item > node.item) node.riChild = add(node.riChild, item);
                    return balance(node);
                }
            }

            private int less(AVLTreeNode node, int item) {
                if (ReferenceEquals(node, null)) {
                    return 0;
                }
                if (node.item < item) {
                    return less(node.riChild, item) + node.count + total(node.leChild);
                } else {
                    return less(node.leChild, item);
                }
            }

            private int more(AVLTreeNode node, int item) {
                if (ReferenceEquals(node, null)) {
                    return 0;
                }
                if (node.item > item) {
                    return more(node.leChild, item) + node.count + total(node.riChild); 
                } else {
                    return more(node.riChild, item);
                }
            }

            private AVLTreeNode balance(AVLTreeNode node) {
                refresh(node);
                if (Math.Abs(score(node)) > 1) {
                    if (isLeHeavy(node)) return isRiHeavy(node.leChild) ? rotateRi2(node) : rotateRi1(node);
                    if (isRiHeavy(node)) return isLeHeavy(node.riChild) ? rotateLe2(node) : rotateLe1(node);
                }
                return node;
            }

            private bool isLeHeavy(AVLTreeNode node) => score(node) > 1;
            private bool isRiHeavy(AVLTreeNode node) => score(node) < 1;

            private int score(AVLTreeNode node) {
                var leHeight = level(node.leChild);
                var riHeight = level(node.riChild);
                return leHeight - riHeight;
            }

            private int level(AVLTreeNode node) => node != null ? node.level : 0;
            private int total(AVLTreeNode node) => node != null ? node.total : 0;

            private void refresh(AVLTreeNode node) {
                node.level = Math.Max(level(node.leChild), level(node.riChild)) + 1;
                node.total = node.count;
                node.total += total(node.leChild);
                node.total += total(node.riChild);
            }

            private AVLTreeNode rotateLe1(AVLTreeNode T) {
                var R = T.riChild;
                var L = T.riChild.leChild;
                T.riChild = L;
                R.leChild = T;
                refresh(T);
                refresh(R);
                return R;
            }

            private AVLTreeNode rotateRi1(AVLTreeNode T) {
                var L = T.leChild;
                var R = T.leChild.riChild;
                T.leChild = R;
                L.riChild = T;
                refresh(T);
                refresh(L);
                return L;
            }

            private AVLTreeNode rotateLe2(AVLTreeNode T) {
                T.riChild = rotateRi1(T.riChild);
                T = rotateLe1(T);
                return T;
            }

            private AVLTreeNode rotateRi2(AVLTreeNode T) {
                T.leChild = rotateLe1(T.leChild);
                T = rotateRi1(T);
                return T;
            }
        }

        private sealed class AVLTreeNode {
            public readonly int item;

            public AVLTreeNode leChild;
            public AVLTreeNode riChild;

            public int level;
            public int count;
            public int total;

            public AVLTreeNode(int item) {
                this.item = item;
                this.level = 1;
                this.count = 1;
                this.total = 1;
            }
        }
    }
}
