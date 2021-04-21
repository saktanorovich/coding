using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class RedBlack {
            public int numTwists(int[] keys) {
                  BinaryTree binaryTree = new BinaryTree(BinaryTreeNode.Empty);
                  foreach (int key in keys) {
                        binaryTree = binaryTree.Insert(key);
                  }
                  return numOfTwists;
            }

            private static int numOfTwists = 0;

            private class BinaryTree {
                  private BinaryTreeNode root;

                  public BinaryTree(BinaryTreeNode binaryTreeNode) {
                        root = binaryTreeNode;
                  }

                  public BinaryTree Insert(int key) {
                        BinaryTreeNode node = InsertImpl(root, key);
                        node.Color = BinaryTreeNodeColor.Blk;
                        return new BinaryTree(node);
                  }

                  private static BinaryTreeNode InsertImpl(BinaryTreeNode root, int key) {
                        if (root.IsEmpty()) {
                              return new BinaryTreeNode(BinaryTreeNodeColor.Red, key);
                        }
                        else {
                              if (key < root.Key) return BalanceImpl(new BinaryTreeNode(root.Color, root.Key, InsertImpl(root.Le, key), root.Ri));
                              if (key > root.Key) return BalanceImpl(new BinaryTreeNode(root.Color, root.Key, root.Le, InsertImpl(root.Ri, key)));
                        }
                        return null;
                  }

                  private static BinaryTreeNode BalanceImpl(BinaryTreeNode R) {
                        if (IsBalanceRequired(R.Le, R.Le.Le)) return BalanceImpl(R.Le.Le, R.Le, R, new BinaryTreeNode[4] { R.Le.Le.Le, R.Le.Le.Ri, R.Le.Ri, R.Ri });
                        if (IsBalanceRequired(R.Le, R.Le.Ri)) return BalanceImpl(R.Le, R.Le.Ri, R, new BinaryTreeNode[4] { R.Le.Le, R.Le.Ri.Le, R.Le.Ri.Ri, R.Ri });
                        if (IsBalanceRequired(R.Ri, R.Ri.Le)) return BalanceImpl(R, R.Ri.Le, R.Ri, new BinaryTreeNode[4] { R.Le, R.Ri.Le.Le, R.Ri.Le.Ri, R.Ri.Ri });
                        if (IsBalanceRequired(R.Ri, R.Ri.Ri)) return BalanceImpl(R, R.Ri, R.Ri.Ri, new BinaryTreeNode[4] { R.Le, R.Ri.Le, R.Ri.Ri.Le, R.Ri.Ri.Ri });
                        return R;
                  }

                  private static BinaryTreeNode BalanceImpl(BinaryTreeNode x, BinaryTreeNode y, BinaryTreeNode z, BinaryTreeNode[] nodes) {
                        numOfTwists = numOfTwists + 1;
                        return new BinaryTreeNode(BinaryTreeNodeColor.Red, y.Key,
                                     new BinaryTreeNode(BinaryTreeNodeColor.Blk, x.Key, nodes[0], nodes[1]),
                                     new BinaryTreeNode(BinaryTreeNodeColor.Blk, z.Key, nodes[2], nodes[3]));
                  }

                  private static bool IsBalanceRequired(BinaryTreeNode child, BinaryTreeNode grandChild) {
                        return child.Color == BinaryTreeNodeColor.Red &&
                                    grandChild.Color == BinaryTreeNodeColor.Red;
                  }
            }

            private class BinaryTreeNode {
                  private sealed class EmptyBinaryTreeNode : BinaryTreeNode {
                        public EmptyBinaryTreeNode() {
                              Color = BinaryTreeNodeColor.Blk;
                              Le = this;
                              Ri = this;
                        }
                  }

                  public static readonly BinaryTreeNode Empty = new EmptyBinaryTreeNode();

                  public BinaryTreeNode Le, Ri;
                  public BinaryTreeNodeColor Color;
                  public int Key;

                  public BinaryTreeNode() {
                  }

                  public BinaryTreeNode(BinaryTreeNodeColor color, int key)
                        : this(color, key, Empty, Empty) {
                  }

                  public BinaryTreeNode(BinaryTreeNodeColor color, int key, BinaryTreeNode lechild, BinaryTreeNode richild) {
                        Key = key;
                        Color = color;
                        Le = lechild;
                        Ri = richild;
                  }

                  public bool IsEmpty() {
                        return object.ReferenceEquals(this, Empty);
                  }
            }

            private enum BinaryTreeNodeColor { Red, Blk }
      }
}