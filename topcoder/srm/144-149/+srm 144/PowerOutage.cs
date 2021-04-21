using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class PowerOutage {
            public int estimateTimeOut(int[] fromJunction, int[] toJunction, int[] ductLength) {
                  int[,] tree = new int[numOfNodes, numOfNodes];
                  int totalLength = 0;
                  for (int edge = 0; edge < ductLength.Length; ++edge) {
                        tree[fromJunction[edge], toJunction[edge]] = ductLength[edge];
                        tree[toJunction[edge], fromJunction[edge]] = ductLength[edge];
                        totalLength += ductLength[edge];
                  }
                  int longestPath = 0;
                  for (int node = 0; node < numOfNodes; ++node) {
                        if (isLeaf(tree, node)) {
                              longestPath = Math.Max(longestPath, pathLegnth(tree, node, -1, 0));
                        }
                  }
                  return 2 * totalLength - longestPath;
            }

            private const int numOfNodes = 50;

            private int pathLegnth(int[,] tree, int node, int prev, int goal) {
                  if (node == goal) {
                        return 0;
                  }
                  for (int next = 0; next < numOfNodes; ++next) {
                        if (next != prev && tree[node, next] > 0) {
                              int length = pathLegnth(tree, next, node, goal);
                              if (length >= 0) {
                                    return length + tree[node, next];
                              }
                        }
                  }
                  return int.MinValue;
            }

            private bool isLeaf(int[,] tree, int node) {
                  int degree = 0;
                  for (int next = 0; next < numOfNodes; ++next) {
                        if (tree[node, next] > 0) {
                              degree = degree + 1;
                        }
                  }
                  return degree == 1;
            }
      }
}