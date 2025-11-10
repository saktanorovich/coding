using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class WordFilter {
        private readonly TrieNode trie;

        public WordFilter(string[] words) {
            trie = new TrieNode();
            for (var i = 0; i < words.Length; ++i) {
                var word = words[i];
                for (var k = 0; k < word.Length; ++k) {
                    add(word.Substring(k) + "*" + word, i);
                }
                add("*" + word, i);
            }
        }

        public int F(string prefix, string suffix) {
            if (string.IsNullOrEmpty(prefix) && string.IsNullOrEmpty(suffix)) {
                return get("*");
            }
            if (string.IsNullOrEmpty(prefix)) {
                return get(suffix + "*");
            }
            if (string.IsNullOrEmpty(suffix)) {
                return get("*" + prefix);
            }
            return get(suffix + "*" + prefix);
        }

        private void add(string word, int weight) {
            var node = trie;
            foreach (var c in word) {
                var h = code(c);
                if (node.Next[h] == null) {
                    node.Next[h] = new TrieNode();
                }
                node = node.Next[h];
                node.Weight = weight;
            }
        }

        private int get(string word) {
            var node = trie;
            foreach (var c in word) {
                node = node.Next[code(c)];
                if (node == null) {
                    return -1;
                }
            }
            return node.Weight;
        }

        private int code(char c) {
            if (c == '*') {
                return 0;
            } else {
                return c - 'a' + 1;
            }
        }

        private class TrieNode {
            public TrieNode[] Next;
            public int Weight;

            public TrieNode() {
                Next = new TrieNode[27];
                Weight = -1;
            }
        }
    }
}
