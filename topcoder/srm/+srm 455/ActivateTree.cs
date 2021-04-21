using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class ActivateTree {
            private const int undefined = -1;

            private class Activator {
                  public int GetMinCost(Tree target, Tree[] trees, int[] costs) {
                        int nodesCount = target.GetTreeNodes().Count;
                        int edgesCount = nodesCount - 1;
                        int total = 1 << edgesCount;
                        int[,] dp = new int[2, total];
                        for (int set = 0; set < total; ++set)
                              dp[0, set] = dp[1, set] = int.MaxValue / 2;
                        dp[0, 0] = 0;
                        List<int>[,] map = GetIsomorphismMap(target, trees);
                        int level = 0;
                        for (int node = 0; node < nodesCount; ++node)
                              for (int tree = 0; tree < trees.Length; ++tree, level ^= 1) {
                                    for (int set = 0; set < total; ++set)
                                          dp[level ^ 1, set] = Math.Min(dp[level ^ 1, set], dp[level, set]);
                                    for (int set = 0; set < total; ++set)
                                          if (dp[level, set] < int.MaxValue / 2)
                                                foreach (int mask in map[node, tree])
                                                      if (dp[level ^ 1, set ^ mask] > dp[level, set] + costs[tree])
                                                            dp[level ^ 1, set ^ mask] = dp[level, set] + costs[tree];
                              }
                        return (dp[level, total - 1] < int.MaxValue / 2 ? dp[level, total - 1] : undefined);
                  }

                  private List<int>[,] GetIsomorphismMap(Tree target, Tree[] trees) {
                        IList<TreeNode> nodes = target.GetTreeNodes();
                        List<int>[,] result = new List<int>[nodes.Count, trees.Length];
                        foreach (TreeNode node in nodes)
                              for (int i = 0; i < trees.Length; ++i) {
                                    result[node.Id, i] = new List<int>();
                                    foreach (int mask in Tree.GetTreeMasks(target, node.Owner, trees[i].GetTreeNodes().Count))
                                          if (IsIsomorphic(node.Owner, nodes.Count, mask, trees[i]))
                                                result[node.Id, i].Add(target.ConvertNodeMaskToEdgeMask(mask));
                              }
                        return result;
                  }

                  private bool IsIsomorphic(Tree target, int totalNodesCount, int mask, Tree tree) {
                        IList<int> id = new List<int>();
                        for (int i = 0; i < totalNodesCount; ++i)
                              if (BitUtils.Contains(mask, i))
                                    id.Add(i);
                        foreach (IList<TreeNode> permutation in PermuteUtils.Permute(tree.GetTreeNodes())) {
                              bool result = true;
                              for (int i = 0; i < permutation.Count; ++i)
                                    for (int j = 0; j < permutation.Count; ++j)
                                          if (i != j)
                                                if (target.IsConnected(id[i], id[j]) ^ tree.IsConnected(permutation[i].Id, permutation[j].Id)) {
                                                      result = false;
                                                      goto next_permutation;
                                                }
                        next_permutation:
                              if (result)
                                    return true;
                        }
                        return false;
                  }
            }

            public int minCost(string[] trees, string target, int[] costs) {
                  return new Activator().GetMinCost(Tree.BuildBy(target),
                        Array.ConvertAll(trees, delegate(string descriptor) {
                              return Tree.BuildBy(descriptor);
                  }), costs);
            }

            private class TreeNode {
                  public int Id { get; private set; }
                  public Tree Owner { get; private set; }
                  public List<Tree> Children { get; private set; }

                  public TreeNode(Tree owner, int id) {
                        Owner = owner;
                        Id = id;
                        Children = new List<Tree>();
                  }
            }

            private class Tree {
                  public TreeNode Node { get; private set; }

                  private Tree(int id) {
                        Node = new TreeNode(this, id);
                  }

                  public static Tree BuildBy(string descriptor) {
                        return BuildBy(0, BuildSnapshotBy(descriptor));
                  }

                  private static Tree BuildBy(int id, List<int>[] shapshoot) {
                        Tree result = new Tree(id);
                        for (int i = 0; i < shapshoot[id].Count; ++i)
                              result.Node.Children.Add(BuildBy(shapshoot[id][i], shapshoot));
                        return result;
                  }

                  private static List<int>[] BuildSnapshotBy(string descriptor) {
                        int[] items = Array.ConvertAll(descriptor.Split(' '), delegate(string item) {
                              return int.Parse(item);
                        });
                        List<int>[] result = new List<int>[items.Length];
                        for (int i = 0; i < items.Length; ++i)
                              result[i] = new List<int>();
                        for (int i = 0; i < items.Length; ++i)
                              if (items[i] != undefined)
                                    result[items[i]].Add(i);
                        return result;
                  }

                  public static IList<int> GetTreeMasks(Tree tree, Tree subtree, int nodesCount) {
                        IList<int> result = new List<int>();
                        int total = tree.GetTreeNodes().Count;
                        for (int set = 0; set < (1 << total); ++set)
                              if (BitUtils.GetCardinality(set) == nodesCount)
                                    if (BitUtils.Contains(set, subtree.Node.Id)) {
                                          bool ok = true;
                                          for (int i = 0; i < total; ++i)
                                                if (BitUtils.Contains(set, i))
                                                      ok &= subtree.IsReachable(i, set);
                                          if (ok)
                                                result.Add(set);
                                    }
                        return result;
                  }

                  public IList<TreeNode> GetTreeNodes() {
                        IList<TreeNode> result = new List<TreeNode>(new TreeNode[] { Node });
                        foreach (Tree child in Node.Children)
                              result = PermuteUtils.Concat(result, child.GetTreeNodes());
                        return result;
                  }

                  public bool IsConnected(int src, int dst) {
                        if (Node.Id == src) {
                              foreach (Tree child in Node.Children)
                                    if (child.Node.Id == dst)
                                          return true;
                              return false;
                        }
                        foreach (Tree child in Node.Children) {
                              bool result = child.IsConnected(src, dst);
                              if (result)
                                    return true;
                        }
                        return false;
                  }

                  public int ConvertNodeMaskToEdgeMask(int mask) {
                        int total = this.GetTreeNodes().Count;
                        int result = 0;
                        for (int i = 0; i < total; ++i)
                              for (int j = 0; j < total; ++j)
                                    if (BitUtils.Contains(mask, i) && BitUtils.Contains(mask, j))
                                          if (i != j && IsConnected(i, j))
                                                result = BitUtils.Set(result, j - 1);
                        return result;
                  }

                  private bool IsReachable(int candidate, int set) {
                        if (Node.Id == candidate)
                              return true;
                        else if (BitUtils.Contains(set, Node.Id)) {
                              foreach (Tree tree in Node.Children)
                                    if (tree.IsReachable(candidate, set))
                                          return true;
                        }
                        return false;
                  }
            }

            private static class PermuteUtils {
                  public static IList<IList<T>> Permute<T>(IList<T> list) {
                        IList<IList<T>> result = new List<IList<T>>();
                        if (list.Count == 0) {
                              result.Add(list);
                        }
                        else {
                              for (int i = 0; i < list.Count; ++i)
                                    foreach (IList<T> res in Permute(Remove(list, i)))
                                          result.Add(Concat(new List<T>(new T[] { list[i] }), res));
                        }
                        return result;
                  }

                  public static IList<T> Concat<T>(IList<T> list1, IList<T> list2) {
                        IList<T> result = new List<T>(list1);
                        foreach (T item in list2)
                              result.Add(item);
                        return result;
                  }

                  public static IList<T> Remove<T>(IList<T> list, int index) {
                        IList<T> result = new List<T>(list);
                        result.RemoveAt(index);
                        return result;
                  }
            }

            private static class BitUtils {
                  public static int GetCardinality(int set) {
                        int result = 0;
                        for (; set > 0; set -= (set & (-set)))
                              ++result;
                        return result;
                  }

                  public static bool Contains(int set, int x) {
                        return ((set & (1 << x)) != 0);
                  }

                  public static int Set(int set, int x) {
                        return (set | (1 << x));
                  }
            }

            // BEGIN CUT HERE
            public void run_test(int Case) {
                  if ((Case == -1) || (Case == 0)) test_case_0();
                  if ((Case == -1) || (Case == 1)) test_case_1();
                  if ((Case == -1) || (Case == 2)) test_case_2();
                  if ((Case == -1) || (Case == 3)) test_case_3();
                  if ((Case == -1) || (Case == 4)) test_case_4();
                  if ((Case == -1) || (Case == 5)) test_case_5();
            }
            private void verify_case(int Case, int Expected, int Received) {
                  Console.Write("Test Case #" + Case + "...");
                  if (Expected == Received)
                        Console.WriteLine("PASSED");
                  else {
                        Console.WriteLine("FAILED");
                        Console.WriteLine("\tExpected: \"" + Expected + '\"');
                        Console.WriteLine("\tReceived: \"" + Received + '\"');
                  }
            }
            private void test_case_0() { string[] Arg0 = new string[] { "-1 0" }; string Arg1 = "-1 0"; int[] Arg2 = new int[] { 5 }; int Arg3 = 5; verify_case(0, Arg3, minCost(Arg0, Arg1, Arg2)); }
            private void test_case_1() { string[] Arg0 = new string[] { "-1 0" }; string Arg1 = "-1 0 0"; int[] Arg2 = new int[] { 5 }; int Arg3 = -1; verify_case(1, Arg3, minCost(Arg0, Arg1, Arg2)); }
            private void test_case_2() { string[] Arg0 = new string[] { "-1 0", "-1 0" }; string Arg1 = "-1 0 0"; int[] Arg2 = new int[] { 1, 101 }; int Arg3 = 102; verify_case(2, Arg3, minCost(Arg0, Arg1, Arg2)); }
            private void test_case_3() { string[] Arg0 = new string[] { "-1 0", "-1 0", "-1 0 3 0" }; string Arg1 = "-1 0 3 0"; int[] Arg2 = new int[] { 5, 10, 21 }; int Arg3 = 20; verify_case(3, Arg3, minCost(Arg0, Arg1, Arg2)); }
            private void test_case_4() { string[] Arg0 = new string[] { "-1 0 1", "-1 0 0", "-1 0", "-1 0 1 0 2" }; string Arg1 = "-1 0 0 0 2"; int[] Arg2 = new int[] { 3885, 13122, 31377, 21317 }; int Arg3 = 17007; verify_case(4, Arg3, minCost(Arg0, Arg1, Arg2)); }
            private void test_case_5() { string[] Arg0 = new string[] { "-1 0 1 2 3", "-1 0 0 1 1", "-1 0 1 2 2", "-1 0 1 2 3", "-1 0 0 0 3" }; string Arg1 = "-1 0 0 0 1 2 2 2 3 3 4 4 4"; int[] Arg2 = new int[] { 3, 4, 7, 5, 4 }; int Arg3 = -1; verify_case(5, Arg3, minCost(Arg0, Arg1, Arg2)); }

            // END CUT HERE

            // BEGIN CUT HERE
            [STAThread]
            public static void Main(string[] args) {
                  ActivateTree item = new ActivateTree();
                  item.run_test(-1);
                  Console.ReadLine();
            }
            // END CUT HERE
      }
}