using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0472 {
        public IList<string> FindAllConcatenatedWordsInADict(string[] words) {
            Array.Sort(words, (x, y) => x.Length - y.Length);
            var trie = new TrieNode();
            var result = new List<string>();
            foreach (var word in words) {
                if (okay(trie, word)) {
                    result.Add(word);
                }
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
            return result;
        }

        private bool okay(TrieNode trie, string word) {
            if (word.Length < 2) {
                return false;
            }
            var f = new bool[word.Length];
            for (var i = 0; i < word.Length; ++i) {
                var node = trie;
                for (var j = i; j >= 0 && !f[i]; --j) {
                    var x = word[j] - 'a';
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
            return f[word.Length - 1];
        }

        private class TrieNode {
            public readonly TrieNode[] next;
            public bool word;

            public TrieNode() {
                next = new TrieNode[26];
            }
        }
    }
}
