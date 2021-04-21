using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0139 {
        public bool WordBreak(string s, IList<string> words) {
            var trie = new TrieNode();
            foreach (var word in words) {
                var node = trie;
                foreach (var c in word.Reverse()) {
                    var x = c - 'a';
                    if (node.next[x] == null) {
                        node.next[x] = new TrieNode();
                    }
                    node = node.next[x];
                }
                node.word = true;
            }
            var f = new bool[s.Length];
            for (var i = 0; i < s.Length; ++i) {
                var node = trie;
                for (var j = i; j >= 0 && !f[i]; --j) {
                    var x = s[j] - 'a';
                    if (node.next[x] != null) {
                        node = node.next[x];
                        f[i] = node.word;
                        if (j > 0) {
                            f[i] &= f[j - 1];
                        }
                    } else {
                        break;
                    }
                }
            }
            return f[s.Length - 1];
        }

        private class TrieNode {
            public TrieNode[] next;
            public bool word;

            public TrieNode() {
                next = new TrieNode[26];
            }
        }
    }
}
