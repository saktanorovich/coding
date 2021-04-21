using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1600 {
        public class ThroneInheritance {
            private readonly Dictionary<string, Tree> tree;
            private readonly Tree king;

            public ThroneInheritance(string kingName) {
                king = new Tree(kingName);
                tree = new Dictionary<string, Tree>();
                tree[kingName] = king;
            }

            public void Birth(string parentName, string childName) {
                var child = new Tree(childName);
                tree[childName] = child;
                tree[parentName].Children.Add(child);
            }

            public void Death(string name) {
                tree[name].IsAlive = false;
            }

            public IList<string> GetInheritanceOrder() {
                return GetInheritanceOrder(king, new List<string>());
            }

            private IList<string> GetInheritanceOrder(Tree person, List<string> order) {
                if (person.IsAlive) {
                    order.Add(person.Name);
                }
                foreach (var child in person.Children) {
                    GetInheritanceOrder(child, order);
                }
                return order;
            }

            private sealed class Tree {
                public readonly string Name;
                public readonly List<Tree> Children;
                public bool IsAlive;

                public Tree(string name) {
                    Name = name;
                    IsAlive = true;
                    Children = new List<Tree>();
                }
            }
        }
    }
}
