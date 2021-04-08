using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0212 {
        public IList<string> FindWords(char[][] board, string[] words) {
            var trie = new TrieNode();
            for (var i = 0; i < words.Length; ++i) {
                var node = trie;
                foreach (var c in words[i]) {
                    var x = c - 'a';
                    if (node.next[x] == null) {
                        node.next[x] = new TrieNode();
                    }
                    node = node.next[x];
                }
                node.indx.Add(i);
            }
            var exist = new bool[words.Length];
            var wasAt = new bool[board.Length, board[0].Length];
            for (var i = 0; i < board.Length; ++i) {
                for (var j = 0; j < board[0].Length; ++j) {
                    var x = board[i][j] - 'a';
                    if (trie.next[x] != null) {
                        MarkWords(trie.next[x], board, exist, i, j, wasAt);
                    }
                }
            }
            var found = new List<string>();
            for (var i = 0; i < words.Length; ++i) {
                if (exist[i]) {
                    found.Add(words[i]);
                }
            }
            return found;
        }

        private void MarkWords(TrieNode trie, char[][] board, bool[] exist, int cx, int cy, bool[,] wasAt) {
            wasAt[cx, cy] = true;
            foreach (var i in trie.indx) {
                exist[i] = true;
            }
            for (var k = 0; k < 4; ++k) {
                var nx = cx + dx[k];
                var ny = cy + dy[k];
                if (0 <= nx && nx < board.Length && 0 <= ny && ny < board[0].Length) {
                    if (!wasAt[nx, ny]) {
                        var x = board[nx][ny] - 'a';
                        if (trie.next[x] != null) {
                            MarkWords(trie.next[x], board, exist, nx, ny, wasAt);
                        }
                    }
                }
            }
            wasAt[cx, cy] = false;
        }

        private class TrieNode {
            public TrieNode[] next;
            public List<int> indx;

            public TrieNode() {
                next = new TrieNode[26];
                indx = new List<int>();
            }
        }

        private readonly int[] dx = { -1, 0, 1, 0 };
        private readonly int[] dy = { 0, -1, 0, 1 };
    }
}
