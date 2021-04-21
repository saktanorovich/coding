using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1268 {
        private Trie trie;

        public IList<IList<string>> SuggestedProducts(string[] products, string word) {
            trie = new Trie();
            foreach (var product in products) {
                add(product);
            }
            var res = new List<IList<string>>();
            for (var i = 0; i < word.Length; ++i) {
                trie = trie.Next[word[i] - 'a'];
                if (trie != null) {
                    res.Add(get(trie).Take(3).ToList());
                } else {
                    for (; i < word.Length; ++i) {
                        res.Add(new List<string>());
                    }
                    break;
                }
            }
            return res;
        }

        private void add(string word) {
            var node = trie;
            foreach (var c in word) {
                var h = c - 'a';
                if (node.Next[h] == null) {
                    node.Next[h] = new Trie();
                }
                node = node.Next[h];
            }
            node.Word = word;
        }

        private IEnumerable<string> get(Trie node) {
            if (node.Word != null) {
                yield return node.Word;
            }
            foreach (var next in node.Next) {
                if (next != null) {
                    foreach (var word in get(next)) {
                        yield return word;
                    }
                }
            }
        }

        private class Trie {
            public Trie[] Next;
            public string Word;

            public Trie() {
                Next = new Trie[27];
            }
        }
    }
}
