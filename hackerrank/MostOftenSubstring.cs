using System;
using System.Collections.Generic;

namespace interview.hackerrank {
    public class MostOftenSubstring {
        private class Trie {
            private TrieNode root = new TrieNode();

            private class TrieNode {
                public int occurence;
                public TrieNode[] children = new TrieNode[26];
            }

            public int add(LinkedList<char> substring) {
                var node = root;
                foreach (var c in substring) {
                    if (node.children[c - 'a'] == null) {
                        node.children[c - 'a'] = new TrieNode();
                    }
                    node = node.children[c - 'a'];
                }
                node.occurence = node.occurence + 1;
                return node.occurence;
            }
        }

        public int count(string s, int k, int l, int m) {
            var trie = new Trie();
            var occurence = new int[26];
            var substring = new LinkedList<char>();
            int result = 0, distinct = 0;
            for (var i = 0; i < k; ++i) {
                substring = append(substring, occurence, s[i], ref distinct);
            }
            if (distinct <= m) {
                result = Math.Max(result, trie.add(substring));
            }
            for (var i = k; i < s.Length; ++i) {
                substring = remove(substring, occurence, s[i - k], ref distinct);
                substring = append(substring, occurence, s[i + 0], ref distinct);
                if (distinct <= m) {
                    result = Math.Max(result, trie.add(substring));
                }
            }
            return result;
        }

        private LinkedList<char> append(LinkedList<char> substring, int[] occurence, char c, ref int distinct) {
            if (occurence[c - 'a'] == 0) {
                distinct = distinct + 1;
            }
            ++occurence[c - 'a'];
            substring.AddLast(c);
            return substring;
        }

        private LinkedList<char> remove(LinkedList<char> substring, int[] occurence, char c, ref int distinct) {
            if (occurence[c - 'a'] == 1) {
                distinct = distinct - 1;
            }
            --occurence[c - 'a'];
            substring.RemoveFirst();
            return substring;
        }
    }
}
