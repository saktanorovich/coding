using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0421 {
        public int FindMaximumXOR(int[] nums) {
            var trie = new TrieNode();
            foreach (var x in nums) {
                var node = trie;
                for (var i = 31; i >= 0; --i) {
                    var b = (x >> i) & 1;
                    if (node.next[b] == null) {
                        node.next[b] = new TrieNode();
                    }
                    node = node.next[b];
                }
            }
            var best = 0;
            foreach (var x in nums) {
                var curr = 0;
                var node = trie;
                for (var i = 31; i >= 0; --i) {
                    var b = (x >> i) & 1;
                    if (node.next[b ^ 1] != null) {
                        node = node.next[b ^ 1];
                        curr |= 1 << i;
                    } else {
                        node = node.next[b];
                    }
                }
                best = Math.Max(best, curr);
            }
            return best;
        }

        private class TrieNode {
            public TrieNode[] next;

            public TrieNode() {
                next = new TrieNode[2];
            }
        }
    }
}
